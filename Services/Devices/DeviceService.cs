using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vaccine_chain_bk.DTO.Device;
using vaccine_chain_bk.Exceptions;
using vaccine_chain_bk.Models;
using vaccine_chain_bk.Repositories.Devices;

namespace vaccine_chain_bk.Services.Devices
{
    public class DeviceService : IDeviceService
    {
        private readonly IMapper _mapper;
        private readonly IDeviceRepository _deviceRepository;

        public DeviceService(IMapper mapper, IDeviceRepository deviceRepository)
        {
            _mapper = mapper;
            _deviceRepository = deviceRepository;
        }

        public DeviceDto CreateDevice(CreateDeviceDto createDeviceDto)
        {
            Device device = _mapper.Map<Device>(createDeviceDto);
            _deviceRepository.SaveDevice(device);

            return _mapper.Map<DeviceDto>(device);
        }

        public string DeleteDevice(string id)
        {
            Device getDevice = _deviceRepository.GetDevice(id) ?? throw new DirectoryNotFoundException("Device does not exists");
            _deviceRepository.DeleteDevice(getDevice);

            return "Delete successful";
        }

        public List<DeviceDto> GetAll()
        {
            List<Device> Devices = _deviceRepository.GetAll();
            return _mapper.Map<List<DeviceDto>>(Devices);
        }

        public DeviceDto GetById(string id)
        {
            Device getDevice = _deviceRepository.GetDevice(id) ?? throw new NotFoundException("Device does not exists");
            return _mapper.Map<DeviceDto>(getDevice);
        }   

        public DeviceDto UpdateDevice(string id, UpdateDeviceDto updateDeviceDto)
        {
            _ = _deviceRepository.GetDevice(id) ?? throw new NotFoundException("Device does not exists");

            Device aMToUpdate = _mapper.Map<Device>(updateDeviceDto);
            aMToUpdate.DeviceId = id;
            _deviceRepository.UpdateDevice(aMToUpdate);

            return _mapper.Map<DeviceDto>(aMToUpdate);
        }
    }
}
