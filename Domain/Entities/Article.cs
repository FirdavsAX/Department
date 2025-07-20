namespace Domain.Entities;
public class Article
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImgUrl { get; set; }
    public string PdfUrl { get; set; }
    public DateTime UploadedAt { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
