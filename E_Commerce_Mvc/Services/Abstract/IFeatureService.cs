using E_Commerce_Shared;
using E_Commerce_Shared.DTO;
using E_Commerce_Shared.Entity;

namespace E_Commerce_Mvc.Services.Abstract
{
    public interface IFeatureService
    {
        Task<ServiceResponse<List<Feature>>> CreateFeatureForProduct(List<FeatureDTO> featureDTO,int productId);
    }
}
