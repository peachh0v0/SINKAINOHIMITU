
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class LightRayController : MonoBehaviour
{
    private LineRenderer line;
    private Transform sourceA;
    private Transform sourceB;
    private Mirror mirror;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();

        line.startWidth = 0.08f;
        line.endWidth = 0.08f;
        line.sortingOrder = 10;
        line.enabled = false;

        mirror = FindAnyObjectByType<Mirror>();

        GameObject[] lights = GameObject.FindGameObjectsWithTag("HIKA");

        if (lights.Length >= 2)
        {
            System.Array.Sort(lights, (a, b) =>
                a.transform.position.x.CompareTo(b.transform.position.x));

            sourceA = lights[0].transform;
            sourceB = lights[lights.Length - 1].transform;
        }
    }

    private void Update()
    {
        if (sourceA == null || sourceB == null)
        {
            line.enabled = false;
            return;
        }

        List<Vector3> points = new List<Vector3>();
        points.Add(sourceA.position);

        if (mirror != null)
        {
            Transform entry = mirror.transform.Find("EntryPoint");
            Transform exit = mirror.transform.Find("ExitPoint");

            if (entry != null && exit != null)
            {
                points.Add(entry.position);
                points.Add(exit.position);
            }
        }

        points.Add(sourceB.position);

        List<Vector3> visiblePoints = new List<Vector3>();
        visiblePoints.Add(points[0]);

        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector3 start = points[i];
            Vector3 end = points[i + 1];

            Vector2 direction = ((Vector2)(end - start)).normalized;
            float distance = Vector2.Distance(start, end);

            RaycastHit2D[] hits = Physics2D.RaycastAll(
                start, direction, distance);

            RaycastHit2D nearestHit = default;
            bool blocked = false;
            float nearestDistance = float.MaxValue;

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider == null)
                    continue;

                if (!hit.collider.CompareTag("Obstacle"))
                    continue;

                if (hit.distance < nearestDistance)
                {
                    nearestDistance = hit.distance;
                    nearestHit = hit;
                    blocked = true;
                }
            }

            if (blocked)
            {
                visiblePoints.Add(nearestHit.point);
                break;
            }

            visiblePoints.Add(end);
        }

        line.positionCount = visiblePoints.Count;
        line.SetPositions(visiblePoints.ToArray());
        line.enabled = true;
    }

    public void Connect(Transform first, Transform second)
    {
        if (first == null || second == null)
            return;

        if (!first.CompareTag("HIKA") ||
            !second.CompareTag("HIKA"))
            return;

        sourceA = first;
        sourceB = second;
    }

    public void ClearConnection()
    {
        sourceA = null;
        sourceB = null;
        line.positionCount = 0;
        line.enabled = false;

        GirlController girl = FindAnyObjectByType<GirlController>();

        if (girl != null)
            girl.StopWalking();
    }

    private void UpdateGirlPath()
    {
        if (sourceA == null || sourceB == null)
            return;

        GirlController girl = FindAnyObjectByType<GirlController>();

        if (girl == null)
            return;

        if (mirror != null)
        {
            Transform entry = mirror.transform.Find("EntryPoint");
            Transform exit = mirror.transform.Find("ExitPoint");

            if (entry != null && exit != null)
            {
                girl.StartWalking(new Vector3[]
                {
                    sourceA.position,
                    entry.position,
                    exit.position,
                    sourceB.position
                });

                return;
            }
        }

        girl.StartWalking(new Vector3[]
        {
            sourceA.position,
            sourceB.position
        });
    }
}
