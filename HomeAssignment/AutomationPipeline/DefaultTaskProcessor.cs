using AutomationPipeline.Interfaces;

namespace AutomationPipeline
{
    public class DefaultTaskProcessor : ITaskProcessor
    {
        public void DoWorkAsync()
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
            
            Thread.Sleep(100);
            Console.WriteLine("\nPress any key to exit!");
            Console.ReadKey();
        }
    }
}
