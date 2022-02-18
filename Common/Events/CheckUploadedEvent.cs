using System;

namespace Common.Events
{
	public interface CheckUploadedEvent
	{
		public Guid CorrelationId { get; }
		public string ResultJson { get; }
	}
}
