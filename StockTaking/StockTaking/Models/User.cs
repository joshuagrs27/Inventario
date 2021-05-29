using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockTaking.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int User_Id { get; set; }
        public string User_Name { get; set; }
        public string User_Password { get; set; }
        public string User_Email { get; set; }
        public int User_Company_ID { get; set; }
        public string User_Permission { get; set; }

    }
}
