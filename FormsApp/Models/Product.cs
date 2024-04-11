using System.ComponentModel.DataAnnotations;

namespace FormsApp.Models;

public class Product
{
    [Display(Name = "Product ID")]
    public int ProductId { get; set; }
    public string? ProductImage { get; set; }
    public string? ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    
    public bool IsActive { get; set; }
    public int CategoryId { get; set; }
    
}