
using UnityEngine;
using UnityEngine.InputSystem;

public class GirlController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Animator animator;

    private string currentAnimation = "";

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed ||
                Keyboard.current.leftArrowKey.isPressed)
                horizontal = -1f;

            if (Keyboard.current.dKey.isPressed ||
                Keyboard.current.rightArrowKey.isPressed)
                horizontal = 1f;

            if (Keyboard.current.wKey.isPressed ||
                Keyboard.current.upArrowKey.isPressed)
                vertical = 1f;

            if (Keyboard.current.sKey.isPressed ||
                Keyboard.current.downArrowKey.isPressed)
                vertical = -1f;
        }

        Vector2 movement = new Vector2(horizontal, vertical);

        if (movement.magnitude > 1f)
            movement.Normalize();

        transform.position += (Vector3)(movement * moveSpeed * Time.deltaTime);

        if (animator == null)
            return;

        if (movement == Vector2.zero)
        {
            animator.SetBool("IsWalking", false);
            currentAnimation = "";
            return;
        }

        animator.SetBool("IsWalking", true);

        string nextAnimation;

        if (vertical > 0f)
            nextAnimation = "usiro";
        else if (vertical < 0f)
            nextAnimation = "mae";
        else if (horizontal < 0f)
            nextAnimation = "walk";
        else
            nextAnimation = "walk2";

        if (currentAnimation != nextAnimation)
        {
            animator.Play(nextAnimation);
            currentAnimation = nextAnimation;
        }
    }

    public void StartWalking(Vector3[] newPath)
    {
    }

    public void StopWalking()
    {
        if (animator != null)
        {
            animator.SetBool("IsWalking", false);
            currentAnimation = "";
        }
    }
}
