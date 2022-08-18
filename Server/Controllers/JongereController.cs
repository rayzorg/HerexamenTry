using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using HerexamenTry.Shared;
using HerexamenTry.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Security.Claims;
using HerexamenTry.Shared.Domain;
using HerexamenTry.Server.Data;
using HerexamenTry.Shared.Services;

namespace HerexamenTry.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
   
    public class JongereController:Controller
    {


        private readonly IJongereService _jongereService;

        public JongereController(IJongereService jongereService)
        {
            _jongereService = jongereService;
        }

        [HttpGet("all")]
        public Task<IEnumerable<Post>> GetPosts()
        {
            return _jongereService.GetPostsAsync();
        }

        [HttpPost("create")]
       // [Authorize(Roles = "Admin")]
        public  Task<Post> CreatePostAsync(PostDTO post)
        {

            return _jongereService.createPostAsync(post);
        }
       
    }
}
