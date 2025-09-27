using AskFm.BLL.DTO;
using Thread = AskFm.DAL.Models.Thread;

namespace AskFm.BLL.Services;
public interface IThreadService
{
    Task<ServiceResult<ThreadResponseDto>> AddThread(int userId, CreateThreadDto createThreadDto);
    Task<ServiceResult<List<ThreadResponseDto>>> GetAllThreads(int userId);
    Task<ServiceResult<ThreadResponseDto>> AnswerThread(ThreadAnswerDto threadAnswerDto);
}