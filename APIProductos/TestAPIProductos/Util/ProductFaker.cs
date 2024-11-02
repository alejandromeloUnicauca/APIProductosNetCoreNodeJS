using APIProductos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAPIProductos.Util
{
    public class ProductFaker
    {
        static int id = 0;
        public static Product generate(){
            return new Bogus.Faker<Product>()
                .CustomInstantiator(f => new Product()
                {
                    Id = ++id,
                    Name = f.Commerce.ProductName(),
                    Description = f.Commerce.ProductDescription(),
                    Price = f.Random.Decimal(100, 9999999999),
                    Stock = f.Random.Int(0, 999999)
                }).Generate();
        }
    }
}
