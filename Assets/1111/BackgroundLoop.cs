
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private Transform[] backgrounds = new Transform[4];

    private float width;
    private Camera camComponent;

    private void Start()
    {
        if (cam == null && Camera.main != null)
            cam = Camera.main.transform;

        if (cam != null)
            camComponent = cam.GetComponent<Camera>();

        if (camComponent == null)
            camComponent = Camera.main;

        if (backgrounds.Length == 0 || backgrounds[0] == null)
        {
            enabled = false;
            return;
        }

        SpriteRenderer sr = backgrounds[0].GetComponent<SpriteRenderer>();

        if (sr == null)
        {
            enabled = false;
            return;
        }

        width = sr.bounds.size.x;
    }

    private void LateUpdate()
    {
        if (cam == null || camComponent == null || width <= 0f)
            return;

        float viewWidth = camComponent.orthographicSize
            * camComponent.aspect * 2f;

        foreach (Transform bg in backgrounds)
        {
            if (bg == null)
                continue;

            if (cam.position.x - viewWidth / 2f >
                bg.position.x + width / 2f)
            {
                float right = backgrounds[0].position.x;

                foreach (Transform other in backgrounds)
                {
                    if (other != null && other.position.x > right)
                        right = other.position.x;
                }

                bg.position = new Vector3(
                    right + width,
                    bg.position.y,
                    bg.position.z
                );
            }
        }
    }
}
