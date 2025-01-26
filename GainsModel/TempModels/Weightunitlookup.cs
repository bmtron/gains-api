using System;
using System.Collections.Generic;

namespace GainsModel.TempModels;

public partial class Weightunitlookup
{
    public int Weightunitlookupid { get; set; }

    public string Weightunitname { get; set; } = null!;

    public string Weightunitlabel { get; set; } = null!;

    public virtual ICollection<Exerciseset> Exercisesets { get; set; } = new List<Exerciseset>();
}
