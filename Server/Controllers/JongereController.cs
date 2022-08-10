using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using HerexamenTry.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerexamenTry.Server.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    public class JongereController:ControllerBase
    {

        private readonly ManagementApiClient _managementApiClient;
        private  static readonly List<JongereDTO> jongereList = new(10);
       public JongereController(ManagementApiClient managementApiClient)
        {
            _managementApiClient = managementApiClient;


            jongereList.AddRange(Enumerable.Range(1, 5).Select(index => new JongereDTO
            {
                Firstname="Rayan",
                Lastname="wilmart",
                Gender="male",
                Date = DateTime.Now.AddDays(index),
                Email="fdogdfgdg",
                Password="65465655"
                //TemperatureC = Random.Next(-20, 55),
                // Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }));
        }
        [HttpGet]
        public IEnumerable<JongereDTO> GetJongeren()
        {
            return jongereList;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<JongereDTO> CreateJongereAsync( JongereDTO jongere)
        {

            jongereList.Add(jongere);
            var user = new UserCreateRequest();
            user.FirstName = jongere.Firstname;
            user.LastName = jongere.Lastname;
            user.Email = jongere.Email;
            user.Password = jongere.Password;
            user.UserName = jongere.Username;
            user.Connection = "Username-Password-Authentication";
           // await _managementApiClient.Users.GetAllAsync(new GetUsersRequest(), new PaginationInfo());
            await _managementApiClient.Users.CreateAsync(user);
            return jongere;
        }
        [HttpPost("delete")]
        [Authorize(Roles = "Admin")]
        public JongereDTO DeleteJongere(JongereDTO jongere)
        {
            jongereList.Remove(jongere);
            return jongere;
        }
    }
}
