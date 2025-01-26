namespace GainsAPI.DataDtos;

public class ExerciseModelFromMobile
{
    public int exerciseId { get; set; }
    public int muscleGroupId { get; set; }
    public string exerciseName { get; set; }
    public string notes { get; set; }
    public DateTime dateAdded { get; set; }
    public DateTime? dateUpdated { get; set; }
}