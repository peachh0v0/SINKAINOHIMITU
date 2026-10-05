using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LightRayController : MonoBehaviour
{
    private LineRenderer line;

    private Transform sourceA;
    private Transform sourceB;

    private Mirror mirror;

    void Awake()
    {
        line = GetComponent<LineRenderer>();

        line.startWidth = 0.08f;
        line.endWidth = 0.08f;
        line.sortingOrder = 10;

        line.enabled = false;

        mirror = FindAnyObjectByType<Mirror>();
    }

    void Update()
    {
        if (sourceA == null || sourceB == null)
        {
            line.enabled = false;
            return;
        }

        line.enabled = true;

        if (mirror != null)
        {
            Transform entry =
                mirror.transform.Find("EntryPoint");

            Transform exit =
                mirror.transform.Find("ExitPoint");

            if (entry != null && exit != null)
            {
                // A Å® Entry Å® Exit Å® B
                line.positionCount = 4;

                line.SetPosition(0, sourceA.position);
                line.SetPosition(1, entry.position);
                line.SetPosition(2, exit.position);
                line.SetPosition(3, sourceB.position);

                return;
            }
        }

        // ãæÇ™Ç»Ç¢èÍçáÇÕïÅí Ç…AÅ®B
        line.positionCount = 2;

        line.SetPosition(0, sourceA.position);
        line.SetPosition(1, sourceB.position);
    }

    public void Connect(
        Transform first,
        Transform second
    )
    {
        if (!first.CompareTag("HIKA") ||
            !second.CompareTag("HIKA"))
        {
            return;
        }

        sourceA = first;
        sourceB = second;

        line.enabled = true;

        UpdateGirlPath();
    }

    public void ClearConnection()
    {
        sourceA = null;
        sourceB = null;

        line.positionCount = 0;
        line.enabled = false;

        GirlController girl =
            FindAnyObjectByType<GirlController>();

        if (girl != null)
        {
            girl.StopWalking();
        }
    }

    private void UpdateGirlPath()
    {
        GirlController girl =
            FindAnyObjectByType<GirlController>();

        if (girl == null)
            return;

        if (mirror != null)
        {
            Transform entry =
                mirror.transform.Find("EntryPoint");

            Transform exit =
                mirror.transform.Find("ExitPoint");

            if (entry != null && exit != null)
            {
                // A Å® Entry Å® Exit Å® B
                girl.StartWalking(
                    new Vector3[]
                    {
                        sourceA.position,
                        entry.position,
                        exit.position,
                        sourceB.position
                    }
                );

                return;
            }
        }

        // A Å® B
        girl.StartWalking(
            new Vector3[]
            {
                sourceA.position,
                sourceB.position
            }
        );
    }
}