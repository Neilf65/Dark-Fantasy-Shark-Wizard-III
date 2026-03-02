using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/ItemInventory")]
public class ItemInventory : ScriptableObject
    {
        [Header("Properties")]
        public float cooldown;
        public itemType item_type;
        public Sprite item_sprite;
    }

   
    public enum itemType { Wand, Key1, Key2 };