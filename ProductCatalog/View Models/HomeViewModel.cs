using System.Collections.Generic;

namespace ProductCatalog.View_Models
{
    public class HomeViewModel
    {
        public ProductViewModel AddOrEditModel { get; set; }
        public IEnumerable<ProductViewModel> products;
    }
}