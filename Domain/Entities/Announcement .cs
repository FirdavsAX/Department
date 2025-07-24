namespace Domain.Entities;
public class Announcement
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string ImgUrl { get; set; }
    public DateTime Date { get; set; }
}
