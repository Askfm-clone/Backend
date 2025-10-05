namespace AskFm.BLL.DTO;

public class ThreadLikeResponseDto
{
    public int userId {get;set;}
    public int  threadId {get;set;}
    public DateTime createdAt {get;set;}
    public string UserName {get;set;}
    string ProfilePicture {get;set;}
    
    
}