using System;
using System.Collections.Generic;

namespace GainsModel.Models;

public partial class Exercise
{
    public int Exerciseid { get; set; }

    public int Musclegroupid { get; set; }

    public string Exercisename { get; set; } = null!;

    public string Notes { get; set; } = null!;

    public DateTime Dateadded { get; set; }

    public DateTime? Dateupdated { get; set; }

    public virtual ICollection<Exerciseset> Exercisesets { get; set; } = new List<Exerciseset>();

    public virtual Musclegroup Musclegroup { get; set; } = null!;
}
