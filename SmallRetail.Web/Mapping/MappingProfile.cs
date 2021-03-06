using AutoMapper;
using SmallRetail.Data.Models;
using SmallRetail.Web.Resources;

namespace SmallRetail.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<Transaction, TransactionResponse>();
            CreateMap<TransactionProduct, TransactionProductResponse>();
            CreateMap<User, UserResponse>();

            CreateMap<ProductRequest, Product>();
            CreateMap<TransactionRequest, Transaction>();
            CreateMap<TransactionProductRequest, TransactionProduct>();
            CreateMap<UserRequest, User>();
        }
    }
}