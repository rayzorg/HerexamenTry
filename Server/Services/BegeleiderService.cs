using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using HerexamenTry.Server.Data;
using HerexamenTry.Shared;
using HerexamenTry.Shared.Domain;
using HerexamenTry.Shared.DTO;
using HerexamenTry.Shared.Services;
using Microsoft.AspNetCore.Authentication;
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
    public class BegeleiderService : IBegeleiderService
    {

        private readonly ManagementApiClient _managementApiClient;
        private readonly HerexamenContext context;
        private readonly DbSet<Jongere> jongeren;
        private readonly DbSet<Post> posts;
        private readonly DbSet<Reactie> reacties;
        private readonly IHttpContextAccessor accessor;

        public BegeleiderService(ManagementApiClient managementApiClient, HerexamenContext context, IHttpContextAccessor accessor)
        {
            _managementApiClient = managementApiClient;
            this.context = context;
            jongeren = context.Jongeren;
            posts = context.Posts;
            reacties = context.Reacties;
            this.accessor = accessor;
        }

        public async Task<Jongere> CreateJongere(JongereDTO jongere)
        {
            var user = accessor?.HttpContext?.User as ClaimsPrincipal;
            string id = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var users = await _managementApiClient.Users.GetAllAsync(new GetUsersRequest(), new PaginationInfo());
            var begeleider = new Begeleider();

            var listBegeleiders = context.Begeleiders.ToList<Begeleider>();

            var newJongere = new Jongere();
            foreach (var item in users)
            {
                if (item.UserId.Equals(id))
                {
                    foreach(var beg in listBegeleiders)
                    {
                        if (item.Email.Equals(beg.Email))
                        {
                            Debug.WriteLine("begelider is gevonden:" );
                            begeleider = beg;
                        }
                        else
                        {
                            Debug.WriteLine("begelider niet is gevonden:" );
                            begeleider.Email = item.Email;
                            begeleider.Firstname = item.UserName;
                            begeleider.Jongeren = new List<Jongere>();
                            begeleider.Jongeren.Add(newJongere);
                            begeleider.Reacties = new List<Reactie>();

                        }
                    }
                    //await _managementApiClient.Users.DeleteAsync(user.UserId);
                    Debug.WriteLine("user is gevonden:" + id);
                    //  begeleider.Firstname = item.NickName;
                    //  begeleider.Email = item.Email;
                    //  begeleider.Jongeren = GetJongeren();

                }
            }
           
            newJongere.Username = jongere.Username;
            newJongere.Firstname = jongere.Firstname;
            newJongere.Lastname = jongere.Lastname;
            newJongere.Gender = jongere.Gender;
            newJongere.Date = jongere.Date;
            newJongere.Email = jongere.Email;
            newJongere.Password = jongere.Password;
            newJongere.Posts = new List<Post>();
            newJongere.Begeleider = begeleider;
          
            var newUser = new UserCreateRequest();
            newUser.FirstName = jongere.Firstname;
            newUser.LastName = jongere.Lastname;
            newUser.Email = jongere.Email;
            newUser.Password = jongere.Password;
            newUser.UserName = jongere.Username;
            //  user.UserId = auto++;
            newUser.Connection = "Username-Password-Authentication";
            
            await _managementApiClient.Users.CreateAsync(newUser);
            jongeren.Add(newJongere);
            context.SaveChanges();
            // await _managementApiClient.Users.GetAllAsync(new GetUsersRequest(), new PaginationInfo());
            //  await _managementApiClient.Users.CreateAsync(user);
            // jongereList.Add(jongere);
           // Debug.WriteLine("My debug string here");
            return newJongere;
        }

        public async Task<Reactie> createReactieAsync(ReactieDTO reactie,int id)
        {
            var user = accessor?.HttpContext?.User as ClaimsPrincipal;
            string idauth = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var users = await _managementApiClient.Users.GetAllAsync(new GetUsersRequest(), new PaginationInfo());
            var begeleider = new Begeleider();

            var listBegeleiders = context.Begeleiders.ToList<Begeleider>();
            var listPosts = context.Posts.ToList<Post>();
            var post = new Post();
            var newReactie = new Reactie();
            foreach (var item in users)
            {
                if (item.UserId.Equals(idauth))
                {
                    foreach (var beg in listBegeleiders)
                    {
                        if (item.Email.Equals(beg.Email))
                        {
                            Debug.WriteLine("begelider is gevonden:");
                            begeleider = beg;
                            foreach(var p in listPosts)
                            {
                                if (p.Id.Equals(id))
                                {
                                    post = p;
                                    newReactie.Text = reactie.Text;
                                    Debug.WriteLine("begelider is gevonden met info:" + beg.Email);
                                    newReactie.Begeleider = beg;
                                    Debug.WriteLine("post is gevonden met info:" + newReactie.Begeleider.Email);
                                    newReactie.Post=post;
                                    reacties.Add(newReactie);
                                    context.SaveChanges();
                                }
                            }
                           
                        }
                      
                    }
                    //await _managementApiClient.Users.DeleteAsync(user.UserId);
                    Debug.WriteLine("user is gevonden:" + id);
                    //  begeleider.Firstname = item.NickName;
                    //  begeleider.Email = item.Email;
                    //  begeleider.Jongeren = GetJongeren();

                }
            }
           

            return newReactie;
        }

        public async Task<Jongere> DeleteJongere(string email)
        {
            var users = await _managementApiClient.Users.GetAllAsync(new GetUsersRequest(), new PaginationInfo());
            foreach (var user in users)
            {
                if (user.Email.Equals(email))
                {
                    await _managementApiClient.Users.DeleteAsync(user.UserId);
                }
            }

            var jongere = new Jongere();
            var list = context.Jongeren
                
                 .ToList<Jongere>();
            foreach (var item in list )
            {
                if (item.Email.Equals(email))
                {
                    jongere = item;
                    // Debug.WriteLine("het is deze jongere:" + item.Firstname + item.Email);
                    // await _managementApiClient.Users.DeleteAsync();
                    var posts1 = context.Posts.Include(x => x.Jongere).Include(x=>x.Reacties).Where(x => x.Jongere.Email == email).ToList();

                    jongeren.Remove(jongere);
                    foreach (var rea in posts1)
                    {
                        posts.Remove(rea);
                        foreach(var r in rea.Reacties)
                        {
                            reacties.Remove(r);
                        }
                    }
                    context.SaveChanges();
                }
            }


            return jongere;
        }

        public void DeletePost(int id)
        {
            var post = new Post();
            var list = context.Posts.ToList<Post>();
            foreach(var item in list)
            {
                if (item.Id.Equals(id))
                {
                    var reacties1 = context.Reacties.Include(x => x.Post).Where(x=>x.Post.Id==id).ToList();

                    post = item;
                    posts.Remove(post);
                    foreach(var rea in reacties1)
                    {
                        reacties.Remove(rea);
                    }
                    context.SaveChanges();
                }
            }
          //  Post post = posts.SingleOrDefault(x => x.Id == id);
           
        }

        public async Task<IEnumerable<Jongere>> GetJongereAsync()
        {
           var user= accessor?.HttpContext?.User as ClaimsPrincipal;
            
            string id = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            var users = await _managementApiClient.Users.GetAllAsync(new GetUsersRequest(), new PaginationInfo());
            var begeleider = new Begeleider();
            foreach (var item in users)
            {
                if (item.UserId.Equals(id))
                {
                    //await _managementApiClient.Users.DeleteAsync(user.UserId);
                    //Debug.WriteLine("user is gevonden:" + id);
                    //  begeleider.Firstname = item.NickName;
                    //  begeleider.Email = item.Email;
                    //  begeleider.Jongeren = GetJongeren();
                    return jongeren
                .Where(x => x.Begeleider.Email == item.Email);
                
                }
            }
            return null;
        }

        public async Task<IEnumerable<Reactie>> GetReacties()
        {
            var user = accessor?.HttpContext?.User as ClaimsPrincipal;

            string id = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var users = await _managementApiClient.Users.GetAllAsync(new GetUsersRequest(), new PaginationInfo());

            foreach (var item in users)
            {
                if (item.UserId.Equals(id))
                {

                    string role = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                    if (role == "Admin")
                    {
                        Debug.WriteLine("email komen mss overeen" + item.Email);
                        // Debug.WriteLine(posts.Where(x=>x.Jongere.))
                        foreach(var re in reacties)
                        {
                            Debug.WriteLine("alle reacties" + re);
                        }
                        //Debug.WriteLine("alle reacties" + reacties);
                        return context.Reacties
                            .Include(b=>b.Post.Jongere)
                            .ToList();
                        //  .Where(x=>x.Jongeren.);

                    }
                    //await _managementApiClient.Users.DeleteAsync(user.UserId);
                    Debug.WriteLine("user is gevonden:" + id);
                    //  begeleider.Firstname = item.NickName;
                    //  begeleider.Email = item.Email;
                    //  begeleider.Jongeren = GetJongeren();
                    return reacties; 
                       // .Where(x=>x.)

                }
            }
            return null;
        }
    }
}
