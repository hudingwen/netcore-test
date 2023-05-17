using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;

namespace netcore.Controllers
{
    [ApiController]
    [Route("api/nightscout")]
    public class NightscoutController : ControllerBase
    {
        private readonly ILogger<NightscoutController> _logger;
        private readonly IConfiguration configuration;

        public NightscoutController(ILogger<NightscoutController> logger, IConfiguration _configuration)
        {
            _logger = logger;
            configuration = _configuration;
        }

        [HttpGet]
        public void Get()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = "-c \"date; date; date\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            _logger.LogInformation(output);
            process.WaitForExit();
        }
    }
}