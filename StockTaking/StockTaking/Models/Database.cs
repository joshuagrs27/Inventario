using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockTaking.Models
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;
        //
        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            //Create The Company Table
            _database.CreateTableAsync<Company>().Wait();
            //Create The Users Table
            _database.CreateTableAsync<User>().Wait();
            //Create The Products Table
            _database.CreateTableAsync<Product>().Wait();
            //Create The Transaction Table
            _database.CreateTableAsync<Transaction>().Wait();
        }

        //Get All Companies Function
        public Task<List<Company>> GetCompaniesAsync()
        {
            return _database.Table<Company>().ToListAsync();
        }
        //Get All Users Function
        public Task<List<User>> GetUsersAsync()
        {
            return _database.Table<User>().ToListAsync();
        }
        //Get All Products Function
        public Task<List<Product>> GetProductsAsync()
        {
            return _database.Table<Product>().ToListAsync();
        }
        //Get All Transactions Function
        public Task<List<Transaction>> GetTransactionsAsync()
        {
            return _database.Table<Transaction>().ToListAsync();
        }
        //Save Company Function
        public Task<int> SaveCompanyAsync(Company company)
        {
            return _database.InsertAsync(company);
        }
        //Save User Function
        public Task<int> SaveUserAsync(User user)
        {
            return _database.InsertAsync(user);
        }
        //Save Product Function
        public Task<int> SaveProductAsync(Product product)
        {
            return _database.InsertAsync(product);
        }
        //Save Transaction Function
        public Task<int> SaveTransactionAsync(Transaction transaction)
        {
            return _database.InsertAsync(transaction);
        }
        //Delete Company Function
        public Task<int> DeleteCompanyAsync(Company company)
        {
            return _database.DeleteAsync(company);
        }
        //Delete User Function
        public Task<int> DeleteUserAsync(User user)
        {
            return _database.DeleteAsync(user);
        }
        //Delete Product Function
        public Task<int> DeleteProductAsync(Product product)
        {
            return _database.DeleteAsync(product);
        }
        //Delete Company Function
        public Task<int> DeleteTransactionAsync(Transaction transaction)
        {
            return _database.DeleteAsync(transaction);
        }

        //Edit Company Function
        public Task<int> EditCompanyAsync(Company company)
        {
            return _database.UpdateAsync(company);
        }
        //Edit User Function
        public Task<int> EditUserAsync(User user)
        {
            return _database.UpdateAsync(user);
        }
        //Edit Product Function
        public Task<int> EditProductAsync(Product product)
        {
            return _database.UpdateAsync(product);
        }
        //Edit Transaction Function
        public Task<int> EditTransactionAsync(Transaction transaction)
        {
            return _database.UpdateAsync(transaction);
        }

        //Search for Company Function
        public Task<Company> SearchCompanyAsync(string companyName)
        {
            //return _database.FindWithQueryAsync<Plan>($"SELECT * FROM [Plan] WHERE PlanTitle = {planName}");
            return _database.FindWithQueryAsync<Company>("SELECT * FROM Company WHERE Company_Name = ?", companyName);
        }
        //Search for Product Function
        public Task<Product> SearchProductAsync(string productName)
        {
            //return _database.FindWithQueryAsync<Plan>($"SELECT * FROM [Plan] WHERE PlanTitle = {planName}");
            return _database.FindWithQueryAsync<Product>("SELECT * FROM Product WHERE Product_Name = ?", productName);
        }


        /*
        public Task<Book> SearchBookAsync(string bookName)
        {
            //return _database.FindWithQueryAsync<Plan>($"SELECT * FROM [Plan] WHERE PlanTitle = {planName}");
            return _database.FindWithQueryAsync<Book>("SELECT * FROM Book WHERE Book_Name = ?", bookName);
        }

        public Task<int> EditBookAsync(Book book)
        {
            return _database.UpdateAsync(book);
        }
        */
    }
}
