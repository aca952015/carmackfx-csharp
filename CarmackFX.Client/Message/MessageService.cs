using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarmackFX.Client.Connection;
using CarmackFX.Client.Protocol;
using Newtonsoft.Json;

namespace CarmackFX.Client.Message
{
	class MessageService : ServiceBase, IMessageService
	{
		public readonly Dictionary<long, MessageQueueItem> queue = new Dictionary<long, MessageQueueItem>();
		private long meesageSeed = 0;

		public MessageService(ServiceManager serviceManager)
			: base(serviceManager)
		{
		}

		public Task<ServiceResponse> Push(MessageType messageType, object messageData)
		{
			IProtocolService protocolService = ServiceManager.Resolve<IProtocolService>();

			MessageIn msgIn = new MessageIn()
			{
				Id = meesageSeed++,
				Type = messageType,
				Token = protocolService.Config.Token,
				Data = JsonConvert.SerializeObject(messageData)
			};

			MessageQueueItem queueItem = new MessageQueueItem()
			{
				Id = msgIn.Id
			};

			queue.Add(msgIn.Id, queueItem);

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

						if (queueItem.Result != null)
						{
							queue.Remove(msgIn.Id);

							if (queueItem.Result.Success == MessageSuccess.Success)
							{
								return new ServiceResponse()
								{
									IsSuccess = true,
									Data = queueItem.Result.Data
								};
							}
							else
							{
								var data = JsonConvert.DeserializeObject<MessageError>(queueItem.Result.Data);
								ExceptionCode code = ExceptionCode.Unknown;

								if (queueItem.Result.Success == MessageSuccess.SeverError)
								{
									code = ExceptionCode.ServerError;
								}
								else if (queueItem.Result.Success == MessageSuccess.DataInvalid)
								{
									code = ExceptionCode.DataInvalid;
								}

								return new ServiceResponse()
								{
									IsSuccess = false,
									Error = new MessageException(code, data.ErrorMessage ?? code.ToString())
								};
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

		public void Completed(MessageOut msgOut)
		{
			ServiceManager.Log(msgOut.ToJson());

			if (queue.ContainsKey(msgOut.Id))
			{
				queue[msgOut.Id].Result = msgOut;
				queue.Remove(msgOut.Id);
			}
		}

	}
}
