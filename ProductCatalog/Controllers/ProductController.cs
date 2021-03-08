using ProductCatalog.Models;
using ProductCatalog.View_Models;
using System.IO;
using System.Web;
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
        public ActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Create(ProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("Create");
       
            var product = new Product
            {
                Name=viewModel.Name,
                Price=viewModel.Price
            };
            _context.Products.Add(product);
            _context.SaveChanges();

            // img upload to server folder
            HttpPostedFileBase file = Request.Files[0];
            string path = Server.MapPath("~/ProductsImages") + "\\" + product.Id + Path.GetExtension(file.FileName);
            file.SaveAs(path);

            return RedirectToAction("Index","Home");
        }
    }
}