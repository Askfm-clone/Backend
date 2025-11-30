using System.ComponentModel.DataAnnotations;
using AskFm.DAL.Enums;
using AskFm.DAL.Models;

namespace AskFm.BLL.DTO;

public class CreateThreadDto
{
    [Required(ErrorMessage = "Asked user ID is required")]
    public int AskedId { get; set; }
    
    [Required(ErrorMessage = "Question content is required")]
    [StringLength(1000, MinimumLength = 2, ErrorMessage = "Question must be between 2 and 1000 characters")]
    public string QuestionContent { get; set; }

    public ThreadStatus Status { get; set; }
    
    public bool IsAnonymous { get; set; }
    
}