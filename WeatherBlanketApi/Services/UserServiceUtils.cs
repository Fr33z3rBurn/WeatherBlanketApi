using System.Security.Cryptography;
using WeatherBlanketApi.Models.API;
using WeatherBlanketApi.Models.DB;

namespace WeatherBlanketApi.Services
{
	internal static class UserServiceUtils
	{
		public static UserDto MapRegisterToDto(RegisterUser registerUser)
		{
			byte[] passwordSalt;
			byte[] passwordHash;

			using (var hmac = new HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerUser.Password));
			}

			UserDto userDto = new UserDto()
			{
				FirstName = registerUser.FirstName,
				LastName = registerUser.LastName,
				Email = registerUser.Email,
				IsAdmin = false,
				IsEmailVerified = false,
				PasswordHash = passwordHash,
				PasswordSalt = passwordSalt
			};

			return userDto;
		}
	}
}
