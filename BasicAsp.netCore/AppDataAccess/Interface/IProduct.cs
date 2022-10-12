using AppDomain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDataAccess.Interface
{
    public interface IProduct
    {

        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);

        Product AddProduct(Product product);
        Product UpdateProduct(Product product);

        Product DeleteProduct(int id);
    }
}
