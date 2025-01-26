namespace GainsAPI.DataDtos;

public class ExerciseSetDto
{
    public int exerciseid { get; set; }

    public decimal weight { get; set; }

    public int weightunitlookupid { get; set; }

    public int repetitions { get; set; }

    public int estimatedrpe { get; set; }

    public DateTime? dateadded { get; set; }
    public int? workoutid { get; set; }
}