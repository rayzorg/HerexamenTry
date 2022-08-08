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

       
        private  static readonly List<JongereDTO> jongereList = new(10);
        static JongereController()
        {
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
        public IEnumerable<JongereDTO> GetForecasts()
        {
            return jongereList;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public JongereDTO CreateForecast( JongereDTO jongere)
        {
            jongereList.Add(jongere);
            return jongere;
        }
        [HttpPost("delete")]
        [Authorize(Roles = "Admin")]
        public JongereDTO DeleteForecast(JongereDTO jongere)
        {
            jongereList.Remove(jongere);
            return jongere;
        }
    }
}
