using System;

namespace Common.Events
{
	public interface CheckSubmittedEvent
	{
		public Guid CorrelationId { get; }
		public string RequestJson { get; }
	}
}
