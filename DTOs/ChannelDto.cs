namespace TECHTALKFORUM.DTOs
{
    public class ChannelDto
    {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string? Description { get; set; }

    }
}
public class ChannelCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}


