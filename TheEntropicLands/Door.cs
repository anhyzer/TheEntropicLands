using System;

namespace TheEntropicLands
{
    public class Door : ICloseable
    {
        private readonly Room _roomA;
        private readonly Room _roomB;
        private bool _canClose;
        private ILockable _lock;
        private bool _open;
        public bool CanClose { get { return _canClose; } set { _canClose = value; } }
        public bool IsOpen { get { return _open; } }
        public bool IsClosed { get { return !_open; } }
        public bool CanOperate { get { return _lock == null?true : _lock.CanOperate; } }
        public bool IsLocked { get { return _lock == null?false:_lock.IsLocked; } }
        public bool IsUnlocked { get { return _lock == null?true:_lock.IsUnlocked; } }
        public ILockable InstalledLock { get => _lock; set => _lock = value; }

        public string KeyName => throw new NotImplementedException();

        public Door(Room roomA, Room roomB) 
        { 
            _roomA = roomA;
            _roomB = roomB;
            _lock = null;
            _open = true;
            _canClose = false;
        }
        public Room GetRoomOnTheOtherSide(Room startingRoom)
        {
            if (startingRoom == _roomA)
            {
                return _roomB;
            } else if(startingRoom == _roomB){
                return _roomA;
            }
            return null;
        }
        public static Door CreateDoor(Room room1, Room room2, string label1) 
        { 
            Door door = new Door(room1, room2);
            room1.SetExit(label1, door);

            return door;
        }
        public static Door CreateDoor(Room room1, Room room2, string label1, string label2, bool closeable) 
        { 
            Door door = new Door(room1, room2);
            room1.SetExit(label1, door);
            room2.SetExit(label2, door);
            door.CanClose = closeable;

            return door;
        }
        public ILockable InstallLock(ILockable theLock) 
        {
            ILockable oldLock = _lock;
            InstalledLock = theLock;

            return oldLock;
        }
        public void Open()
        {
            if (IsUnlocked)
            {
                _open = true;
            }
        }
        public void Close()
        {
            if (IsOpen && CanOperate)
            {
                _open = false;
            }
        }
        public void Lock()
        {
            if (_lock != null)
            {
                InstalledLock.Lock();
            }        
        }
        public bool HasLock() 
        {
            return InstalledLock != null;
        }
        public void Unlock()
        {
            if(_lock != null) 
            {
                InstalledLock.Unlock();
            }
        }

        public IItem RemoveKey()
        {
            throw new NotImplementedException();
        }

        public IItem InsertKey(IItem key)
        {
            throw new NotImplementedException();
        }
    }
}
