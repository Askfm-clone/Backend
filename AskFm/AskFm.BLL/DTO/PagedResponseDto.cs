namespace AskFm.BLL.DTO;

public class PagedResponseDto<T>
{
    public List<T> Items { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public bool HasMore { get; set; }
}