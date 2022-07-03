namespace WeatherBlanketApi.Models.API
{
	public class WeatherBlanketWebUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public byte[] Token { get; set; }
		public bool IsAdmin { get; set; }
	}
}
