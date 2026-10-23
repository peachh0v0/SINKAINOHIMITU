
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectInspection : MonoBehaviour
{
    [TextArea(2, 5)]
    [SerializeField]
    private string message =
        "？？？？";

    private bool playerNearby;

    private void Update()
    {
        if (!playerNearby || Keyboard.current == null)
            return;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log(message);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Eキーでしらべる");
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
