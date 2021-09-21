using Api.Domain.DTOs.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile

    {
        public DtoToModelProfile()
        {
            CreateMap<UserModel, UserDTO>().ReverseMap(); // ReverseMap serve para fazer a convetsão contraria tambem
        }

    }
}
