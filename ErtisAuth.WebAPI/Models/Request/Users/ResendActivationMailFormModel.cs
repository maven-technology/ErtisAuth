using Newtonsoft.Json;

namespace ErtisAuth.WebAPI.Models.Request.Users;

public class ResendActivationMailFormModel
{
	#region Properties

	[JsonProperty("email_address")]
	public string EmailAddress { get; set; }

	#endregion
}