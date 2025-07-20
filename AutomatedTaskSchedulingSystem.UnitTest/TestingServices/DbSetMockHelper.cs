using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTaskSchedulingSystem.UnitTest.TestingServices
{
    public class DbSetMockHelper
    {


       
            public static Mock<DbSet<T>> CreateMockSet<T>(List<T> sourceList) where T : class
            {
                var queryable = sourceList.AsQueryable();
                var mockSet = new Mock<DbSet<T>>();

                mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
                mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
                mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
                mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

                mockSet.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(sourceList.Add);
                mockSet.Setup(m => m.Remove(It.IsAny<T>())).Callback<T>(t => sourceList.Remove(t));

                return mockSet;
            }
        
    }




}

