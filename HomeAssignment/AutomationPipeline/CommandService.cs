using AutomationPipeline.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPipeline
{
    public class CommandService : ICommandService
    {
        public Task Donothing()
        {
            return Task.CompletedTask;
        }

        public Task FileCopy(string sourceFilePath, string destinationFilePath)
        {
            throw new NotImplementedException();
        }

        public Task FileDelete(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
