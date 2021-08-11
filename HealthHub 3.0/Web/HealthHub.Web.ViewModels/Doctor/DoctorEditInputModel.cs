namespace HealthHub.Web.ViewModels.Doctor
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AutoMapper;
    using HealthHub.Data.Models;
    using HealthHub.Services.Mapping;

    public class DoctorEditInputModel : DoctorInputModel, IMapFrom<Doctor>
    {
        public string Id { get; set; }

    }
}
