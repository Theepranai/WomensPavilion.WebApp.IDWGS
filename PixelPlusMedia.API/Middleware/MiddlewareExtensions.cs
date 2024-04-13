namespace PixelPlusMedia.API.Middleware;
public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandle(this IApplicationBuilder build)
    {
        return build.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
