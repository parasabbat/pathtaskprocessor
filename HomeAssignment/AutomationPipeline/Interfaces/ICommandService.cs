namespace AutomationPipeline.Interfaces
{
    public interface ICommandService
    {
        public void FileCopy(string sourceFilePath, string destinationFilePath);

        public void FileDelete(string filePath);

        public Task Donothing();
    }
}
