using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGivers.Models;

public class ReliefProject
{ 
    [Key]
    public int ProjectID { get; set; }

    [Required, StringLength(150)]
    public string ProjectName { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string Province { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string DisasterType { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; }

    [Required, StringLength(30)]
    public string Status { get; set; } = "Active";

    [StringLength(1000)]
    public string Summary { get; set; } = string.Empty;

    public ICollection<ProjectUpdate> Updates { get; set; } = [];
}
