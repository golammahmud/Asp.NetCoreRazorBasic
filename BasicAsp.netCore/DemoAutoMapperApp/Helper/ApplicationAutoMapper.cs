using AppDomain.DataModels;
using AutoMapper;
using DemoAutoMapperApp.Models;

namespace DemoAutoMapperApp.Helper
{
    public class ApplicationAutoMapper:Profile
    {
        public ApplicationAutoMapper()
        {
            CreateMap<Product, ProductViewModel>().ForMember(dest=>dest.Category,opt=>opt.MapFrom(src=>src.CategoriList)).ReverseMap();
        }
    }
}
