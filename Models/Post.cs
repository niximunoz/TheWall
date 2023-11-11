#pragma warning disable CS8618
using TheWall.Models;

namespace TheWall.Models;
public class Post
{

    public User? Creator { get; set; }
    public Message? Message { get; set; }
    public Comment? Comment { get; set; }
}