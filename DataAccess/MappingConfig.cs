using System;
using AutoMapper;
using BussinessObject;
using DataAccess.Models;

namespace DataAccess
{
	public class MappingConfig
	{
		public static MapperConfiguration RegisterMaps()
		{
			var mappingConfig = new MapperConfiguration(config =>
			{
				config.CreateMap<Member, MemberDto>();
				config.CreateMap<MemberDto, Member>();
				config.CreateMap<Category, CategoryDto>();
				config.CreateMap<CategoryDto, Category>();
				config.CreateMap<Product, ProductDto>();
				config.CreateMap<ProductDto, Product>();
                config.CreateMap<OrderDto, Order>()
                    .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
                    .ForMember(dest => dest.MemberId, opt => opt.MapFrom(src => src.MemberId))
                    .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                    .ForMember(dest => dest.RequiredDate, opt => opt.MapFrom(src => src.RequiredDate))
                    .ForMember(dest => dest.ShippedDate, opt => opt.MapFrom(src => src.ShippedDate))
                    .ForMember(dest => dest.Freight, opt => opt.MapFrom(src => src.Freight))
                    .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => MapDtoToOrderDetails(src.ProductList)));

                config.CreateMap<Order, OrderDto>()
                    .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
                    .ForMember(dest => dest.MemberId, opt => opt.MapFrom(src => src.MemberId))
                    .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                    .ForMember(dest => dest.RequiredDate, opt => opt.MapFrom(src => src.RequiredDate))
                    .ForMember(dest => dest.ShippedDate, opt => opt.MapFrom(src => src.ShippedDate))
                    .ForMember(dest => dest.Freight, opt => opt.MapFrom(src => src.Freight))
                    .ForMember(dest => dest.ProductList, opt => opt.MapFrom(src => MapOrderDetailsToDto(src.OrderDetails)));
            });
			return mappingConfig;
		}
        private static ICollection<OrderDetail> MapDtoToOrderDetails(List<ProductOrderDto> productOrderDtos)
        {
            var orderDetails = new List<OrderDetail>();

            foreach (var productOrderDto in productOrderDtos)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = productOrderDto.ProductId,
                    UnitPrice = productOrderDto.UnitPrice,
                    Quantity = productOrderDto.Quantity,
                    Discount = productOrderDto.Discount
                };

                orderDetails.Add(orderDetail);
            }

            return orderDetails;
        }

        private static List<ProductOrderDto> MapOrderDetailsToDto(ICollection<OrderDetail> orderDetails)
        {
            var productOrderDtos = new List<ProductOrderDto>();

            foreach (var orderDetail in orderDetails)
            {
                var productOrderDto = new ProductOrderDto
                {
                    ProductId = orderDetail.ProductId,
                    UnitPrice = orderDetail.UnitPrice,
                    Quantity = orderDetail.Quantity,
                    Discount = orderDetail.Discount
                };
                productOrderDtos.Add(productOrderDto);
            }

            return productOrderDtos;
        }
    }
}

