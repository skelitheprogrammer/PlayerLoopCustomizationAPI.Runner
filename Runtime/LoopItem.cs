using System;

namespace PlayerLoopCustomizationAPI.Addons.Runner
{
    public abstract class LoopItem : ILoopItem, IDisposable
    {
        protected bool Disposed;
        public abstract bool MoveNext();
        protected abstract void OnMoveNext();

        public virtual void Dispose()
        {
            Disposed = true;
        }
    }
}