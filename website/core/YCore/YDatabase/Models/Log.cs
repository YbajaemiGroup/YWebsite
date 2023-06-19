using System;
using System.Collections.Generic;

namespace YDatabase.Models;

public partial class Log
{
    public long Id { get; set; }

    public DateTime DateTime { get; set; }

    public string Severety { get; set; } = null!;

    public string Source { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string? Exception { get; set; }
}
