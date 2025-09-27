using AskFm.BLL.Services;
using AskFm.BLL.Services.UserIdentityService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AskFm.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class ThreadLikeController
{
    private readonly ILogger<CommentController> _logger;
    private readonly IUserService _userService;
    private readonly IThreadService _threadService;

    public ThreadLikeController(
        ILogger<CommentController> logger,
        IUserService userService,
        IThreadService threadService)
    {
        _logger = logger;
        _userService = userService;
        _threadService = threadService;
    }
    
    
    // POST api/threads/{id}/likes - Add a Like to the thread with id = {id}
    // GET api/threads/{id}/likes - Get all the Likes to the thread with id = {id}
    // DELETE api/threads/{id}/likes - Unlike to the thread with id = {id}
    
    
    
}