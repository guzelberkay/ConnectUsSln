using ConnectUs.Entity.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Entity.Entities
{
    [Table("tbl_comment")] // Veritabanı tablosu adı
    public class Comment
    {
        // Primary Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        // Project ID
        public long ProjectId { get; set; }

        // Company Name
        public string CompanyName { get; set; }

        // Name
        public string Name { get; set; }

        // Surname
        public string Surname { get; set; }

        // Email
        public string Email { get; set; }

        // Comment content
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string CommentContent { get; set; }

        // Status - Enum tipi
        [EnumDataType(typeof(EStatus))]
        public EStatus Status { get; set; } = EStatus.PENDING;  // Default olarak PENDING

        // Parametresiz constructor
        public Comment() { }

        // Parametreli constructor
        public Comment(long id, long projectId, string companyName, string name, string surname, string email, string commentContent, EStatus status)
        {
            Id = id;
            ProjectId = projectId;
            CompanyName = companyName;
            Name = name;
            Surname = surname;
            Email = email;
            CommentContent = commentContent;
            Status = status;
        }
    }
}
