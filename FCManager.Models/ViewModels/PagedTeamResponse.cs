namespace FCManager.Models.ViewModels
{
    public class PagedTeamResponse<T>
    {
        public List<T> ResponseData { get; set; } = new List<T>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
