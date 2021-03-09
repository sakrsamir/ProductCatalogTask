using ProductCatalog.Models;
using ProductCatalog.View_Models;
using System;
using System.Collections.Generic;
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


        public ActionResult Index()
        {
            var products = _context.Products
                .Where(p=>p.IsDeleted != true)
                .Select(p => 
                new ProductViewModel
                {
                    Id = p.Id, Name = p.Name,Price=p.Price
                }).ToList();
            var HomeViewModel = new HomeViewModel
            {
                products = products,
                AddOrEditModel = new ProductViewModel { }
            };
            ViewBag.Title = "New Product";
            return View(HomeViewModel);
        }

        public ActionResult _ListAllProducts(string name="") // select all && use for filter
        {
            List<ProductViewModel> products;
           
                products = _context.Products
                .Where(p => p.IsDeleted != true&&p.Name.Contains(name))
                .Select(p =>
                new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                }).ToList();
            
            return PartialView(products);
        }

        [HttpPost]
        public ActionResult DeleteProduct(int Id)
        {
            var product = _context.Products.Where(p => p.Id == Id).FirstOrDefault();
            if (product!=null)
            {
                product.IsDeleted = true;
                _context.SaveChanges();
            }
            return RedirectToAction("_ListAllProducts","Product");
        }

        public ActionResult _CreateOrUpdate(int Id = 0)
        {
            var viewModel = new ProductViewModel();
            ViewBag.Title = "New Product";
            if (Id != 0) // Edit
            {
                var product = _context.Products.Where(p => p.Id == Id).FirstOrDefault();
                if (product != null)
                {
                    viewModel.Id = product.Id;
                    viewModel.Image = viewModel.GetImage(Server.MapPath("~/ProductsImages"));
                    viewModel.Name = product.Name;
                    viewModel.Price = product.Price;

                    ViewBag.Title = "Update Product";

                }
            }
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult _CreateOrUpdate(ProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("_ListAllProducts");



            if (viewModel.Id != 0) // update
            {
                var product = _context.Products.Where(p => p.Id == viewModel.Id).FirstOrDefault();
                product.Name = viewModel.Name;
                product.Price = viewModel.Price;
                product.LastUpdated = DateTime.Now;


            }

            else // create new 
            {
                var product = new Product
                {
                    Name = viewModel.Name,
                    Price = viewModel.Price
                };
                _context.Products.Add(product);


                if (Request.Files.Count>0)
                {
                    viewModel.Id = product.Id; // update id to save image named with this id
                    viewModel.SaveImageToserver(Request.Files[0], Server.MapPath("~/ProductsImages"));
                }
                


            }


            _context.SaveChanges();

            return RedirectToAction("_ListAllProducts");
        }
    }
}