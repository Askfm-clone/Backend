using AskFm.BLL.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AskFm.BLL.Services;

public interface IThreadLikeService
{
    public Task<ServiceResult<ThreadLikeResponseDto>> AddLike(int id, int userId);
    public Task<ServiceResult<List<ThreadLikeResponseDto>>> GetLikes(int id);
    public Task<ServiceResult<bool>> RemoveLike(int threadId, int userId);
}