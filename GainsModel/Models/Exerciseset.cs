using System;
using System.Collections.Generic;

namespace GainsModel.Models;

public partial class Exerciseset
{
    public int Exercisesetid { get; set; }

    public int Exerciseid { get; set; }

    public int Workoutid { get; set; }

    public decimal Weight { get; set; }

    public int Weightunitlookupid { get; set; }

    public int Repetitions { get; set; }

    public int Estimatedrpe { get; set; }

    public DateTime Dateadded { get; set; }

    public DateTime? Dateupdated { get; set; }

    public virtual Exercise Exercise { get; set; } = null!;

    public virtual Weightunitlookup Weightunitlookup { get; set; } = null!;

    public virtual Workout Workout { get; set; } = null!;
}
