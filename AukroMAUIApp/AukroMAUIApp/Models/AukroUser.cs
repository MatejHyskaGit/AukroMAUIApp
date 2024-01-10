using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AukroMAUIApp.Models
{
    [Table("Users")]
    internal class AukroUser
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [SQLite.MaxLength(250), Unique, Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<AukroItem> Items { get; set; }
        public int IsLoggedIn { get; set; }
    }
}
