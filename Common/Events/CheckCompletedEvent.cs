using Common.Models;
using System;

namespace Common.Events
{
	public interface CheckCompletedEvent
	{
		public Guid CorrelationId { get; }
		public string ResultJson { get; }
		public bool HasResult { get; }
	}
}
