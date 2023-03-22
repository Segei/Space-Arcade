using System;

public interface IUpdate
{
    Action<IUpdate> Remove { get; set; }
    void Update(float timeDelta);
}
