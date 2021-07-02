using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmallRetail.Data;
using SmallRetail.Data.Models;

namespace SmallRetail.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly SmallRetailDbContext _db;
        public int Count => _db.Transactions.Count();

        public TransactionService(SmallRetailDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Transaction> GetAll(int limit = 10, int page = 1)
        {
            var query = _db.Transactions
                .OrderByDescending(t => t.DateUpdated)
                .Include(x => x.TransactionProducts);

            if (limit == 0)
                return query;

            return query
                .Skip(limit * (page - 1))
                .Take(limit);
        }

        public Transaction Get(params object[] keyValues)
        {
            return _db.Transactions.Find(keyValues);
        }

        public Transaction Find(Func<Transaction, bool> predicate)
        {
            return _db.Transactions.FirstOrDefault(predicate);
        }

        public IEnumerable<Transaction> Where(Func<Transaction, bool> predicate)
        {
            return _db.Transactions.Where(predicate);
        }

        public void Create(Transaction transaction)
        {
            var products = new List<TransactionProduct>(transaction.TransactionProducts.Count);
            var newTransaction = new Transaction
            {
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };
            foreach (var item in transaction.TransactionProducts)
            {
                var product = _db.Products.Find(item.ProductId);
                if (product == null)
                    throw new ArgumentException($"There's no product with id: {item.ProductId}", nameof(transaction));

                var newItem = new TransactionProduct
                {
                    ProductId = product.Id,
                    Transaction = newTransaction,
                    Quantity = item.Quantity
                };
                products.Add(newItem);
            }

            newTransaction.TransactionProducts = products;

            _db.Transactions.Add(newTransaction);
            _db.SaveChanges();
        }

        public void Update(Transaction transaction, params object[] keyValues)
        {
            if (keyValues == null)
                throw new ArgumentNullException(nameof(keyValues));
            if (keyValues.Length == 0)
                throw new ArgumentException("Key isn't specified", nameof(keyValues));

            var existingTransaction = _db.Transactions.Find(keyValues);
            if (existingTransaction == null)
                throw new ArgumentException("Transaction not found");

            existingTransaction.DateUpdated = DateTime.UtcNow;
            existingTransaction.TransactionProducts = transaction.TransactionProducts;
            _db.Transactions.Update(existingTransaction);
            _db.SaveChanges();
        }

        public void Delete(params object[] keyValues)
        {
            var transaction = Get(keyValues);
            if (transaction == null)
                throw new ArgumentException("Transaction not found");

            _db.Transactions.Remove(transaction);
            _db.SaveChanges();
        }
    }
}