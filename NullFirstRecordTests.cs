using System.Data;
using System.Linq;
using Dapper;
using Xunit;

namespace DapperTests
{
    /// <summary>
    /// Demonstrates a bug where Dapper assumes the wrong data type for a column whose first record is NULL
    /// </summary>
    public class NullFirstRecordTests
    {
        private static IDbConnection GetDb()
        {
            var cn = Database.GetConnection();

            cn.Execute("CREATE TABLE Foo ( Value INT )");
            cn.Execute("INSERT INTO Foo VALUES ( NULL )");
            cn.Execute("INSERT INTO Foo VALUES ( 1 )");
            return cn;
        }

        public class Foo
        {
            public int Value { get; set; }
        }

        public class FooNullable
        {
            public int? Value { get; set; }
        }

        [Fact]
        // assumes column is int64 when source column is NULL and destination field is non-nullable int
        public void NullFirstValueNoCast()
        {
            var cn = GetDb();

            Assert.Throws<DataException>(() => cn.Query<Foo>("SELECT Value FROM Foo"));
        }

        [Fact]
        // assumes column is int64 when first record is NULL and destination field is nullable int
        public void NullFirstValueNullableDestination()
        {
            var cn = GetDb();

            Assert.Throws<DataException>(() => cn.Query<FooNullable>("SELECT Value FROM Foo").ToList());
        }

        [Fact]
        // detects correct data type when first record is NULL and CAST(... AS INTEGER) is used
        public void NullFirstValueWithCast() 
        {
            var cn = GetDb();
            
            // does not throw when NULL is casted to INTEGER first
            var result = cn.Query<Foo>("SELECT CAST(Value AS INTEGER) FROM Foo").ToList();

            Assert.Equal(0, result.First().Value);
            Assert.Equal(2, result.Count);
        }
    }
}