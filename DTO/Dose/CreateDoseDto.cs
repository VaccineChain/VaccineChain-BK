﻿using vaccine_chain_bk.Constraints;

namespace vaccine_chain_bk.DTO.Dose
{
    public class CreateDoseDto
    {
        public DateTime DateAdministered { get; set; }

        public string LocationAdministered { get; set; }

        public string Administrator { get; set; }

        public string VaccineId { get; set; }
    }
}