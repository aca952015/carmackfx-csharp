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

		public static Task<ServiceResponse> Push(MessageType messageType, object messageData)
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

			ServiceTask task = new ServiceTask(() =>
			{
				IConnectionService service = ServiceManager.Resolve<IConnectionService>();
				service.Send(msgIn);

				try
				{
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

							if (queyeItem.Result.Success == MessageSuccess.Success)
							{
								return new ServiceResponse()
								{
									IsSuccess = true,
									Data = queyeItem.Result.Data
								};
							}
							else if (queyeItem.Result.Success == MessageSuccess.SeverError)
							{
								throw new MessageException(ExceptionCode.ServerError);
							}
							else if (queyeItem.Result.Success == MessageSuccess.DataInvalid)
							{
								throw new MessageException(ExceptionCode.DataInvalid);
							}
						}

						Task.Delay(10).Wait();
					}

					queue.Remove(msgIn.Id);

					throw new MessageException(ExceptionCode.Timeout);
				}
				catch(Exception ex)
				{
					return new ServiceResponse()
					{
						IsSuccess = false,
						Error = ex
					};
				}
			});

			task.Start();

			return task;
		}

		internal static void Completed(MessageOut msgOut)
		{
			if (queue.ContainsKey(msgOut.Id))
			{
				queue[msgOut.Id].Result = msgOut;
				queue.Remove(msgOut.Id);
			}
		}

	}
}
