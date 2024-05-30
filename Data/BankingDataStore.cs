using BankingAPI.Models;

namespace BankingAPI.Data
{
	public static class BankingDataStore
	{
		public static List<User> Users { get; set; } = new List<User>();
		public static List<Account> Accounts { get; set; } = new List<Account>();
	}
}
