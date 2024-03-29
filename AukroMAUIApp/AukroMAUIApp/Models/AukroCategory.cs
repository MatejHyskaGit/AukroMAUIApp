﻿using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AukroMAUIApp.Models
{
    [Table("Categories")]
    internal class AukroCategory
    {
        [PrimaryKey, Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
