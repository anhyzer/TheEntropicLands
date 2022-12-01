using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace TheEntropicLands
{
    public class GameClock
    {
        private Timer _timer;
        private int _timeInGame;
        public Timer Timer { get => _timer; set => _timer = value; }
        public int TimeInGame { get => _timeInGame; set => _timeInGame = value; }

        public GameClock(int interval)
        {
            Timer = new Timer(interval);
            Timer.Elapsed += OnTimedEvent;
            Timer.AutoReset = true;
            Timer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //Console.WriteLine("Tick!");
            Notification notification = new Notification("GameClockTick", this);
            NotificationCenter.Instance.PostNotification(notification);
        }
    }
    public class FightingClock : GameClock
    {
        private Timer _timer;

        public new Timer Timer { get => _timer; set => _timer = value; }
        public FightingClock(int interval) : base(interval)
        {
            Timer = new Timer(interval);
            Timer.Elapsed += OnCombatRound;
            Timer.AutoReset = true;
            Timer.Enabled = true;
        }
        private void OnCombatRound(Object source, ElapsedEventArgs e)
        {
            //Console.WriteLine("Tick!");
            Notification notification = new Notification("FightingClockTick", this);
            NotificationCenter.Instance.PostNotification(notification);
        }
    }
}
