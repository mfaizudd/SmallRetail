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

        public TransactionService(SmallRetailDbContext db)
        {
            _db = db;
        }
        
        public IEnumerable<Transaction> GetAll()
        {
            return _db.Transactions
                .Include(x => x.TransactionProducts)
                .ThenInclude(x=>x.Product)
                .ToList();
        }

        public Transaction Get(int id)
        {
            return _db.Transactions.Find(id);
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