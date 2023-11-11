using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TheWall.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace TheWall.Controllers;

public class PostController : Controller
{
    private readonly ILogger<PostController> _logger;
    private MyContext _context;

    public PostController(ILogger<PostController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }
    /*Metodo Get*/
    [HttpGet("messages")]
    [SessionCheck]
    public IActionResult AllPost()

    {
        List<Message> AllPosts = _context.Messages
        .Include(m => m.Comments) // Incluye los comentarios asociados a cada mensaje
            .ThenInclude(c => c.User) // Incluye el usuario asociado a cada comentario
        .Include(m => m.Creator) // Incluye el usuario creador asociado a cada mensaje
        .ToList();

        return View("AllPost", AllPosts);
    }

    /*Metodo Post*/
    [HttpPost]
    [Route("message/add")]
    public IActionResult AddMessage(Message newMessage)
    {
        if (ModelState.IsValid)
        {
            _context.Messages.Add(newMessage);

            _context.SaveChanges();
            return RedirectToAction("AllPost");
        }
        return View("AllPost");
    }

    [HttpPost]
    [Route("comment/add")]
    public IActionResult AddComment(Comment newComment)
    {
        if (ModelState.IsValid)
        {
            _context.Comments.Add(newComment);

            _context.SaveChanges();
            return RedirectToAction("AllPost");
        }
        return View("AllPost");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public class SessionCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? email = context.HttpContext.Session.GetString("UserEmail");
            if (email == null)
            {
                context.Result = new RedirectToActionResult("Index", "Authentication", null);
            }
        }
    }
}
