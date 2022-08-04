using Microsoft.AspNetCore.Mvc;
using HerexamenTry.Shared;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace HerexamenTry.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private static readonly List<WeatherForecast> forecasts = new();

        static WeatherForecastController()
        {
            forecasts.AddRange(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                //TemperatureC = Random.Next(-20, 55),
               // Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }));
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> GetForecasts()
        {
            return forecasts;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public WeatherForecast CreateForecast(WeatherForecast forecast)
        {
            forecasts.Add(forecast);
            return forecast;
        }
    }
}