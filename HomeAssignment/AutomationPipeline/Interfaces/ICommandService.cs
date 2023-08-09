namespace AutomationPipeline.Interfaces
{
    public interface ICommandService
    {
        public Task FileCopy(string sourceFilePath, string destinationFilePath);

        public Task FileDelete(string filePath);

        public Task Donothing();
    }
}
