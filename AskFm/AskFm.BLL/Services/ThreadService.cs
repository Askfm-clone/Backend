using AskFm.BLL.DTO;
using AskFm.DAL.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Thread = AskFm.DAL.Models.Thread;

namespace AskFm.BLL.Services;

public class ThreadService : IThreadService
{
    
    private IUnitOfWork _unitOfWork;
    private readonly ILogger<CommentLikeService> _logger;
    private readonly IMapper _mapper;
    public ThreadService(IUnitOfWork unitOfWork,  ILogger<CommentLikeService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task<ServiceResult<ThreadResponseDto>> AddThread(int askerId, CreateThreadDto createThreadDto)
    {
        var transaction = await _unitOfWork.BeginTransactionAsync();

        try
        {
            // check if the Asked user exite or not
            var askedId = createThreadDto.AskedId;
            
            var askedUser = await _unitOfWork.Users.GetByIdAsync(askedId);
            if (askedUser == null)
            {
                await transaction.RollbackAsync();
                return await ServiceResult<ThreadResponseDto>.Failure(new List<string>(){"Could not find asked user"});
            }
            
            // getting the Asker user object 
            var askerUser = await _unitOfWork.Users.GetByIdAsync(askerId);
            if (askerUser == null)
            {
                await transaction.RollbackAsync();
                return await ServiceResult<ThreadResponseDto>.Failure(new List<string>(){"Could not find asker user"});
            }
            

            
            // check if the Asked User's id == Asker User's Id
            if (askedId == askerId)
            {
                await transaction.RollbackAsync();
                return await ServiceResult<ThreadResponseDto>.Failure(new List<string>(){"User can not ask him self"});
            }
            
            
            // check if the QuestionContent is empty or not
            if (string.IsNullOrEmpty(createThreadDto.QuestionContent))
            {
                await transaction.RollbackAsync();
                return await ServiceResult<ThreadResponseDto>.Failure(new List<string>(){"Question Can't be null or empty"});
            }
            
            // creating a new thread
            Thread thread = new Thread
            {
                AskedId = askedId,
                
                AskerId = askerId,

                QuestionContent = createThreadDto.QuestionContent,
                
                AnswerContent = "",
                Status = createThreadDto.Status,
                isAnonymous = createThreadDto.isAnonymous,
                CreatedAt = DateTime.Now,
            };
            
            await _unitOfWork.Threads.AddAsync(thread);
            askerUser.AskedThreads?.Add(thread);
            askedUser.ReceivedThreads?.Add(thread);
            await _unitOfWork.Users.UpdateAsync(askedUser);
            await _unitOfWork.Users.UpdateAsync(askerUser);
            await _unitOfWork.SaveAsync();
            
            transaction.Commit();
            
            var responseDto = new ThreadResponseDto
            {
                Id = thread.Id,
                QuestionContent = thread.QuestionContent,
                Status = thread.Status,
                IsAnonymous = thread.isAnonymous,
                CreatedAt = thread.CreatedAt,
                AskerId = thread.AskerId.Value,
                AskerName = thread.Asker?.Name ?? "Unknown",
                AskedId = thread.AskedId,
                AskedName = thread.Asked?.Name ?? "Unknown"
            };
        
            return await ServiceResult<ThreadResponseDto>.Success(responseDto);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return await ServiceResult<ThreadResponseDto>.Failure(new List<string>(){e.Message});
        }
    }

    public async Task<ServiceResult<List<ThreadResponseDto>>> GetAllThreads(int userId)
    {
        var user = await _unitOfWork.Users.FindAsync(
            predicate: u => u.Id == userId,
            includes: new[] { "ReceivedThreads" }
        );
        
        
        if (user == null)
        {
            return await ServiceResult<List<ThreadResponseDto>>.Failure(new List<string>(){"Could not find user"});
        }

        var res = user.ReceivedThreads.Select(t => new ThreadResponseDto
        {
            Id = t.Id,
            QuestionContent = t.QuestionContent,
            IsAnonymous = t.isAnonymous,
            CreatedAt = t.CreatedAt,
            AskedId = t.AskedId,
            Status = t.Status,
            AskedName = t.Asked?.Name ?? "Unknown",
            AskerId = t.AskerId.Value,
            AskerName = t.Asker?.Name ?? "Unknown"
            
        }).ToList();
        
        return await ServiceResult<List<ThreadResponseDto>>.Success(res);
    }
}