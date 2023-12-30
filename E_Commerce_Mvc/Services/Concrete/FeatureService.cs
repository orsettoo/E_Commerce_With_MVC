using E_Commerce_Mvc.Areas.Identity.Data;
using E_Commerce_Mvc.Services.Abstract;
using E_Commerce_Shared;
using E_Commerce_Shared.DTO;
using E_Commerce_Shared.Entity;
using System.Collections.Generic;

namespace E_Commerce_Mvc.Services.Concrete
{
    public class FeatureService : IFeatureService
    {
        private readonly E_Commerce_MvcContext _context;
        private readonly IProductService _productService;
        public FeatureService(E_Commerce_MvcContext context,IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public async Task<ServiceResponse<List<Feature>>> CreateFeatureForProduct(List<FeatureDTO> featureDTO, int productId)
        {
            var result = await _productService.GetProduct(productId);
            ServiceResponse<List<Feature>> _response = new ServiceResponse<List<Feature>>();
            {
                if (result != null)
                {
                    List<Feature> features = new List<Feature>();
                    foreach (var item in featureDTO)
                    {
                        Feature feature = new Feature();
                        feature.ProductId = productId;
                        feature.FeatureName = item.FeatureName;
                        features.Add(feature);
                    }
                    await _context.Features.AddRangeAsync(features);
                    if(await _context.SaveChangesAsync() > 0)
                    {
                        _response.Success = true;
                        _response.Message = "Operation Successfull";
                        _response.Data = features;
                        return _response;
                    }

                }
                return null;
            }
        }
    }
}
