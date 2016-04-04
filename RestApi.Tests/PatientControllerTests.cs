namespace RestApi.Tests
{
    using Microsoft.Practices.Unity;
    using NUnit.Framework;
    using RestApi.App_Start;
    using RestApi.Controllers;
    using RestApi.Interfaces;
    using RestApi.Models;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Dependencies;

    [TestFixture]
    public class PatientControllerTests
    {
        private IUnityContainer _container = null;
        private IDependencyResolver _resolver = null;

        [SetUp]
        public void TestSetup()
        {
            // Prepare the IoC configuration and dependency resolver
            _container = IocContainer.ConfigureIocContainer();
            _resolver = IocContainer.GetDependencyResolver(_container);
        }


        [Test]
        public void Given_Memory_And_Incorrect_PatientId_When_Get_Called_Then_Return_404()
        {
            // Override the Db Context with the in memory one and pass it in to the controller
            var context = OverrideIocDependencyAndResolveContext();

            // Check the dependency was overriden
            if (context.GetType() != typeof(InMemoryPatientContext))
            {
                Assert.Inconclusive("Incorrect Context for test");
            }
            else
            {
                var patientController = PrepareController(context);

                patientController.Request = new HttpRequestMessage();
                patientController.Configuration = new HttpConfiguration();

                // Make a request for an incorrect patientId Expect an exception response
                HttpResponseException ex = Assert.Throws<HttpResponseException>(() => patientController.Get(1001));

                // Check the response was a 404 response
                Assert.That(ex.Response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }

        [Test]
        public void Given_Memory_And_Correct_PatientId_When_Get_Called_Then_Return_Correct_Details()
        {
            // Override the Db Context with the in memory one and pass it in to the controller
            var context = OverrideIocDependencyAndResolveContext();

            // Check the dependency was overriden
            if (context.GetType() != typeof(InMemoryPatientContext))
            {
                Assert.Inconclusive("Incorrect Context for test");
            }
            else
            {
                var patientController = PrepareController(context);

                // Make a request for a correct patientId Expect a Patient object response
                var response = patientController.Get(1);
                
                Assert.IsTrue(response.PatientId == 1);
                Assert.IsTrue(response.Episodes != null);
                Assert.IsTrue(response.Episodes.Count() > 0);
            }
        }

        [Test]
        public void Given_DbContext_And_Incorrect_PatientId_When_Get_Called_Then_Return_404()
        {
            // Resolve the Db Context and pass it in to the controller
            var patientController = PrepareController(ResolveContext());

            // Make a request for an incorrect patientId Expect an exception response
            HttpResponseException ex = Assert.Throws<HttpResponseException>(() => patientController.Get(1001));

            // Check the response was a 404 response
            Assert.That(ex.Response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public void Given_DbContext_And_Correct_PatientId_When_Get_Called_Then_Return_Correct_Details()
        {
            // Resolve the Db Context and pass it in to the controller
            var patientController = PrepareController(ResolveContext());

            var response = patientController.Get(1);

            // Make a request for a correct patientId Expect a Patient object response
            Assert.IsTrue(response.PatientId == 1);
            Assert.IsTrue(response.Episodes != null);
            Assert.IsTrue(response.Episodes.Count() > 0);
        }

        private IDatabaseContext OverrideIocDependencyAndResolveContext()
        {
            _container.RegisterType<IDatabaseContext, InMemoryPatientContext>();
            return ResolveContext();
        }

        private IDatabaseContext ResolveContext()
        {
            return _container.Resolve<IDatabaseContext>();
        }

        private PatientsController PrepareController(IDatabaseContext context)
        {
            // Resolve the Db Context and pass it in to the controller
            var patientController = new PatientsController(context);

            patientController.Request = new HttpRequestMessage();
            patientController.Configuration = new HttpConfiguration();

            return patientController;
        }
    }
}
