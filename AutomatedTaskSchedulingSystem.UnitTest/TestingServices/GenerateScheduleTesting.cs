using AutomatedTaskSchedulingSystem.DataAccess.Model;
using AutomatedTaskSchedulingSystem.BusinessLogic.Services;
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
    public class GenerateScheduleTesting
    {
        private Mock<ATSSEntities> _mockContext;
        private Mock<DbSet<tblEmployee>> _mockEmployeeSet;
        private Mock<DbSet<tblEmployeeAvail>> _mockAvailSet;
        private Mock<DbSet<tblSetupTask>> _mockTaskSet;
        private Mock<DbSet<tblSetupLoc>> _mockLocSet;
        private Mock<DbSet<tblSchedule>> _mockScheduleSet;

        // ✅ Moved outside Setup and removed [TestInitialize]
        private Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet;
        }

        [TestInitialize]
        public void Setup()
        {
            _mockContext = new Mock<ATSSEntities>();

            var empData = new List<tblEmployee>
        {
            new tblEmployee { EmpID = "E1", FullName = "John Doe", Position = "Cargo Handler", Sex = "M" }
        }.AsQueryable();

            var availData = new List<tblEmployeeAvail>
        {
            new tblEmployeeAvail { EmpID = "E1", AvailDate = DateTime.Today, Avail = true }
        }.AsQueryable();

            var taskData = new List<tblSetupTask>
        {
            new tblSetupTask { Task = "Trk Unloader", MinEmployees = 1, MaxEmployees = 1, LocID = 1 }
        }.AsQueryable();

            var locData = new List<tblSetupLoc>
        {
            new tblSetupLoc { LocID = 1, Location = "Dock 1" }
        }.AsQueryable();

            var scheduleData = new List<tblSchedule>().AsQueryable();

            _mockEmployeeSet = CreateMockDbSet(empData);
            _mockAvailSet = CreateMockDbSet(availData);
            _mockTaskSet = CreateMockDbSet(taskData);
            _mockLocSet = CreateMockDbSet(locData);
            _mockScheduleSet = CreateMockDbSet(scheduleData);

            _mockContext.Setup(c => c.tblEmployee).Returns(_mockEmployeeSet.Object);
            _mockContext.Setup(c => c.tblEmployeeAvail).Returns(_mockAvailSet.Object);
            _mockContext.Setup(c => c.tblSetupTask).Returns(_mockTaskSet.Object);
            _mockContext.Setup(c => c.tblSetupLoc).Returns(_mockLocSet.Object);
            _mockContext.Setup(c => c.tblSchedule).Returns(_mockScheduleSet.Object);

            _mockScheduleSet.Setup(m => m.AddRange(It.IsAny<IEnumerable<tblSchedule>>()));
            _mockContext.Setup(m => m.SaveChanges()).Returns(1);
        }

        [TestMethod]
        public void GenerateTaskSchedule_ShouldCreateSchedule_WhenEmployeesAvailable()
        {
            // Arrange
            var scheduleDate = DateTime.Today;
            var service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.GenerateSchedule(_mockContext.Object);

            // Act
            var result = service.GenerateTaskSchedule(scheduleDate);

            // Assert
            _mockScheduleSet.Verify(m => m.AddRange(It.IsAny<IEnumerable<tblSchedule>>()), Times.Once);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
            Assert.AreEqual("created", result);
        }

        [TestMethod]
        public void GenerateTaskSchedule_ShouldNotCreateSchedule_WhenNoEmployeesAvailable_FailedTest()
        {
            // Arrange
            var empData = new List<tblEmployee>
        {
            new tblEmployee { EmpID = "E1", FullName = "John Doe", Position = "Cargo Handler", Sex = "M" }
        }.AsQueryable();

            var availData = new List<tblEmployeeAvail>().AsQueryable(); // No availability
            var taskData = new List<tblSetupTask>
        {
            new tblSetupTask { Task = "Trk Unloader", MinEmployees = 1, MaxEmployees = 1, LocID = 1 }
        }.AsQueryable();

            var locData = new List<tblSetupLoc>
        {
            new tblSetupLoc { LocID = 1, Location = "Dock 1" }
        }.AsQueryable();

            var scheduleData = new List<tblSchedule>().AsQueryable();

            _mockEmployeeSet = CreateMockDbSet(empData);
            _mockAvailSet = CreateMockDbSet(availData);
            _mockTaskSet = CreateMockDbSet(taskData);
            _mockLocSet = CreateMockDbSet(locData);
            _mockScheduleSet = CreateMockDbSet(scheduleData);

            _mockContext.Setup(c => c.tblEmployee).Returns(_mockEmployeeSet.Object);
            _mockContext.Setup(c => c.tblEmployeeAvail).Returns(_mockAvailSet.Object);
            _mockContext.Setup(c => c.tblSetupTask).Returns(_mockTaskSet.Object);
            _mockContext.Setup(c => c.tblSetupLoc).Returns(_mockLocSet.Object);
            _mockContext.Setup(c => c.tblSchedule).Returns(_mockScheduleSet.Object);

            var service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.GenerateSchedule(_mockContext.Object);
            var scheduleDate = DateTime.Today;

            // Act
            var result = service.GenerateTaskSchedule(scheduleDate);

            // Assert
            
            _mockScheduleSet.Verify(m => m.AddRange(It.IsAny<IEnumerable<tblSchedule>>()), Times.Never);
            _mockContext.Verify(m => m.SaveChanges(), Times.Never);  // <--- this is the correction

            Assert.AreEqual("created", result);
        }




        [TestMethod]
        public void GenerateTaskSchedule_ShouldNotAssignTrailerUnloader_ToFemaleEmployees()
        {
            // Arrange
            var empData = new List<tblEmployee>
    {
        new tblEmployee { EmpID = "E1", FullName = "Jane Doe", Position = "Cargo Handler", Sex = "F" }
    }.AsQueryable();

            var availData = new List<tblEmployeeAvail>
    {
        new tblEmployeeAvail { EmpID = "E1", AvailDate = DateTime.Today, Avail = true }
    }.AsQueryable();

            var taskData = new List<tblSetupTask>
    {
        new tblSetupTask { Task = "Trailer Unloader", MinEmployees = 1, MaxEmployees = 1, LocID = 1 }
    }.AsQueryable();

            var locData = new List<tblSetupLoc>
    {
        new tblSetupLoc { LocID = 1, Location = "Dock A" }
    }.AsQueryable();

            var scheduleData = new List<tblSchedule>().AsQueryable();

            // ✅ Use CreateMockDbSet for ALL sets, including schedule
            _mockEmployeeSet = CreateMockDbSet(empData);
            _mockAvailSet = CreateMockDbSet(availData);
            _mockTaskSet = CreateMockDbSet(taskData);
            _mockLocSet = CreateMockDbSet(locData);
            _mockScheduleSet = CreateMockDbSet(scheduleData);

            var capturedSchedules = new List<tblSchedule>();
            _mockScheduleSet.Setup(m => m.AddRange(It.IsAny<IEnumerable<tblSchedule>>()))
                            .Callback<IEnumerable<tblSchedule>>(s => capturedSchedules.AddRange(s));

            _mockContext.Setup(c => c.tblEmployee).Returns(_mockEmployeeSet.Object);
            _mockContext.Setup(c => c.tblEmployeeAvail).Returns(_mockAvailSet.Object);
            _mockContext.Setup(c => c.tblSetupTask).Returns(_mockTaskSet.Object);
            _mockContext.Setup(c => c.tblSetupLoc).Returns(_mockLocSet.Object);
            _mockContext.Setup(c => c.tblSchedule).Returns(_mockScheduleSet.Object);
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);

            var service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.GenerateSchedule(_mockContext.Object);
            var scheduleDate = DateTime.Today;

            // Act
            var result = service.GenerateTaskSchedule(scheduleDate);

            // Assert
            Assert.AreEqual("created", result);
            Assert.IsFalse(capturedSchedules.Any(s => s.Task == "Trailer Unloader" && s.Name.Contains("Jane Doe")));
        }




        [TestMethod]
        public void GenerateTaskSchedule_ShouldNotAssignSameTaskToEmployeeOnConsecutiveDays()
        {
            // Arrange
            var scheduleDate = DateTime.Today;
            var yesterday = scheduleDate.AddDays(-1);


                 var empData = new List<tblEmployee>
                {
                    new tblEmployee { EmpID = "E1", FullName = "John Doe", Position = "Cargo Handler", Sex = "M" },
                    new tblEmployee { EmpID = "E2", FullName = "Alice Smith", Position = "Cargo Handler", Sex = "F" }
                }.AsQueryable();

                            var availData = new List<tblEmployeeAvail>
                {
                    new tblEmployeeAvail { EmpID = "E1", AvailDate = scheduleDate, Avail = true },
                    new tblEmployeeAvail { EmpID = "E2", AvailDate = scheduleDate, Avail = true }
                }.AsQueryable();









            var taskData = new List<tblSetupTask>
    {
        new tblSetupTask { Task = "Trk Unloader", MinEmployees = 1, MaxEmployees = 1, LocID = 1 }
    }.AsQueryable();

            var locData = new List<tblSetupLoc>
    {
        new tblSetupLoc { LocID = 1, Location = "Dock 1" }
    }.AsQueryable();

            // Simulate John Doe did "Trk Unloader" yesterday
            var scheduleData = new List<tblSchedule>
    {
        new tblSchedule
        {
            SchDate = yesterday,
            Task = "Trk Unloader",
            Name = "John Doe",
            Location = "Dock 1"
        }
    }.AsQueryable();

            // Setup mocks with IQueryable-compatible mock sets
            _mockEmployeeSet = CreateMockDbSet(empData);
            _mockAvailSet = CreateMockDbSet(availData);
            _mockTaskSet = CreateMockDbSet(taskData);
            _mockLocSet = CreateMockDbSet(locData);
            _mockScheduleSet = CreateMockDbSet(scheduleData);

            // Capture added schedule items
            //var capturedSchedules = new List<tblSchedule>();
            //_mockScheduleSet.As<DbSet<tblSchedule>>()
            //    .Setup(m => m.AddRange(It.IsAny<IEnumerable<tblSchedule>>()))
            //    .Callback<IEnumerable<tblSchedule>>(s => capturedSchedules.AddRange(s));



            _mockScheduleSet = CreateMockDbSet(scheduleData);
            var capturedSchedules = new List<tblSchedule>();
            _mockScheduleSet.Setup(m => m.AddRange(It.IsAny<IEnumerable<tblSchedule>>()))
                            .Callback<IEnumerable<tblSchedule>>(s => capturedSchedules.AddRange(s));


            _mockContext.Setup(c => c.tblEmployee).Returns(_mockEmployeeSet.Object);
            _mockContext.Setup(c => c.tblEmployeeAvail).Returns(_mockAvailSet.Object);
            _mockContext.Setup(c => c.tblSetupTask).Returns(_mockTaskSet.Object);
            _mockContext.Setup(c => c.tblSetupLoc).Returns(_mockLocSet.Object);
            _mockContext.Setup(c => c.tblSchedule).Returns(_mockScheduleSet.Object);
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);

            var service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.GenerateSchedule(_mockContext.Object);

            // Act
            var result = service.GenerateTaskSchedule(scheduleDate);

            // Assert
            Assert.AreEqual("created", result);

            ////Assert.IsFalse(capturedSchedules.Any(s => s.Task == "Trk Unloader" && s.Name.Contains("John Doe")),
            ////    "John Doe should not be assigned to the same task on consecutive days.");


            Assert.IsFalse(capturedSchedules
                .Any(s => s.Task == "Trk Unloader" && s.Name.Contains("John Doe") && s.SchDate == scheduleDate),
                "John Doe should not be assigned to the same task on consecutive days.");

        }













    }











}
