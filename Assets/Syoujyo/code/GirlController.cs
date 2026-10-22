
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class GirlController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Animator animator;
    [SerializeField] private Tilemap floorTilemap;

    private Rigidbody2D rb;
    private Vector2 movement;
    private string currentAnimation = "";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        rb.collisionDetectionMode =
            CollisionDetectionMode2D.Continuous;

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

        movement = new Vector2(horizontal, vertical);

        if (movement.sqrMagnitude > 1f)
            movement.Normalize();

        string nextAnimation;

        if (movement == Vector2.zero)
        {
            nextAnimation = "idol";
        }
        else if (vertical != 0f)
        {
            nextAnimation = vertical < 0f ? "mae" : "usiro";
        }
        else
        {
            nextAnimation = horizontal < 0f ? "walk" : "walk2";
        }

        PlayAnimation(nextAnimation);
    }

    private void FixedUpdate()
    {
        if (floorTilemap == null)
            return;

        Vector2 currentPosition = rb.position;
        Vector2 move = movement * moveSpeed * Time.fixedDeltaTime;

        // 横移動：床がある場所だけ進む
        if (move.x != 0f)
        {
            Vector2 target = currentPosition
                + new Vector2(move.x, 0f);

            if (HasFloor(target))
                currentPosition = target;
        }

        // 縦移動：床がある場所だけ進む
        if (move.y != 0f)
        {
            Vector2 target = currentPosition
                + new Vector2(0f, move.y);

            if (HasFloor(target))
                currentPosition = target;
        }

        rb.MovePosition(currentPosition);
    }

    private bool HasFloor(Vector2 position)
    {
        Vector3Int cell = floorTilemap.WorldToCell(position);
        return floorTilemap.HasTile(cell);
    }

    private void PlayAnimation(string animationName)
    {
        if (animator == null ||
            currentAnimation == animationName)
            return;

        currentAnimation = animationName;
        animator.Play(animationName, 0, 0f);
    }

    public void StartWalking(Vector3[] newPath)
    {
        // 今はいったんキーボード移動を使用
    }

    public void StopWalking()
    {
        movement = Vector2.zero;
        PlayAnimation("idol");
    }
}

