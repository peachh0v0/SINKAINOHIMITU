
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

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (itemData == null)
            {
                Debug.LogWarning("ItemDataÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒÅI");
                return;
            }

            if (itemData.isNote)
            {
                NoteUI.Instance?.ShowNote(itemData);
            }
            else
            {
                Inventory.Instance?.AddItem(itemData);
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = false;
    }
}
