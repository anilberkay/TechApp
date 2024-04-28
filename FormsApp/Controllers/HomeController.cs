using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Diagnostics.HealthChecks;

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
    public IActionResult Index(string searchString, string category)
    {
        ViewBag.SearchString = searchString;
        var products = Repository.Products;
        if (!string.IsNullOrEmpty(searchString))
        {
            string str1 = searchString.ToLower(new CultureInfo("en-EN", false));
            products = products.Where(p => p.ProductName.ToLower(new CultureInfo("en-EN", false)).Contains(str1))
                .ToList();
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

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Category = new SelectList(Repository.Categories, "CategoryId", "CategoryName");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product model, IFormFile imageFile)
    {


        var extension = "";
        if (imageFile != null)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            extension = Path.GetExtension(imageFile.FileName);
            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("imageFile", "Invalid file type. Please use .jpg, .jpeg, or .png.");
            }
        }

        if (ModelState.IsValid)
        {
            var randomName = Guid.NewGuid().ToString() + extension;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            model.ProductImage = randomName;
            model.ProductId = Repository.Products.Count + 1;
            Repository.CreateProduct(model);
            return RedirectToAction("Index");
        }

        ViewBag.Category = new SelectList(Repository.Categories, "CategoryId", "CategoryName");

        return View(model);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        ViewBag.Category = new SelectList(Repository.Categories, "CategoryId", "CategoryName");
        var product = Repository.Products.FirstOrDefault(p => p.ProductId == id);
        return View(product);
    }

    [HttpPost]

    public async Task<IActionResult> Edit(int id, Product model, IFormFile? imageFile)
    {
        if (id != model.ProductId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };


            if (imageFile != null)
            {
                var extension = Path.GetExtension(imageFile.FileName);
                var randomName = Guid.NewGuid().ToString() + extension;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                model.ProductImage = randomName;
            }

            Repository.EditProduct(model);
            return RedirectToAction("Index");
        }

        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "CategoryName");
        return View(model);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id);
        if (entity == null)
        {
            return NotFound();
        }
        Repository.DeleteProduct(entity);
        return RedirectToAction("Index");
    }
}


    
    
    