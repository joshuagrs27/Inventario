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
        public string Product_Name { get; set; }
        public string Product_Description { get; set; }
        public int Product_CompanyID { get; set; }
        public int Product_Current_Stock { get; set; }
        public int Product_Low_Level { get; set; }
        public string Product_Level { get; set; }

    }
}
