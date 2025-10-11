using AskFm.BLL.DTO;
using AskFm.DAL.Models;

namespace AskFm.BLL.Services;

public interface ICommentService
{
    // Get a specific comment by ID
    Task<ServiceResult<CommentResponseDto>> GetCommentAsync(int id);
    
    // Add a comment to a thread
    Task<ServiceResult<CommentResponseDto>> AddComment(int threadId, int userId, CreateCommentDto commentDto);
    
    // Get all comments for a thread with pagination
    Task<ServiceResult<PagedResponseDto<CommentResponseDto>>> GetCommentsByThreadId(int threadId, int page, int pageSize);
    
    // Delete a specific comment
    Task<ServiceResult<bool>> DeleteComment(int threadId, int commentId, int userId);
}