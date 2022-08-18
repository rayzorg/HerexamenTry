using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using HerexamenTry.Server.Data;
using HerexamenTry.Shared;
using HerexamenTry.Shared.Domain;
using HerexamenTry.Shared.DTO;
using HerexamenTry.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HerexamenTry.Server.Services
{
    public class JongereService : IJongereService
    {
        private readonly ManagementApiClient _managementApiClient;
        private readonly HerexamenContext context;
        private readonly DbSet<Post> posts;
        private readonly IHttpContextAccessor accessor;

        public JongereService(ManagementApiClient managementApiClient, HerexamenContext context, IHttpContextAccessor accessor)
        {
            _managementApiClient = managementApiClient;
            this.context = context;
            posts = context.Posts;
            this.accessor = accessor;
        }

        public async Task<Post> createPostAsync(PostDTO post)
        {
            var newPost = new Post();
            var user = accessor?.HttpContext?.User as ClaimsPrincipal;
            string id = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var users = await _managementApiClient.Users.GetAllAsync(new GetUsersRequest(), new PaginationInfo());

            var jongeren = context.Jongeren.ToList();

            Debug.WriteLine("hello");
            foreach (var item in users)
            {
                if (item.UserId.Equals(id))
                {
                    
                    Debug.WriteLine("hello");
                    Debug.WriteLine("user id en email"+ item.Email,item.UserId);
                    foreach (var beg in jongeren)
                    {
                        Debug.WriteLine("hello "+item.Email+"email beg"+beg.Email);
                       if (item.Email.Equals(beg.Email))
                        {
                            //geraakt niet in deze if dus if statement klopt niet
                            Debug.WriteLine("hello");
                            Debug.WriteLine("post wordt aangemaakt ");
                            newPost.Text=post.Text;
                            newPost.Jongere = beg;
                            newPost.Reacties = new List<Reactie>();

                            posts.Add(newPost);
                            context.SaveChanges();
                            Debug.WriteLine("post is aangemaakt ");
                            Debug.WriteLine("aantal"+posts.Count());
                        }
                       
                    }
                    //await _managementApiClient.Users.DeleteAsync(user.UserId);
                    //Debug.WriteLine("user is gevonden:" + id);
                    //  begeleider.Firstname = item.NickName;
                    //  begeleider.Email = item.Email;
                    //  begeleider.Jongeren = GetJongeren();

                }
            }
            return newPost;
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            var user = accessor?.HttpContext?.User as ClaimsPrincipal;

            string id = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var users = await _managementApiClient.Users.GetAllAsync(new GetUsersRequest(), new PaginationInfo());
           
            foreach (var item in users)
            {
                if (item.UserId.Equals(id))
                {
                  
                    string role = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                   if(role == "Admin")
                    {
                        Debug.WriteLine("email komen mss overeen" + item.Email);
                        // Debug.WriteLine(posts.Where(x=>x.Jongere.))
                        return context.Posts
                            .Include(b=>b.Jongere.Begeleider).Include(b=>b.Reacties)
                            .Where(x=>x.Jongere.Begeleider.Email == item.Email)
                            .ToList();
                        //  .Where(x=>x.Jongeren.);

                    }
                    //await _managementApiClient.Users.DeleteAsync(user.UserId);
                    Debug.WriteLine("user is gevonden:" + id);
                    //  begeleider.Firstname = item.NickName;
                    //  begeleider.Email = item.Email;
                    //  begeleider.Jongeren = GetJongeren();
                    return context.Posts
                           .Include(b => b.Jongere.Begeleider).Include(b => b.Reacties)
                  .Where(x => x.Jongere.Username == item.UserName).ToList();

                }
            }
            return null;
        }

       
    }
}
