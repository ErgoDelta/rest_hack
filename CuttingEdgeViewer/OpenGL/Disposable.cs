using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CuttingEdge
{
    public class Disposable : IDisposable
    {
        ~Disposable()
        {
            Debug.Assert(false, "The finalizer should never be called as this object should have been disposed");
            Dispose(false);
        }

        public void Dispose()
        {
            Debug.Assert(!disposed, "Objects should not be disposed twice");
            Dispose(true);
            GC.SuppressFinalize(this);
            disposed = true;
        }

#if DEBUG
        bool disposed = false;
#endif

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
