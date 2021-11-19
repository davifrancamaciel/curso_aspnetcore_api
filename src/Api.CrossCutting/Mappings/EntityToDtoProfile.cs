using Api.Domain.DTOs.City;
using Api.Domain.DTOs.State;
using Api.Domain.DTOs.User;
using Api.Domain.DTOs.ZipCode;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<UserDTO, UserEntity>().ReverseMap();
            CreateMap<UserCreateResultDTO, UserEntity>().ReverseMap();
            CreateMap<UserUpdateResultDTO, UserEntity>().ReverseMap();

            CreateMap<StateDTO, StateEntity>().ReverseMap();
            
            CreateMap<CityDTO, CityEntity>().ReverseMap();
            CreateMap<CityCompleteDTO, CityEntity>().ReverseMap();
            CreateMap<CityCreateResultDTO, CityEntity>().ReverseMap();
            CreateMap<CityUpdateResultDTO, CityEntity>().ReverseMap();

            CreateMap<ZipCodeDTO, ZipCodeEntity>().ReverseMap();
            CreateMap<ZipCodeCreateResultDTO, ZipCodeEntity>().ReverseMap();
            CreateMap<ZipCodeUpdateResultDTO, ZipCodeEntity>().ReverseMap();
        }
    }
}
