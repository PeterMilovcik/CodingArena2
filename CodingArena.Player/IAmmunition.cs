namespace CodingArena.Player
{
    public interface IAmmunition
    {
        double Speed { get; }
        double Damage { get; }
        int Remaining { get; }
    }
}