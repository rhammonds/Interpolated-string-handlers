using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpolated_string_handlers
{
    public class SampleCode
    {
        ILogger<SampleCode> _logger;

        public SampleCode (ILogger<SampleCode> logger)
        {
            _logger = logger;
        }
        public void LogSomething()
        {
            var time = DateTime.Now;
            
            _logger.LogWarning($"Error Level. CurrentTime: {time:t}. This is an error. It will be printed.");
        }
    }
}
