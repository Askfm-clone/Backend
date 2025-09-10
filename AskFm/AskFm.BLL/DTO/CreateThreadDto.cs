using AskFm.DAL.Enums;
using AskFm.DAL.Models;

namespace AskFm.BLL.DTO;

public class CreateThreadDto
{
    public int AskedId { get; set; }
    
    public string QuestionContent { get; set; }

    public ThreadStatus Status { get; set; }
    
    public bool isAnonymous { get; set; }
    
}