using BankingAPI.Data;
using BankingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
	{
		[HttpPost]
		public IActionResult CreateUser(User user)
		{
			user.Id = BankingDataStore.Users.Count + 1;
			BankingDataStore.Users.Add(user);
			return Ok(user);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteUser(int id)
		{
			var user = BankingDataStore.Users.FirstOrDefault(u => u.Id == id);
			if (user == null)
				return NotFound();

			BankingDataStore.Users.Remove(user);
			return NoContent();
		}

		[HttpGet("{id}")]
		public IActionResult GetUserById(int id)
		{
			var user = BankingDataStore.Users.FirstOrDefault(u => u.Id == id);
			if (user == null)
				return NotFound();

			return Ok(user);
		}
	}
}
