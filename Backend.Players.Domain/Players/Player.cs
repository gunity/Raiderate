namespace Backend.Players.Domain.Players;

public class Player
{
    public Guid Id { get; private set; }
    public string Nickname { get; private set; } = null!;
    public int Rating { get; private set; }
    public int VotesCount { get; private set; }

    public Player() { } // EF

    public Player(string nickname)
    {
        Id = Guid.NewGuid();
        Nickname = nickname.Trim().ToLowerInvariant();
        Rating = 0;
        VotesCount = 0;
    }

    public void ApplyVote(int delta)
    {
        Rating += delta;
        VotesCount++;
    }
}