using DelicousWebApp.Models;

namespace DelicousWebApp.ViewModels.ChefVM
{
    public class CreateChefVM
    {
        public IFormFile Image { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public int JobId { get; set; }
        public Job? Job { get; set; }
        public List<Job>? Jobs { get; set; } 
        public string? ImageName { get; set; }
    }
}
