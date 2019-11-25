using System;
using System.Collections.Generic;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using CLI.Inerfaces;
using CLI.Logic;
using CLI.Model;
using Microsoft.Win32;

namespace WinCCConnectionTool.Gui
{
    public class MainViewModel: ViewModelBase
    {
        public IConnectionService ConnectionService { get; }
        public IDatabaseService DatabaseService { get; }
        private string projectPath;

        public MainViewModel(IConnectionService connectionService, IDatabaseService databaseService)
        {
            ConnectionService = connectionService;
            DatabaseService = databaseService;
            Initialize();
        }

        public sealed override void Initialize()
        {
            OpenProjectCommand = ReactiveCommand.CreateFromTask(OpenProject);
        }

        private Task OpenProject()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "(*.mcx)|*.mcx";
            var result = openFileDialog.ShowDialog();
            if(result ?? false)
                ProjectPath = openFileDialog.FileName;
            return Task.FromResult(Unit.Default);
        }

        private async Task<IEnumerable<Connection>> LoadDatabase(string path)
        {
            await DatabaseService.LoadDatabase(@".\WINCC", path);
            await ConnectionService.LoadConnections();
            return ConnectionService.Connections;
        }

        public string ProjectPath
        {
            get => projectPath;
            set
            {
                if (projectPath == value) return;
                projectPath = value;
                raisePropertyChanged();
            }
        }

        public ReactiveCommand<Unit, Unit> OpenProjectCommand { get; set; }
    }
    
}