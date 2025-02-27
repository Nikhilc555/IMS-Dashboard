using System.ComponentModel.DataAnnotations;

namespace IMS_Dashboard.ViewModels.CategoryVM
{
    public class DisplayCategoryViewModel
    {
        //public Guid CategoryId { get; set; }

        //[Display(Name = "Category Name")]
        //public string CategoryName { get; set; }

        //[Display(Name = "Description")]
        //public string? Description { get; set; }

        public int id { get; set; }

        [Display(Name = "Category Name")]
        public string category_name { get; set; }
    }
}
