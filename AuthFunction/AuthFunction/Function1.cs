using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;

namespace AuthFunction
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            var (authenticationStatus, authenticationResponse) = await req.HttpContext.AuthenticateAzureFunctionAsync();
            if (authenticationStatus)
            {
                return new OkObjectResult($"Hello {req.HttpContext.User.GetDisplayName()}");
            }
            req.HttpContext.VerifyUserHasAnyAcceptedScope("<scope name>");

            return authenticationResponse;
        }
    }
}
