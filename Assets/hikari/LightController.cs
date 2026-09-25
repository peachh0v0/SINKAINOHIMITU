using UnityEngine;
using UnityEngine.InputSystem;

public class LightController : MonoBehaviour
{
    [Header("光源の移動速度")]
    [SerializeField] private float moveSpeed = 2f;

    void Update()
    {
        if (Keyboard.current == null)
            return;

        float horizontal = 0f;

        // 左右キーで光源を移動
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            horizontal = -1f;
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            horizontal = 1f;
        }

        transform.position +=
            Vector3.right * horizontal * moveSpeed * Time.deltaTime;
    }
}