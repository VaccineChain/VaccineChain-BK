using AutoMapper;
using vaccine_chain_bk.DTO.Vaccine;
using vaccine_chain_bk.Exceptions;
using vaccine_chain_bk.Models;
using vaccine_chain_bk.Repositories.Vaccines;

namespace vaccine_chain_bk.Services.Vaccines
{
    public class VaccineService : IVaccineService
    {
        private readonly IMapper _mapper;
        private readonly IVaccineRepository _vaccineRepository;

        public VaccineService(IMapper mapper, IVaccineRepository vaccineRepository)
        {
            _mapper = mapper;
            _vaccineRepository = vaccineRepository;
        }

        public VaccineDto CreateVaccine(CreateVaccineDto createVaccineDto)
        {
            Vaccine vaccine = _mapper.Map<Vaccine>(createVaccineDto);
            _vaccineRepository.SaveVaccine(vaccine);

            return _mapper.Map<VaccineDto>(vaccine);
        }

        public string DeleteVaccine(string id)
        {
            Vaccine getVaccine = _vaccineRepository.GetVaccineById(id) ?? throw new NotFoundException("Vaccine does not exists");
            _vaccineRepository.DeleteVaccine(getVaccine);

            return "Delete successful";
        }

        public List<VaccineDto> GetAll()
        {
            List<Vaccine> vaccines = _vaccineRepository.GetAll();
            return _mapper.Map<List<VaccineDto>>(vaccines);
        }

        public VaccineDto GetById(string id)
        {
            Console.WriteLine(id);
            Vaccine getVaccine = _vaccineRepository.GetVaccineById(id) ?? throw new NotFoundException("Vaccine does not exists");
            return _mapper.Map<VaccineDto>(getVaccine);

        }

        public List<VaccineDto> GetByName(string name)
        {
            List<Vaccine> getVaccine = _vaccineRepository.GetVaccineByName(name) ?? throw new NotFoundException("Vaccine does not exists");
            return _mapper.Map<List<VaccineDto>>(getVaccine);

        }

        public VaccineDto UpdateVaccine(string id, UpdateVaccineDto updateVaccineDto)
        {
            _ = _vaccineRepository.GetVaccineById(id) ?? throw new NotFoundException("Vaccine does not exists");

            Vaccine aMToUpdate = _mapper.Map<Vaccine>(updateVaccineDto);
            aMToUpdate.VaccineId = id;
            _vaccineRepository.UpdateVaccine(aMToUpdate);

            return _mapper.Map<VaccineDto>(aMToUpdate);
        }


    }
}
