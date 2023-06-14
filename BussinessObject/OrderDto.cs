using System;
namespace BussinessObject
{
	public class OrderDto
	{
        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public double Freight { get; set; }
        public List<ProductOrderDto> ProductList { get; set; }
    }
}

