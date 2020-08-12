using Microsoft.AspNetCore.Http;

namespace DatingAppLatest.API.Helpers
{
    public static class Extensions
    {
        // msg - extension method to handle errors, this methos has been called in startup class when adding the error handler middleware(UseExceptionHandler)
        public static void AddApplicationError(this HttpResponse respons, string message)
        {
            respons.Headers.Add("Application-Error", message);
            respons.Headers.Add("Access-Control-Expose-Headers","Application-Error"); 
            // from the browser console, we can see the error added to the headers in the name of 'Application-Error'
            respons.Headers.Add("Access-Control-Allow-Origin","*");

        }
    }
}