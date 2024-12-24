using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Entity.Entities
{
    [Table("tblphone")] // Veritabanı tablosu adı
    public class Phone
    {
        // Primary Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        // Description
        public string Description { get; set; }

        // Value (Phone number or similar)
        public string Value { get; set; }

        // Parametresiz constructor
        public Phone() { }

        // Parametreli constructor
        public Phone(long id, string description, string value)
        {
            Id = id;
            Description = description;
            Value = value;
        }
    }
}