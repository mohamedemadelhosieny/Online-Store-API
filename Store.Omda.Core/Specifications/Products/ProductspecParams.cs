using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Omda.Core.Specifications.Products
{
    public class ProductspecParams
    {
        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }

        public string? Sort {  get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 1;
    }
}
