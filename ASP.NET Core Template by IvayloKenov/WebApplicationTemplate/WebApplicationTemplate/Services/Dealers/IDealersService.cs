namespace WebApplicationTemplate.Services.Dealers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IDealersService
    {
        bool IsDealer(string userId);

        string GetId(string userId);
    }
}
