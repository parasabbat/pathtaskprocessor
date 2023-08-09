using AutomationPipeline.Interfaces;

namespace AutomationPipeline
{
    public class CommandService : ICommandService
    {
        public void CreateFolder(string folderPath, string folderName)
        {
            Directory.CreateDirectory(Path.Combine(folderPath, folderName));
        }

        public async Task DownloadFile(string sourcePath, string outputFileName)
        {
            using (var client = new HttpClient())
            {
                using (var s = client.GetStreamAsync(sourcePath))
                {
                    using (var fs = new FileStream(outputFileName, FileMode.OpenOrCreate))
                    {
                        await s.Result.CopyToAsync(fs);
                    }
                }
            }
        }

        public void FileCopy(string sourceFilePath, string destinationFilePath)
        {
            File.Copy(sourceFilePath, destinationFilePath, true);
        }

        public void FileDelete(string filePath)
        {
            File.Delete(filePath);
        }

        public void QueryFolderFiles(string folderPath)
        {
            string[] filePaths = Directory.GetFiles(folderPath);
            foreach (string filePath in filePaths)
            {
                Console.WriteLine(Path.GetFileName(filePath));
            }
        }

        public int SearchStringReturnsRowCount(string filePath, string stringToSearch)
        {
            var searchCount = 0;
            string line;
            using (var reader = File.OpenText(filePath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(stringToSearch))
                    {
                        searchCount++;
                    }
                }
            }
            return searchCount;
        }

        public async Task Wait(int waitTimeInSeconds)
        {
            await Task.Delay(waitTimeInSeconds*1000);
        }
    }
}
