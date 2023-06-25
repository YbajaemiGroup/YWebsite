namespace YDatabase.Models;

public partial class Image
{
    public int Id { get; set; }

    public string ImageName { get; set; } = null!;

    public bool IsStaff { get; set; }

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
