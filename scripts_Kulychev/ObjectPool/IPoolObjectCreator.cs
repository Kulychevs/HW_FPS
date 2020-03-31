namespace Geekbrains
{
    public interface IPoolObjectCreator
    {
        Ammunition Create(AmmunitionType type);
    }
}
