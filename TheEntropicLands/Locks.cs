using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public class DeadBoltLock : ILockable
    {
        private bool _locked;
        private IItem _insertedKey;
        private IItem _originalKey;
        private string _keyName;
        public bool IsLocked { get { return _locked; } }

        public bool IsUnlocked { get { return !_locked; } }

        public bool CanOperate { get { return IsUnlocked; } }

        public string KeyName { get => _keyName; set => _keyName = value; }

        public DeadBoltLock()
        {
            _locked = false;
            _insertedKey = null;
            _originalKey = null;
            _keyName = null;
        }
        public void Lock()
        {
            if (_insertedKey == _originalKey)
            {
                _locked = true;
            }
        }

        public void Unlock()
        {
            if (_insertedKey == _originalKey)
            {
                _locked = false;
            }
        }

        public IItem RemoveKey()
        {
            return InsertKey(null);
        }
        public IItem InsertKey(IItem key)
        {
            IItem oldKey = _insertedKey;
            _insertedKey = key;

            return oldKey;
        }
        public void SetKey(IItem key) 
        { 
            KeyName = key.Name;
        }
    }

    public class RegularLock : ILockable
    {
        private bool _locked;

        public string KeyName { get; set; }
        public bool IsLocked { get { return _locked; } }

        public bool IsUnlocked { get { return !_locked; } }

        public bool CanOperate { get { return true;  } }

        private IItem _originalKey;

        private IItem _insertedKey;

        public RegularLock()
        {
            _locked = false;
            _originalKey = null;
            _insertedKey = null;
        }
        public void Lock()
        {
            _locked = true;
        }
        public void Unlock()
        {
            if (_insertedKey == _originalKey)
            {
                _locked = false;
            }
        }
        public IItem RemoveKey()
        {            
            return InsertKey(null);
        }
        public IItem InsertKey(IItem key)
        {
            IItem oldKey = _insertedKey;
            _insertedKey = key;

            return oldKey;
        }
    }
}
