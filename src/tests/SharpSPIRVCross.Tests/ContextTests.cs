using System.IO;
using Xunit;

namespace SharpSPIRVCross.Tests
{
    public class ContextTests
    {
        [Fact]
        public void TestContextCreation()
        {
            var bytecode = File.ReadAllBytes("Shaders/triangle.vert.spv");
            using (var context = new Context())
            {
                Assert.NotNull(context);
                //context.ParseIr();
            }
        }
    }
}
