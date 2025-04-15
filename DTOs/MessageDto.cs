namespace TECHTALKFORUM.Dtos;

public class MessageDto
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public string Username { get; set; } = string.Empty;
    public string ChannelName { get; set; } = string.Empty;
}
public class MessageCreateDto
{
    public string Content { get; set; } = string.Empty;
    public int UserId { get; set; }
    public int ChannelId { get; set; }
}

public class MessageUpdateDto
{
    public string Content { get; set; } = null!;

}

