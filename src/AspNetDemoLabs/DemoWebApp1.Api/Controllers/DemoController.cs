using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebApp1.Api.Controllers;


/// <summary>
///     The API Controller used to demonstrate the XMLHttpRequest readyState transitions.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class DemoController : ControllerBase
{


    /// <summary>
    /// Returns a greeting message after an artificial delay.
    /// </summary>
    /// <remarks>
    /// The delay is intentionally introduced to allow the client-side
    /// JavaScript to clearly observe XHR readyState transitions.
    /// </remarks>
    /// <returns>A string containing "Hello world".</returns>
    /// <response code="200">Returns the greeting string.</response>
    [HttpGet("GetHello")]
    public async Task<IActionResult> GetHello()
    {
        // Simulate server-side processing delay
        await Task.Delay(3000);

        return Ok("Hello world");
    }



    /// <summary>
    ///     Streams a greeting message slowly to demonstrate incremental data arrival in XMLHttpRequest.
    /// </summary>
    /// <remarks>
    ///     Each chunk is sent with a delay so that the browser fires 
    ///         readyState == 3 
    ///     repeatedly while receiving data.
    /// </remarks>
    /// <returns>
    ///     A streamed text response.
    /// </returns>
    [HttpGet("GetHelloStream")]
    public async Task GetHelloStream()
    {
        
        // Defines the Content Type of the Response from the API endpoint.
        Response.ContentType = "text/plain";

        // IMPORTANT: Disable Caching and Buffering so that the streamed content can be sent immediately!
        Response.Headers.Append("Cache-Control", "no-cache");
        Response.Headers.Append("X-Accel-Buffering", "no");


        // IMPORTANT: Ensure that response streaming starts immediately!
        await Response.StartAsync();

        var parts = new[]
        {
            "Hello",
            " ",
            "world",
            " ",
            "from",
            " ",
            ".NET",
            " ",
            "streaming",
            " ",
            "API!"
        };

        foreach (var part in parts)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(part);

            // Write the chunk
            await Response.Body.WriteAsync(bytes, 0, bytes.Length);

            // Immediately send it to the client
            await Response.Body.FlushAsync();

            // Create an artificial delay so the client will see incremental loading.
            // So, when the next chunk is sent, the XHR object raises the event setting its readystate to 3.
            await Task.Delay(3000);
        }
    
    }

}
