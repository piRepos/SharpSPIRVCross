using System;
using System.Collections.Generic;
using static SharpSPIRVCross.spvc;

namespace SharpSPIRVCross
{
    public sealed class ParseIr : IDisposable
    {
        public readonly IntPtr _handle;

        internal ParseIr(IntPtr handle)
        {
            _handle = handle;
        }

        public void Dispose()
        {
        }
    }
}
