using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LightRayController : MonoBehaviour
{
    private LineRenderer line;

    private Transform sourceA;
    private Transform sourceB;


    // =========================
    // 初期設定
    // =========================
    void Awake()
    {
        line = GetComponent<LineRenderer>();

        line.startWidth = 0.08f;
        line.endWidth = 0.08f;
        line.sortingOrder = 10;

        line.enabled = false;
    }


    // =========================
    // 光を更新
    // =========================
    void Update()
    {
        if (sourceA == null || sourceB == null)
        {
            line.enabled = false;
            return;
        }

        // 光源が無効なら光も消す
        if (!sourceA.gameObject.activeInHierarchy ||
            !sourceB.gameObject.activeInHierarchy)
        {
            line.enabled = false;
            return;
        }

        line.enabled = true;


        // 鏡が光の線上にあるか確認
        Mirror mirror = FindMirrorOnPath();


        // =========================
        // 鏡なし
        // =========================
        if (mirror == null)
        {
            line.positionCount = 2;

            line.SetPosition(
                0,
                sourceA.position
            );

            line.SetPosition(
                1,
                sourceB.position
            );

            return;
        }


        // =========================
        // 鏡あり
        // =========================

        Vector2 start =
            sourceA.position;

        Vector2 direction =
            ((Vector2)sourceB.position - start)
            .normalized;

        float distance =
            Vector2.Distance(
                sourceA.position,
                sourceB.position
            );


        RaycastHit2D hit =
            Physics2D.Raycast(
                start,
                direction,
                distance
            );


        if (hit.collider != null &&
            hit.collider.GetComponent<Mirror>() != null)
        {
            Vector2 mirrorPoint =
                hit.point;


            // 反射
            Vector2 reflectedDirection =
                mirror.Reflect(direction);


            // 反射する長さ
            float reflectedDistance =
                Vector2.Distance(
                    mirrorPoint,
                    sourceB.position
                );


            Vector2 reflectedEnd =
                mirrorPoint +
                reflectedDirection *
                reflectedDistance;


            line.positionCount = 3;

            line.SetPosition(
                0,
                sourceA.position
            );

            line.SetPosition(
                1,
                mirrorPoint
            );

            line.SetPosition(
                2,
                reflectedEnd
            );
        }
    }


    // =========================
    // 2つの光源を接続
    // =========================
    public void Connect(
        Transform first,
        Transform second
    )
    {
        sourceA = first;
        sourceB = second;

        line.enabled = true;

        UpdateGirlPath();
    }


    // =========================
    // 光を消す
    // =========================
    public void ClearConnection()
    {
        sourceA = null;
        sourceB = null;

        line.positionCount = 0;
        line.enabled = false;


        // 女の子も停止
        GirlController girl =
            FindAnyObjectByType<GirlController>();

        if (girl != null)
        {
            girl.StopWalking();
        }
    }


    // =========================
    // 光の直線上に鏡があるか
    // =========================
    private Mirror FindMirrorOnPath()
    {
        if (sourceA == null ||
            sourceB == null)
        {
            return null;
        }


        Vector2 start =
            sourceA.position;

        Vector2 direction =
            ((Vector2)sourceB.position - start)
            .normalized;

        float distance =
            Vector2.Distance(
                sourceA.position,
                sourceB.position
            );


        RaycastHit2D hit =
            Physics2D.Raycast(
                start,
                direction,
                distance
            );


        if (hit.collider != null)
        {
            Mirror mirror =
                hit.collider.GetComponent<Mirror>();

            if (mirror != null)
            {
                return mirror;
            }
        }

        return null;
    }


    // =========================
    // 女の子のルートを設定
    // =========================
    private void UpdateGirlPath()
    {
        GirlController girl =
            FindAnyObjectByType<GirlController>();

        if (girl == null)
            return;


        Mirror mirror =
            FindMirrorOnPath();


        // =========================
        // 鏡なし
        // =========================
        if (mirror == null)
        {
            girl.StartWalking(
                new Vector3[]
                {
                    sourceA.position,
                    sourceB.position
                }
            );

            return;
        }


        // =========================
        // 鏡あり
        // =========================

        Vector2 start =
            sourceA.position;

        Vector2 direction =
            ((Vector2)sourceB.position - start)
            .normalized;

        float distance =
            Vector2.Distance(
                sourceA.position,
                sourceB.position
            );


        RaycastHit2D hit =
            Physics2D.Raycast(
                start,
                direction,
                distance
            );


        if (hit.collider == null)
            return;


        Vector2 mirrorPoint =
            hit.point;


        Vector2 reflectedDirection =
            mirror.Reflect(direction);


        float reflectedDistance =
            Vector2.Distance(
                mirrorPoint,
                sourceB.position
            );


        Vector2 reflectedEnd =
            mirrorPoint +
            reflectedDirection *
            reflectedDistance;


        girl.StartWalking(
            new Vector3[]
            {
                sourceA.position,
                mirrorPoint,
                reflectedEnd
            }
        );
    }
}