using Microsoft.AspNetCore.Mvc;
using SalesManagementLibrary.Models;
using SalesManagementLibrary.Models.Dtos;
using SalesManagementLibrary.Repo.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRolesController(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        // Create
        // POST api/<UserRolesController>
        [HttpPost]
        public async Task<ActionResult<UserRoleModel?>> CreateUserRole(UserRoleCreateDto userRoleCreateDto)
        {
            var userRole = await _userRoleRepository.CreateAsync(userRoleCreateDto);

            if (userRole == null)
            {
                return BadRequest("UserRole Creation Failed");
            }

            return Ok(userRole);
        }





        // GET: api/<UserRolesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserRolesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        

        // PUT api/<UserRolesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserRolesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
