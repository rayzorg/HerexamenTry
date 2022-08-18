using Auth0.ManagementApi;
using HerexamenTry.Server.Data;
using HerexamenTry.Shared;
using HerexamenTry.Shared.Domain;
using HerexamenTry.Shared.DTO;
using HerexamenTry.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerexamenTry.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
   
    public class BegeleiderController : Controller
    {
        private readonly IBegeleiderService _begeleiderService;

        public BegeleiderController(IBegeleiderService begeleiderService)
        {
            _begeleiderService = begeleiderService;
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public Task<IEnumerable<Jongere>> GetJongeren()
        {
            return _begeleiderService.GetJongereAsync();
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public  Task<Jongere> CreateJongereAsync(JongereDTO jongere)
        {
            return _begeleiderService.CreateJongere(jongere);
          
        }
        [HttpPost("createReactie/{id}")]
        [Authorize(Roles = "Admin")]
        public Task<Reactie> CreateReactieAsync(ReactieDTO reactie,int id)
        {
            return _begeleiderService.createReactieAsync(reactie,id);

        }

        [HttpGet("allReacties")]
        public Task<IEnumerable<Reactie>> GetReacties()
        {
            return _begeleiderService.GetReacties();
        }
        [HttpDelete("delete/{email}")]
        [Authorize(Roles = "Admin")]
        public  Task<Jongere> DeleteJongereAsync(string email)
        {

            return _begeleiderService.DeleteJongere(email);
           
        }

        [HttpDelete("deletePost/{id}")]
        [Authorize(Roles = "Admin")]
        public void DeletePost(int id)
        {
            _begeleiderService.DeletePost(id);
        }
    }
}
