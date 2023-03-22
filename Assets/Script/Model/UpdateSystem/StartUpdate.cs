using System;
using System.Collections.Generic;
using System.Timers;

namespace Assets.Script.Model.UpdateSystem
{
    public class StartUpdate
    {
        private Timer timer;
        private DateTime dataTimeLastCall;

        private List<IUpdate> updates = new List<IUpdate>();
        public Action OnUpdate;
        public int FramePerSecond;

        public void Start()
        {
            timer = new Timer(1f / FramePerSecond * 1000);
            timer.Elapsed += Update;
            timer.AutoReset = true;
            timer.Enabled = true;
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

        public void AddListener(IUpdate update)
        {
            updates.Add(update);
            update.Remove += RemoveListener;
        }

        private void RemoveListener(IUpdate update)
        {
            updates.Remove(update);
            update.Remove -= RemoveListener;
        }

        private void Update(object source, ElapsedEventArgs e)
        {
            TimeSpan timeSpan = e.SignalTime - dataTimeLastCall;
            dataTimeLastCall = e.SignalTime;

            foreach (IUpdate update in updates)
            {
                update.Update((float)timeSpan.TotalSeconds);
            }

            OnUpdate();
        }
    }
}
