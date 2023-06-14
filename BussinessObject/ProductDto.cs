using System;
namespace BussinessObject
{
	public class ProductDto
	{
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public int Weight { get; set; }
        public double UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
    }
}

