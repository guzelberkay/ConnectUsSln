using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConnectUs.Entity.Entities
{
    [Table("tblproject")] // Veritabanı tablosu adı
    public class Project
    {
        // Primary Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        // Employer (İşveren)
        public string Employer { get; set; }

        // Title (İşin adı)
        [Column(TypeName = "TEXT")]  // TEXT türünde sütun oluşturulmasını sağlar
        public string Title { get; set; }

        // Location (Yer)
        public string Location { get; set; }

        // Date (Tarih)
        public string Date { get; set; }

        // Description (Kapsam)
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string Description { get; set; }

        // Parametresiz constructor
        public Project() { }

        // Parametreli constructor
        public Project(long id, string employer, string title, string location, string date, string description)
        {
            Id = id;
            Employer = employer;
            Title = title;
            Location = location;
            Date = date;
            Description = description;
        }
    }
}
