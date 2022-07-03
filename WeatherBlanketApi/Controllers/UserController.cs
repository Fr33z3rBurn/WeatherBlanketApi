using Microsoft.AspNetCore.Mvc;
using WeatherBlanketApi.Models.API;
using WeatherBlanketApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeatherBlanketApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}
		// GET: api/<UserController>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<UserController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<UserController>
		[HttpPost]
		public void Post([FromBody] RegisterUser registerUser)
		{
			_userService.CreateNewUser(registerUser);
		}

		// PUT api/<UserController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<UserController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}

		//POST api/<UserCOntroller>/authenticate
		[HttpPost("authenticate")]
		public WeatherBlanketWebUser Authenticate([FromBody] LoginUser loginUser)
		{
			return _userService.AuthenticateUser(loginUser);
		}
	}
}
