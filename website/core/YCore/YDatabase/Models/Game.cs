using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace YDatabase.Models;

public partial class Game
{
    public int Id { get; set; }

    public int? UpdationId { get; set; }

    public int Player1 { get; set; }

    public int Player2 { get; set; }

    public int? Round { get; set; }

    public bool? IsUpper { get; set; }

    public bool IsGroup { get; set; }

    public int? Winner { get; set; }

    public virtual Player Player1Navigation { get; set; } = null!;

    public virtual Player Player2Navigation { get; set; } = null!;
}
