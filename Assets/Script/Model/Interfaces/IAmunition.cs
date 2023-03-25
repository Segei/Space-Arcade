namespace Script.Model.Interfaces
{
    public interface IAmunition : IWeapon
    {
        int CurrentCountBullet { get; set; }
        float TimeReloadNextBullet { get; set; }

    }
}
