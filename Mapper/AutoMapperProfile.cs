using AutoMapper;
using vaccine_chain_bk.DTO.Device;
using vaccine_chain_bk.DTO.Dose;
using vaccine_chain_bk.DTO.Log;
using vaccine_chain_bk.DTO.User;
using vaccine_chain_bk.DTO.Vaccine;
using vaccine_chain_bk.Models;

namespace vaccine_chain_bk.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterDto, User>();

            CreateMap<CreateVaccineDto, Vaccine>();
            CreateMap<UpdateVaccineDto, Vaccine>();
            CreateMap<Vaccine, VaccineDto>();

            CreateMap<CreateDeviceDto, Device>();
            CreateMap<UpdateDeviceDto, Device>();
            CreateMap<Device, DeviceDto>();

            CreateMap<CreateDoseDto, Dose>();
            CreateMap<UpdateDoseDto, Dose>();
            CreateMap<Dose, DoseDto>();

            CreateMap<CreateLogDto, Log>();
            CreateMap<LogDto, Log>(); //Create new connections
            CreateMap<LogDto, CreateLogDto>(); //Get all connections
            CreateMap<Log, LogDto>();

            CreateMap<Role, RoleDto>();
            CreateMap<User, UserDto>();
        }
    }
}
