namespace GainsAPI.DataDtos;

public class WorkoutDto
{
    public DateTime DateStarted { get; set; }
    public List<ExerciseSetDto> ExerciseSets { get; set; }
}