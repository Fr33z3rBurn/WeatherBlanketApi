using WeatherBlanketApi.Models.API;

namespace WeatherBlanketApi.Services
{
	public interface IUserService
	{
		void CreateNewUser(RegisterUser registerUser);
		WeatherBlanketWebUser AuthenticateUser(LoginUser loginUser);
	}
}
