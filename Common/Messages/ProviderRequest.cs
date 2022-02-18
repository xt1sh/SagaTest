namespace Common.Messages
{
	public record ProviderRequest
	{
		public string Provider { get; set; }
		public string Action { get; set; }
		public string Data { get; set; }
	}
}
