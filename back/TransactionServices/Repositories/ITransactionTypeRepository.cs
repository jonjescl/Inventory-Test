using TransactionServices.Models;

namespace TransactionServices.Repositories
{
    public interface ITransactionTypeRepository
    {
        Task<List<MTransactionTypes>> TransactionTypesList();
    }
}
