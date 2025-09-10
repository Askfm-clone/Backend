using AskFm.BLL.DTO;
using AskFm.BLL.Services;
using AskFm.BLL.Services.UserIdentityService;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AskFm.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class ThreadController : ControllerBase
{
    private readonly ILogger<CommentController> _logger;
    private readonly IUserService _userService;
    private readonly IThreadService _threadService;

    public ThreadController(
        ILogger<CommentController> logger,
        IUserService userService,
        IThreadService threadService)
    {
        _logger = logger;
        _userService = userService;
        _threadService = threadService;
    }
    
    
    // POST api/threads/ - Ask a question
    [HttpPost]
    [Route("thread")]
    public async Task<IActionResult>  AskQuestion(CreateThreadDto createThreadDto)
    {
        // Asker Id -> Current User
        var user = await _userService.GetCurrentUserAsync();
        if (!user.success)
        {
            return BadRequest(user.Errors);
        }
        var userId = user.Data.Id;
        
        var result = await _threadService.AddThread(userId, createThreadDto);

        if (!result.success)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Data);

    }


    [HttpGet]
    [Route("thread/{id}")]
    public async Task<IActionResult> GetAllThreads([FromRoute] int id)
    {
        var threads = await _threadService.GetAllThreads(id);
        if (!threads.success)
        {
            return BadRequest(threads.Errors);
        }
        return  Ok(threads.Data);
    }
    
    // GET api/threads/{id} - Getting the Thread with id = {id}

    [HttpGet]
    [Route("thread/{id}")]
    void GetThreadWithId([FromRoute] string id)
    {
        
    }
    
    // PUT api/threads/{id}/answer - Add an Answer on the thread with id = {id}
    [HttpPut]
    [Route("threads/{id}/answer")]
    void AnswerQuestion([FromRoute] string id)
    {
        
    }

    // POST api/threads/{id}/likes - Add a Like to the thread with id = {id}
    // GET api/threads/{id}/likes - Get all the Likes to the thread with id = {id}
    // DELETE api/threads/{id}/likes - Unlike to the thread with id = {id}


    // POST api/threads/{id}/comments - Add a comment to the thread with id = {id}
    // GET api/threads/{id}/comments - Get all the comments to the thread with id = {id}
    // DELETE api/threads/{id}/comments - Remove the comment to the thread with id = {id}

}