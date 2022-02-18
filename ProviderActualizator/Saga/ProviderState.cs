using Automatonymous;
using Common.Models;
using System;

namespace ProviderActualizator.Saga
{
	public record ProviderState : SagaStateMachineInstance
	{
		public Guid CorrelationId { get; set; }
		public string CurrentState { get; set; }
		public TestRequest Request { get; set; }
		public string ResponseJson { get; set; }
		public DateTime CreatedOn { get; set; }
	}
}
