using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required] // Zorunlu alan
        public string Name { get; set; }

        // Type
        [Required] // Zorunlu alan
        public string Type { get; set; }

        // File (byte array)
        [Required]
        [Column(TypeName = "VARBINARY(MAX)")] // SQL Server için binary veri türü
        public byte[] File { get; set; }

        // Title
        [Required]
        public string Title { get; set; }

        // Description
        [Required]
        [Column(TypeName = "NVARCHAR(MAX)")] // SQL Server'da uzun metin için uygun tür
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
