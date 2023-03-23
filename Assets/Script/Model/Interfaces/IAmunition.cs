namespace Script.Model.Interfaces
{
    public interface IAmunition
    {
        int CurrentCountBullet { get; set; }
        float TimeReloadNextBullet { get; set; }

    }
}
