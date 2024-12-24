using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Entity.Entities
{
    [Table("tbl_address")] // Veritabanı tablosu adı
    public class Address  // "Adress" yerine "Address" olarak düzeltilmiştir.
    {
        // Primary Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        // Description alanı
        public string Description { get; set; }

        // İçeriği tutacak alan (Lob tipi karşılığı String)
        [Column(TypeName = "TEXT")] // Veritabanında TEXT türünde bir sütun oluşturulmasını sağlar
        public string Value { get; set; }

        // Parametresiz constructor (Entity Framework Core'da zaten otomatik olarak oluşur)
        public Address() { }

        // Parametreli constructor
        public Address(long id, string description, string value)
        {
            Id = id;
            Description = description;
            Value = value;
        }
    }
}