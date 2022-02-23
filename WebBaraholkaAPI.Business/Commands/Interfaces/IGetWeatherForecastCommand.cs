using System.Collections.Generic;
using WebBaraholkaAPI.Models.Db.WeatherTest;
using WebBaraholkaAPI.Core.Responses;

namespace WebBaraholkaAPI.Business.Commands.Interfaces;

public interface IGetWeatherForecastCommand
{
    CommandResultResponse<IEnumerable<WeatherForecast>> Execute();
}