using Common.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProviderActualizator.Consumers
{
	public class ActualizeConsumer : IConsumer<ProviderRequest>
	{
		public async Task Consume(ConsumeContext<ProviderRequest> context)
		{
			throw new NotImplementedException();
		}
	}
}
