using AutoMapper;
using vaccine_chain_bk.DTO.Dose;
using vaccine_chain_bk.Exceptions;
using vaccine_chain_bk.Models;
using vaccine_chain_bk.Repositories.Does;
using vaccine_chain_bk.Repositories.Vaccines;

namespace vaccine_chain_bk.Services.Doses
{
    public class DoseService : IDoseService
    {
        private readonly IMapper _mapper;
        private readonly IDoseRepository _doseRepository;
        private readonly IVaccineRepository _vaccineRepository;

        public DoseService(IMapper mapper, IDoseRepository doseRepository, IVaccineRepository vaccineRepository)
        {
            _mapper = mapper;
            _doseRepository = doseRepository;
            _vaccineRepository = vaccineRepository;
        }

        public DoseDto CreateDose(CreateDoseDto createDoseDto)
        {

            Vaccine getVaccine = _vaccineRepository.GetVaccine(createDoseDto.VaccineId) ?? throw new NotFoundException("VaccineId is not exist!");
            Dose dose = _mapper.Map<Dose>(createDoseDto);
            _doseRepository.SaveDose(dose);

            return _mapper.Map<DoseDto>(dose);
        }

        public string DeleteDose(int id)
        {
            Dose getDose = _doseRepository.GetDose(id) ?? throw new NotFoundException("Dose does not exists");
            _doseRepository.DeleteDose(getDose);

            return "Delete successful";
        }

        public List<DoseDto> GetAll()
        {
            List<Dose> doses = _doseRepository.GetAll();
            return _mapper.Map<List<DoseDto>>(doses);
        }

        public DoseDto GetById(int id)
        {
            Dose getDose = _doseRepository.GetDose(id) ?? throw new NotFoundException("Dose does not exists");
            return _mapper.Map<DoseDto>(getDose);
        }

        public DoseDto UpdateDose(int id, UpdateDoseDto updateDoseDto)
        {
            _ = _doseRepository.GetDose(id) ?? throw new NotFoundException("Dose does not exists");
            updateDoseDto.DoseNumber = id;

            Dose aMToUpdate = _mapper.Map<Dose>(updateDoseDto);
            _doseRepository.UpdateDose(aMToUpdate);

            return _mapper.Map<DoseDto>(aMToUpdate); throw new NotImplementedException();
        }
    }
}
