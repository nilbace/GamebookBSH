using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Items/ItemSO", order = 0)]
    public class ItemSO : ScriptableObject
    {
        public string itemName;
        public Sprite itemSprite;
    }
}