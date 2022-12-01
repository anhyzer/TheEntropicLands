using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public interface IRoomDelegate //Delegation Pattern
    {
        Room ContainingRoom { set; get; }
        NPC GetNPC(string npcName);
        Door GetExit(string exitName);
        Item GetItem(string itemName);
        string GetNPCS();
        string GetItems();
        string GetExits();
        string Description();
        Dictionary<string, Door> ContainingRoomExits { set; }
        Dictionary<string, NPC> ContainingRoomNPCS { set;  }
        Dictionary<string, Item> ContainingRoomItems { set; }
    }
    public interface ILockable
    {
        bool IsLocked { get; }
        bool IsUnlocked { get; }
        bool CanOperate { get; }
        string KeyName { get; }
        void Lock();
        void Unlock();
        IItem RemoveKey();
        IItem InsertKey(IItem key);
    }
    public interface ICloseable : ILockable
    {
        bool IsOpen { get; }
        bool IsClosed { get; }
        void Open();
        void Close();
    }
    public interface ICharacterState //State Pattern
    {
        string Name { get; }
        GameClock Timer { get; }        
    }
    public interface ICharacter
    {
        string Name { get; }
        string Vuln { get; }
        int MaxHealth { set; get; }
        int HealthPoints { set; get; }
        int ManaPoints { set; get; }
        int MovePoints { set; get; }
        int Currency { set; get; }
        int Strength { set; get; }
        int Intelligence { set; get; }
        int Wisdom { set; get; }
        int Constitution { set; get; }
        int Dexterity { set; get; }
        int Level { set; get; }
        int HitRoll { get; }
        Room CurrentRoom { get; }
        string DamageType { get; set; }
        void DropAll();
        void Drop(string itemName);
        void Dies();
        void Look();
        IItem TakeAway(string itemName);
        void Give(IItem item);
        List<ICharacterState> States { get; }
        void RemoveState(string stateName);
        void Attack(string enemyName);
        void OutputMessage(string message);
        void SuccessMessage(string message);
        void WarningMessage(string message);
        void VictoryMessage(string message);
    }
    public interface IItem //Prototype Pattern
    {
        //int VNum { get; }
        string Type { get; }
        int HitRoll { get; }
        float Weight { get; }
        bool Wearable { get; }
        string Name { get; }
        string Description { get; }
        string DamageType { get; }
        float Value { get; }
        void AddDecorator(IItem decorator);
        //string ShortName { get; }
        string LongName { get; }
        bool IsContainer { get; }
        void Add(IItem itemName);
        IItem Remove(string itemName);
        //string ShortDescription { get; }
        //string LongDescription { get; }
        //string Price { get; }
    }
}
