using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Inerfaces;
using McMaster.Extensions.CommandLineUtils;

namespace CLI.Commands
{
    public class CommandBase
    {
        protected IConnectionService ConnectionService { get; set; }

        public CommandBase(IConnectionService connectionService)
        {
            this.ConnectionService = connectionService;
        }

        protected async Task LoadDatabase(string path)
        {
            await ConnectionService.LoadDatabase(@".\WINCC", path);
        }

        protected void Close()
        {
            ConnectionService.CloseDatabase();
        }
    }
}
