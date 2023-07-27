namespace SNUS_PROJECT.DTO
{
    public class FilterSortDto
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int Sort { get; set; }
        public FilterSortDto() { }

        public FilterSortDto(DateTime from, DateTime to, int sort)
        {
            From = from;
            To = to;
            Sort = sort;
        }
    }
}
