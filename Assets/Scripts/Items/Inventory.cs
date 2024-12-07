using System;

namespace Items
{
    [Serializable]
    public struct Inventory
    {
        [Flags]
        public enum Item
        {
            Cake = 1 << 0,
            Muffler = 1 << 1,
            Chocolate = 1 << 2,
            Teddybear = 1 << 3,
            DogItem = 1 << 4,
            Ticket = 1 << 5
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
        
        public void AddItem(string item)
        {
            holder |= (int)Enum.Parse(typeof(Item), item);
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
            holder = 1;
        }
    }
}