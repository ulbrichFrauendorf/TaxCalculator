namespace TaxCalculatorAPI.Authorization.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUserService userService, IJwtTokenValidator jwtTokenValidator)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = jwtTokenValidator.Validate(token);
        if (userId != null)
        {
            // attach user to context on successful jwt validation
            context.Items["ApiUser"] = userService.GetById(userId.Value);
        }

        await _next(context);
    }
}