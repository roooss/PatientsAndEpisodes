using RestApi.Interfaces;
using RestApi.Models;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace RestApi.Controllers
{
    public class PatientsController : ApiController
    {
        public IDatabaseContext DatabaseContext { get; set; }

        public PatientsController(IDatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        [HttpGet]
        public Patient Get(int patientId)
        {
            if (this.DatabaseContext == null)
            {
                throw new System.Exception("Context cannot be null.");
            }

            var patientsAndEpisodes =
                from p in this.DatabaseContext.Patients
                join e in this.DatabaseContext.Episodes on p.PatientId equals e.PatientId
                where p.PatientId == patientId
                select new {p, e};

            if (patientsAndEpisodes.Any())
            {
                var first = patientsAndEpisodes.First().p;
                first.Episodes = patientsAndEpisodes.Select(x => x.e).ToArray();
                return first;
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }
    }
}