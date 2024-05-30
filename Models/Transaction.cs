namespace BankingAPI.Models
{
	public class Transaction
	{
		public int AccountId { get; set; }
		public decimal Amount { get; set; }
		public string TransactionType { get; set; } 
		
	}
}
