using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeoCare.Models.ViewModels
{
    public class ProductUserVM
    {
        public ProductUserVM()
        {
            ProductList = new List<Product>();
        }

        public AddressDetails addressDetails { get; set; }
        public IList<Product> ProductList { get; set; }
    }
}
