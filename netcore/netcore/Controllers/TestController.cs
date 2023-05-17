using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;

namespace netcore.Controllers
{
    [ApiController]
    [Route("api/nightscout")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IConfiguration configuration;

        public TestController(ILogger<TestController> logger, IConfiguration _configuration)
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

                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.StartInfo.Arguments = $"-c \"   docker stop test-mongo; docker rm test-mongo;  docker run -i --restart=always --name test-mongo  -e TZ=Asia/Shanghai  -p 27017:27017  -d mongo:4.4 \"";
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            _logger.LogInformation(output);
            process.WaitForExit();
        }
    }
}