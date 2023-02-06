namespace ReversiApi.Models;

public class SpelGegevens
{
    public int ID { get; set; }
    public string spelToken { get; set; }
    public DateTime datum  { get; set; }
    public string waarde  { get; set; }
    public string type  { get; set; }
    public string spelerToken  { get; set; }
}