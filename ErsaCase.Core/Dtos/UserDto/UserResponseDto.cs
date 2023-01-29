using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErsaCase.Core.Dtos.UserDto
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Image { get; set; }
        public int? PageId { get; set; }

        public int? DefaultCurrencyId { get; set; }
        public string? DefaultCurrencyName { get; set; }
        public string? DefaultCurrencySymbol { get; set; }
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
        public int? CompanyId { get; set; }
        public string TokenRaw { get; set; }
        public string? CompanyName { get; set; }
    }
}
