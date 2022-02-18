using System;

namespace Common.Events
{
	public interface CheckActualizedEvent
	{
		public Guid CorrelationId { get; }
		public string ResultJson { get; }
	}
}
