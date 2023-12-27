namespace KEngine
{

    public class FileUtils
    {
        public static long ReadCounter;
        public static long FileReadStat;
        // TODO(): Must be multi threaded
        public static System.Diagnostics.Stopwatch Stopwatch = new();
        public static string PathToMainFolder = ""; // Todo: Temporary solution to make assets work in kcg-maps
        
        // Todo: Temporary solution needs refactoring.
        // Used when find directory is used...
        public static bool FileExistsFull(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }

        public static void DeleteFileFull(string filePath)
        {
            System.IO.File.Delete(filePath);
        }

        public static bool FileExists(string filePath)
        {
            return System.IO.File.Exists(PathToMainFolder + filePath);
        }

        public static bool DirectoryExists(string directoryPath)
        {
            return System.IO.Directory.Exists(PathToMainFolder + directoryPath);
        }
        
        public static bool DirectoryExistsFull(string directoryPath)
        {
            return System.IO.Directory.Exists(directoryPath);
        }

        public static void CreateDirectory(string directoryPath)
        {
            System.IO.Directory.CreateDirectory(PathToMainFolder + directoryPath);
        }
        
        public static void CreateDirectoryFull(string directoryPath)
        {
            System.IO.Directory.CreateDirectory(directoryPath);
        }
        
        public static void WriteAllBytes(string filePath, byte[] data)
        {
            System.IO.File.WriteAllBytes(PathToMainFolder + filePath, data);
        }
        
        public static void WriteAllBytesFull(string filePath, byte[] data)
        {
            System.IO.File.WriteAllBytes(filePath, data);
        }

        public static byte[] ReadAllBytes(string filePath)
        {
            return ReadAllBytesFull(PathToMainFolder + filePath);
        }
        
        public static byte[] ReadAllBytesFull(string filePath)
        {
            // timing the read functions and adding difference to counter
            Stopwatch.Reset();
            Stopwatch.Start();

            // file operation here
            byte[] result = System.IO.File.ReadAllBytes(filePath);
            
            Stopwatch.Stop();
            // adding the elpased ticks to ReadCounter
            ReadCounter += Stopwatch.ElapsedTicks;
            
            // increment the number of file reads
            FileReadStat++;
            
            return result;
        }
        
        // returns the directory paths of the sub directories
        public static string[] GetDirectories(string directoryPath, string searchPattern, SearchOption option)
        {
            System.IO.SearchOption searchOption = System.IO.SearchOption.AllDirectories;
            
            switch (option)
            {
                case SearchOption.AllDirectories:
                {
                    searchOption = System.IO.SearchOption.AllDirectories ;
                    break;
                }
                case SearchOption.TopDirectoryOnly :
                {
                    searchOption = System.IO.SearchOption.TopDirectoryOnly;
                    break;
                }
            };
            
            return System.IO.Directory.GetDirectories(PathToMainFolder + directoryPath, searchPattern, searchOption);
        }
        
        public static string[] GetDirectoriesFull(string directoryPath, string searchPattern, SearchOption option)
        {
            System.IO.SearchOption searchOption = System.IO.SearchOption.AllDirectories;
            
            switch (option)
            {
                case SearchOption.AllDirectories:
                {
                    searchOption = System.IO.SearchOption.AllDirectories ;
                    break;
                }
                case SearchOption.TopDirectoryOnly :
                {
                    searchOption = System.IO.SearchOption.TopDirectoryOnly;
                    break;
                }
            };
            
            return System.IO.Directory.GetDirectories(directoryPath, searchPattern, searchOption);
        }
        
        // returns the file paths of the files in the directory
        public static string[] GetFiles(string directoryPath, string searchPattern, SearchOption option)
        {
            System.IO.SearchOption searchOption = System.IO.SearchOption.AllDirectories;
            
            switch (option)
            {
                case SearchOption.AllDirectories:
                {
                    searchOption = System.IO.SearchOption.AllDirectories ;
                    break;
                }
                case SearchOption.TopDirectoryOnly :
                {
                    searchOption = System.IO.SearchOption.TopDirectoryOnly;
                    break;
                }
            };
            
            return System.IO.Directory.GetFiles(PathToMainFolder + directoryPath, searchPattern, searchOption);
        }
        
        public static string[] GetFilesFull(string directoryPath, string searchPattern, SearchOption option)
        {
            System.IO.SearchOption searchOption = System.IO.SearchOption.AllDirectories;
            
            switch (option)
            {
                case SearchOption.AllDirectories:
                {
                    searchOption = System.IO.SearchOption.AllDirectories ;
                    break;
                }
                case SearchOption.TopDirectoryOnly :
                {
                    searchOption = System.IO.SearchOption.TopDirectoryOnly;
                    break;
                }
            };
            
            return System.IO.Directory.GetFiles(directoryPath, searchPattern, searchOption);
        }
        
        
        public static void ClearDirectory(string directoryPath)
        {
            // Delete all files inside the folder
            foreach (string filePath in System.IO.Directory.GetFiles(PathToMainFolder + directoryPath))
            {
                System.IO.File.Delete(filePath);
            }

            // Delete all subdirectories inside the folder
            foreach (string subdirectoryPath in System.IO.Directory.GetDirectories(PathToMainFolder + directoryPath))
            {
                System.IO.Directory.Delete(PathToMainFolder + subdirectoryPath, true);
            }
        }
        
        public static void ClearDirectoryFull(string directoryPath)
        {
            // Delete all files inside the folder
            foreach (string filePath in System.IO.Directory.GetFiles(directoryPath))
            {
                System.IO.File.Delete(filePath);
            }

            // Delete all subdirectories inside the folder
            foreach (string subdirectoryPath in System.IO.Directory.GetDirectories(directoryPath))
            {
                System.IO.Directory.Delete(subdirectoryPath, true);
            }
        }

        public static long GetFileLastWriteTime(string filePath)
        {
            // Get the last modify time of the file
            FileInfo fileInfo = new FileInfo( filePath);
            
            return fileInfo.LastWriteTime.Ticks;
        }
        
        public static float GetFileReadTimeInSeconds()
        {
            TimeSpan timeSpan = new TimeSpan(ReadCounter);

            return (float)timeSpan.TotalSeconds;
        }
    }
}