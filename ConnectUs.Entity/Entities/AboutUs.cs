using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Entity.Entities
{
    [Table("tbl_aboutus")] // Veritabanı tablosu adı
    public class AboutUs
    {
        // Primary Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        // İçeriği tutacak alan (Lob tipi karşılığı String)
        [Column(TypeName = "TEXT")] // Veritabanında TEXT türünde bir sütun oluşturulmasını sağlar
        public string Content { get; set; }

        // Parametresiz constructor (Entity Framework Core'da zaten otomatik olarak oluşur)
        public AboutUs() { }

        // Parametreli constructor
        public AboutUs(long id, string content)
        {
            Id = id;
            Content = content;
        }
    }
}
