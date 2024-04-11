using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FormsApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
    }

    /// <summary>
    /// Displays the home page with a list of products based on the search string.
    /// </summary>
    /// <param name="searchString">The string used to filter the products.</param>
    /// <returns>The view containing the list of products.</returns>
    public IActionResult Index(string searchString, string category )
    {
        ViewBag.SearchString = searchString;
        var products = Repository.Products;
        if (!string.IsNullOrEmpty(searchString))
        {
            string str1 = searchString.ToLower(new CultureInfo("en-EN", false));
            products = products.Where(p => p.ProductName.ToLower(new CultureInfo("en-EN",false)).Contains(str1)).ToList();
        }

        if (!string.IsNullOrEmpty(category) && category != "0")
        {
            products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();
        }
        
        
        
        
        var model = new ProductViewModel
        {
            Products = products,
            Categories = Repository.Categories,
            SelectedCategory = category
        };
        
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    
}