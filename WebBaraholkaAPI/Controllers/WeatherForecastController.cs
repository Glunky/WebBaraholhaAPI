using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebBaraholkaAPI.Business.Commands.Interfaces;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Models.Db.WeatherTest;

namespace WebBaraholkaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet]
    public CommandResultResponse<IEnumerable<WeatherForecast>> Get([FromServices] IGetWeatherForecastCommand getWeatherForecastCommand)
    {
        return getWeatherForecastCommand.Execute();
    }
}