namespace DelicousWebApp.ViewModels
{
    public class PaginationVM<T>
    {
        public List<T> Chefs   { get; set; }
        public int CurrentPage { get; set; } 
        public int PageCount { get; set; } 
    }
}
