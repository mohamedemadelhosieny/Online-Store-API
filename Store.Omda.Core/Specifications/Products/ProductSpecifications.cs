using Store.Omda.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Omda.Core.Specifications.Products
{
    public class ProductSpecifications :BaseSpecifications<Product , int>
    {
        public ProductSpecifications(int id) : base(P => P.Id == id)
        {
            ApplyIncludes();
        }

        public ProductSpecifications(ProductspecParams productSpecParams) :base
            (
                P =>
                (string.IsNullOrEmpty(productSpecParams.Search) || P.Name.ToLower().Contains(productSpecParams.Search))
                &&
                (!productSpecParams.BrandId.HasValue || productSpecParams.BrandId == P.BrandId)
                &
                (!productSpecParams.TypeId.HasValue || productSpecParams.TypeId == P.TypeId)
            )
        {


            if (!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch (productSpecParams.Sort) 
                {
                    case "PriceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;

                }
            }
            else
            {
                AddOrderBy(P => P.Name);
            }

            ApplyIncludes();

            ApplyPagination(productSpecParams.PageSize * (productSpecParams.PageIndex - 1) , productSpecParams.PageSize);
        } 

        private void ApplyIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);
        }
    }
}
