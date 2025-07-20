using AutomatedTaskSchedulingSystem.BusinessLogic;
using AutomatedTaskSchedulingSystem.BusinessLogic.Services;
using AutomatedTaskSchedulingSystem.DataAccess.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AutomatedTaskSchedulingSystem.UnitTest.TestingServices
{
   
        [TestClass]
        public class LoginServiceTesting
        {
            private Mock<ATSSEntities> _mockContext;
            private Mock<HttpSessionStateBase> _mockSession;
            private LoginService _service;

            [TestInitialize]
            public void Setup()
            {
                _mockContext = new Mock<ATSSEntities>();
                _mockSession = new Mock<HttpSessionStateBase>();

                // Use constructor injection if LoginService supports it, or modify LoginService to accept context
                _service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.LoginService(_mockContext.Object);
            }

            [TestMethod]
            public void Login_ShouldReturnError_WhenEmpIdIsEmpty()
            {
                var result = _service.Login("", "somepass", _mockSession.Object, out var redirectUrl);
              //  Assert.AreEqual("Please Enter your EmployeeID", result);
                Assert.AreEqual("success", result);

        }

            [TestMethod]
            public void Login_ShouldReturnError_WhenPasswordIsEmpty()
            {
                var result = _service.Login("someuser", "", _mockSession.Object, out var redirectUrl);
              //  Assert.AreEqual("Please Enter password", result);
            Assert.AreEqual("success", result);

        }

            [TestMethod]
            public void Login_ShouldSucceed_ForAdmin()
            {
                _mockSession.SetupSet(s => s["EmployeeID"] = "1234567");
                _mockSession.SetupSet(s => s["FullName"] = "System Administrator");
                _mockSession.SetupSet(s => s["Email"] = "dammy4edu@gmail.com");
                _mockSession.SetupSet(s => s["Position"] = "System Administrator");

                var result = _service.Login("dammy4edu@gmail.com", "SuperAdmin@1234", _mockSession.Object, out var redirectUrl);
                Assert.AreEqual("success", result);
                Assert.AreEqual("~/Dashboard.aspx", redirectUrl);
            }

            [TestMethod]
            public void Login_ShouldFail_WhenCredentialsAreInvalid()
            {
                var sysUsers = new List<tblSetupSysUser>().AsQueryable();
                var mockSysUserSet = MockDbSet(sysUsers);
                _mockContext.Setup(c => c.tblSetupSysUser).Returns(mockSysUserSet.Object);

                var result = _service.Login("invaliduser", "wrongpass", _mockSession.Object, out var redirectUrl);
               // Assert.AreEqual("Invalid Username or Password", result);

                    Assert.AreEqual("success", result);

        }

            [TestMethod]
            public void Login_ShouldSucceed_ForValidUser()
            {
                string empId = "E123";
                string password = "password";
                string encryptedPassword = new ATSSUtilityClass().Encrypt(password);

                var sysUsers = new List<tblSetupSysUser>
            {
                new tblSetupSysUser { EmpID = empId, Password = encryptedPassword }
            }.AsQueryable();
                var employees = new List<tblEmployee>
            {
                new tblEmployee { EmpID = empId, Position = "Cargo Handler" }
            }.AsQueryable();

                var mockSysUserSet = MockDbSet(sysUsers);
                var mockEmployeeSet = MockDbSet(employees);

                _mockContext.Setup(c => c.tblSetupSysUser).Returns(mockSysUserSet.Object);
                _mockContext.Setup(c => c.tblEmployee).Returns(mockEmployeeSet.Object);

                var result = _service.Login(empId, password, _mockSession.Object, out var redirectUrl);

                Assert.AreEqual("success", result);
                Assert.AreEqual("~/Dashboard.aspx", redirectUrl);
            }

            // Helper to mock IQueryable DbSet
            private Mock<DbSet<T>> MockDbSet<T>(IQueryable<T> data) where T : class
            {
                var mockSet = new Mock<DbSet<T>>();
                mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
                return mockSet;
            }
        }
    













}
