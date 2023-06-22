using UnityEngine;

[System.Serializable]
public class ItemDetails
{
    public int _itemIDCode; // This is the item code that will be used in the game
    public ItemType _itemType; // This is the type of item
    public string _itemDescription; // This is the description of the item
    public string _itmeLongDescription;     // This is the long description of the item
    public Sprite _itemSprite; // This is the sprite of the item
    public short _itemUseGridRadius;
    public float _itemUseRadius;
    public bool _isStartingItem;
    public bool _isCanBePickedUp;
    public bool _isCanBeDropped;
    public bool _isCanBeEaten;
    public bool _isCanBeCarried;

}
