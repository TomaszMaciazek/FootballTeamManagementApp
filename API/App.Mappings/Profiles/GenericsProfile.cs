using App.Mappings.Converters;
using App.Mappings.Generics;
using App.ServiceLayer.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Mappings.Profiles
{
    public class GenericsProfile : Profile
    {
        public GenericsProfile()
        {
            CreateMap(typeof(Source<>), typeof(Destination<>));
            CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>)).ConvertUsing(typeof(PaginatedListConverter<,>));
        }
    }
}
