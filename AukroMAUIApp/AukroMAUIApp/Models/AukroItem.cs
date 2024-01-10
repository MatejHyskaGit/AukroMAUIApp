using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AukroMAUIApp.Models
{
    [Table("Items")]
    internal class AukroItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [SQLite.MaxLength(250), Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public float Price { get; set; }
        [ForeignKey(typeof(AukroUser))]
        public int OwnerId { get; set; }
        [ManyToOne, Ignore]
        public AukroUser Owner { get; set; }
        [ForeignKey(typeof(AukroCategory))]
        public string CategoryName { get; set; }
        [ForeignKey(typeof(AukroUser))]
        public int LastBidderId { get; set; }
    }
}
