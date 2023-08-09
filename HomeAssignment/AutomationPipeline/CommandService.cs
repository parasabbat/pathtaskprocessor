using AutomationPipeline.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AutomationPipeline
{
    public class CommandService : ICommandService
    {
        public Task Donothing()
        {
            return Task.CompletedTask;
        }

        public void FileCopy(string sourceFilePath, string destinationFilePath)
        {
            File.Copy(sourceFilePath, destinationFilePath, true);
        }

        public void FileDelete(string filePath)
        {
            File.Delete(filePath);
        }
    }
}
