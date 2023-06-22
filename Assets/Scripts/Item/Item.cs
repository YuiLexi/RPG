using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Item : MonoBehaviour
{
    [SerializeField] private int _itemCode;

    private SpriteRenderer _spriteRenderer;
    public int ItemCode { get => _itemCode; set => _itemCode = value; }
    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        if (_itemCode != 0)
            Init(_itemCode);
    }
    public void Init(int itemCode)
    {
        if (itemCode != 0)
        {
            ItemCode = itemCode;
            ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(itemCode);
            _spriteRenderer.sprite = itemDetails._itemSprite;
            if (itemDetails._itemType == ItemType.ReapableScenary)
                gameObject.AddComponent<ItemNudge>();
        }
    }
}
