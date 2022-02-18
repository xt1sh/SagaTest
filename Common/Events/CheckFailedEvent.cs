using Common.Enums;
using System;

namespace Common.Events
{
	public interface CheckFailedEvent
	{
		public Guid CorrelationId { get; }
		public FailSource FailSource { get; }
		public string Message { get; }
	}
}
