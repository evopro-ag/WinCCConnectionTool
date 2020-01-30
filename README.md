# WinCCConnectionTool

[![Build status](https://ci.appveyor.com/api/projects/status/u0gav7sv0jt2v6wg?svg=true)](https://ci.appveyor.com/project/fbarresi/winccconnectiontool)


## Usage:
`WinCCCT [options] [command]`


### Options:
 - `--path` : Path of MDF file
 - `--dbName` : Name of MDF file

`-?|-h|--help`  Show help information

### Commands: 

  - `list`: List all WinCC connections
  - `setCon`: Set WinCC connection string
  - `setOpc` : Update a WinCC connection string of type OPC UA
  - `setOpcNS` : Update OPC Namespace number of all variables in a connection

Run `WinCCCT [command] --help` for more information about a command.

### Examples
 - List all WinCC connections: `WinCCCT list --dbName HMI_577I.MDF`
 - Change parameter of a generic connection: `WinCCCT setCon connection1 "IP,10.10.100.119,,0,2,02"`
 - Change OPC UA connection with hostname and portnumber: `WinCCCT setOpc connection1 MyNewHostName 4048`
 - Change OPC UA namespace number: `WinCCCT setOpcNS connection1 4`

