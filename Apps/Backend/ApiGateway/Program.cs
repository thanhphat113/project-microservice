using System.Security.Claims;
using System.Text;
using ApiGateway.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.HttpOverrides;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var frontEndUrls = builder.Configuration.GetValue<string>("FRONTEND_URLS");
Console.WriteLine("tôi là FRONTEND_URLS:" + frontEndUrls);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins(frontEndUrls)
                  .AllowAnyHeader()
                  .AllowCredentials()
                  .AllowAnyMethod();
        });
});

builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    var jobKey = new JobKey("DailyApiJob");
    q.AddJob<RemoveRevokedOrExpiredToken>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("DailyApiJob-trigger")
        .WithCronSchedule("0 0 0 * * ?"));
});

builder.Services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);

builder.Configuration.AddJsonFile($"ocelot.{builder.Configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT")}.json", optional: false, reloadOnChange: true);

builder.Services.AddAuthentication("Bearer")
        .AddJwtBearer("Bearer", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var token = context.Request.Cookies["access"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        context.Token = token;
                    }

                    return Task.CompletedTask;
                }
            };
        });

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedFor
});

app.UseCors("AllowSpecificOrigins");



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    if (context.User.Identity?.IsAuthenticated == true)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Console.WriteLine("Claims: " + userId);
        if (!string.IsNullOrEmpty(userId))
        {
            context.Request.Headers["X-User-Id"] = userId;
        }
    }

    await next();
});

await app.UseOcelot();

app.Run();
