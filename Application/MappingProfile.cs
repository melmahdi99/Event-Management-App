using System;
using Application.Comment;
using AutoMapper;
using Domain;

namespace Application;

//For AutoMapper extension
//Does need to be registered in our DI Container in Program.cs
public class MappingProfile : Profile //Inherit Profile from AutoMapper
{
    //Constructor
    public MappingProfile()
    {
        //Uses methods from the Profile class
        //We're going to have two different maps
        //ReadActivity mapper
        CreateMap<Domain.Activity, ReadActivityDto>();
        //CreateActivity mapper
        CreateMap<CreateActivityDto, Domain.Activity>();
        CreateMap<Domain.Activity, FullReadActivityDto>();
        CreateMap<FullReadActivityDto, Domain.Activity>();
        CreateMap<CreateCommentDto, Domain.Comment>();
        CreateMap<Domain.Comment, ReadCommentDto>();
    }
}
