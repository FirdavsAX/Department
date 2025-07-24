namespace Application.DTOs.MemberDto;

public class ReadMemberDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public string Info { get; set; } = string.Empty;
    public string PhotoUrl { get; set; } = string.Empty;
}
