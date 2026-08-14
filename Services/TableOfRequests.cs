/*
using CarsShop.Db;
using CarsShop.Db.Models;
using CarsShop.Dto.Responses.VehicleRequests;
using CarsShop.Interfeces.Services;
using Microsoft.EntityFrameworkCore;


public class RequestService : ITableOfRequests
{

    private readonly AppDbContext context;


    public RequestService(AppDbContext context)
    {
        this.context = context;
    }



    public async Task<IEnumerable<TableOfRequests>> GetRequests(string? search)
    {

        return await context.VehicleRequests

        .Include(x => x.Vehicle)

        .Include(x => x.Status)


        .Select(x => new TableOfRequests

        {

            Id = x.Id,


            Vehicle =
        x.Vehicle.Color + " " +
        x.Vehicle.Model + " " +
        x.Vehicle.Date.Date,


           Image = x.Vehicle.Image,


            Message = x.Message,


            Status = x.Status.Name,


            RequestedOn = x.CreatedAt.Date,


            LastUpdate = x.LastUpdate.Date


        })

        .ToListAsync();

    }

   
}
*/

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


        public async Task<IEnumerable<VehicleRequestResponse>> GetRequests(string? search)
        {
            var query = context.VehicleRequests
                .Include(x => x.Vehicle)
                .Include(x => x.Status)
                .AsQueryable();


            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x =>
                    x.Message.Contains(search) ||
                    x.Vehicle.Model.Contains(search) ||
                    x.Vehicle.Color.Contains(search));
            }


            return await query
                .Select(x => new VehicleRequestResponse
                {
                    Id = x.Id,

                    Vehicle =
                        x.Vehicle.Color + " " +
                        x.Vehicle.Model + " " +
                        x.Vehicle.Date.Date,


                    Image = x.Vehicle.Image,
                    Message = x.Message,

                    Status = x.Status.Name,

                    RequestedOn = x.CreatedAt,

                    LastUpdate = x.LastUpdate
                })
                .ToListAsync();
        }
    }
}