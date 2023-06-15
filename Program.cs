// using archiving.Archiver;

namespace zipper
{
    class Zipper
    {
        enum ExitCode : int {
            Success = 0,
            UnknownError = 10
            }
        static int Main(string[] args)
        {
            // Determine and initialize UI type
            UserInterface? ui = null;
            var interfaceType = "console";
            if (interfaceType == "console") {
                ui = new consoleUI();
            } else {
                // Placeholder for handling a GUI-style UX
                // var ui = new CLASSHERE;
            }
            if (ui == null) {
                Console.WriteLine("The interface type specified does not exist");
                return (int)ExitCode.UnknownError;
            }
            ui.init();

            // Backup or Restore?
            ExitCode? task = null;

            while (true) {
                var mode = ui.SingleSelectFromList(
                    prefix: new string[] {
                        "Welcome to Ringdown Redeploy", 
                        "_______"
                        }, 
                    items: new string[] {
                        "Backup", 
                        "Restore", 
                        "Quit"
                        }, 
                    suffix: new string[] {
                        "_______", 
                        "Please select a mode..."
                        }
                    );
                if (mode == 0)
                    task = Backup(ui);
                else if (mode == 1)
                    task = Restore(ui);
                else if (mode == 2)
                    return (int)ExitCode.Success;
                else
                    return (int)ExitCode.UnknownError;
                if (task == null)
                    return (int)ExitCode.UnknownError;
                else if (task != 0)
                    return (int)task;
            }
        
            // Archiver.ExtractZipFile("testFolder.zip", "testPassword", "targetDump");
            // List<string> roamingFolders = MultiSelectFromList(prefix: new string[] { "Files in [directory]", "_______" }, items: spider.ls(roaming), suffix: new string[] { "_______", "Please select the desired directories..." });
        }
        static ExitCode Backup(UserInterface ui)
        {
            string? path = null;
            Console.Clear();
            // Get location for storing backup
            for (bool validPath = false; validPath == false; validPath = CheckPath(path))
            {
                Console.WriteLine("Please enter a destination folder for your backups:");
                path = Console.ReadLine();
                Console.Clear();
            }
            // Get name for folder
            string? backupName = null;
            for (bool validName = false; validName == false; validName = SanitizePath(backupName))
            {
                Console.WriteLine("Please enter a name for the backup folder:");
                backupName = Console.ReadLine();
                Console.Clear();
            }
            // Create the folder
            string backupPath = path + "\\" + backupName;
            Console.Write("Creating a new backup at: " + backupPath + "... ");
            Directory.CreateDirectory(backupPath);
            Console.Write("Done!");
            // Select folders to back up
            var roamingFolders = ui.MultiSelectFromList(
                prefix: new string[] { 
                        "Files in Application Data (Roaming)", 
                        "_______" 
                    }, 
                    items: Spider.ls(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), folders:true), 
                    suffix: new string[] {
                        "_______", 
                        "Please select the desired directories..." 
                    }
                    );
            var localFolders = ui.MultiSelectFromList(
                prefix: new string[] { 
                        "Files in Application Data (Local)", 
                        "_______" 
                    }, 
                    items: Spider.ls(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), folders:true), 
                    suffix: new string[] {
                        "_______", 
                        "Please select the desired directories..." 
                    }
                    );
            // Create backups
            Console.WriteLine("Archiving data. Please wait...");
            Directory.CreateDirectory(backupPath + "\\Roaming");
            foreach (string folder in roamingFolders)
                Archiver.CreateSample(backupPath + "\\Roaming\\" + folder + ".zip", "Null",Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + folder);
            Directory.CreateDirectory(backupPath + "\\Local");
            foreach (string folder in localFolders)
                Archiver.CreateSample(backupPath + "\\Local\\" + folder + ".zip", "Null",Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + folder);
            Console.WriteLine("Archives created. Press any key to continue...");
            Console.ReadKey();
            return (int)ExitCode.Success;
        }
        static ExitCode Restore(UserInterface ui)
        {
            string? path = null;
            Console.Clear();
            // Get location of backup
            for (bool validPath = false; validPath == false; validPath = BackupSearch(path))
            {
                Console.WriteLine("Please enter the location your backup:");
                path = Console.ReadLine();
                Console.Clear();
            }
            // Scan for archives
            var roamingBackups = ui.MultiSelectFromList(
                prefix: new string[] { 
                        "Roaming Application Data backups:", 
                        "_______" 
                    }, 
                    items: Spider.ls(path + "\\Roaming", files:true), 
                    suffix: new string[] {
                        "_______", 
                        "Please select the desired backups to restore..." 
                    }
                    );
            var localBackups = ui.MultiSelectFromList(
                prefix: new string[] { 
                        "Local Application Data backups:", 
                        "_______" 
                    }, 
                    items: Spider.ls(path + "\\Local", files:true), 
                    suffix: new string[] {
                        "_______", 
                        "Please select the desired backups to restore..." 
                    }
                    );
            // Extract backups
            Console.WriteLine("Restoring data. Please wait...");
            foreach (string folder in roamingBackups)
                Archiver.ExtractZipFile(path + "\\Roaming\\" + folder, "Null", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + folder.Substring(0, folder.LastIndexOf('.')));
            foreach (string folder in localBackups)
                Archiver.ExtractZipFile(path + "\\Local\\" + folder, "Null", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + folder.Substring(0, folder.LastIndexOf('.')));
            Console.WriteLine("Restore complete. Press any key to continue...");
            Console.ReadKey();
            return (int)ExitCode.Success;
        }
        public static bool BackupSearch(string? path)
        {
            if (path == null) {
                Console.WriteLine("Please enter a valid folder path...");
                return false;
            }
            if (!Directory.Exists(path)) {
                Console.WriteLine("The following directory doesn't exist:");
                Console.WriteLine(path);
                return false;
            }
            if ((Directory.Exists(path + "\\Roaming")) || (Directory.Exists(path + "\\Local")))
                return true;
            else {
                Console.WriteLine("Folder doesn't appear to have valid backups.");
                return false;
            }
        }
        public static bool CheckPath(string? path)
        {
            if (!Directory.Exists(path)) {
                Console.WriteLine("The following directory doesn't exist:");
                Console.WriteLine(path);
                return false;
            }
            if (Directory.GetFiles(path).Length == 0) {
                Console.WriteLine("The following directory is not empty:");
                Console.WriteLine(path);
                return false;
            }
            try
            {
                using (FileStream fs = File.Create(Path.Combine(path, Path.GetRandomFileName()), 1, FileOptions.DeleteOnClose)
                )
                { }
                return true;
            }
            catch
            {
                Console.WriteLine("The following directory can't be written to:");
                Console.WriteLine(path);
                return false;
            }
        }
        public static bool SanitizePath(string? path)
        {
            // Check for invalid characters
            if (path == null) {
                Console.WriteLine("Please specify a folder name.");
                return false;
            }
            char[] invalidChars = Path.GetInvalidPathChars();
            if (path.IndexOfAny(invalidChars) >= 0) {
                Console.WriteLine("The specified path contains invalid characters.");
                return false;
            }
            return true;
        }
    }
}