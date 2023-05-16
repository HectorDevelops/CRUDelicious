using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618 // ignores the current fields that need to be non-nulled 

public class Dish 
{
    [Key]
    public int DishId {get; set;}

    [Required(ErrorMessage = "Provide a Dish Name.")]
    public string Name {get; set;}

     [Required(ErrorMessage = "Provide a Chef's Name.")]
    public string Chef {get; set;}

    [Required]
    [Range(1, 5, ErrorMessage = "Tastiness must be rated 1 - 5. ")]
    public int Tastiness {get; set;}

    [Required]
    [Range(1, Int32.MaxValue, ErrorMessage = "Calories must be greater than 0.")]
    public int Calories {get; set;}

    [Required]
    [StringLength(150, MinimumLength =5, ErrorMessage = "Description is required. ")]
    public string Description {get; set;}

    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;
}