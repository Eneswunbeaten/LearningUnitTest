using FluentAssertions;
using JobApplicationLibrary.Models;
using JobApplicationLibrary.Services;
using Moq;

namespace JobApplicationLibrary.UnitTest
{
    public class ApplicationEvaluatorUnitTest
    {
        //[SetUp]
        //public void Setup()
        //{

        //}
        [Test]
        public void ApplicantEvaluate_ShouldAgeSmaller_TransFerredToAutoRejected()
        {
            var evaluator = new ApplicationEvaluator(null);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 17
                }
            };
            var appResult=evaluator.Evaluate(form);

            appResult.Should().Be(ApplicationResult.AutoRejected);
        }
        [Test]
        public void ApplicantEvaluate_WithNoTechStack_TransferedToAutoRejected()
        {
            var mockValidator = new Mock<IIdentityValidator>();
            mockValidator.DefaultValue = DefaultValue.Mock;
            mockValidator.Setup(x => x.CountryDataProvider.CountryData.Country).Returns("TURKEY");
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            
            var evaluator=new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 19
                },
                TechStackList = new List<string>() { "", "" }
            };
            var appResult=evaluator.Evaluate(form);
            appResult.Should().Be(ApplicationResult.AutoRejected);
        }
        [Test]
        public void ApplicantEvaluate_WithTechStackOver75_TransferredToAutoAccepted()
        {
            var mockValidator = new Mock<IIdentityValidator>();
            mockValidator.DefaultValue = DefaultValue.Mock;
            mockValidator.Setup(x => x.CountryDataProvider.CountryData.Country).Returns("TURKEY");
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);

            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 19
                },
                TechStackList=new List<string>() { "C#", "RabbitMQ", "Microservice", "Visual Studio" },
                YearsOfExperience=16
            };
            var appResult = evaluator.Evaluate(form);
            appResult.Should().Be(ApplicationResult.AutoAccepted);
        }
        [Test]
        public void ApplicantEvaluate_WithInvalidIdentityNumber_TransferredToHR()
        {
            var mockValidator = new Mock<IIdentityValidator>();
            mockValidator.DefaultValue=DefaultValue.Mock;
            mockValidator.Setup(x => x.CountryDataProvider.CountryData.Country).Returns("TURKEY");
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(false);

            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 19
                }
            };
            var appResult = evaluator.Evaluate(form);
            appResult.Should().Be(ApplicationResult.TransferredToHR);
        }
        [Test]
        public void ApplicantEvaluate_WithOfficeLocation_TransferredToCto()
        {
            var mockValidator = new Mock<IIdentityValidator>();
            mockValidator.DefaultValue = DefaultValue.Mock;
            mockValidator.Setup(x => x.CountryDataProvider.CountryData.Country).Returns("GREECE");
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);

            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 19
                }
            };
            var appResult = evaluator.Evaluate(form);
            appResult.Should().Be(ApplicationResult.TransferredToCTO);
        }
    }
}