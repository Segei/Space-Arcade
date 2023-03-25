using System;

namespace Script.Model.Interfaces
{
    public interface IUpdate
    {
        Action<IUpdate> OnRemove { get; set; }
        void Update(float timeDelta);
    }
}