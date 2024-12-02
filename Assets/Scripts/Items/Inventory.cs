using System;

namespace Items
{
    [Serializable]
    public struct Inventory
    {
        [Flags]
        public enum Item
        {
            A = 1 << 0,
            B = 1 << 1,
            C = 1 << 2,
            D = 1 << 3,
            E = 1 << 4,
            F = 1 << 5
        }

        public Inventory(int holder = 0)
        {
            this.holder = holder;
        }
        

        public int holder;
        
        public void AddItem(Item item)
        {
            holder |= (int)item;
        }
        
        public void RemoveItem(Item item)
        {
            holder &= ~(int)item;
        }
        
        public bool HasItem(Item item)
        {
            return (holder & (int)item) != 0;
        }
        
        public void Clear()
        {
            holder = 0;
        }
    }
}