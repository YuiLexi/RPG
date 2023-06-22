using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryBar : MonoBehaviour
{
    [SerializeField] private Sprite _blank16X16Sprite = null;
    [SerializeField] private UIInevntorySlot[] _inventorySlots = null;
    private RectTransform _rectTransform; // 用于调整库存栏位置
    private bool _isInventoryBarPositionBotton = true;  // 库存栏是否在屏幕底部
    public GameObject _inventoryBarDraggedItem;
    public Vector2 _pivot = new Vector2(0.5f, 0);
    public Vector2 _anchorMin = new Vector2(0.5f, 0);
    public Vector2 _anchorMax = new Vector2(0.5f, 0);
    public Vector2 _anchoredPosition = new Vector2(0, 2.5f);
    public bool IsInventoryBarPositionBotton
    { get => _isInventoryBarPositionBotton; set => _isInventoryBarPositionBotton = value; }
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.pivot = _pivot;
        _rectTransform.anchorMin = _anchorMin;
        _rectTransform.anchorMax = _anchorMax;
        _rectTransform.anchoredPosition = _anchoredPosition;
    }
    private void OnEnable()
    {
        EventHandler.InventoryUpdatedEvent += InventoryUpdated;
    }
    private void OnDisable()
    {
        EventHandler.InventoryUpdatedEvent -= InventoryUpdated;
    }
    private void Update()
    {
        SwitchInventoryBarPosition();
    }

    private void SwitchInventoryBarPosition()
    {
        Vector3 playerPosition = Player.Instance.GetPlayerViempointPosition();
        if (playerPosition.y > 0.3f && !IsInventoryBarPositionBotton)
        {
            _rectTransform.pivot = new Vector2(0.5f, 0);
            _rectTransform.anchorMin = new Vector2(0.5f, 0);
            _rectTransform.anchorMax = new Vector2(0.5f, 0);
            _rectTransform.anchoredPosition = new Vector2(0, 2.5f);
            IsInventoryBarPositionBotton = true;
        }
        else if (playerPosition.y < 0.3f && IsInventoryBarPositionBotton)
        {
            _rectTransform.pivot = new Vector2(0.5f, 1.0f);
            _rectTransform.anchorMin = new Vector2(0.5f, 1.0f);
            _rectTransform.anchorMax = new Vector2(0.5f, 1.0f);
            _rectTransform.anchoredPosition = new Vector2(0, -2.5f);
            IsInventoryBarPositionBotton = false;
        }
    }

    private void ClearInventorySlot()
    {
        if (_inventorySlots.Length > 0)
        {
            for (int i = 0; i < _inventorySlots.Length; i++)
            {
                _inventorySlots[i]._itemDetails = null;
                _inventorySlots[i]._itemQuantity = 0;
                _inventorySlots[i]._itemImage.sprite = _blank16X16Sprite;
                _inventorySlots[i]._itemQuantityText.text = "";
            }
        }
    }
    private void InventoryUpdated(InventoryLocation inventoryLocation, List<InventoryItem> inventoryList)
    {
        if (inventoryLocation == InventoryLocation.Player)
        {
            ClearInventorySlot();
            if (_inventorySlots.Length > 0 && inventoryList.Count > 0)
            {
                for (int i = 0; i < _inventorySlots.Length; i++)
                {
                    if (i < inventoryList.Count)
                    {
                        int itemCode = inventoryList[i]._itemCode;
                        ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(itemCode);
                        if (itemDetails != null)
                        {
                            _inventorySlots[i]._itemDetails = itemDetails;
                            _inventorySlots[i]._itemQuantity = inventoryList[i]._itemQuantity;
                            _inventorySlots[i]._itemImage.sprite = itemDetails._itemSprite;
                            _inventorySlots[i]._itemQuantityText.text = inventoryList[i]._itemQuantity.ToString();
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}