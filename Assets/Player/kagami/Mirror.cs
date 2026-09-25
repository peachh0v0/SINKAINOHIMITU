using UnityEngine;

public class Mirror : MonoBehaviour
{
    // 鏡で光を反射させる
    public Vector2 Reflect(Vector2 incomingDirection)
    {
        // 鏡の右方向を法線として使う
        Vector2 normal = transform.right;

        // 反射方向を計算
        Vector2 reflectedDirection =
            Vector2.Reflect(incomingDirection, normal);

        return reflectedDirection.normalized;
    }
}