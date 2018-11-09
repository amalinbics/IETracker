using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using IETracker.DTO;
using IETracker.Models;

namespace IETracker.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDto>();
                cfg.CreateMap<CategoryDto, Category>();
            });

        }
    }
}