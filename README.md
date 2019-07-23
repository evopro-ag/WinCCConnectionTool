# WinCCConnectionTool

[![Build status](https://ci.appveyor.com/api/projects/status/ww2gbkq05yyh59ng?svg=true)](https://ci.appveyor.com/project/StefanDoubleU/winccconnectiontool)

## Usage:
cli.exe `<Database> <cmd> <params>`


### Database:  
Full qualified path to *.mdf database file `.\HMI_577I\HMI_577I.MDF`

### cmd: 
* `list`: Lists all connections within the WinCC project
* `set`: Set the parameter to a given connection name.

 Example:
 * S7 Connection: `cli.exe "C:\temp\project1\HMI_577I\HMI_577I.MDF" set "connection1" "IP,10.10.100.119,,0,2,02"` 
 * OPC-UA: `cli.exe <path> set "connection1" "opc.tcp://10.10.100.119:4840,1,1,0; <UA>; 0.00; 0; 0; 1"` 

