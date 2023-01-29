using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ErsaCase.Core.Model
{
    public class User : BaseEntity
    {
        //public User()
        //{
        //    Role = new HashSet<Role>();
        //}

        public string? UserName { get; set; }
        public string? Email { get; set; }

        [JsonIgnore]
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Image { get; set; }
        public  Role? Role { get; set; }
        public int? RoleId { get; set; }

        //[NotMapped]
        //public string TokenRaw { get; set; }
        //public string? Hash { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
