using CoreVision.Infrastructure;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

const string DefaultDatabaseName = "CoreVision";

builder.Host.UseSerilog((ctx, lc) =>
{
    lc.ReadFrom.Configuration(ctx.Configuration)
      .Enrich.FromLogContext()
      .Enrich.WithMachineName()
      .Enrich.WithThreadId()
      .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}");

    var columnOptions = new ColumnOptions();
    columnOptions.AdditionalColumns = new SqlColumn[]
    {
        new SqlColumn
        {
            ColumnName = "SourceContext",
            PropertyName = "SourceContext",
            DataType = SqlDbType.NVarChar,
            DataLength = 512
        },
        new SqlColumn
        {
            ColumnName = "TargetContext",
            PropertyName = "TargetContext",
            DataType = SqlDbType.NVarChar,
            DataLength = 512
        }
    };

    lc.WriteTo.Logger(it => it
        .Filter.ByIncludingOnly($"TargetContext = '{DefaultDatabaseName}'")
        .WriteTo.MSSqlServer(
            connectionString: ctx.Configuration.GetConnectionString("DefaultConnectionString"),
            sinkOptions: new MSSqlServerSinkOptions { TableName = "CoreVisionLogEvents", AutoCreateSqlTable = true },
            columnOptions: columnOptions
        )
     );
});

var allowedHosts = builder.Configuration.GetSection("AllowedHosts").Get<string>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(cfg =>
    {
        cfg.AllowAnyOrigin();
        cfg.AllowAnyHeader();
        cfg.AllowAnyMethod();
    });

    options.AddPolicy("AnyOrigin", cfg =>
    {
        cfg.AllowAnyOrigin();
        cfg.AllowAnyHeader();
        cfg.AllowAnyMethod();
    });
});

builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (Serilog.Context.LogContext.PushProperty("TargetContext", DefaultDatabaseName))
{
    var projectName = Assembly.GetExecutingAssembly().GetName().Name;

    Log.Information("{Project} Iniciada | Entorno: {Environment} | DB: {Database}", projectName, app.Environment.EnvironmentName, DefaultDatabaseName);
}

app.UseSerilogRequestLogging();

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandler = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandler?.Error;

        if (exception != null)
        {
            if (exception.Message != "test")
            {
                using (Serilog.Context.LogContext.PushProperty("TargetContext", DefaultDatabaseName))
                {
                    string routeWithMessage = $"Route/endpoint: {exceptionHandler?.Path} \n \n {exception.Message}";
                    app.Logger.LogError(exception, routeWithMessage);
                }
            }

            var details = new ProblemDetails
            {
                Title = "Ha ocurrido un error inesperado.",
                Detail = exception.Message,
                Status = StatusCodes.Status500InternalServerError,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Instance = exceptionHandler?.Path
            };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(details);
        }
    });
});


app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapGet("/status", () => new
{
    Status = "API Funcionando",
    Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
    Environment = app.Environment.EnvironmentName
});

app.MapGet("/error/test", [EnableCors("AnyOrigin")] () => { throw new Exception("test"); });

app.MapControllers().RequireCors("AnyOrigin");

app.Run();