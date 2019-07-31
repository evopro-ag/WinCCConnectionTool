using System;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WinCCConnectionTool.Gui
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            OpenProjectCommand = ReactiveCommand.CreateFromTask(OpenProject);

        }

        private Task OpenProject()
        {
            return Task.FromResult(Unit.Default);
        }

        public ReactiveCommand<Unit, Unit> OpenProjectCommand { get; set; }
    }
    
}