using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sample.Infrastructure.UnitTest
{
    public static class MockDbSetHelper
    {
        public static IDbSet<T> ToMockDbSetObject<T>(this List<T> data) where T : class
        {
            var mock = new Mock<DbSet<T>>();
            var queryData = data.AsQueryable();
            mock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryData.Provider);
            mock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryData.Expression);
            mock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryData.ElementType);
            mock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryData.GetEnumerator());

            return mock.Object;
        }

        public static IDbSet<T> ToMockDbSetObject<T>(this T data) where T : class
        {
            return ToMockDbSetObject(new List<T> { data });
        }
    }
}
