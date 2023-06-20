using System;
using System.Collections.Generic;

namespace YDatabase.Models;

public partial class Player
{
    public int Id { get; set; }

    public string Nickname { get; set; } = null!;

    public int? ImageId { get; set; }

    public string? Descr { get; set; }

    public int? GroupNumber { get; set; }

    public int? Won { get; set; }

    public int? Lose { get; set; }

    public int? Points { get; set; }

    public virtual ICollection<Game> GamePlayer1Navigations { get; set; } = new List<Game>();

    public virtual ICollection<Game> GamePlayer2Navigations { get; set; } = new List<Game>();

    public virtual Image? Image { get; set; }

    public virtual ICollection<Link> Links { get; set; } = new List<Link>();
}
