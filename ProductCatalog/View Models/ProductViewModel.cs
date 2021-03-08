using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;

namespace ProductCatalog.View_Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float Price { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public string Image { get; set; }



        public string GetImage(string path)
        {
            //var path = Server.MapPath("~/ProductsImages");
            var images = Directory.GetFiles(path);
            foreach (var item in images)
            {
                if (Path.GetFileNameWithoutExtension(item) == Id.ToString())
                {
                   return Image = "ProductsImages/" + Path.GetFileName(item);
                }
            }
            return "default-Img-JPG";
           
        }



        public void SaveImageToserver(HttpPostedFileBase file,string path)
        {
             path +=  "\\" + Id + Path.GetExtension(file.FileName);
            file.SaveAs(path);
        }


        

        
    }
}