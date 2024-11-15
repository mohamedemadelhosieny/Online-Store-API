using AutoMapper;
using Store.Omda.Core;
using Store.Omda.Core.Dtos.Products;
using Store.Omda.Core.Entities;
using Store.Omda.Core.Helper;
using Store.Omda.Core.Services.Contract;
using Store.Omda.Core.Specifications.Products;
using Store.Omda.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Omda.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<ProductDto>> GetAllProductsAsync(ProductspecParams productSpecParams)
        {
            var spec = new ProductSpecifications(productSpecParams);
            var mappedProduct = _mapper.Map<IEnumerable<ProductDto>>(await _unitOfWork.Repository<Product, int>().GetAllWithSpecAsync(spec));

            var countSpec = new ProductWithCountSpecifications(productSpecParams);
            var count = await _unitOfWork.Repository<Product, int>().GetCountAsync(countSpec);

            return new PaginationResponse<ProductDto>(productSpecParams.PageSize, productSpecParams.PageIndex, count, mappedProduct);
        }
        public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
        {
            return _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repository<ProductBrand,int>().GetAllAsync());
        }



        public async Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync()
        {
            return _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repository<ProductType,int>().GetAllAsync());
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var spec = new ProductSpecifications(id);
            return _mapper.Map<ProductDto>(await _unitOfWork.Repository<Product, int>().GetByIdWithSpecAsync(spec));
        }
    }
}
