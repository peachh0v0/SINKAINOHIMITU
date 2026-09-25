using UnityEngine;

public class GirlController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.5f;

    private Vector3[] path;
    private int targetIndex;
    private bool isWalking;


    // 女の子を歩かせる
    public void StartWalking(Vector3[] newPath)
    {
        path = newPath;

        if (path == null || path.Length < 2)
        {
            isWalking = false;
            return;
        }

        // 最初の光の線上で一番近い場所を探す
        Vector3 closestPoint = ClosestPointOnLine(
            transform.position,
            path[0],
            path[1]
        );

        float distance = Vector3.Distance(
            transform.position,
            closestPoint
        );

        // 光の線の近くにいるときだけ歩く
        if (distance < 1.0f)
        {
            transform.position = closestPoint;

            targetIndex = 1;
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }


    // 毎フレーム移動
    void Update()
    {
        if (!isWalking || path == null)
            return;

        if (targetIndex >= path.Length)
        {
            isWalking = false;
            return;
        }

        Vector3 target = path[targetIndex];

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            moveSpeed * Time.deltaTime
        );

        // 目的地に到着
        if (Vector3.Distance(
            transform.position,
            target
        ) < 0.01f)
        {
            transform.position = target;

            targetIndex++;

            if (targetIndex >= path.Length)
            {
                isWalking = false;
            }
        }
    }


    // 女の子を停止する
    public void StopWalking()
    {
        isWalking = false;
        path = null;
    }


    // 線の上で一番近い場所を取得
    private Vector3 ClosestPointOnLine(
        Vector3 point,
        Vector3 lineStart,
        Vector3 lineEnd
    )
    {
        Vector3 line = lineEnd - lineStart;

        float length = line.sqrMagnitude;

        if (length == 0)
            return lineStart;

        float t = Vector3.Dot(
            point - lineStart,
            line
        ) / length;

        t = Mathf.Clamp01(t);

        return lineStart + line * t;
    }
}