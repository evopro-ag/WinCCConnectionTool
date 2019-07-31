using System.Reactive.Disposables;
using DynamicData.Annotations;
using ReactiveUI;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace WinCCConnectionTool.Gui
{
    public abstract class ViewModelBase : ReactiveObject, IDisposable
    {
        protected CompositeDisposable Disposables = new CompositeDisposable();
        private bool disposed;

        ~ViewModelBase()
        {
            Dispose(false);
        }

        public virtual void Dispose()
        {
            Dispose(true);
        }

        public abstract void Initialize();

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            Disposables?.Dispose();
            Disposables = null;

            disposed = true;
        }

        [NotifyPropertyChangedInvocator]
        // ReSharper disable once InconsistentNaming
        protected void raisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.RaisePropertyChanged(propertyName);
        }
    }
}