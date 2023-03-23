using System;

namespace Script.Model.Interfaces
{
    public interface IUpdate
    {
        Action<IUpdate> Remove { get; set; }
        void Update(float timeDelta);
    }
}