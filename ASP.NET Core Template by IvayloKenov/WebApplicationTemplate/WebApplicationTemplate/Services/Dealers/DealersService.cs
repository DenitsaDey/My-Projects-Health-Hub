namespace WebApplicationTemplate.Services.Dealers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebApplicationTemplate.Data;

    public class DealersService : IDealersService
    {
        public readonly ApplicationDbContext data;

        public DealersService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public string GetId(string userId)
        {
            return this.data
                .Dealers
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();
        }

        public bool IsDealer(string userId)
        {
            return this.data.Dealers.Any(d => d.UserId == userId);
        }
    }
}
