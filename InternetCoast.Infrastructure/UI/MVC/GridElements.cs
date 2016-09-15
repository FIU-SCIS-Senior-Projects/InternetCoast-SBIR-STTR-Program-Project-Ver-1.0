namespace InternetCoast.Infrastructure.UI.MVC
{
    public class GridElements
    {
        public string Filter { get; set; }

        public int PageNumber { get; set; }
        
        public int ItemsPerPage { get; set; }
        
        public int TotalElements { get; set; }
        
        public int TotalPages
        {
            get
            {
                return TotalElements <= ItemsPerPage
                    ? 1
                    : TotalElements % ItemsPerPage != 0 ? TotalElements / ItemsPerPage + 1 : TotalElements / ItemsPerPage;
            }
        }

        public GridElements(int itemsPerPage)
        {
            ItemsPerPage = itemsPerPage;
            PageNumber = 1;
        }
    }
}
