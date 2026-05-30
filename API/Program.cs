using Application;
using Application.Comment;
using Application.Activity.Queries;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//To establish our connection to the database
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IActivityService, ActivityService_Impl>();
builder.Services.AddScoped<IActivityRepo, ActivityRepo_Impl>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentRepo, CommentRepo>();

// For manual mapper creation
// builder.Services.AddScoped<IActivityMapper, ActivityMapper_Impl>();

//For AutoMapper mapper creation
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<GetActivityList.Handler>());
//For logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://127.0.0.1:3000", "https://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

//be careful with the order of everything added after this line
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}



app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
    await DbInitializer.SeedData(context);
}
catch(Exception e)
{
    logger.LogError(e, "An error occurred during migration");
}

app.Run();
