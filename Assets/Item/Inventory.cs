
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<ItemData> items = new List<ItemData>();

    [SerializeField] private int maxItems = 4;

    private void Awake()
    {
        Instance = this;
    }

    // アイテムを拾う
    public bool AddItem(ItemData item)
    {
        if (item == null) return false;

        if (items.Count >= maxItems)
        {
            Debug.Log("持ち物がいっぱい！");
            return false;
        }

        items.Add(item);
        Debug.Log(item.itemName + "を拾った！");
        return true;
    }

    // アイテムを使用する
    public void UseItem(int index)
    {
        if (index < 0 || index >= items.Count)
            return;

        ItemData item = items[index];

        if (item == null) return;

        // 手紙なら文章を表示する
        if (item.isNote)
        {
            if (NoteUI.Instance != null)
            {
                NoteUI.Instance.ShowNote(item);
            }
        }
        else
        {
            // 通常アイテムの使用
            Debug.Log(item.itemName + "を使用した！");
        }
    }

    // アイテムを消費する
    public void RemoveItem(ItemData item)
    {
        if (item == null) return;

        items.Remove(item);
    }

    // ステージ移動時に持ち物をリセット
    public void ResetInventory()
    {
        items.Clear();
        Debug.Log("持ち物をリセットしました！");
    }
}
