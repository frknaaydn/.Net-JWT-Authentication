using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErsaCase.Core.Dtos.UserDto
{
    public class ActiveUserDto
    {
        public int Id { get; set; }
        public Guid LogicalRef { get; set; }
        public string Name { get; set; }
    }
}
