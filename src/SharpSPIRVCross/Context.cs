using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static SharpSPIRVCross.spvc;

namespace SharpSPIRVCross
{
    public sealed class Context : IDisposable
    {
        public readonly IntPtr _context;

        public Context()
        {
            var result = spvc_context_create(out _context);
            if (result != spvc_result.SPVC_SUCCESS)
            {

            }
        }

        public void Dispose()
        {
            spvc_context_destroy(_context);
        }

        public ParseIr ParseIr(byte[] spirv)
        {
            unsafe
            {
                var result = spvc_context_parse_spirv(_context,
                    (byte*)Unsafe.AsPointer(ref spirv[0]),
                    new IntPtr(spirv.Length / 4),
                    out var parsed_ir);
                return new ParseIr(parsed_ir);
            }
        }
    }
}
