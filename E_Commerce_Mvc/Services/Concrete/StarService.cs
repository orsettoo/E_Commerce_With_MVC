using AutoMapper;
using E_Commerce_Mvc.Areas.Identity.Data;
using E_Commerce_Mvc.Services.Abstract;
using E_Commerce_Shared;
using E_Commerce_Shared.DTO;
using E_Commerce_Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Mvc.Services.Concrete
{
    public class StarService : IStarService
    {
        private readonly E_Commerce_MvcContext _context;
        private readonly IMapper _mapper;
        public StarService(E_Commerce_MvcContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<string>> GiveStarProduct(StarDTO model)
        {
            ServiceResponse<string> _response = new ServiceResponse<string>();
                int count = 0;
            double? calc = 0;
            var product = await _context.Products.FindAsync(model.ProductId);
            var objDTO= _mapper.Map<Star>(model);
            _context.Stars.Add(objDTO);
            await _context.SaveChangesAsync();
            var calculateStar = await _context.Stars.Where(x => x.ProductId ==model.ProductId).ToListAsync();
            foreach (var item in calculateStar)
            {
                count++;
                calc = calc + item.RateStar;
            }
            product.RateAvg = calc / count;
            await _context.SaveChangesAsync();
           _response.Success = true;
            _response.Message = "Your Star is Sended";
            return _response;

        }
    }
}
