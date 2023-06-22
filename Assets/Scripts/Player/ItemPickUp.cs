using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(item.ItemCode);

            //测试输出
            // Debug.Log("物品代码是: " + item.ItemCode + ", 物品名称是: " + itemDetails._itemDescription);

            if (itemDetails._isCanBePickedUp)
            {
                InventoryManager.Instance.AddItem(InventoryLocation.Player, item);
                other.gameObject.SetActive(false);
                Destroy(other.gameObject, 0.5f);
            }
        }
    }
}
