namespace DelicousWebApp.Models
{
    public class Job
    {
        public Job()
        {
            Chefs=new List<Chef>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Chef> Chefs { get; set; } = null!; 
    }
}
