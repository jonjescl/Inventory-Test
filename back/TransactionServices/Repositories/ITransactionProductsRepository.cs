using TransactionServices.Models;

namespace TransactionServices.Repositories
{
    public interface ITransactionProductsRepository
    {
        Task<MResponse> TransactionProductsCreate(int IdTransaction,MTransactionProducts obj);
        
    }
}
