using AutoMapper;
using SmallRetail.Data.Models;
using SmallRetail.Web.Resources;

namespace SmallRetail.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResource>();
            CreateMap<Transaction, TransactionResource>();
            CreateMap<TransactionProduct, TransactionProductResource>();
            CreateMap<User, UserResource>();
        }
    }
}