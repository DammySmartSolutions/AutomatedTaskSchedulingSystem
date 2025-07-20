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
    public class AddEmployeeTesting
    {
        private Mock<ATSSEntities> _mockContext;
        private Mock<DbSet<tblEmployee>> _mockEmployeeSet;
        private Mock<DbSet<tblEmployeeeTemp>> _mockTempSet;
        private Mock<IFileService> _mockFileService; // Add this


        private AutomatedTaskSchedulingSystem.BusinessLogic.Services.AddEmployee _service;
        private List<tblEmployee> _employeeData;
        private List<tblEmployeeeTemp> _tempData;
        private Mock<IDatabaseService> _mockDbService;

        [TestInitialize]
        public void Setup()
        {
            
            _employeeData = new List<tblEmployee>();
            _tempData = new List<tblEmployeeeTemp>();

            _mockEmployeeSet = DbSetMockHelper.CreateMockSet(_employeeData);
            _mockTempSet = DbSetMockHelper.CreateMockSet(_tempData);

            _mockContext = new Mock<ATSSEntities>();
            _mockContext.Setup(c => c.tblEmployee).Returns(_mockEmployeeSet.Object);
            _mockContext.Setup(c => c.tblEmployeeeTemp).Returns(_mockTempSet.Object);
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);

            _mockFileService = new Mock<IFileService>(); // Create the mock
           
            _mockDbService = new Mock<IDatabaseService>();
            _mockDbService.Setup(x => x.ExecuteSql(It.IsAny<string>())).Returns(1);


          


            _service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.AddEmployee(_mockContext.Object, _mockFileService.Object, _mockDbService.Object);


        }

        [TestMethod]
        public void SaveEmployee_ShouldCreate_WhenNotExists()
        {
            var emp = new tblEmployee { EmpID = "E001", FirstName = "John", LastName = "Doe", Position = "Driver", Sex = "M" };

            var result = _service.SaveEmployee(emp);

            Assert.AreEqual("created", result);
            Assert.AreEqual(1, _employeeData.Count);
        }

        [TestMethod]
        public void SaveEmployee_ShouldUpdate_WhenExists()
        {
            _employeeData.Add(new tblEmployee { EmpID = "E001", FirstName = "Old", LastName = "Name", Position = "OldPos", Sex = "M" });

            var updatedEmp = new tblEmployee { EmpID = "E001", FirstName = "Jane", LastName = "Smith", Position = "Admin", Sex = "F" };

            var result = _service.SaveEmployee(updatedEmp);

            Assert.AreEqual("updated", result);
            Assert.AreEqual("Jane", _employeeData.First().FirstName);
        }



        [TestMethod]
        public void SaveUploadEmployee_ShouldAdd_NewEntries()
        {
            _tempData.Add(new tblEmployeeeTemp
            {
                EmpID = "E002",
                FirstName = "Lisa",
                LastName = "Brown",
                Position = "Helper",
                Sex = "F"
            });

            var result = _service.SaveUploadEmployee();

            Assert.AreEqual("success", result);
            Assert.AreEqual(1, _employeeData.Count);
        }

        [TestMethod]
        public void SaveUploadEmployee_ShouldSkip_IfAlreadyExists()
        {
            _employeeData.Add(new tblEmployee { EmpID = "E003" });
            _tempData.Add(new tblEmployeeeTemp
            {
                EmpID = "E003",
                FirstName = "Skip",
                LastName = "Me",
                Position = "Technician",
                Sex = "M"
            });

            var result = _service.SaveUploadEmployee();

            Assert.AreEqual("success", result);
            Assert.AreEqual(1, _employeeData.Count); // Should not add new
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SaveEmployee_ShouldThrow_WhenModelIsNull_FailedTest()
        {
            _service.SaveEmployee(null);
        }

        [TestMethod]

   
        public void SaveEmployee_ShouldReturnInvalid_WhenEmpIDIsEmpty_FailedTest()
        {
            var emp = new tblEmployee { EmpID = "", FirstName = "X", LastName = "Y", Position = "Z", Sex = "M" };

            var result = _service.SaveEmployee(emp);

           /// Assert.AreEqual("invalid", result);              // ❌ no "created"


            Assert.AreEqual("created", result);
            Assert.AreEqual(0, _employeeData.Count);         // ❌ nothing saved
        }







        [TestMethod]
        public void SaveUploadEmployee_ShouldReturnMessage_WhenTempIsEmpty_FailedTest()
        {
            var result = _service.SaveUploadEmployee();
            Assert.AreEqual("uploaded", result);

           // Assert.AreEqual("No records found in temp table.", result);
        }

        [TestMethod]
        public void SaveUploadEmployee_ShouldSkipRows_WhenEmpIDIsNullOrEmpty()
        {
            _tempData.Add(new tblEmployeeeTemp { EmpID = null, FirstName = "A", LastName = "B", Position = "C", Sex = "M" });
            _tempData.Add(new tblEmployeeeTemp { EmpID = "", FirstName = "X", LastName = "Y", Position = "Z", Sex = "F" });

            var result = _service.SaveUploadEmployee();

            Assert.AreEqual("success", result);
            Assert.AreEqual(0, _employeeData.Count); // Both records should be skipped
        }




        [TestMethod]
        public void UploadEmployee_ShouldReturnError_WhenFileDoesNotExist_FailedTest()
        {
            var mockFile = new Mock<IFileService>();
            mockFile.Setup(f => f.Exists(It.IsAny<string>())).Returns(false);

            var service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.AddEmployee(_mockContext.Object, mockFile.Object);

            var result = service.UploadEmployee("missing.csv");

            Assert.AreEqual("uploaded", result);

           // Assert.AreEqual("error: File not found.", result);
        }









        [TestMethod]
        public void UploadEmployee_ShouldReturnSuccess_WhenCSVIsValid()
        {
            // Arrange
            string csvData = "EmpID,FirstName,LastName,Sex,Position\r\n1234567,John,Enock,M,Cargo Handler\r\n1234568,Sonia,Smith,F,Cargo Handler";

            var mockFile = new Mock<IFileService>();
            mockFile.Setup(f => f.Exists(It.IsAny<string>())).Returns(true);
            mockFile.Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(csvData);

            var mockDb = new Mock<ATSSEntities>();
            mockDb.Setup(d => d.SaveChanges()).Returns(1);

            var mockDbService = new Mock<IDatabaseService>();
            mockDbService.Setup(d => d.ExecuteSql(It.IsAny<string>())).Verifiable();

            


            string TestConnStr = "Data Source= OLUWADAMILOLA; initial catalog=ATSS_TEST;persist security info=True;user id=sa;password=Manchester43";



            var service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.AddEmployee(
                mockDb.Object,
                mockFile.Object,
                mockDbService.Object,
                TestConnStr
            );


           

            // Act
            var result = service.UploadEmployee("employees.csv");

            // Assert
            Assert.AreEqual("success", result);
            mockDbService.Verify(d => d.ExecuteSql("DELETE FROM tblEmployeeeTemp"), Times.Once);
        }












    }

















}








