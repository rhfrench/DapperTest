using System;
using System.Collections.Generic;
using System.Text;
using static DapperTests.NullFirstRecordTests;

namespace DapperTests
{
    public class program
    {
        public static void Main(string[] args)
        {
            NullFirstRecordTests testObj = new NullFirstRecordTests();

            Console.WriteLine("First test is with cast. Press enter to continue.");
            Console.ReadLine();
            testObj.NullFirstValueWithCast();
            Console.WriteLine("Second test is with no cast. Press enter to continue.");
            Console.ReadLine();
            testObj.NullFirstValueNoCast();
            Console.ReadLine();
        }
    }
}
