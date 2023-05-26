namespace DelicousWebApp.Models
{
    public class Chef
    {
        public int Id { get; set; }
        public string ImageName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public int JobId { get; set; }
        public Job Job { get; set; } = null!;
    }
}
