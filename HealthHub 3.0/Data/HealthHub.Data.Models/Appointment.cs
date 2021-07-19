﻿namespace HealthHub.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using HealthHub.Data.Common.Models;
    using HealthHub.Data.Models.Enums;

    using static HealthHub.Data.Common.DataConstants;

    public class Appointment : BaseDeletableModel<string>
    {
        public Appointment() => this.Id = Guid.NewGuid().ToString();

        //зa да показва само дата без час използваме атрибут [DataType(...Date)] 
        //или във view-то направо с DateTime.UTCNow.ToString("dd-MM-yyyy")
        public DateTime AppointmentTime { get; set; }

        [Required]
        public string ProcedureId { get; set; }

        public virtual Service ProcedureBooked { get; set; }

        //patients issue additional description
        [MaxLength(MessageMaxLength)]
        public string Message { get; set; }

        [Required]
        public string PatientId { get; set; }

        public ApplicationUser Patient { get; set; }

        [Required]
        public string DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        //the Doctor can confirm or decline appointment
        public AppointmentStatus AppointmentStatus { get; set; }

        // For every past (and confirmed) appointment the Patient can Rate the Doctor
        // But rating can be given only once for each appointment
        public bool HasBeenVoted { get; set; }

        public string RatingId { get; set; }

        public virtual Rating Rating { get; set; }
    }
}