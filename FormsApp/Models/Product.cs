using System.ComponentModel.DataAnnotations;

namespace FormsApp.Models;


public class Product
{
    [Display(Name = "Product ID")]
    public int ProductId { get; set; }
    public string? ProductImage { get; set; }
    [Required(ErrorMessage = "Please enter a product name.")]
    public string? ProductName { get; set; }
    
    [Required(ErrorMessage = "Please enter a product price.")]
    [Range(0,100000, ErrorMessage = "Please enter a value between 0 and 100000.")]
    public decimal? ProductPrice { get; set; }
    
    public bool IsActive { get; set; }
    
    [Display(Name = "Category")]
    [Required(ErrorMessage = "Please select a category.")]
    public int? CategoryId { get; set; }
    
}