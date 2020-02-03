# WinCCConnectionTool

[![Build status](https://ci.appveyor.com/api/projects/status/u0gav7sv0jt2v6wg?svg=true)](https://ci.appveyor.com/project/fbarresi/winccconnectiontool)

This software change the connection parameters of a WinCC Project.
In this way you can keep a single project and change the connected target machine directly from the runtime without recompiling the project.

 [Download it here](https://github.com/evopro-ag/WinCCConnectionTool/releases/latest)

## Usage:
`WinCCCT [command] [options]`

### Options:
 - `--path` : Path (absolute or relative) of MDF file 
 - `--dbName` : Name or pattern of MDF file (i.E.: `HMI_577I.MDF` or `HMI_*.MDF`)

if no `--path` or `--dbName` is set the tool will search for a WinCC database in all subfolders.
Use one of theese options if you have many elegible .MDF file in your subfolders

`-?|-h|--help`: Show help information

### Commands: 

  - `list` : List all WinCC connections
  - `setCon` : Set WinCC connection string
  - `setOpc` : Update a WinCC connection string of type OPC UA
  - `setOpcNS` : Update OPC Namespace number of all variables in a connection

Run `WinCCCT [command] --help` for more information about a command.

### Examples
 - List all WinCC connections: `WinCCCT list --dbName HMI_577I.MDF`
 - Change parameter of a generic connection: `WinCCCT setCon connection1 "IP,10.10.100.119,,0,2,02" --dbName HMI_577I.MDF`
 - Change OPC UA connection with host name and port number: `WinCCCT setOpc connection1 192.168.1.10 4048 --dbName HMI_577I.MDF`
 - Change OPC UA namespace number for all variables in a connection: `WinCCCT setOpcNS connection1 81 --dbName HMI_577I.MDF`

