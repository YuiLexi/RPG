using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInevntorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Camera _mainCamera;
    private Transform _parentTransform;
    private GameObject _draggedItem;
    [SerializeField] private UIInventoryBar _inventoryBar = null;
    [SerializeField] private GameObject _itemPrefab = null;
    [SerializeField] private int _slotIndex = -1;

    public Image _itemInventoryHighlight;
    public Image _itemImage;
    public TextMeshProUGUI _itemQuantityText;

    [HideInInspector] public ItemDetails _itemDetails;
    [HideInInspector] public int _itemQuantity;

    private void Start()
    {
        _mainCamera = Camera.main;
        _parentTransform = GameObject.FindGameObjectWithTag(Tags.ItemParentTransform).transform;
    }
    private void DropSelectedItemAtMousePosition()
    {
        if (_itemDetails != null)
        {
            Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -_mainCamera.transform.position.z));
            GameObject itemGameObject = Instantiate(_itemPrefab, worldPosition, Quaternion.identity, _parentTransform);
            Item item = itemGameObject.GetComponent<Item>();
            item.ItemCode = _itemDetails._itemIDCode;

            InventoryManager.Instance.RemoveItem(InventoryLocation.Player, item.ItemCode);
        }
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        if (_itemDetails != null)
        {
            Player.Instance.DisablePlayerInputAndResetMovement();

            _draggedItem = Instantiate(_inventoryBar._inventoryBarDraggedItem, _inventoryBar.transform);
            Image draggedItemImage = _draggedItem.GetComponentInChildren<Image>();
            draggedItemImage.sprite = _itemImage.sprite;
        }
    }
    public void OnDrag(PointerEventData pointerEventData)
    {
        if (_draggedItem != null)
        {
            _draggedItem.transform.position = Input.mousePosition;
        }
    }
    public void OnEndDrag(PointerEventData pointerEventData)
    {
        if (_draggedItem != null)
        {
            Destroy(_draggedItem);
            _draggedItem = null;
            GameObject objectUnderMouse = pointerEventData.pointerCurrentRaycast.gameObject;
            if (objectUnderMouse?.GetComponent<UIInevntorySlot>() != null)
            {
                int toSlotIndex = objectUnderMouse.GetComponent<UIInevntorySlot>()._slotIndex;
                InventoryManager.Instance.SwapInventoryItems(InventoryLocation.Player, _slotIndex, toSlotIndex);
            }
            else
            {
                if (_itemDetails._isCanBeDropped)
                {
                    // DropSelectedItemAtMousePosition();
                }
            }

            Player.Instance.EnablePlayerInput();
        }
    }
}
