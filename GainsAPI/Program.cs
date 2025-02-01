using GainsAPI.DataAccess;
using GainsAPI.DataDtos;
using GainsModel.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile($"appsettings.private.json", true);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

builder.Services.AddDbContextPool<GainsContext>(options => options.UseNpgsql());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.Urls.Add("http://0.0.0.0:5000");
}


app.UseHttpsRedirection();
app.UseCors();

app.MapGet("/arewelive", () =>
{
    Console.WriteLine("Hit live check endpoint.");
    return "Yes, we are alive";
});

app.MapGet("/weightunit", async () =>
{
    var dataAccess = new GainsContext(app.Configuration["DB_CONN"] ?? "connection string error");
    return await dataAccess.Weightunitlookups.ToListAsync();
});

app.MapGet("/exercise", async () =>
{
    var dataAccess = new DataAccessor(app.Configuration["DB_CONN"] ?? "connection string error");
    return await dataAccess.GetExercises();
});

app.MapGet("/workout", async () =>
{
    var dataAccess = new DataAccessor(app.Configuration["DB_CONN"] ?? "connection string error");
    return await dataAccess.GetWorkouts();
});

app.MapGet("/exerciseset", async () =>
{
    var dataAccess = new DataAccessor(app.Configuration["DB_CONN"] ?? "connection string error");
    return await dataAccess.GetExerciseSets();
});

app.MapGet("/exerciseset/dto", async () =>
{
    var dataAccess = new DataAccessor(app.Configuration["DB_CONN"] ?? "connection string error");
    return await dataAccess.GetExerciseSetsAsDtos();
});

app.MapGet("/musclegroup", async () =>
{
    var dataAccess = new DataAccessor(app.Configuration["DB_CONN"] ?? "connection string error");
    return await dataAccess.GetMuscleGroups();
});

// app.MapPost("/exerciseset", async (ExerciseSetDto exerciseSetDto) =>
// {
//     var dataAccess = new DataAccessor(app.Configuration["DB_CONN"] ?? "connection string error");
//     var result = await dataAccess.AddExerciseSet(exerciseSetDto);
//     return result;
// });

app.MapPost("/exercise", async (ExerciseDto exerciseDto) =>
{
    var dataAccess = new DataAccessor(app.Configuration["DB_CONN"] ?? "connection string error");
    var result = await dataAccess.AddExercise(exerciseDto);
    return result;
});

app.MapPost("/sync/exercise", async (List<ExerciseModelFromMobile> exerciseModels) =>
{
    var dataAccess = new DataAccessor(app.Configuration["DB_CONN"] ?? "connection string error");
    var result = await dataAccess.SyncExercises(exerciseModels);
    return result;
});

// app.MapPost("/workout/old", async (DateTime dateStarted) =>
// {
//     var dataAccess = new DataAccessor(app.Configuration["DB_CONN"] ?? "connection string error");
//     var result = await dataAccess.AddWorkout(dateStarted);
//     return result;
// });

app.MapPost("/workout", async (WorkoutDto workoutDto) =>
{
    var dataAccess = new DataAccessor(app.Configuration["DB_CONN"] ?? "connection string error");
    var result = await dataAccess.AddWorkout(workoutDto);
    return result;
});

app.MapPost("/exercise/new", async (ExerciseDto exerciseDto) =>
{
    var dataAccess = new DataAccessor(app.Configuration["DB_CONN"] ?? "connection string error");
    var result = await dataAccess.AddExerciseWithResult(exerciseDto);
    return result;
});
app.Run();



