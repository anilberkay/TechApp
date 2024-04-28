namespace FormsApp.Models;

public class Repository
{
    private static readonly List<Product> _products = new();
    private static readonly List<Category> _categories = new();

    static Repository()
    {
        _products.Add(new Product
        {
            ProductId = 1,
            ProductImage = "~/img/2.jpg",
            ProductName = "Iphone 11",
            ProductPrice = 10.99m,
            
            IsActive = true,
            CategoryId = 1
        });
        _products.Add(new Product
        {
            ProductId = 2,
            ProductImage = "~/img/2.jpg",
            ProductName = "Macbook",
            ProductPrice = 19.99m,
            
            IsActive = true,
            CategoryId = 2
        });
        
        _products.Add(new Product
        {
            ProductId = 3,
            ProductImage = "~/img/3.jpg",
            ProductName = "Iphone 13",
            ProductPrice = 5.99m,
            
            IsActive = true,
            CategoryId = 1
        });
        _products.Add(new Product
                {
                    ProductId = 4,
                    ProductImage = "~/img/5.jpg",
                    ProductName = "Macbook Pro",
                    ProductPrice = 7.99m,
                    IsActive = true,
                    CategoryId = 2
                });
        _categories.Add(new Category { CategoryId = 1, CategoryName = "Phone" });
        _categories.Add(new Category { CategoryId = 2, CategoryName = "Computer" });
    }
     
    

    public static List<Product> Products
    {
        get
        {
            return _products;
        }
    }
    
    public static List<Category> Categories
    {
        get
        {
            return _categories;
        }
    }

    public static void EditProduct(Product updatedProduct)
    {
        var entity = _products.FirstOrDefault(p => p.ProductId == updatedProduct.ProductId);
        if (entity != null)
        {
            entity.ProductName = updatedProduct.ProductName;
            entity.ProductPrice = updatedProduct.ProductPrice;
            entity.IsActive = updatedProduct.IsActive;
            entity.CategoryId = updatedProduct.CategoryId;
            entity.ProductImage = updatedProduct.ProductImage;
        }
    }
    public static void CreateProduct(Product product)
    {
        _products.Add(product);
    }

    public static void DeleteProduct(Product product)
    {
        var entity = _products.FirstOrDefault(p=> p.ProductId == product.ProductId);
        if (entity != null)
        {
            _products.Remove(entity);
        }
    }
    
    
}