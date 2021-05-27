using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockTaking.Models
{
    public class Company
    {
        [PrimaryKey, AutoIncrement]
        public int Company_Id { get; set; }
        public string Company_Name { get; set; }
        public string Company_Description { get; set; }
        public string Company_Admin_Username { get; set; }
        public string Company_Admin_Password { get; set; }
    }
}
