namespace ReversiApi.Responses;

public class DoneResponse
{
    public string Speler1 { get; set; }
    public string Speler2 { get; set; }
    public bool Speler1Gewonnen { get; set; } = false;
    public bool Speler2Gewonnen { get; set; } = false;
    public bool GelijkSpel { get; set; } = false;
}