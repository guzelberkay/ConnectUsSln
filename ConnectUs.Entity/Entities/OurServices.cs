using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Entity.Entities
{
    [Table("tbl_ourservices")] // Veritabanı tablosu adı
    public class OurServices
    {
        // Primary Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        // Name
        public string Name { get; set; }

        // Type
        public string Type { get; set; }

        // File (byte array)
        [Column(TypeName = "LONGBLOB")]  // Veritabanında binary veri olarak tutulmasını sağlar
        public byte[] File { get; set; }

        // Title
        public string Title { get; set; }

        // Description
        [Column(TypeName = "TEXT")] // Veritabanında TEXT türünde bir sütun oluşturulmasını sağlar
        public string Description { get; set; }

        // Parametresiz constructor
        public OurServices() { }

        // Parametreli constructor
        public OurServices(long id, string name, string type, byte[] file, string title, string description)
        {
            Id = id;
            Name = name;
            Type = type;
            File = file;
            Title = title;
            Description = description;
        }
    }
}
