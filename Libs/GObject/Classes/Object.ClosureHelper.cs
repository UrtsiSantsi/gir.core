using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GObject
{
    public partial class Object
    {
        protected internal delegate void ActionRefValues(ref Value[] items);

        protected internal class ClosureHelper : IDisposable
        {
            // We need to store a reference to MarshalCallback to
            // prevent the delegate from being collected by the GC
            private ClosureMarshal? _marshalCallback;
            private ActionRefValues? _callback;
            public IntPtr Handle { get; private set; }

            public ClosureHelper(ActionRefValues action)
            {
                _callback = action;
                _marshalCallback = MarshalCallback;

                Handle = Closure.Native.new_simple((uint) Marshal.SizeOf(typeof(Closure)), IntPtr.Zero);
                Closure.Native.@ref(Handle);
                Closure.Native.sink(Handle);
                Closure.Native.set_marshal(Handle, _marshalCallback);
            }

            ~ClosureHelper()
            {
                ReleaseUnmanagedResources();
            }

            private void MarshalCallback(IntPtr closure, ref Value returnValue, uint nParamValues,
                Value[] paramValues, IntPtr invocationHint, IntPtr marshalData)
            {
                Debug.Assert(
                    condition: paramValues.Length == nParamValues,
                    message: "Values were not marshalled correctly. Breakage may occur"
                );

                _callback?.Invoke(ref paramValues);
            }

            private void ReleaseUnmanagedResources()
            {
                _callback = null;
                _marshalCallback = null;

                Closure.Native.invalidate(Handle);
                Closure.Native.unref(Handle);

                Handle = IntPtr.Zero;
            }

            public void Dispose()
            {
                if (Handle != IntPtr.Zero)
                {
                    ReleaseUnmanagedResources();
                    GC.SuppressFinalize(this);
                }
            }
        }
    }
}
