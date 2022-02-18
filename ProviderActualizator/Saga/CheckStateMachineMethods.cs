using Automatonymous;
using Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProviderActualizator.Saga
{
	public sealed partial class CheckStateMachine
	{
		private async Task CheckAsync(BehaviorContext<ProviderState, CheckSubmittedEvent> context)
		{

		}

		private async Task UploadAsync(BehaviorContext<ProviderState, CheckCompletedEvent> context)
		{

		}

		private async Task ActualizeAsync(BehaviorContext<ProviderState, CheckUploadedEvent> context)
		{

		}
	}
}
