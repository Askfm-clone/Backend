using AskFm.DAL.Enums;

namespace AskFm.BLL.DTO;

public class ThreadResponseDto
{
    public int Id { get; set; }
    
    public string QuestionContent { get; set; }
    
    public ThreadStatus Status { get; set; }
    
    public bool IsAnonymous { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    // Simplified user info instead of full objects
    public int AskerId { get; set; }
    
    public string AskerName { get; set; }
    
    public int AskedId { get; set; }
    
    public string AskedName { get; set; }

    public string answer;
}