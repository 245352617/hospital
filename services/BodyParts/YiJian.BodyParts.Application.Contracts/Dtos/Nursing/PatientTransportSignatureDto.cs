using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    public class PatientTransportSignatureDto
    {
        public string Id { get; set; }

        public string PI_ID { get; set; }

        public string signImage { get; set; }

        public string SignNurseName { get; set; }
        public string SignNurseCode { get; set; }

        public string signImage2 { get; set; }
        public string SignNurseName2 { get; set; }
        public string SignNurseCode2 { get; set; }

    }
}
