using ErsaCase.Core.Dtos.RoleDto;
using ErsaCase.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ErsaCase.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Bütün Rolleri Getirir
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllByNonDeleted()
        {
            var result = await _roleService.GetAllByNonDeleted();

            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorDefination);
            }

            return Ok(result.Data);

        }

        /// <summary>
        /// Girilen Id Değeri İle İlişkili Rolleri Getirir
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _roleService.GetAsync(id);

            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorDefination);
            }

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoleAddDto variantAddDto)
        {
            var variant = await _roleService.AddAsync(variantAddDto);

            if (!variant.Succeeded)
            {
                return BadRequest();
            }

            return Created("", variant.Data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(RoleUpdateDto variantUpdateDto)
        {
            var result = await _roleService.Update(variantUpdateDto);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _roleService.Delete(id);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return NoContent(); //front end isteğine göre data da dönderilebilir.
        }
    }
}
