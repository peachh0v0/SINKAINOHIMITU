using UnityEngine;

public class GirlController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.5f;

    private Vector3[] path;
    private int targetIndex;
    private bool isWalking;

    public void StartWalking(Vector3[] newPath)
    {
        path = newPath;

        if (path == null || path.Length < 2)
        {
            isWalking = false;
            return;
        }

        // ¡‚¢‚éêŠ‚©‚çˆê”Ô‹ß‚¢êŠ‚ð’T‚·
        Vector3 closestPoint = transform.position;
        float closestDistance = float.MaxValue;
        int closestSegment = 0;

        for (int i = 0; i < path.Length - 1; i++)
        {
            Vector3 point = ClosestPointOnLine(
                transform.position,
                path[i],
                path[i + 1]
            );

            float distance =
                Vector3.Distance(
                    transform.position,
                    point
                );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPoint = point;
                closestSegment = i;
            }
        }

        // Œõ‚Ìü‚ª­—‚Ì‹ß‚­‚É—ˆ‚½‚ç•à‚­
        if (closestDistance <= 1.5f)
        {
            transform.position = closestPoint;

            targetIndex = closestSegment + 1;
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

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

    public void StopWalking()
    {
        isWalking = false;
        path = null;
    }

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