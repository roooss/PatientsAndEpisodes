using System.Data.Entity;
using RestApi.Interfaces;
using System.Collections.Generic;
using System;

namespace RestApi.Models
{
    public class InMemoryPatientContext : IDatabaseContext
    {
        private readonly InMemoryDbSet<Patient> _patients = new InMemoryDbSet<Patient>();
        private readonly InMemoryDbSet<Episode> _episodes = new InMemoryDbSet<Episode>();

        public InMemoryPatientContext()
        {
            // NOTE: This was the appropriate place I could find to put this stub data 
            // without recasting the context and tainting the tests
            _patients.Add(new Patient
            {
                DateOfBirth = new DateTime(1972, 10, 27),
                FirstName = "Millicent",
                PatientId = 1,
                LastName = "Hammond",
                NhsNumber = "1111111111"
            });

            _episodes.Add(new Episode
            {
                AdmissionDate = new DateTime(2014, 11, 12),
                Diagnosis = "Irritation of inner ear",
                DischargeDate = new DateTime(2014, 11, 27),
                EpisodeId = 1,
                PatientId = 1
            });
        }

        public IDbSet<Patient> Patients
        {
            get { return _patients; }
        }

        public IDbSet<Episode> Episodes
        {
            get { return _episodes; }
        }
    }
}