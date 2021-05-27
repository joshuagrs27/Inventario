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
    }
}
