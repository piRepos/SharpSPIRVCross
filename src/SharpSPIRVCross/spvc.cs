// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using spvc_context = System.IntPtr;
using spvc_compiler = System.IntPtr;
using spvc_compiler_options = System.IntPtr;
using spvc_variable_id = System.UInt32;
using spvc_resources = System.IntPtr;

using spvc_set = System.IntPtr;

namespace SharpSPIRVCross
{
    internal static unsafe class spvc
    {
        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result spvc_context_create(out spvc_context context);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern void spvc_context_destroy(spvc_context context);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern void spvc_context_release_allocations(spvc_context context);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void spvc_error_callback(IntPtr userData, [MarshalAs(UnmanagedType.LPStr)] string description);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern void spvc_context_set_error_callback(spvc_context context, spvc_error_callback callback, IntPtr userData);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr spvc_context_get_last_error_string(spvc_context context);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result spvc_context_parse_spirv(spvc_context context, void* spirv, IntPtr word_count, out IntPtr parsed_ir);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result spvc_context_create_compiler(spvc_context context, Backend backend,
                                                        IntPtr parsed_ir, CaptureMode mode,
                                                        out spvc_compiler compiler);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result spvc_compiler_create_compiler_options(spvc_compiler compiler, out spvc_compiler_options options);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result spvc_compiler_options_set_bool(spvc_compiler_options options, CompilerOption option, byte value);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result spvc_compiler_options_set_uint(spvc_compiler_options options, CompilerOption option, uint value);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result spvc_compiler_install_compiler_options(spvc_compiler compiler, spvc_compiler_options options);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result spvc_compiler_compile(spvc_compiler compiler, out IntPtr source);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result spvc_compiler_add_header_line(spvc_compiler compiler, string line);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result spvc_compiler_require_extension(spvc_compiler compiler, string extension);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result spvc_compiler_flatten_buffer_block(spvc_compiler compiler, spvc_variable_id id);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result spvc_compiler_hlsl_set_root_constants_layout(spvc_compiler compiler, HLSLRootConstants* constant_info, IntPtr count);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result spvc_compiler_hlsl_add_vertex_attribute_remap(spvc_compiler compiler, HLSLVertexAttributeRemap.__Native* remap, IntPtr remaps);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern spvc_variable_id spvc_compiler_hlsl_remap_num_workgroups_builtin(spvc_compiler compiler);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result spvc_compiler_get_active_interface_variables(spvc_compiler compiler, out spvc_set set);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result spvc_compiler_set_enabled_interface_variables(spvc_compiler compiler, spvc_set set);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result spvc_compiler_create_shader_resources(spvc_compiler compiler, out spvc_resources resources);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result spvc_compiler_create_shader_resources_for_active_variables(spvc_compiler compiler, out spvc_resources resources, spvc_set active);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern Result spvc_resources_get_resource_list_for_type(
            spvc_resources resources,
            ResourceType resourceType,
            ReflectedResource.__Native** resource_list,
            out IntPtr resource_size);
    }
}
