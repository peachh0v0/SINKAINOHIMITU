using UnityEngine;
using UnityEngine.InputSystem;

public class LightController : MonoBehaviour
{
    [Header("光の移動速度")]
    [SerializeField] private float moveSpeed = 2f;

    void Update()
    {
        float horizontal = 0f;

        // 左右キー
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            horizontal = -1f;
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            horizontal = 1f;
        }

        // 光を左右に動かす
        transform.position += Vector3.right * horizontal * moveSpeed * Time.deltaTime;
    }
}
