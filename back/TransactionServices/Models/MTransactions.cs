namespace TransactionServices.Models
{
    public class MTransactions
    {
        public int? Id { get; set; }
        public DateTime? Date { get; set; }
        public int TransactionTypeId { get; set; }
        public MTransactionTypes? TransactionType { get; set; }
        public decimal TotalPrice { get; set; }
        public string Detail { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<MTransactionProducts>? TransactionProducts { get; set; }


    }
}
