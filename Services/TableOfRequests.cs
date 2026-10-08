

using CarsShop.Db;
using CarsShop.Db.Models;
using CarsShop.Dto.Responses.VehicleRequests;
using CarsShop.Interfeces.Services;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.Services
{
    public class RequestService : ITableOfRequests
    {
        private readonly AppDbContext context;

        public RequestService(AppDbContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<VehicleRequestResponse>> GetRequests(string? search, int? statusId,DateTime? fromDate,
    DateTime? toDate)
        {
            var query = context.VehicleRequests
                .Include(x => x.Vehicle)
                .Include(x => x.Status)
                .AsQueryable();


            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(x =>
                    x.Message.Contains(search) ||
                    x.Vehicle.Model.Contains(search) ||

                    x.Vehicle.Color.Contains(search) );
            }

            if (statusId.HasValue)
            {
                query = query.Where(x => x.Status.Id == statusId.Value);
            }

           

            if (fromDate.HasValue)
            {
                var from = fromDate.Value.Date;

                query = query.Where(x => x.CreatedAt >= from);
            }

            if (toDate.HasValue)
            {
                var to = toDate.Value.Date.AddDays(1);

                query = query.Where(x => x.CreatedAt < to);
            }


            return await query
                .Select(x => new VehicleRequestResponse
                {
                    Id = x.Id,
                    UserId = x.UserId,

                    Vehicle =
                        x.Vehicle.Color + " " +
                        x.Vehicle.Model + " " +
                        x.Vehicle.Date.Date,


                    Image = x.Vehicle.Image,
                    Message = x.Message,

                    // Latest message sent by the customer
                    LastMessage = context.Messages
                        .Where(m =>
                            m.RequestId == x.Id &&
                            m.CreatedByUserId == x.UserId)
                        .OrderByDescending(m => m.CreatedAt)
                        .Select(m => m.MessageText)
                        .FirstOrDefault(),

                    Status = x.Status.Name,

                    RequestedOn = x.CreatedAt,

                    LastUpdate = x.LastUpdate
                })
                .ToListAsync();
        }
    }
}