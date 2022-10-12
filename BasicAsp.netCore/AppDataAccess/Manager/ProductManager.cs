using AppDataAccess.Data;
using AppDataAccess.Interface;
using AppDomain.DataModels;
using BeratenKatuwahDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDataAccess.Manager
{
    public class ProductManager : BaseDataManager, IProduct
    {
        public ProductManager(AppDbContext model) : base(model)
        {
        }

        public Product AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Product DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return GetEntityListData<Product>();
        }

        public Product UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
