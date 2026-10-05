using UnityEngine;

public class JellyfishFloat : MonoBehaviour
{
    Vector3 startPos;
    float time;

    public float height = 15f;
    public float speed = 1f;
    public float rotateAmount = 2f;

    void Start()
    {
        startPos = transform.position;

        // クラゲごとに開始タイミングを変える
        time = Random.Range(0f, 10f);

        // クラゲごとに速度も少し変える
        speed = Random.Range(0.8f, 1.2f);
    }

    void Update()
    {
        time += Time.deltaTime * speed;

        // 上下にふわふわ
        float y = Mathf.Sin(time) * height;

        // 少しだけ左右に揺れる
        float x = Mathf.Sin(time * 0.6f) * 3f;

        // ほんの少し傾く
        float rotation = Mathf.Sin(time * 0.8f) * rotateAmount;

        transform.position = startPos + new Vector3(x, y, 0);
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}