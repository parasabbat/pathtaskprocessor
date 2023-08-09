namespace AutomationPipeline.Interfaces
{
    public interface ICommandService
    {
        public void FileCopy(string sourceFilePath, string destinationFilePath);

        public void FileDelete(string filePath);

        public void QueryFolderFiles(string folderPath);

        public void CreateFolder(string folderPath,string folderName);

        public Task DownloadFile(string sourcePath,string outputFileName);

        public Task Wait(int waitTimeInSeconds);

        public int SearchStringReturnsRowCount(string filePath,string stringToSearch);
    }
}
