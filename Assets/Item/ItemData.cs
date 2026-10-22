
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
public class ItemData : ScriptableObject
{
    [Header("基本情報")]
    public string itemName;

    [TextArea(3, 10)]
    public string description;

    public Sprite icon;

    [Header("アイテムの種類")]
    public bool isNote;

    [Header("手紙の内容")]
    [TextArea(5, 15)]
    public string noteText;

    [Header("使用設定")]
    public bool consumeOnUse = true;
}
