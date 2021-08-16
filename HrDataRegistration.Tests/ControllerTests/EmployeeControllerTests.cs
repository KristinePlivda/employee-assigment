using HrDataRegistration.Controllers;
using HrDataRegistration.DataAccess;
using HrDataRegistration.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HrDataRegistration.Tests.ControllerTests
{
    public class EmployeeControllerTests
    {
        private Mock<IEmployeeRepository> _employeeRepositoryMock;

        private EmployeeController _target;

        [SetUp]
        public void Setup()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _target = new EmployeeController(_employeeRepositoryMock.Object);
        }

        [Test]
        public void EmployeeList_EmptyList_ReturnsEmptyList()
        {
            _employeeRepositoryMock.Setup(e => e.GetEmployees()).Returns(Task.FromResult(new List<Employee>()));
            var actual = _target.EmployeeList().Result;

            Assert.IsInstanceOf(typeof(OkObjectResult), actual);
            var result = actual as OkObjectResult;
            Assert.IsNotNull(result.Value);
            CollectionAssert.IsEmpty(result.Value as List<Employee>);
        }

        [Test]
        public void EmployeeList_EmployeeList_ReturnsEmployees()
        {
            var expected = new List<Employee>()
            {
                new Employee { Id = 1, FirstName = "Meredith", LastName = "White", SocialSecurityNumber = "21019311334", PhoneNumber = "20239184", CreationDate = DateTime.Parse("2021-08-01") },
                new Employee { Id = 2, FirstName = "Cindy", LastName = "Carter", SocialSecurityNumber = "21039111234", PhoneNumber = "20239184", CreationDate = DateTime.Parse("2021-08-01") },
                new Employee { Id = 3, FirstName = "Bart", LastName = "Li", SocialSecurityNumber = "22019611334", PhoneNumber = "20239444", CreationDate = DateTime.Parse("2021-08-01") },
            };
            _employeeRepositoryMock.Setup(e => e.GetEmployees())
                .Returns(Task.FromResult(expected));
            var actual = _target.EmployeeList().Result;

            Assert.IsInstanceOf(typeof(OkObjectResult), actual);
            var result = actual as OkObjectResult;
            Assert.IsNotNull(result.Value);
            CollectionAssert.AreEqual(result.Value as List<Employee>, expected);
        }

        [Test]
        public void EmployeeList_ExceptionOccured_BadRequest()
        {
            _employeeRepositoryMock.Setup(e => e.GetEmployees())
                .Throws(new Exception());
            var actual = _target.EmployeeList().Result;

            Assert.IsInstanceOf(typeof(BadRequestObjectResult), actual);
            var result = actual as BadRequestObjectResult;
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf(typeof(Exception), result.Value);
        }

        [Test]
        public void CreateEmployee_InvalidModelState_BadRequest()
        {
            var expected = new Employee { Id = 1, FirstName = "Meredith", LastName = "White", SocialSecurityNumber = "21019311334", PhoneNumber = "20239184", CreationDate = DateTime.Parse("2021-08-01") };
            _employeeRepositoryMock.Setup(e => e.InsertEmployee(It.IsAny<Employee>())).Returns(Task.FromResult(expected));
            _target.ModelState.AddModelError("test","error");

            var actual = _target.CreateEmployee(null).Result;

            Assert.IsInstanceOf(typeof(BadRequestResult), actual);
        }

        [Test]
        public void CreateEmployee_RepositoryReturnedNull_BadRequest()
        {
            var expected = new Employee { Id = 1, FirstName = "Meredith", LastName = "White", SocialSecurityNumber = "21019311334", PhoneNumber = "20239184", CreationDate = DateTime.Parse("2021-08-01") };

            _employeeRepositoryMock.Setup(e => e.InsertEmployee(It.IsAny<Employee>())).Returns(Task.FromResult<Employee>(null));

            var actual = _target.CreateEmployee(expected).Result;

            Assert.IsInstanceOf(typeof(BadRequestResult), actual);
        }

        [Test]
        public void CreateEmployee_Exception_BadRequest()
        {
            var expected = new Employee { Id = 1, FirstName = "Meredith", LastName = "White", SocialSecurityNumber = "21019311334", PhoneNumber = "20239184", CreationDate = DateTime.Parse("2021-08-01") };

            _employeeRepositoryMock.Setup(e => e.InsertEmployee(It.IsAny<Employee>()))
                .Throws(new Exception());

            var actual = _target.CreateEmployee(expected).Result;

            Assert.IsInstanceOf(typeof(BadRequestObjectResult), actual);
            var result = actual as BadRequestObjectResult;
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf(typeof(Exception), result.Value);
        }

        [Test]
        public void CreateEmployee_GoodRequest_EmployeeAdded()
        {
            var expected = new Employee { Id = 1, FirstName = "Meredith", LastName = "White", SocialSecurityNumber = "21019311334", PhoneNumber = "20239184", CreationDate = DateTime.Parse("2021-08-01") };

            _employeeRepositoryMock.Setup(e => e.InsertEmployee(It.IsAny<Employee>())).Returns(Task.FromResult(expected));

            var actual = _target.CreateEmployee(expected).Result;

            Assert.IsInstanceOf(typeof(OkObjectResult), actual);
            var result = actual as OkObjectResult;
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(result.Value as Employee, expected);
        }

    }
}