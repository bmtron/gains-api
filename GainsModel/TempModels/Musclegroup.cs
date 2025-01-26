using System;
using System.Collections.Generic;

namespace GainsModel.TempModels;

public partial class Musclegroup
{
    public int Musclegroupid { get; set; }

    public string Musclegroupname { get; set; } = null!;

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
}
