// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace SharpSPIRVCross
{
    public struct ReflectedResource
    {
        public uint Id;
        public uint BaseTypeId;
        public uint TypeId;
        public string Name;

        #region Marshal
        internal readonly struct __Native
        {
            public readonly uint Id;
            public readonly uint base_type_id;
            public readonly uint type_id;
            public readonly System.IntPtr name;
        }
        #endregion
    }
}
