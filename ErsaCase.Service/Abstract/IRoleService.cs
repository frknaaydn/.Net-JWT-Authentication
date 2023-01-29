using ErsaCase.Core.Model;
using ErsaCase.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErsaCase.Core.Dtos.RoleDto;

namespace ErsaCase.Service.Abstract
{
    public interface IRoleService
    {
        Task<DataResult<IList<Role>>> GetAllByNonDeleted();
        Task<DataResult<Role>> GetAsync(int roleId);
        Task<DataResult<Role>> AddAsync(RoleAddDto roleAddDto);
        Task<DataResult<Role>> Update(RoleUpdateDto roleUpdateDto);
        Task<DataResult<Role>> Delete(int roleId);
        Task<DataResult<Role>> HardDelete(int roleId);
    }
}
