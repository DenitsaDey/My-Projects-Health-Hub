using System;
using System.Collections.Generic;
using System.Text;

namespace HealthHub.Services
{
    public interface IDateTimeParserService
    {
        DateTime ConvertStrings(string date, string time);
    }
}
