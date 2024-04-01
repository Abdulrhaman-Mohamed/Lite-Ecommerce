using LiteEcommerceApi.Dots;
using LiteEcommerceApi.Helper;
using LiteEcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LiteEcommerceApi.Services
{
    public class Product : IProduct
    {
        private readonly ApplicationDbContext dbContext;
        public Product(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<Resposne> addProductService(ProductDots product)
        {
            if( await dbContext.Products.AnyAsync(p=>p.ProductCode == product.ProductCode)) return new Resposne { Message="Product is Here" ,Status=404};


            try
            {
                var imagePath="";
                string filename = "";
                if (product.Image is not null) {
                     filename = Guid.NewGuid().ToString() + "_" + product.Image.FileName;

                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", filename);

                    product.Image.CopyTo(new FileStream(imagePath, FileMode.Create));
                }
   

                var product_ = new Models.Product { 
                    ProductCode = product.ProductCode,
                    Price = product.Price,
                    ProductName = product.ProductName,
                    Discount = product.Discount,
                    Quantity = product.Quantity,
                    Image= filename,
                    CategoryId = product.Category
                };

                await dbContext.Products.AddAsync(product_);

                await dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return new Resposne { Message=ex.Message , Status = 500};
            }

            return new Resposne { Message="Created" , Status = 201};
        }

        public async Task<List<Models.Product>> getProducts()
        {
            return await dbContext.Products.Include(c=>c.Category).ToListAsync();
        }
    }
}
