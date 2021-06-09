using System.Collections.Generic;
using SmallRetail.Data.Models;

namespace SmallRetail.Services
{
    public interface ITransactionService
    {
        public IEnumerable<Transaction> GetAll();
        public Transaction Get(int id);
        public void Create(Transaction transaction);
        public void Update(Transaction transaction);
        public void Delete(int id);
    }
}