//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Xunit;

//namespace UnitTests.Directory
//{
//    public class BoilerPlateFixture : IDisposable
//    {
//        // Declare objects

//        public BoilerPlateFixture()
//        {
//            // Assign objects
//        }

//        public void Dispose()
//        {
//            // Assign Objects
//              GC.SuppressFinalize(this);
//        }
//    }

//    public class BoilerPlateTests : IClassFixture<BoilerPlateFixture>
//    {
//        private readonly BoilerPlateFixture Fixture;
//        public BoilerPlateTests(BoilerPlateFixture fixture)
//        {
//            Fixture = fixture;
//        }

//        [Fact]
//        public void MethodName_WithParamters_ShouldDoSomething()
//        {

//        }

//    }
//}
