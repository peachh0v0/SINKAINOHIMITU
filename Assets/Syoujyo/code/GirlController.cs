using UnityEngine;

public class GirlController : MonoBehaviour
{
    [Header("­—‚ª’Ç‚¢‚©‚¯‚éŒõ")]
    [SerializeField] private Transform lightTarget;

    [Header("ˆÚ“®‘¬“x")]
    [SerializeField] private float moveSpeed = 1.5f;

    void Update()
    {
        if (lightTarget == null)
            return;

        // Œõ‚Ì•ûŒü‚ğŒvZ
        Vector3 direction = lightTarget.position - transform.position;

        // Œõ‚ÉŒü‚©‚Á‚ÄˆÚ“®
        transform.position += direction.normalized * moveSpeed * Time.deltaTime;
    }
}