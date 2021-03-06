﻿using System;
using System.Linq;
using Property;
using Util;

namespace Tree {
    public class TreeItemWrapper : ISobjWrapper<TreeItemSobj>, IUnlockable {
        public TreeItemWrapper(TreeItemSobj sobj) {
            Sobj = sobj;
            Sobj.wrapper = this;
        }

        public TreeItemSobj Sobj { get; }


        /// <returns> True for can unlock, false otherwise. </returns>
        /// <remarks> Set manually. CAN be null. </remarks>
        public Func<TreeItemWrapper, bool> unlockPredicate;

        public Action<TreeItemWrapper> onUnlocked;

        public bool Unlocked { get; private set; }

        public bool CanUnlock() {
            if(Unlocked)
                return false;

            // trigger event instead (?)
            if(!PropertyManager.Instance.CanSubtractProperty(Sobj.unlockCost))
                return false;

            if(Sobj.prevTreeItems.Any(item => !item.wrapper.Unlocked))
                return false;

            if(!(unlockPredicate?.Invoke(this) ?? true))
                return false;

            return true;
        }

        public bool TryUnlock() {
            if(!CanUnlock())
                return false;
            PropertyManager.Instance.SubtractProperty(Sobj.unlockCost);
            Unlocked = true;
            onUnlocked?.Invoke(this);
            return true;
        }

        /// <summary> Call this when rebuilding the tree </summary>
        public void ForceUnlock() {
            Unlocked = true;
            onUnlocked?.Invoke(this);
        }

        public void Lock() {
            Unlocked = false;
        }
    }
}
