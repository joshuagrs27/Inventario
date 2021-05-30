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
        public int Transaction_Company_ID { get; set; }
        public int Transaction_Product_ID { get; set; }
        public string Transaction_Product_Name { get; set; }
        public string Transaction_Type { get; set; }
        public int Transaction_Amount { get; set; }
        public string Transaction_Notes { get; set; }
        public DateTime Transaction_Date { get; set; }
    }
}
