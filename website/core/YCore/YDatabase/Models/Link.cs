using System;
using System.Collections.Generic;

namespace YDatabase.Models;

public partial class Link
{
    public int Id { get; set; }

    public string Link1 { get; set; } = null!;

    public int? Player { get; set; }

    public string? Descr { get; set; }

    public virtual Player? PlayerNavigation { get; set; }
}
