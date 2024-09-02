namespace MetroFibre.RecipeApp.API.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly string _apiKey;
        public AuthenticationMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
            _apiKey = "MetroFibre.ApiKey";
        }

        public async Task InvokeAsync(HttpContext context)
        {

            if (!context.Request.Headers.TryGetValue(_apiKey, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key not provided.Please refer to the AppSettings Of the API to test it.");
            }

            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(_apiKey);

            if (extractedApiKey != appSettings["MetroFibre.ApiKey"])
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorised Client");
                return;
            }

            //context.Request.Headers.(_apiKey, extractedApiKey);

            await _requestDelegate(context);//Send request to controllers
        }
    }
}
