using System.ComponentModel.DataAnnotations;

namespace FormsApp.Models;

public class Category
{
    [Display(Name = "Category Name")]
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
}