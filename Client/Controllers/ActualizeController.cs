using Common.Messages;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ActualizeController : ControllerBase
	{
		private readonly IRequestClient<ProviderRequest> _requestClient;

		public ActualizeController(IRequestClient<ProviderRequest> requestClient)
		{
			_requestClient = requestClient;
		}

		[HttpPost]
		public async Task<IActionResult> Actualize(CancellationToken cancellationToken = default)
		{
			ProviderRequest request = new()
			{
				Provider = "MinJust",
				Action = "",
			};
			Response<ProviderResponse> response = await _requestClient.GetResponse<ProviderResponse>(request, cancellationToken);

			return Ok();
		}
	}
}
