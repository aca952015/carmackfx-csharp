﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarmackFX.Client.Connection;
using CarmackFX.Client.Protocol;
using Newtonsoft.Json;

namespace CarmackFX.Client.Message
{
	class MessageManager
	{
		public static readonly Dictionary<long, MessageQueueItem> queue = new Dictionary<long, MessageQueueItem>();
		private static long meesageSeed = 0;

        public static Task<String> Push(String serviceName, String methodName, object[] args)
        {
            RpcMessageData data = new RpcMessageData();
            data.ServiceName = serviceName;
            data.MethodName = methodName;

            if (args != null && args.Length == 0)
            {
                List<String> values = new List<string>();

                foreach (var arg in args)
                {
                    values.Add(JsonConvert.SerializeObject(arg));
                }

                data.Arguments = values.ToArray();
            }

            return Push<String>(MessageType.SERVER, data);
        }

		public static Task<TOut> Push<TOut>(MessageType messageType, object messageData)
		{
			IProtocolService protocolService = ServiceManager.Resolve<IProtocolService>();

			MessageIn msgIn = new MessageIn()
			{
				Id = meesageSeed++,
				Type = messageType,
				Token = protocolService.Config.Token,
				Data = JsonConvert.SerializeObject(messageData)
			};

			MessageQueueItem queyeItem = new MessageQueueItem()
			{
				Id = msgIn.Id
			};

			queue.Add(msgIn.Id, queyeItem);

			Task<TOut> task = new Task<TOut>(() =>
			{
				IConnectionService service = ServiceManager.Resolve<IConnectionService>();
				service.Send(msgIn);

				DateTime start = DateTime.Now;
				while (true)
				{
					if ((DateTime.Now - start).TotalSeconds > 2)
					{
						break;
					}

					if (queyeItem.Result != null)
					{
						queue.Remove(msgIn.Id);

						if (queyeItem.Result.Success == MessageSuccess.SUCCESS)
						{
							return JsonConvert.DeserializeObject<TOut>(queyeItem.Result.Data);
						}
						else if(queyeItem.Result.Success == MessageSuccess.SERVERERROR)
						{
							throw new MessageException(ExceptionCode.ServerError);
						}
						else if (queyeItem.Result.Success == MessageSuccess.DATAINVALID)
						{
							throw new MessageException(ExceptionCode.DataInvalid);
						}
					}

                    Task.Delay(10).Wait();
				}

				queue.Remove(msgIn.Id);

				throw new MessageException(ExceptionCode.Timeout);
			});

			task.Start();

			return task;
		}

		public static void Completed(MessageOut msgOut)
		{
			if (queue.ContainsKey(msgOut.Id))
			{
				queue[msgOut.Id].Result = msgOut;
				queue.Remove(msgOut.Id);
			}
		}
	}
}
