namespace Backend.Ratings.Domain.RatingReasons;

public class RatingReason
{
    public long Id { get; private set; }
    public string Code { get; private set; } = null!;
    public int Value { get; private set; }
    public bool IsActive { get; private set; }

    public RatingReason() { } // EF

    public RatingReason(string code, int value, bool isActive = true)
    {
        Code = code.Trim().ToLowerInvariant();
        Value = value;
        IsActive = isActive;
    }

    public void UpdateCode(string requestCode)
    {
        Code = requestCode.ToLowerInvariant().Trim();
    }

    public void UpdateValue(int value)
    {
        Value = value;
    }

    public void UpdateIsActive(bool isActive)
    {
        IsActive = isActive;
    }
}