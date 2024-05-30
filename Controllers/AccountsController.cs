using BankingAPI.Data;
using BankingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BankingApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AccountsController : ControllerBase
	{
		[HttpPost]
		public IActionResult CreateAccount(Account account)
		{
			var user = BankingDataStore.Users.FirstOrDefault(u => u.Id == account.UserId);
			if (user == null)
				return NotFound("User not found");

			account.Id = BankingDataStore.Accounts.Count + 1;
			user.Accounts.Add(account);
			BankingDataStore.Accounts.Add(account);
			return Ok(account);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteAccount(int id)
		{
			var account = BankingDataStore.Accounts.FirstOrDefault(a => a.Id == id);
			if (account == null)
				return NotFound();

			var user = BankingDataStore.Users.FirstOrDefault(u => u.Id == account.UserId);
			if (user != null)
				user.Accounts.Remove(account);

			BankingDataStore.Accounts.Remove(account);
			return NoContent();
		}

		[HttpPost("{id}/deposit")]
		public IActionResult Deposit(int id, [FromBody] decimal amount)
		{
			if (amount > 10000)
				return BadRequest("Deposit amount exceeds $10,000 limit.");

			var account = BankingDataStore.Accounts.FirstOrDefault(a => a.Id == id);
			if (account == null)
				return NotFound();

			account.Balance += amount;
			return Ok(account);
		}

		[HttpPost("{id}/withdraw")]
		public IActionResult Withdraw(int id, [FromBody] decimal amount)
		{
			var account = BankingDataStore.Accounts.FirstOrDefault(a => a.Id == id);
			if (account == null)
				return NotFound();

			if (amount > 0.9m * account.Balance)
				return BadRequest("Cannot withdraw more than 90% of the balance in a single transaction.");

			if (account.Balance - amount < 100)
				return BadRequest("Account balance cannot be less than $100.");

			account.Balance -= amount;
			return Ok(account);
		}

		[HttpGet("{id}")]
		public IActionResult GetAccountById(int id)
		{
			var account = BankingDataStore.Accounts.FirstOrDefault(a => a.Id == id);
			if (account == null)
				return NotFound();

			return Ok(account);
		}
	}
}
