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
        public class SetupLocationTesting
        {
            private Mock<ATSSEntities> _mockContext;
            private Mock<DbSet<tblSetupLoc>> _mockLocSet;
            private AutomatedTaskSchedulingSystem.BusinessLogic.Services.SetupLocation _service;

            [TestInitialize]
            public void Setup()
            {
                _mockContext = new Mock<ATSSEntities>();
                _mockLocSet = new Mock<DbSet<tblSetupLoc>>();
                _mockContext.Setup(c => c.tblSetupLoc).Returns(_mockLocSet.Object);
                _service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.SetupLocation(_mockContext.Object);
            }

            [TestMethod]
            public void LoadLocation_ShouldReturnAllLocations()
            {
                var data = new List<tblSetupLoc>
            {
                new tblSetupLoc { LocID = 1, Location = "Dock A" },
                new tblSetupLoc { LocID = 2, Location = "Dock B" }
            }.AsQueryable();

                _mockLocSet = CreateMockDbSet(data);
                _mockContext.Setup(c => c.tblSetupLoc).Returns(_mockLocSet.Object);
                _service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.SetupLocation(_mockContext.Object);

                var result = _service.LoadLocation().ToList();

                Assert.AreEqual(2, result.Count);
                Assert.AreEqual("Dock A", result[0].Location);
            }

            [TestMethod]
            public void SaveLocation_ShouldCreate_WhenNotExisting()
            {
                var model = new tblSetupLoc { LocID = 1, Location = "New Dock" };
                var data = new List<tblSetupLoc>().AsQueryable();
                _mockLocSet = CreateMockDbSet(data);

                _mockContext.Setup(c => c.tblSetupLoc).Returns(_mockLocSet.Object);
                _mockContext.Setup(c => c.SaveChanges()).Returns(1);
                _mockLocSet.Setup(m => m.Add(It.IsAny<tblSetupLoc>())).Verifiable();
                _service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.SetupLocation(_mockContext.Object);

                var result = _service.SaveLocation(model);

                Assert.AreEqual("created", result);
                _mockLocSet.Verify(m => m.Add(It.IsAny<tblSetupLoc>()), Times.Once);
            }

            [TestMethod]
            public void SaveLocation_ShouldUpdate_WhenExists()
            {
                var existing = new tblSetupLoc { LocID = 1, Location = "Old Dock" };
                var data = new List<tblSetupLoc> { existing }.AsQueryable();
                _mockLocSet = CreateMockDbSet(data);

                _mockContext.Setup(c => c.tblSetupLoc).Returns(_mockLocSet.Object);
                _mockContext.Setup(c => c.SaveChanges()).Returns(1);
                _service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.SetupLocation(_mockContext.Object);

                var model = new tblSetupLoc { LocID = 1, Location = "Updated Dock" };
                var result = _service.SaveLocation(model);

                Assert.AreEqual("updated", result);
                Assert.AreEqual("Updated Dock", existing.Location);
            }

            [TestMethod]
            public void DeleteLocation_ShouldReturnSuccess_WhenExists()
            {
                var loc = new tblSetupLoc { LocID = 1, Location = "To Delete" };
                _mockLocSet.Setup(m => m.Find(1)).Returns(loc);
                _mockContext.Setup(c => c.tblSetupLoc).Returns(_mockLocSet.Object);
                _mockContext.Setup(c => c.SaveChanges()).Returns(1);

                var result = _service.DeleteLocation(1);

                Assert.AreEqual("Location deleted successfully", result);
                _mockLocSet.Verify(m => m.Remove(It.IsAny<tblSetupLoc>()), Times.Once);
            }

            [TestMethod]
            public void DeleteLocation_ShouldReturnNotFound_WhenNotExists()
            {
                _mockLocSet.Setup(m => m.Find(It.IsAny<int>())).Returns((tblSetupLoc)null);
                _mockContext.Setup(c => c.tblSetupLoc).Returns(_mockLocSet.Object);

                var result = _service.DeleteLocation(99);

                Assert.AreEqual("Location not found", result);
            }

        [TestMethod]
        public void LoadLocation_ShouldReturnIncorrectCount_FailTest()
        {
                var data = new List<tblSetupLoc>
                {
                    new tblSetupLoc { LocID = 1, Location = "Dock A" },
                    new tblSetupLoc { LocID = 2, Location = "Dock B" }
                }.AsQueryable();

            _mockLocSet = CreateMockDbSet(data);
            _mockContext.Setup(c => c.tblSetupLoc).Returns(_mockLocSet.Object);
            _service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.SetupLocation(_mockContext.Object);

            var result = _service.LoadLocation().ToList();

            // ❌ This will FAIL because there are actually 2 records
            Assert.AreEqual(3, result.Count, "Expected 3 locations but got 2");
        }






        private Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
            {
                var mockSet = new Mock<DbSet<T>>();
                mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
                return mockSet;
            }
        }
    }













