using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HerexamenTry.Shared;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace HerexamenTry.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]

    public class UserController : ControllerBase
    {
        private readonly ManagementApiClient _managementApiClient;

        public UserController(ManagementApiClient managementApiClient)
        {
            _managementApiClient = managementApiClient;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto.Index>> GetUsers()
        {
            var users = await _managementApiClient.Users.GetAllAsync(new GetUsersRequest(), new PaginationInfo());
            return users.Select(x => new UserDto.Index
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Picture=x.Picture
               
            });
        }
    }
}