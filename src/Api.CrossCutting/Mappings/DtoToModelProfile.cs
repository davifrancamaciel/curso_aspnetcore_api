using Api.Domain.DTOs.City;
using Api.Domain.DTOs.State;
using Api.Domain.DTOs.User;
using Api.Domain.DTOs.ZipCode;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile

    {
        public DtoToModelProfile()
        {
            CreateMap<UserModel, UserCreateDTO>().ReverseMap(); // ReverseMap serve para fazer a convetsão contraria tambem
            CreateMap<UserModel, UserUpdateDTO>().ReverseMap(); // ReverseMap serve para fazer a convetsão contraria tambem
            CreateMap<UserModel, UserDTO>().ReverseMap(); // ReverseMap serve para fazer a convetsão contraria tambem

            CreateMap<StateModel, StateDTO>().ReverseMap(); // ReverseMap serve para fazer a convetsão contraria tambem

            CreateMap<CityModel, CityDTO>().ReverseMap(); // ReverseMap serve para fazer a convetsão contraria tambem
            CreateMap<CityModel, CityCreateDTO>().ReverseMap(); // ReverseMap serve para fazer a convetsão contraria tambem
            CreateMap<CityModel, CityUpdateDTO>().ReverseMap(); // ReverseMap serve para fazer a convetsão contraria tambem

            CreateMap<ZipCodeModel, ZipCodeDTO>().ReverseMap(); // ReverseMap serve para fazer a convetsão contraria tambem
            CreateMap<ZipCodeModel, ZipCodeCreateDTO>().ReverseMap(); // ReverseMap serve para fazer a convetsão contraria tambem
            CreateMap<ZipCodeModel, ZipCodeUpdateDTO>().ReverseMap(); // ReverseMap serve para fazer a convetsão contraria tambem

        }

    }
}
