using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ItemCodeDescriptionAttribute))]
public class ItemCodeDescriptionDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property) * 2.0f;
        //return base.GetPropertyHeight(property, label);
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        if (property.propertyType == SerializedPropertyType.Integer)
        {
            EditorGUI.BeginChangeCheck();
            var newValue = EditorGUI.IntField(new Rect(position.x, position.y, position.width, position.height / 2), label, property.intValue);
            EditorGUI.LabelField(new Rect(position.x, position.y + position.height / 2, position.width, position.height / 2),
              "Item Description", GetItemDescription(property.intValue));
            if (EditorGUI.EndChangeCheck())
            {
            }
        }
        EditorGUI.EndProperty();
    }
    private GUIStyle GetItemDescription(int itemCode)
    {
        //Assets/ScriptaleObject/Item/so_ItemList.asset
        SO_ItemList itemList = AssetDatabase.LoadAssetAtPath("Assets/ScriptaleObject/Item/so_ItemList.asset", typeof(SO_ItemList)) as SO_ItemList;
        List<ItemDetails> itemDetailsList = itemList._itemDetailsList;
        ItemDetails itemDetails = itemDetailsList.Find((x) => x._itemIDCode == itemCode);
        if (itemDetails != null)
        {
            return itemDetails._itemDescription;
        }
        else
            return "null";
    }
}
