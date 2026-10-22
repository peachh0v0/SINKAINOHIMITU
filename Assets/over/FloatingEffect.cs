using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    [Header("上下のふわふわ")]
    [SerializeField] private float floatAmount = 0.15f;
    [SerializeField] private float floatSpeed = 1.0f;

    [Header("左右のゆらゆら")]
    [SerializeField] private float swayAmount = 0.08f;
    [SerializeField] private float swaySpeed = 0.7f;

    [Header("回転")]
    [SerializeField] private float rotateAmount = 2.0f;
    [SerializeField] private float rotateSpeed = 0.5f;

    private Vector3 startPosition;
    private float randomOffset;

    void Start()
    {
        startPosition = transform.localPosition;

        // オブジェクトごとに動きを少し変える
        randomOffset = Random.Range(0f, 10f);
    }

    void Update()
    {
        float time = Time.time + randomOffset;

        // 上下にふわふわ
        float y = Mathf.Sin(time * floatSpeed) * floatAmount;

        // 左右にゆらゆら
        float x = Mathf.Sin(time * swaySpeed) * swayAmount;

        transform.localPosition = startPosition + new Vector3(x, y, 0);

        // 少しだけ傾く
        float rotation = Mathf.Sin(time * rotateSpeed) * rotateAmount;
        transform.localRotation = Quaternion.Euler(0, 0, rotation);
    }
}