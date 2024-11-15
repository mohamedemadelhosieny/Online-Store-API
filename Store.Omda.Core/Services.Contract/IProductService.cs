using Store.Omda.Core.Dtos.Products;
using Store.Omda.Core.Entities;
using Store.Omda.Core.Helper;
using Store.Omda.Core.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Omda.Core.Services.Contract
{
    public interface IProductService
    {
        Task<PaginationResponse<ProductDto>> GetAllProductsAsync(ProductspecParams productSpecParams);
        Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync();
        Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync();

        Task<ProductDto> GetProductById(int id); 
    }
}
