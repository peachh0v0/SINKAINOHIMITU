
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    private bool playerNearby;

    private void Update()
    {
        if (!playerNearby || Keyboard.current == null)
            return;

        // Eキーでアイテムを拾う
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            // アイテムデータが設定されているか確認
            if (itemData == null)
            {
                Debug.LogWarning("ItemDataが設定されていません！");
                return;
            }

            // インベントリが存在するか確認
            if (Inventory.Instance == null)
            {
                Debug.LogWarning("Inventoryが見つかりません！");
                return;
            }

            // インベントリに追加できたときだけ、アイテムを消す
            if (Inventory.Instance.AddItem(itemData))
            {
                // 拾ったメッセージを表示
                Debug.Log(itemData.itemName + "を拾った！");

                // 画面上のアイテムを削除
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
