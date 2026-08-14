using CarsShop.Db;
using CarsShop.Dto.Responses;
using CarsShop.Dto.Responses.VehicleRequests;
using CarsShop.Interfeces.Services;
//using CarsShop.DTOs;
//using CarsShop.Models;
using Microsoft.EntityFrameworkCore;
using CarsShop.Db.Models;
using CarsShop.Dto.Responses.VehicleRequests;
using CarsShop.Dto;
//using Microsoft.EntityFrameworkCore;

namespace CarsShop.Services
{
    public class RequestFilterService
    {
        private readonly AppDbContext _context;

        public RequestFilterService(AppDbContext context)
        {
            _context = context;
        }

        /*
        public async Task<PagedResult<VehicleRequestResponse>> GetRequestsAsync(
            int userId,
            RequestFilterDto filter)
        {
            var query = _context.VehicleRequests
                .Include(x => x.Vehicle)
                    .ThenInclude(v => v.VehicleType)
                .Include(x => x.Status)
                .Where(x => x.UserId == userId);


            


                var search = filter.Search.Trim().ToLower();

                query = query.Where(x =>
                    x.Vehicle.Model.ToLower().Contains(search) ||
                    x.Vehicle.Color.ToLower().Contains(search) ||
                    x.Vehicle.VehicleType.Name.ToLower().Contains(search) ||
                    x.Status.Name.ToLower().Contains(search) ||
                    x.Message.ToLower().Contains(search)
                     );

                }


            if (filter.StatusId.HasValue)
            {
                query = query.Where(x =>
                    x.RequestStatusId == filter.StatusId.Value);
            }


            if (filter.From.HasValue)
            {
                query = query.Where(x =>
                    x.CreatedAt >= filter.From.Value);
            }


            if (filter.To.HasValue)
            {
                query = query.Where(x =>
                    x.CreatedAt <= filter.To.Value);
            }


            var total = await query.CountAsync();


            var data = await query
             .OrderByDescending(x => x.CreatedAt)
             .Skip((filter.Page - 1) * filter.PageSize)
             .Take(filter.PageSize)
              .Select(x => new VehicleRequestResponse
              {
               Id = x.Id,
               Vehicle = x.Vehicle.Model,
               Color = x.Vehicle.Color,
               Type = x.Vehicle.VehicleType.Name,
               Message = x.Message,
               Status = x.Status.Name,
               RequestedOn = x.CreatedAt,
                LastUpdate = x.LastUpdate
                })
              .ToListAsync();


            return new PagedResult<VehicleRequestResponse>
            {
                Items = data,
                TotalCount = total,
                Page = filter.Page,
                PageSize = filter.PageSize
            };
        }
        */

        /*
        public async Task<PagedResult<VehicleRequestResponse>> GetRequestsAsync(
     int userId,
     RequestFilterDto filter)
        {
            var query = _context.VehicleRequests
                .Include(x => x.Vehicle)
                    .ThenInclude(v => v.VehicleType)
                .Include(x => x.Status)
                .Where(x => x.UserId == userId)
                .AsQueryable();

            // SEARCH
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                var search = filter.Search.Trim();


                query = query.Where(x =>
                    x.Vehicle.Model.Contains(search) ||
                    x.Vehicle.Color.Contains(search) ||
                    x.Vehicle.VehicleType.Name.Contains(search) ||
                    x.Status.Name.Contains(search) ||
                    x.Message.Contains(search));
            }

            // STATUS
            if (filter.StatusId.HasValue)
            {
                query = query.Where(x =>
                    x.RequestStatusId == filter.StatusId.Value);
            }

            // FROM DATE
            if (filter.FromDate.HasValue)
            {
                var fromDate = filter.FromDate.Value.Date;

                query = query.Where(x =>
                    x.CreatedAt >= fromDate);
            }

            // TO DATE
            if (filter.ToDate.HasValue)
            {
                var toDate = filter.ToDate.Value.Date.AddDays(1);

                query = query.Where(x =>
                    x.CreatedAt < toDate);
            }

            var total = await query.CountAsync();

            var data = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(x => new VehicleRequestResponse
                {
                    Id = x.Id,
                    Vehicle = x.Vehicle.Model,
                    Color = x.Vehicle.Color,
                    Type = x.Vehicle.VehicleType.Name,
                    Message = x.Message,
                    Status = x.Status.Name,
                    RequestedOn = x.CreatedAt,
                    LastUpdate = x.LastUpdate
                })
                .ToListAsync();

            return new PagedResult<VehicleRequestResponse>
            {
                Items = data,
                TotalCount = total,
                Page = filter.Page,
                PageSize = filter.PageSize
            };
        }
        */


        public async Task<PagedResult<VehicleRequestResponse>> GetRequestsAsync(
    int userId,
    RequestFilterDto filter)
        {
            var query = _context.VehicleRequests
                .Include(x => x.Vehicle)
                    .ThenInclude(v => v.VehicleType)
                .Include(x => x.Status)
                .Where(x => x.UserId == userId)
                .AsQueryable();

            // SEARCH
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                var search = filter.Search.Trim().ToLower();

                query = query.Where(x =>
                    (x.Vehicle.Model != null &&
                     x.Vehicle.Model.ToLower().Contains(search)) ||

                    (x.Vehicle.Color != null &&
                     x.Vehicle.Color.ToLower().Contains(search)) ||

                    (x.Vehicle.VehicleType != null &&
                     x.Vehicle.VehicleType.Name.ToLower().Contains(search)) ||

                    (x.Status != null &&
                     x.Status.Name.ToLower().Contains(search)) ||

                    (x.Message != null &&
                     x.Message.ToLower().Contains(search))
                            );
            }

            // STATUS
            if (filter.StatusId.HasValue)
            {
                query = query.Where(x =>
                    x.RequestStatusId == filter.StatusId.Value);
            }

            // FROM DATE
            if (filter.FromDate.HasValue)
            {
                var fromDate = filter.FromDate.Value.Date;

                query = query.Where(x =>
                    x.CreatedAt >= fromDate);
            }

            // TO DATE
            if (filter.ToDate.HasValue)
            {
                var toDate = filter.ToDate.Value.Date.AddDays(1);

                query = query.Where(x =>
                    x.CreatedAt < toDate);
            }

            var total = await query.CountAsync();

            var data = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(x => new VehicleRequestResponse
                {
                    Id = x.Id,
                    Vehicle = x.Vehicle.Model,
                    Color = x.Vehicle.Color,
                    Type = x.Vehicle.VehicleType.Name,
                    Message = x.Message,
                    Status = x.Status.Name,
                    RequestedOn = x.CreatedAt,
                    LastUpdate = x.LastUpdate
                })
                .ToListAsync();

            return new PagedResult<VehicleRequestResponse>
            {
                Items = data,
                TotalCount = total,
                Page = filter.Page,
                PageSize = filter.PageSize
            };
        }
    }
}