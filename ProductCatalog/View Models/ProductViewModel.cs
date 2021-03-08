using System.ComponentModel.DataAnnotations;

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

        public void SaveImageToServerFolder()
        {

        }

        

        
    }
}