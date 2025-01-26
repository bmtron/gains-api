using GainsAPI.DataDtos;
using GainsModel.Models;
using Microsoft.EntityFrameworkCore;

namespace GainsAPI.DataAccess;

public class DataAccessor
{
    private GainsContext _gainsContext;

    public DataAccessor(string connectionString)
    {
        _gainsContext = new GainsContext(connectionString);
    }

    public DataAccessor()
    {
        _gainsContext = new GainsContext();
    }

    public async Task<List<Exercise>> GetExercises()
    {
        return await _gainsContext.Exercises.ToListAsync();
    }

    public async Task<bool> AddExercise(ExerciseDto exercise)
    {
        var newExercise = new Exercise
        {
            Exercisename = exercise.Name,
            Notes = exercise.Notes,
            Musclegroupid = exercise.MuscleGroupId,
            Dateadded = DateTime.Now,
        };
        
        try
        {
            await _gainsContext.Exercises.AddAsync(newExercise);
            await _gainsContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }

        return true;
    }

    public async Task<bool> SyncExercises(List<ExerciseModelFromMobile> exerciseDtos)
    {
        var existingExercises = await _gainsContext.Exercises.ToListAsync();
        var exercisesToAdd = new List<Exercise>();
        var exercisesToUpdate = new List<Exercise>();
        foreach (var exercise in exerciseDtos)
        {
            if (existingExercises.All(e => e.Exerciseid != exercise.exerciseId))
            {
                var exerciseToAdd = new Exercise
                {
                    Exerciseid = exercise.exerciseId,
                    Musclegroupid = exercise.muscleGroupId,
                    Notes = exercise.notes,
                    Exercisename = exercise.exerciseName,
                    Dateadded = exercise.dateAdded,
                    Dateupdated = exercise.dateUpdated
                };
                exercisesToAdd.Add(exerciseToAdd);
            }
            else
            {
                var exerciseToUpdate = existingExercises.Single(e => e.Exerciseid == exercise.exerciseId);
                exerciseToUpdate.Exercisename = exercise.exerciseName;
                exerciseToUpdate.Notes = exercise.notes;
                exerciseToUpdate.Musclegroupid = exercise.muscleGroupId;
                exerciseToUpdate.Dateupdated = DateTime.Now;
                
                exercisesToUpdate.Add(exerciseToUpdate);
            }
        }

        try
        {
            _gainsContext.Exercises.AddRange(exercisesToAdd);
            _gainsContext.Exercises.UpdateRange(exercisesToUpdate);
            await _gainsContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error writing exercise sync to database.");
            Console.WriteLine(e.Message);
            return false;
        }

        return true;
    }
    public async Task<List<Workout>> GetWorkouts()
    {
        return await _gainsContext.Workouts.ToListAsync();
    }

    public async Task<bool> AddWorkout(WorkoutDto workoutDto)
    {
        var newWorkout = new Workout
        {
            Datestarted = workoutDto.DateStarted.ToLocalTime(),
            Dateadded = DateTime.Now.ToLocalTime(),
        };
        await _gainsContext.Workouts.AddAsync(newWorkout);

        var sets = workoutDto.ExerciseSets.Select(set => new Exerciseset
            {
                Exerciseid = set.exerciseid,
                Estimatedrpe = set.estimatedrpe,
                Repetitions = set.repetitions,
                Weightunitlookupid = set.weightunitlookupid,
                Weight = set.weight,
                Dateadded = DateTime.Now.ToLocalTime(),
                Dateupdated = DateTime.Now.ToLocalTime(),
                Workout = newWorkout
            })
            .ToList();
        await _gainsContext.Exercisesets.AddRangeAsync(sets);
        
        try
        {
            await _gainsContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }

    public async Task<List<Exerciseset>> GetExerciseSets()
    {
        return await _gainsContext.Exercisesets.ToListAsync();
    }
    public async Task<List<ExerciseSetDto>> GetExerciseSetsAsDtos()
    {
        var rawSets = await _gainsContext.Exercisesets.ToListAsync();
        var sets = rawSets.Select(set => new ExerciseSetDto
        {
            exerciseid = set.Exerciseid,
            estimatedrpe = set.Estimatedrpe,
            repetitions = set.Repetitions,
            weightunitlookupid = set.Weightunitlookupid,
            dateadded = set.Dateadded,
            weight = set.Weight,
            workoutid = set.Workoutid
        }).ToList();
        return sets;
    }
    public async Task<bool> AddExerciseSet(ExerciseSetDto exerciseSet)
    {
        var newSet = new Exerciseset
        {
            Exerciseid = exerciseSet.exerciseid,
            Estimatedrpe = exerciseSet.estimatedrpe,
            Repetitions = exerciseSet.repetitions,
            Weightunitlookupid = exerciseSet.weightunitlookupid,
            Weight = exerciseSet.weight,
            Dateadded = DateTime.Now,
            Dateupdated = DateTime.Now
        };
        try
        {
            await _gainsContext.Exercisesets.AddAsync(newSet);
            await _gainsContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }

    public async Task<List<Musclegroup>> GetMuscleGroups()
    {
        return await _gainsContext.Musclegroups.ToListAsync();
    }
}