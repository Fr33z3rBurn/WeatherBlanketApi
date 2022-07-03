using MongoDB.Driver;
using System.Security.Cryptography;
using WeatherBlanketApi.Models;
using WeatherBlanketApi.Models.API;
using WeatherBlanketApi.Models.DB;

namespace WeatherBlanketApi.Services
{
	public class UserService : IUserService
	{
		private readonly IMongoCollection<UserDto> users;

		public UserService(IUserDatabaseSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);
			users = database.GetCollection<UserDto>(settings.UsersCollectionName);
		}

		public void CreateNewUser(RegisterUser registerUser)
		{
			var user = UserServiceUtils.MapRegisterToDto(registerUser);
			users.InsertOne(user);
		}

		public WeatherBlanketWebUser AuthenticateUser(LoginUser loginUser)
		{
			var user = users.Find(x => x.Email == loginUser.Email).Single(); //tolower?

			var webuser = new WeatherBlanketWebUser()//TODO mapper
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				Token = user.PasswordHash,
				IsAdmin = user.IsAdmin,
			};

			bool passwordCorrect = true;

			using (var hmac = new HMACSHA512(user.PasswordSalt))
			{
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(loginUser.Password));
				for (int i = 0; i < computedHash.Length; i++)
				{
					if (computedHash[i] != user.PasswordHash[i])
					{
						passwordCorrect = false;
					}
				}
			}
			if (passwordCorrect)
			{
				return webuser;
			}
			return new WeatherBlanketWebUser();//TODO Update
		}
	}
}
