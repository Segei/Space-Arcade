using System;
using System.Collections.Generic;
using System.Timers;
using Script.Model.Interfaces;

namespace Script.Model.UpdateSystem
{
    public class StartUpdate
    {
        private Timer timer;
        private DateTime dataTimeLastCall;

        private List<IUpdate> updates = new();
        private List<IUpdate> addOnLateUpdate = new();
        public int UpdateCount => updates.Count;
        public Action OnUpdate;
        public int FramePerSecond;

        public void Start()
        {
            timer = new Timer(1f / FramePerSecond * 1000);
            timer.Elapsed += Update;
            timer.AutoReset = true;
            timer.Enabled = true;
            dataTimeLastCall = DateTime.Now;
        }

        public void Stop()
        {
            if (timer == null)
            {
                return;
            }

            timer.Stop();
            timer.Dispose();
            timer = null;
        }

        public void AddUpdateOnEnd(IUpdate update)
        {
            addOnLateUpdate.Add(update);
        }

        public void AddListener(IUpdate update)
        {
            update.OnRemove += RemoveListener;
            updates.Add(update);
        }

        private void RemoveListener(IUpdate update)
        {
            update.OnRemove -= RemoveListener;
            _ = updates.Remove(update);
        }

        private void Update(object source, ElapsedEventArgs e)
        {
            TimeSpan timeSpan = e.SignalTime - dataTimeLastCall;
            dataTimeLastCall = e.SignalTime;
            foreach (IUpdate update in updates)
            {
                update.Update((float)timeSpan.TotalSeconds);
            }

            foreach (IUpdate update in addOnLateUpdate)
            {
                AddListener(update);
            }
            addOnLateUpdate.Clear();

            OnUpdate();
        }
    }
}
