using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public class PlayerState : ICharacterState
    {
        private string _name;

        private GameClock _timer;
        public virtual string Name { get => _name; set => _name = value; }
        public virtual GameClock Timer { get => _timer; set => _timer = value; }
        public virtual bool IsFighting { get => false; }
        public virtual bool IsHungry{ get => false; }
        public virtual bool IsThirsty{ get => false; }
        public virtual bool IsAlive{ get => true; }
        public PlayerState() : base() 
        {
        }
        public virtual void SetTimer(int time) 
        { 
            _timer = new GameClock(time);
        }
    }
    public class FightingState : PlayerState 
    {
        private string _name = "fighting";

        private ICharacter _target;
        public override string Name { get => _name; }
        public override bool IsFighting { get => true; }
        public ICharacter Target { get => _target; set => _target = value; }

        public FightingState(ICharacter target) 
        { 
            Target = target;
        }
    }
    public class HungryState : PlayerState
    {
        private string _name = "hungry";

        private GameClock _hungerTimer = new GameClock(15000);
        public override string Name { get => _name; }
        public override GameClock Timer { get => _hungerTimer; }
        public override bool IsHungry { get => true; }
    }
    public class AliveState : PlayerState
    {
        private string _name = "alive";
        public override string Name { get => _name; }
        public override bool IsAlive{ get => true; }
    }
}
