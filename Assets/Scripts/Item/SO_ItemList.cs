using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "so_ItemList", menuName = "ScriptableObjects/ItemList", order = 1)]
public class SO_ItemList : ScriptableObject
{
    [SerializeField]
    public List<ItemDetails> _itemDetailsList;
}
