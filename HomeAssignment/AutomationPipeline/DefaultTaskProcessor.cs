using AutomationPipeline.Interfaces;

namespace AutomationPipeline
{
    public class DefaultTaskProcessor : ITaskProcessor
    {
        private readonly ICommandService _commandService;
        public DefaultTaskProcessor(ICommandService commandService) 
        {
            _commandService = commandService;
        }
        public async Task DoWorkAsync()
        {
            Console.WriteLine("Please Enter command Number of your choice from below \n");
            var availableCommands = new Dictionary<int, string>()
            {   
                {1, "Copy File" },
                {2, "Delete File" },
                {3, "Query Folder Files" },
                {4, "Create Folder" },
                {5, "Download File" },
                {6, "Wait" },
                {7, "Search Row Count" }
            };

            foreach (var command in availableCommands)
            {
                Console.WriteLine($"{command.Key}-{command.Value} \n");
            }

            int selectedCommand;
            while (!int.TryParse(Console.ReadLine(), out selectedCommand) 
                || selectedCommand <= 0 
                || selectedCommand > availableCommands.Count)
            {
                Console.Write("Invalid Input,Please input again \n");
            }

            Console.WriteLine($"You have selected {availableCommands[selectedCommand]} Command");

            switch (selectedCommand)
            {
                case 1:
                    // Copy File
                    Console.WriteLine("Source File Path \n");
                    string? sourceFilePath = Console.ReadLine();
                    Console.WriteLine("Destination File Path \n");
                    string? destinationFilePath = Console.ReadLine();
                    _commandService.FileCopy(sourceFilePath, destinationFilePath);
                    break;
                case 2:
                    // Delete File
                    Console.WriteLine("File Path \n");
                    string? filePath = Console.ReadLine();
                    _commandService.FileDelete(filePath);
                    break;
                case 3:
                    // Query Folder Files
                    Console.WriteLine("Folder Path \n");
                    string? folderPath = Console.ReadLine();
                    _commandService.QueryFolderFiles(folderPath);
                    break;
                case 4:
                    // Create Folder
                    Console.WriteLine("Folder Path \n");
                    folderPath = Console.ReadLine();
                    Console.WriteLine("Folder Name \n");
                    string? folderName = Console.ReadLine();
                    _commandService.CreateFolder(folderPath,folderName);
                    break;
                case 5:
                    // Download File
                    Console.WriteLine("Source Path \n");
                    string? sourcePath = Console.ReadLine();
                    Console.WriteLine("Output File Name \n");
                    string? outputFileName = Console.ReadLine();
                    await _commandService.DownloadFile(sourcePath, outputFileName);
                    break;
                case 6:
                    // Wait
                    int waitTime;
                    Console.WriteLine("Wait time in seconds \n");
                    while (!int.TryParse(Console.ReadLine(), out waitTime))
                    {
                        Console.Write("Invalid Input,Please input again \n");
                    }
                    await _commandService.Wait(waitTime);
                    break;
                case 7:
                    // Search Row Count
                    Console.WriteLine("File Path \n");
                    filePath = Console.ReadLine();
                    Console.WriteLine("String to search \n");
                    string? stringToSearch = Console.ReadLine();
                    int searchCount = _commandService.SearchStringReturnsRowCount(filePath, stringToSearch);
                    Console.WriteLine($"String {stringToSearch} appeared in {searchCount} row/rows");
                    break;
            }

            await Task.Delay(100);
            Console.WriteLine("\nPress any key to exit!");
            Console.ReadKey();
        }
    }
}
