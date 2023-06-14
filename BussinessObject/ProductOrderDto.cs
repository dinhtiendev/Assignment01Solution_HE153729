using System;
namespace BussinessObject
{
	public class ProductOrderDto
	{
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
    }
}

