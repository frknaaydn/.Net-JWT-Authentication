using ErsaCase.Core;
using ErsaCase.Core.Dtos.RoleDto;
using ErsaCase.Core.Model;
using ErsaCase.Repository.Abstract;
using ErsaCase.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ErsaCase.Service.Concrete
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DataResult<IList<Role>>> GetAllByNonDeleted()
        {
            try
            {
                var roles = await _unitOfWork.Role.GetAllAsync();

                if (roles.Count > 0)
                {
                    return new DataResult<IList<Role>>(roles);
                }
                return new DataResult<IList<Role>>(HttpStatusCode.NotFound, "Role Bulunamadı");
            }
            catch (Exception ex)
            {
                return new DataResult<IList<Role>>(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<DataResult<Role>> GetAsync(int roleId)
        {
            try
            {
                var Role = await _unitOfWork.Role.GetAsync(x => x.Id == roleId);

                if (Role != null)
                {
                    return new DataResult<Role>(Role);
                }
                return new DataResult<Role>(HttpStatusCode.NotFound, "Role Bulunamadı");
            }
            catch (Exception ex)
            {
                return new DataResult<Role>(HttpStatusCode.InternalServerError,ex.Message);
            }
        }

        public async Task<DataResult<Role>> AddAsync(RoleAddDto roleAddDto)
        {
            try
            {
                var role = new Role();
                role.Name = roleAddDto.Name;
                role.Description = roleAddDto.Description;


                role.EditTime = DateTime.UtcNow;

                await _unitOfWork.Role.AddAsync(role);
                await _unitOfWork.SaveChanges();

                return new DataResult<Role>(role);
            }
            catch (Exception ex)
            {
                return new DataResult<Role>(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        public async Task<DataResult<Role>> Update(RoleUpdateDto roleUpdateDto)
        {
            try
            {

                var oldRole = await _unitOfWork.Role.GetAsync(x => x.Id == roleUpdateDto.Id);

                oldRole.Name = roleUpdateDto.Name;
                oldRole.Description = roleUpdateDto.Description;


                oldRole.EditTime = DateTime.Now;

                await _unitOfWork.Role.UpdateAsync(oldRole);
                await _unitOfWork.SaveChanges();

                return new DataResult<Role>(oldRole);
            }
            catch (Exception ex)
            {
                return new DataResult<Role>(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        public async Task<DataResult<Role>> Delete(int roleId)
        {

            try
            {
                var result = await _unitOfWork.Role.AnyAsync(x => x.Id == roleId);

                if (result)
                {
                    var role = await _unitOfWork.Role.GetAsync(x => x.Id == roleId);

                    role.IsDeleted = true;
                    role.EditTime = DateTime.Now;

                    await _unitOfWork.Role.UpdateAsync(role);
                    await _unitOfWork.SaveChanges();

                    return new DataResult<Role>(HttpStatusCode.NoContent,"Başarıyla silindi");
                }

                return new DataResult<Role>(false, null);

            }
            catch (Exception ex)
            {
                return new DataResult<Role>(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        public async Task<DataResult<Role>> HardDelete(int roleId)
        {
            try
            {
                var result = await _unitOfWork.Role.AnyAsync(x => x.Id == roleId);

                if (result)
                {
                    var role = await _unitOfWork.Role.GetAsync(x => x.Id == roleId);

                    await _unitOfWork.Role.DeleteAsync(role);
                    await _unitOfWork.SaveChanges();

                    return new DataResult<Role>(HttpStatusCode.NoContent, "Başarıyla silindi");
                }
                return new DataResult<Role>(HttpStatusCode.BadRequest, "RoleId Bulunamadı");

            }
            catch (Exception ex)
            {
                return new DataResult<Role>(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }

}