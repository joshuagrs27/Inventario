using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockTaking.Models
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int Transaction_Id { get; set; }
    }
}
