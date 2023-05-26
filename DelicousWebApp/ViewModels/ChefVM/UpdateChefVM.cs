using DelicousWebApp.Models;

namespace DelicousWebApp.ViewModels.ChefVM
{
    public class UpdateChefVM
    {
        public IFormFile? Image { get; set; } 
        public string? Name { get; set; }
        public string? Surname { get; set; } 
        public int JobId { get; set; }
        public List<Job>? Jobs { get; set; }
        public string? ImageName { get; set; } 
    }
}
