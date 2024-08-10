namespace vaccine_chain_bk.DTO.Vaccine
{
    public class CreateVaccineDto
    {
        public string VaccineId { get; set; }

        public string VaccineName { get; set; }

        public string Manufacturer { get; set; }

        public string BatchNumber { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}   
