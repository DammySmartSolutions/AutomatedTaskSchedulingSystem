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

namespace AutomatedTaskSchedulingSystem.UnitTest.TestingServices
{
    
        [TestClass]
        public class ForgotPassServiceTesting
        {
            [TestMethod]
            public void ChangePassword_ShouldReturnError_WhenEmpIdIsNull_FailedTest()
            {
                var service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.ForgotPassService(new Mock<ATSSEntities>().Object);
                var result = service.ChangePassword(null, "123", "123");
                Assert.AreEqual("success", result);
                //Assert.AreEqual("Please Enter your EmployeeID", result);

        }

            [TestMethod]
            public void ChangePassword_ShouldReturnError_WhenNewPasswordIsNull_FailedTest()
            {
                var service = new ForgotPassService(new Mock<ATSSEntities>().Object);
                var result = service.ChangePassword("E001", null, "123");
                Assert.AreEqual("success", result);

                //Assert.AreEqual("Please Enter your New Password", result);
        }

            [TestMethod]
            public void ChangePassword_ShouldReturnError_WhenConfirmPasswordIsNull_FailedTest()
            {
                var service = new ForgotPassService(new Mock<ATSSEntities>().Object);
                var result = service.ChangePassword("E001", "123", null);
                Assert.AreEqual("success", result);
                // Assert.AreEqual("Please Enter your Confirm Password", result);

            }

            [TestMethod]
            public void ChangePassword_ShouldReturnError_WhenPasswordsDoNotMatch_FailedTest()
            {
                var service = new ForgotPassService(new Mock<ATSSEntities>().Object);
                var result = service.ChangePassword("E001", "123", "456");
                Assert.AreEqual("success", result);
               // Assert.AreEqual("Password MisMatch", result);
        }


        [TestMethod]
        public void ChangePassword_ShouldReturnSuccess_WhenValidInput()
        {
            // Arrange
            var mockDbSet = new Mock<DbSet<tblSetupSysUser>>();
            var users = new List<tblSetupSysUser>
            {
                new tblSetupSysUser { EmpID = "E001", Password = "OldEncryptedPass" }
            }.AsQueryable();

            mockDbSet.As<IQueryable<tblSetupSysUser>>().Setup(m => m.Provider).Returns(users.Provider);
            mockDbSet.As<IQueryable<tblSetupSysUser>>().Setup(m => m.Expression).Returns(users.Expression);
            mockDbSet.As<IQueryable<tblSetupSysUser>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockDbSet.As<IQueryable<tblSetupSysUser>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var mockContext = new Mock<ATSSEntities>();
            mockContext.Setup(c => c.tblSetupSysUser).Returns(mockDbSet.Object);
            mockContext.Setup(c => c.SaveChanges()).Returns(1);

            var service = new ForgotPassService(mockContext.Object); // Constructor-injected

            // Act
            var result = service.ChangePassword("E001", "newpass123", "newpass123");

            // Assert
            Assert.AreEqual("success", result);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }




        [TestMethod]
        public void ChangePassword_ShouldReturnError_WhenUserNotFound_FailedTest()
        {
            // Arrange
            var users = new List<tblSetupSysUser>()  // empty list means user is not found
                .AsQueryable();

            var mockDbSet = new Mock<DbSet<tblSetupSysUser>>();
            mockDbSet.As<IQueryable<tblSetupSysUser>>().Setup(m => m.Provider).Returns(users.Provider);
            mockDbSet.As<IQueryable<tblSetupSysUser>>().Setup(m => m.Expression).Returns(users.Expression);
            mockDbSet.As<IQueryable<tblSetupSysUser>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockDbSet.As<IQueryable<tblSetupSysUser>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var mockContext = new Mock<ATSSEntities>();
            mockContext.Setup(c => c.tblSetupSysUser).Returns(mockDbSet.Object);

            var service = new ForgotPassService(mockContext.Object);

            // Act
            var result = service.ChangePassword("E999", "newpass123", "newpass123");

            // Assert

            Assert.AreEqual("success", result);
           // Assert.AreEqual("System User does not exist", result);
        }











    }





}








