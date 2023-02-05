using ReversiApi.Enums;

namespace ReversiApi.Responses;

public class DoeZetResponse
{
    public int status { get; set; }
    public string message { get; set; }
    public Kleur? kleur { get; set; }
}