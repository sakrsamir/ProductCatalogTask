using ProductCatalog.Models;
using ProductCatalog.View_Models;
using System.Linq;
using System.Web.Mvc;

namespace ProductCatalog.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Product
        public ActionResult CreateOrUpdate(int Id=0)
        {
            var viewModel = new ProductViewModel();
            ViewBag.Title = "New Product";
            if (Id!=0) // Edit
            {
                var product = _context.Products.Where(p => p.Id == Id).FirstOrDefault();
                if (product!=null)
                {
                    viewModel.Id = product.Id;
                    viewModel.Image=viewModel.GetImage(Server.MapPath("~/ProductsImages"));
                    viewModel.Name = product.Name;
                    viewModel.Price = product.Price;

                    ViewBag.Title = "Update Product";

                }
            }
            return View(viewModel);
        }



        [HttpPost]
        public ActionResult CreateOrUpdate(ProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("Create");

            if (viewModel.Id!=0) // update
            {


            }

            else // create new 
            {
                var product = new Product
                {
                    Name = viewModel.Name,
                    Price = viewModel.Price
                };
                _context.Products.Add(product);
                _context.SaveChanges();


                viewModel.Id = product.Id; // update id to save image named with this id
                viewModel.SaveImageToserver(Request.Files[0], Server.MapPath("~/ProductsImages"));

                
            }
            

            

            return RedirectToAction("Index","Home");
        }


        public ActionResult ListAllProducts()
        {
            var products = _context.Products
                .Select(p => 
                new ProductViewModel
                {
                    Id = p.Id, Name = p.Name,Price=p.Price
                }).ToList();
            return View(products);

            //List<ProductViewModel> productViewModels = new List<ProductViewModel>();
            //foreach (var p in products)
            //{
            //    ProductViewModel productViewModel=new ProductViewModel {Id=p.Id,Name=p.Name }
            //}
            
        }
    }
}