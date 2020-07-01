using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Commands;
using Application.Exceptions;
using EfDataAccess;
using Application.DTO;
using Application.Queries;
using Api.Helpers;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {

        private readonly IGetRoleCommand _getRole;
        private readonly IGetRolesCommand _getRoles;
        private readonly IAddRoleCommand _addRole;
        private readonly IEditRoleCommand _editRole;
        private readonly IDeleteRoleCommand _deleteRole;

        public RolesController(IGetRoleCommand getRole, 
                               IGetRolesCommand getRoles, 
                               IAddRoleCommand addRole,
                               IEditRoleCommand editRole,
                               IDeleteRoleCommand deleteRole)
        {
            _getRole = getRole;
            _getRoles = getRoles;
            _addRole = addRole;
            _editRole = editRole;
            _deleteRole = deleteRole;
        }

        /// <summary>
        /// Returns all roles
        /// </summary>

        // GET: api/Roles
        [HttpGet]
        public ActionResult<IEnumerable<RoleDto>> Get([FromQuery]RoleQuery query)
        {
            try
            {
                return Ok(_getRoles.Execute(query));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Returns one specific role
        /// </summary>

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_getRole.Execute(id));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

        }

        /// <summary>
        /// Insert new role
        /// </summary>

        // POST: api/Roles
        [LoggedIn("Admin")]
        [HttpPost]
        public IActionResult Post([FromBody] RoleDto dto)
        {
            try
            {
                _addRole.Execute(dto);
                return StatusCode(200);
            }
            catch(EntityAlreadyExistsException)
            {
                return StatusCode(422);
            }
        }

        /// <summary>
        /// Role edit
        /// </summary>

        // PUT: api/Roles/5
        [LoggedIn("Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RoleDto dto)
        {
            try
            {
                dto.Id = id;
                _editRole.Execute(dto);
                return StatusCode(204);
                
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
            catch(EntityAlreadyExistsException)
            {
                return StatusCode(422);
            }
        }


        /// <summary>
        /// Role delete
        /// </summary>
        /// 

        // DELETE: api/Roles/5
        [LoggedIn("Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteRole.Execute(id);
                return StatusCode(204);
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
