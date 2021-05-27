using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockTaking.Models
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int Product_Id { get; set; }
    }
}
