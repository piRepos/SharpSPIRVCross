// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static SharpSPIRVCross.spvc;

namespace SharpSPIRVCross
{
    public sealed class Compiler
    {
        internal readonly IntPtr Handle;

        public readonly CompilerOptions Options;

        internal Compiler(IntPtr handle)
        {
            Handle = handle;

            spvc_compiler_create_compiler_options(handle, out var optionsPtr).CheckError();
            Options = new CompilerOptions(optionsPtr);
        }

        public string Compile()
        {
            // Apply options.
            if (Options.IsDirty)
            {
                spvc_compiler_install_compiler_options(Handle, Options.Handle);
                Options.IsDirty = false;
            }

            var result = spvc_compiler_compile(Handle, out var source);
            result.CheckError();
            return Marshal.PtrToStringAnsi(source);
        }

        public void AddHeaderLine(string line)
        {
            spvc_compiler_add_header_line(Handle, line).CheckError();
        }

        public void RequireExtension(string extension)
        {
            spvc_compiler_require_extension(Handle, extension).CheckError();
        }

        public void FlattenBufferBlock(uint id)
        {
            spvc_compiler_flatten_buffer_block(Handle, id).CheckError();
        }

        public unsafe void HLSLSetRootConstantsLayout(params HLSLRootConstants[] constants)
        {
            spvc_compiler_hlsl_set_root_constants_layout(
                Handle,
                (HLSLRootConstants*)Unsafe.AsPointer(ref constants),
                new IntPtr(constants.Length)
                ).CheckError();
        }

        public unsafe void HLSLAddVertexAttributeRemap(params HLSLVertexAttributeRemap[] remaps)
        {
            var nativeRemaps = stackalloc HLSLVertexAttributeRemap.__Native[remaps.Length];
            for (var i = 0; i < remaps.Length; i++)
            {
                remaps[i].__MarshalTo(ref nativeRemaps[i]);
            }

            spvc_compiler_hlsl_add_vertex_attribute_remap(Handle,
                nativeRemaps,
                new IntPtr(remaps.Length)).CheckError();

            for (var i = 0; i < remaps.Length; i++)
            {
                remaps[i].__MarshalFree(ref nativeRemaps[i]);
            }
        }

        public uint HLSLRemapNumWorkgroupsBuiltin()
        {
            return spvc_compiler_hlsl_remap_num_workgroups_builtin(Handle);
        }

        public ShaderResources CreateShaderResources()
        {
            spvc_compiler_create_shader_resources(Handle, out var resourcesPtr).CheckError();
            return new ShaderResources(resourcesPtr);
        }
    }
}
