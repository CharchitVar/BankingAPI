namespace BankingAPI.Models
{
	public class Account
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public decimal Balance { get; set; } = 100;
	}
}
