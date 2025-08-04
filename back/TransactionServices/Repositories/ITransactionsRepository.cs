using TransactionServices.Models;

namespace TransactionServices.Repositories
{
    public interface ITransactionsRepository
    {

        Task<List<MTransactions>> TransactionsList();
        Task<MTransactions> TransactionsDetails(int Id);
        Task<MResponse> TransactionsCreate(MTransactions obj);
        Task<MResponse> TransactionsModify(MTransactions obj);
        Task<MResponse> TransactionsDelete(int Id);
    }
}
