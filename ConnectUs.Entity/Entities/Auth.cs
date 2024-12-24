using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Entity.Entities
{
    [Table("tbl_auths")]
    public class Auth
    {
        // Primary Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        // Email alanı
        [Required(ErrorMessage = "Email is required.")] // Zorunlu alan
        [EmailAddress(ErrorMessage = "Invalid email format.")] // Email format kontrolü
        [Column(TypeName = "nvarchar(255)")] // Kolon tipi
        public string Email { get; set; }

        // Şifre alanı
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        // Code alanı (isteğe bağlı)
        public string Code { get; set; }

        // Zaman damgası
        public long CodeTimestamp { get; set; }

        // Parametresiz constructor
        public Auth()
        {
        }

        // Tüm alanları içeren constructor
        public Auth(long id, string email, string password, string code, long codeTimestamp)
        {
            Id = id;
            Email = email;
            Password = password;
            Code = code;
            CodeTimestamp = codeTimestamp;
        }

        // ToString override
        public override string ToString()
        {
            return $"Id: {Id}, Email: {Email}, Password: {Password}, Code: {Code}, CodeTimestamp: {CodeTimestamp}";
        }
    }
}
