using Automatonymous;
using Common.Events;
using Common.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace ProviderActualizator.Saga
{
	public sealed partial class CheckStateMachine : MassTransitStateMachine<ProviderState>
	{
		public CheckStateMachine(ILogger<CheckStateMachine> logger)
		{
			InstanceState(x => x.CurrentState);
			ConfigureCorrelationIds();

			Initially(
				When(CheckSubmitted)
				.Then(c => c.Instance.CreatedOn = DateTime.Now)
				.Then(c => c.Instance.Request = JsonConvert.DeserializeObject<TestRequest>(c.Data.RequestJson))
				.Then(c => logger.LogInformation("Submitted check with correlation ID: {correlation_id}. Data: {request}", c.Data.CorrelationId, c.Data.RequestJson))
				.ThenAsync(c => CheckAsync(c))
				.TransitionTo(Submitted)
				);

			DuringAny(
				When(CheckFailed)
				.Then(c => logger.LogError("Check failed in source {fail_source} with correlation ID: {correlation_id}. Error message: {error}",
					c.Data.FailSource, c.Data.CorrelationId, c.Data.Message))
				.TransitionTo(Failed)
				.Finalize()
				);

			During(Submitted,
				When(CheckCompleted, i => i.Data.HasResult)
				.Then(c => c.Instance.ResponseJson = c.Data.ResultJson)
				.TransitionTo(Completed)
				.Finalize(),

				When(CheckCompleted, i => !i.Data.HasResult)
				.ThenAsync(c => UploadAsync(c)),

				When(CheckUploaded)
				.ThenAsync(c => ActualizeAsync(c))
				.TransitionTo(Uploaded)
				);

			During(Uploaded,
				When(CheckActualized)
				.Then(c => c.Instance.ResponseJson = c.Data.ResultJson)
				.TransitionTo(Completed)
				.Finalize()
				);
		}

		public State Submitted { get; private set; }
		public State Completed { get; private set; }
		public State Uploaded { get; private set; }
		public State Actualized { get; private set; }
		public State Failed { get; private set; }

		public Event<CheckSubmittedEvent> CheckSubmitted { get; private set; }
		public Event<CheckCompletedEvent> CheckCompleted { get; private set; }
		public Event<CheckUploadedEvent> CheckUploaded { get; private set; }
		public Event<CheckActualizedEvent> CheckActualized { get; private set; }
		public Event<CheckFailedEvent> CheckFailed { get; private set; }

		private void ConfigureCorrelationIds()
		{
			Event(() => CheckSubmitted, x => x.CorrelateById(i => i.Message.CorrelationId).SelectId(i => i.Message.CorrelationId));
			Event(() => CheckCompleted, x => x.CorrelateById(i => i.Message.CorrelationId));
			Event(() => CheckUploaded, x => x.CorrelateById(i => i.Message.CorrelationId));
			Event(() => CheckActualized, x => x.CorrelateById(i => i.Message.CorrelationId));
			Event(() => CheckFailed, x => x.CorrelateById(i => i.Message.CorrelationId));
		}
	}
}
