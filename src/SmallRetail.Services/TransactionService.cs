using System;
using System.Collections.Generic;
using SmallRetail.Data;
using SmallRetail.Data.Models;

namespace SmallRetail.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly SmallRetailDbContext _db;

        public TransactionService(SmallRetailDbContext db)
        {
            _db = db;
        }
        
        public IEnumerable<Transaction> GetAll()
        {
            return _db.Transactions;
        }

        public Transaction Get(int id)
        {
            return _db.Transactions.Find(id);
        }

        public void Create(Transaction transaction)
        {
            transaction.DateCreated = DateTime.UtcNow;
            transaction.DateUpdated = DateTime.UtcNow;
            _db.Transactions.Add(transaction);
            _db.SaveChanges();
        }

        public void Update(Transaction transaction)
        {
            var existingTransaction = _db.Transactions.Find(transaction.Id);
            if (existingTransaction == null)
                throw new ArgumentException("Transaction not found");
            
            existingTransaction.DateUpdated = DateTime.UtcNow;
            existingTransaction.TransactionProducts = transaction.TransactionProducts;
            _db.Transactions.Update(existingTransaction);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var transaction = Get(id);
            if (transaction == null)
                throw new ArgumentException("Transaction not found");

            _db.Transactions.Remove(transaction);
            _db.SaveChanges();
        }
    }
}