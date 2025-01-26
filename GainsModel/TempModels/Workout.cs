using System;
using System.Collections.Generic;

namespace GainsModel.TempModels;

public partial class Workout
{
    public int Workoutid { get; set; }

    public DateTime Datestarted { get; set; }

    public DateTime Dateadded { get; set; }

    public DateTime Dateupdated { get; set; }

    public virtual ICollection<Exerciseset> Exercisesets { get; set; } = new List<Exerciseset>();
}
