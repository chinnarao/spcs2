using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Log.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogger _logger;
        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public void Log1([FromBody] string log)
        {
            _logger.Log(LogLevel.Information, log);
        }

        [HttpPost]
        public void Error([FromBody] string error)
        {
            _logger.Log(LogLevel.Error, error);
        }

        [HttpPost]
        public void Log(Error log)
        {
            _logger.Log(LogLevel.Information, log.ToString());
        }
    }

    public class Error
    {
        public int level { get; set; }
        public string message { get; set; }
        public override string ToString()
        {
            string tostring = "{ level: " + Enum.GetName(typeof(NgxLoggerLevel), level) + ", message: " + message + " }";
            return tostring;
        }
    }
    enum NgxLoggerLevel
    {
        TRACE = 0,
        DEBUG = 1,
        INFO = 2,
        LOG = 3,
        WARN = 4,
        ERROR = 5,
        OFF = 6,
    }
}
