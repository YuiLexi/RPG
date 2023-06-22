[System.Serializable]
public struct InventoryItem
{
    public int _itemCode;
    public int _itemQuantity;
    public InventoryItem(int itemCode, int itemQuantity)
    {
        this._itemCode = itemCode;
        this._itemQuantity = itemQuantity;
    }
}