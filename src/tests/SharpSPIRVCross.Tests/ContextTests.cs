using System;
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
                foreach(var uniformBuffer in resources.GetResources(ResourceType.UniformBuffer))
                {
                    Console.WriteLine($"ID: {uniformBuffer.Id}, BaseTypeID: {uniformBuffer.BaseTypeId}, TypeID: {uniformBuffer.Id}, Name: {uniformBuffer.Name})");
                    var set = compiler.GetDecoration(uniformBuffer.Id, SpvDecoration.DescriptorSet);
                    var binding = compiler.GetDecoration(uniformBuffer.Id, SpvDecoration.Binding);
                    Console.WriteLine($"  Set: {set}, Binding: {binding}");
                }

                compiler.Options.SetOption(CompilerOption.HLSL_ShaderModel, 50);
                var hlsl_source = compiler.Compile();
            }
        }
    }
}
