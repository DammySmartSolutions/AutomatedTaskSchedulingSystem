using AutomatedTaskSchedulingSystem.BusinessLogic.Services;
using AutomatedTaskSchedulingSystem.BusinessLogic.Services.Interfaces;
using AutomatedTaskSchedulingSystem.DataAccess.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTaskSchedulingSystem.UnitTest.TestingServices
{
    
        [TestClass]
        public class AddEmployeeAvailTesting
        {
            private Mock<ATSSEntities> _mockContext;
            private Mock<DbSet<tblEmployee>> _mockEmployeeSet;
            private Mock<DbSet<tblEmployeeAvail>> _mockAvailSet;
            private AutomatedTaskSchedulingSystem.BusinessLogic.Services.AddEmployeeAvail _service;

       
            private List<tblEmployee> _employeeData;
            private List<tblEmployeeAvail> _availData;

            [TestInitialize]
            public void Setup()
            {
                _employeeData = new List<tblEmployee>
            {
                new tblEmployee { EmpID = "E001", FullName = "John Doe" }
            };

                _availData = new List<tblEmployeeAvail>
            {
                new tblEmployeeAvail { EmpID = "E001", AvailDate = DateTime.Today, Avail = true, AvailID = 1 }
            };

                _mockEmployeeSet = DbSetMockHelper.CreateMockSet(_employeeData);
                _mockAvailSet = DbSetMockHelper.CreateMockSet(_availData);

                _mockContext = new Mock<ATSSEntities>();
                _mockContext.Setup(c => c.tblEmployee).Returns(_mockEmployeeSet.Object);
                _mockContext.Setup(c => c.tblEmployeeAvail).Returns(_mockAvailSet.Object);
                _mockContext.Setup(c => c.SaveChanges()).Returns(1);



            _service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.AddEmployeeAvail(_mockContext.Object);




            }

            [TestMethod]
            public void LoadAvailable_ShouldReturnFormattedData()
            {
                var result = _service.LoadAvailable().ToList();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual("John Doe - E001", result[0].EmpData);
                Assert.AreEqual(true, result[0].Avail);
            }

            [TestMethod]
            public void SaveEmployeeAvail_ShouldCreate_WhenNotExists()
            {
                var newAvail = new tblEmployeeAvail
                {
                    EmpID = "E002",
                    AvailDate = DateTime.Today,
                    Avail = true
                };

                var result = _service.SaveEmployeeAvail(newAvail);

                Assert.AreEqual("created", result);
            }

            [TestMethod]
            public void SaveEmployeeAvail_ShouldUpdate_WhenExists()
            {
                var updated = new tblEmployeeAvail
                {
                    EmpID = "E001",
                    AvailDate = DateTime.Today,
                    Avail = false
                };

                var result = _service.SaveEmployeeAvail(updated);

                Assert.AreEqual("updated", result);
            }




            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void SaveEmployeeAvail_ShouldThrow_WhenModelIsNull_FailedTest()
            {
            var result = _service.SaveEmployeeAvail(null);

            Assert.AreEqual("created", result);


            }
        }
   

}






