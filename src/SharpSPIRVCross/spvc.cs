using System;
using System.Runtime.InteropServices;

namespace SharpSPIRVCross
{
    internal static unsafe class spvc
    {
        public enum spvc_result
        {
            /* Success. */
            SPVC_SUCCESS = 0,

            /* The SPIR-V is invalid. Should have been caught by validation ideally. */
            SPVC_ERROR_INVALID_SPIRV = -1,

            /* The SPIR-V might be valid or invalid, but SPIRV-Cross currently cannot correctly translate this to your target language. */
            SPVC_ERROR_UNSUPPORTED_SPIRV = -2,

            /* If for some reason we hit this, new or malloc failed. */
            SPVC_ERROR_OUT_OF_MEMORY = -3,

            /* Invalid API argument. */
            SPVC_ERROR_INVALID_ARGUMENT = -4,

            SPVC_ERROR_INT_MAX = 0x7fffffff
        }

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern spvc_result spvc_context_create(out IntPtr context);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern void spvc_context_destroy(IntPtr context);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern void spvc_context_release_allocations(IntPtr context);

        [DllImport("cspirv_cross", CallingConvention = CallingConvention.Cdecl)]
        public static extern spvc_result spvc_context_parse_spirv(IntPtr context, byte* spirv, IntPtr word_count, out IntPtr parsed_ir);
    }
}
