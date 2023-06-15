# Ringdown Zipper
A primitive version of the planned Ringdown Redeploy
## Overview
Zipper is a C# console application useful for quickly creating and restoring backups of Windows application data. It scans your user account's Local and Roaming directories for folders and allows you to pick as many of them to back up as you want, then it compresses them into the backup location you specify. Restoring works the same way, but effectively in reverse.

This sofware was written to help dramatically speed up the process of creating backups of Windows PCs as well as the process of migrating between different PCs or installations of Windows.

The eventual goal is to develop this and other components I've written into the full Ringdown Redeploy application, which will include significantly greater automation of the backup and restore progress, with different degrees of detail you can opt to backup/restore to. The application will be able to retrieve information from a database that will allow it to automatically identify installed applications based on a fingerprint of sorts, and you can choose to back up an entire application, rather than it's individual components through that option.

[Software Demo Video](https://drive.google.com/file/d/1Mr_h9A0cEvwNAveHzmybfh9GoBbCBF9g/view)

## Development Environment

This application was developed using the .NET 7 SDK for optimal compatibility with modern Windows platforms. I develop the application simply using Visual Studio Code with the C# extension installed.

To set up your environment:
1. Make sure your Microsoft Store is up to date
2. Open a Terminal in the location you want the project to be
3. Run the following commands:
```powershell
winget install Git.Git
winget install Microsoft.VisualStudioCode
winget install Microsoft.DotNet.SDK.7
git clone https://github.com/salsonn/zipper
cd zipper
dotnet install
```
Now with the project setup, you can run the test version of the application with `dotnet run`

## Useful Websites
- [Microsoft - .NET Installation Guide](https://learn.microsoft.com/en-us/dotnet/core/install/windows)
- [Microsoft - .NET API Documentation](https://learn.microsoft.com/en-us/dotnet/api)
- [NuGet](https://www.nuget.org/)
- [SharpZipLib Documentation](https://github.com/icsharpcode/SharpZipLib/wiki/)

## Future Work
- Database API
- Intelligent Application Detection
- Storage medium selection
- Command-line parameters
- GUI Wizard
- Backup size estimate
- Figure out how to compile it into a nice, final application.
- FIX THE RUNTIME REQUIREMENT