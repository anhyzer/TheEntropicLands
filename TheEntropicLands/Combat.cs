using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public class Combat
    {
        private ICharacter _attacker;
        private ICharacter _defender;
        private bool _fighting;
        private FightingClock _fightClock;
        public ICharacter Attacker { get => _attacker; set => _attacker = value; }
        public ICharacter Defender { get => _defender; set => _defender = value; }
        public FightingClock Timer { get => _fightClock; set => _fightClock = value; }
        public Combat(ICharacter character1, ICharacter character2)
        {
            Attacker = character1;
            Defender = character2;
            _fighting = true;
            Timer = new FightingClock(3000);
            NotificationCenter.Instance.AddObserver("FightingClockTick", FightingClockTick);
        }
        public void FightingClockTick(Notification notification)
        {
            while (_fighting)
            {
                Attacks(Attacker, Defender);
            }
        }
        public void Attacks(ICharacter attacker, ICharacter defender)
        {

            if (attacker.DamageType == defender.Vuln)
            {
                int damageDone = (attacker.HitRoll * (attacker.Strength / 7));
                defender.HealthPoints -= damageDone;
                if (defender.HealthPoints <= 0)
                {
                    try
                    {
                        IItem item = defender.TakeAway("skeleton key");
                        if (item != null)
                        {
                            attacker.Give(item);
                            attacker.SuccessMessage("You've received a key!");
                        }
                    }
                    catch { Exception e; } //BAD. TODO: Fix the DropAll()
                    try 
                    {
                        IItem item = defender.TakeAway("diamond");
                        if (item != null)
                        {
                            attacker.Give(item);
                            attacker.SuccessMessage("You've received a diamond!");
                        }
                    }
                    catch { Exception e; } //BAD. TODO: Fix the DropAll() issue 
                    attacker.VictoryMessage(defender.Name + " has been defeated!");
                    End();
                    return;
                }
                else if (defender.HealthPoints > 0)
                {
                    attacker.OutputMessage("Your slash does " + damageDone + " damage to " + defender.Name + ".");
                    defender.OutputMessage(attacker.Name + "'s slash does " + damageDone + " to you.\n");
                }
            }
            else
            {
                int damageDone = attacker.HitRoll;
                defender.HealthPoints -= damageDone;
                if (defender.HealthPoints <= 0)
                {
                    try
                    {
                        IItem item = defender.TakeAway("skeleton key");
                        if (item != null)
                        {
                            attacker.Give(item);
                            attacker.SuccessMessage("You've received a key!");
                        }
                    }
                    catch { Exception e; } //BAD. TODO: Fix the DropAll()
                    try
                    {
                        IItem item = defender.TakeAway("diamond");
                        if (item != null)
                        {
                            attacker.Give(item);
                            attacker.SuccessMessage("You've received a diamond!");
                        }
                    }
                    catch { Exception e; } //BAD. TODO: Fix the DropAll()

                    attacker.VictoryMessage(defender.Name + " has been defeated!");
                    End();
                    return;
                }
                else
                {
                    attacker.OutputMessage("Your slash does " + damageDone + " to " + defender.Name + ".");
                    defender.WarningMessage(attacker.Name + "'s slash does " + damageDone + " to you.\n");
                }
            }
            if (defender.DamageType == attacker.Vuln)
            {
                int damageDone = (defender.HitRoll * (defender.Strength / 7) * 2);
                attacker.HealthPoints -= damageDone;
                if (attacker.HealthPoints <= 0)
                {
                    attacker.RemoveState("fighting");
                    defender.RemoveState("fighting");
                    attacker.RemoveState("alive");
                    attacker.OutputMessage("You have been defeated!");
                    defender.OutputMessage(attacker.Name + " has been defeated!");
                    attacker.Dies();
                    End();
                    return;
                }
                else
                {
                    defender.OutputMessage("Your slash does " + damageDone + " damage to " + attacker.Name + ".");
                    attacker.WarningMessage(defender.Name + "'s slash does " + damageDone + " to you.\n");
                }
            }
            else
            {
                int damageDone = defender.HitRoll;
                attacker.HealthPoints -= damageDone;
                if (attacker.HealthPoints <= 0)
                { 
                    attacker.WarningMessage("You have been defeated!");
                    defender.OutputMessage(attacker.Name + " has been defeated!");
                    attacker.Dies();
                    attacker.Look();
                    End();
                    return;
                }
                else if (attacker.HealthPoints > 0)
                {
                    defender.OutputMessage("\nYour slash does " + damageDone + " to " + attacker.Name + ".");
                    attacker.WarningMessage(defender.Name + "'s slash does " + damageDone + " to you.\n");
                }
            }
            
        }
        public void Fight() 
        {
            FightingState attackerState = new FightingState(Defender);
            FightingState defenderState = new FightingState(Attacker);

            Attacker.States.Add(attackerState);
            Attacker.States.Add(defenderState);
            _fighting = true;
        }
        public void End() 
        {
            Attacker.RemoveState("fighting");
            Attacker.RemoveState("fighting");
            _fighting = false;
        }
    }
}
