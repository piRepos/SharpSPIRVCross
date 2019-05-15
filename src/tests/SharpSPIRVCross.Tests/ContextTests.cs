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
                var ir = context.ParseIr(bytecode);
                var compiler = context.CreateCompiler(Backend.HLSL, ir);

                var resources = compiler.CreateShaderResources();
                var ss = resources.GetResources(ResourceType.UniformBuffer);

                compiler.Options.SetOption(CompilerOption.HLSL_ShaderModel, 50);
                var hlsl_source = compiler.Compile();
            }
        }
    }
}
