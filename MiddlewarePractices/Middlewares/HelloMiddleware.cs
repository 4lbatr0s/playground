namespace MiddlewarePractices.Middlewares
{
    public class HelloMiddleware
    {
        
        private readonly RequestDelegate _next;
        //this delegate will come from the next middleware and with the help of the Invoke method, we'll be able to execute the next middleware.
        public HelloMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context) //calls the next middleware.
        {
            Console.WriteLine("Hello World");
            await _next.Invoke(context);
            System.Console.WriteLine("Bye bye world!");
        }
    }
    //should be called by an application builder(example: app.Use())
    static public class HelloMiddlewareExtension
    {
                        //call this middleware by app.UseHello()
        public static IApplicationBuilder UseHello(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HelloMiddleware>();
        }
    }
}