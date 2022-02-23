using System;
using System.Collections.Generic;
using System.Linq;
using WebBaraholkaAPI.Business.Commands.Interfaces;
using WebBaraholkaAPI.Core.Enums;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Models.Db.WeatherTest;

namespace WebBaraholkaAPI.Business.Commands.Implementations;

public class GetWeatherForecastCommand : IGetWeatherForecastCommand
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    public CommandResultResponse<IEnumerable<WeatherForecast>> Execute()
    {
        return new()
        {
            Status = CommandResultStatus.Succeed,
            Body = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray(),
            Errors = new ()
        };
    }
}