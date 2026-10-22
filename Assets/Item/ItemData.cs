
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    [TextArea(3, 10)]
    public string description;

    public Sprite icon;
    public bool isNote;
    [TextArea(5, 15)]
    public string noteText;
}
