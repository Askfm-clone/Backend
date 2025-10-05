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
public class ThreadLikeController : ControllerBase
{
    private readonly ILogger<CommentController> _logger;
    private readonly IUserService _userService;
    private readonly IThreadLikeService _threadLikeService;

    public ThreadLikeController(
        ILogger<CommentController> logger,
        IUserService userService,
        IThreadLikeService threadService)
    {
        _logger = logger;
        _userService = userService;
        _threadLikeService = threadService;
    }
    
    
    // POST api/threads/{id}/likes - Add a Like to the thread with id = {id}
    [HttpPost]
    [Route("threads/{id}/likes")]
    public async Task<IActionResult> LikeThread([FromRoute] int id)
    {
        var user = await _userService.GetCurrentUserAsync();
        if (!user.success)
        {
            return BadRequest(user.Errors);
        }
        var res = await _threadLikeService.AddLike(id, user.Data.Id);
        if (!res.success)
        {
            return BadRequest(res.Errors);
        }
        return Ok(res.Data);
    
    }
    
    
    // GET api/threads/{id}/likes - Get all the Likes to the thread with id = {id}
    [HttpGet]
    [Route("threads/{id}/likes")]
    public async Task<IActionResult> GetLikes([FromRoute] int id)
    {
        var likes = await _threadLikeService.GetLikes(id);
        if (!likes.success)
        {
            return  BadRequest(likes.Errors);
        }
        return Ok(likes.Data);
    }
    
    // DELETE api/threads/{id}/likes - Unlike to the thread with id = {id}
    [HttpDelete]
    [Route("threads/{id}/likes")]
    public async Task<IActionResult> UnlikeThread([FromRoute] int id)
    {
        var user = await _userService.GetCurrentUserAsync();
        if (!user.success)
        {
            return BadRequest(user.Errors);
        }
        
        var res = await _threadLikeService.RemoveLike(id, user.Data.Id);
        if (!res.success)
        {
            return BadRequest(res.Errors);
        }
        
        return Ok(new { message = "Thread unliked successfully" });
    }
}