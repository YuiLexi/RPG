using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InventoryManager : SingletonMonoBehaviour<InventoryManager>
{
    private Dictionary<int, ItemDetails> _itemDic; //物品字典{id:物品信息}

    public List<InventoryItem>[] _inventoryItemList; //库存列表
    [HideInInspector] public int[] _inventoryListCapacityInArray; //库存容量索引
    [SerializeField] private SO_ItemList _itemList = null;

    protected override void Awake()
    {
        base.Awake();
        CreatInventoryList();
        CreatItemDetailsDictionary();
    }

    /// <summary>
    /// 创建物品字典
    /// </summary>
    private void CreatItemDetailsDictionary()
    {
        _itemDic = new Dictionary<int, ItemDetails>();
        foreach (ItemDetails item in _itemList._itemDetailsList)
        {
            _itemDic.Add(item._itemIDCode, item);
        }
    }

    /// <summary>
    /// 创建库存列表
    /// </summary>
    private void CreatInventoryList()
    {
        _inventoryItemList = new List<InventoryItem>[(int)InventoryLocation.Count];
        for (int i = 0; i < (int)InventoryLocation.Count; i++)
        {
            _inventoryItemList[i] = new List<InventoryItem>();
        }
        _inventoryListCapacityInArray = new int[(int)InventoryLocation.Count];
        _inventoryListCapacityInArray[(int)InventoryLocation.Player] = Settings._playerInitInventoryCapacity;
    }
    /// <summary>
    /// 通过给定的物品ID获取物品信息
    /// </summary>
    /// <param name="itemCode">物品ID</param>
    /// <returns>物品信息</returns>
    public ItemDetails GetItemDetails(int itemCode)
    {
        if (_itemDic == null)
            CreatItemDetailsDictionary();
        if (_itemDic.TryGetValue(itemCode, out ItemDetails item))
            return item;
        else
            return null;
    }

    public void AddItem(InventoryLocation location, Item item)
    {
        int itemCode = item.ItemCode;
        List<InventoryItem> inventoryItemList = _inventoryItemList[(int)location];
        int index = FindIndexInInventory(location, itemCode);
        AddItemByIndex(inventoryItemList, itemCode, index);
        EventHandler.CallInventoryUpdatedEvent(location, inventoryItemList);
    }

    /// <summary>
    /// 在库存中查找物品
    /// </summary>
    /// <param name="location">库存的位置。0：玩家；1：箱子</param>
    /// <param name="itemCode">物品的ID</param>
    /// <returns>返回在库存中的位置。如果没有，则返回-1</returns>
    public int FindIndexInInventory(InventoryLocation location, int itemCode)
    {
        List<InventoryItem> inventoryItemList = _inventoryItemList[(int)location];
        for (int i = 0; i < inventoryItemList.Count; i++)
        {
            if (_inventoryItemList[(int)location][i]._itemCode == itemCode)
                return i;
        }
        return -1;
    }

    public void AddItemByIndex(List<InventoryItem> inventoryItemList, int itemCode, int index)
    {
        if (index == -1)
        {
            inventoryItemList.Add(new InventoryItem(itemCode, 1));
            // Debug.Log($"Add item {itemCode} to inventory");
            // DebugPrintList(inventoryItemList);
        }
        else
        {
            int quantity = inventoryItemList[index]._itemQuantity;
            inventoryItemList[index] = new InventoryItem(itemCode, quantity + 1);
            // Debug.Log($"Add item {itemCode} to inventory");
            // DebugPrintList(inventoryItemList);
        }
    }

    public void RemoveItemByIndex(List<InventoryItem> inventoryItemList, int itemCode, int index)
    {
        int quantity = inventoryItemList[index]._itemQuantity - 1;
        if (quantity > 0)
        {
            InventoryItem inventoryItem = new InventoryItem(itemCode, quantity);
            inventoryItemList[index] = inventoryItem;
        }
        else
        {
            inventoryItemList.RemoveAt(index);
        }
    }

    public void RemoveItem(InventoryLocation location, int itemCode)
    {
        List<InventoryItem> inventoryItems = _inventoryItemList[(int)location];
        int index = FindIndexInInventory(location, itemCode);
        if (index != -1)
        {
            RemoveItemByIndex(inventoryItems, itemCode, index);
        }
        EventHandler.CallInventoryUpdatedEvent(location, inventoryItems);
    }
    public void SwapInventoryItems(InventoryLocation location, int indexOne, int indexTwo)
    {
        if (indexOne < _inventoryItemList[(int)location].Count && indexTwo < _inventoryItemList[(int)location].Count
            && indexOne >= 0 && indexTwo >= 0 && indexOne != indexTwo)
        {
            InventoryItem fromItem = _inventoryItemList[(int)location][indexOne];
            InventoryItem toItem = _inventoryItemList[(int)location][indexTwo];
            _inventoryItemList[(int)location][indexOne] = toItem;
            _inventoryItemList[(int)location][indexTwo] = fromItem;

            // InventoryItem temp = _inventoryItemList[(int)location][indexOne];
            // _inventoryItemList[(int)location][indexOne] = _inventoryItemList[(int)location][indexTwo];
            // _inventoryItemList[(int)location][indexTwo] = temp;

            EventHandler.CallInventoryUpdatedEvent(location, _inventoryItemList[(int)location]);
        }
    }

    public void DebugPrintList(List<InventoryItem> inventoryItemList)
    {
        foreach (InventoryItem item in inventoryItemList)
        {
            Debug.Log($"Item code: {item._itemCode}, quantity: {item._itemQuantity}");
        }
        Debug.Log("*******************************************************************");
    }
}
