using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("Components")]
    private Rigidbody2D rb;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask groundLayer;

    [Header("Movement Variables")]
    [SerializeField] private float moveAcceleration = 50f;
    [SerializeField] private float maxMoveSpeed = 12f;
    [SerializeField] private float groundLinearDrag = 10f;

    [Header("Jump Variables")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float airLinearDrag = 2.5f;
    [SerializeField] private float fallMultiplier = 8f;
    [SerializeField] private float lowJumpFallMultiplier = 5f;
    [SerializeField] private int extraJumps = 1;
    private int extraJumpsValue;
    private bool canJump => Input.GetButtonDown("Jump") && (onGround || extraJumpsValue > 0);

    [Header("Ground Collision Variables")]
    [SerializeField] private float groundRaycastLength;
    private bool onGround;

    private float horizontalDirection;
    private bool changingDirection => (rb.velocity.x > 0f && horizontalDirection < 0f) || (rb.velocity.x < 0f && horizontalDirection > 0f);

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        horizontalDirection = GetInput().x;
        if(canJump) Jump();
    }

    private void FixedUpdate() {
        CheckCollisions();
        MovePlayer();
        if(onGround) {
            extraJumpsValue = extraJumps;
            ApplyGroundLinearDrag();
        } else {
            ApplyAirLinearDrag();
            FallMultiplier();
        }
    }

    private Vector2 GetInput() {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MovePlayer() {
        rb.AddForce(new Vector2(horizontalDirection, 0f) * moveAcceleration);
        
        if(Mathf.Abs(rb.velocity.x) > maxMoveSpeed)
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxMoveSpeed, rb.velocity.y);
    }

    private void ApplyGroundLinearDrag() {
        if(Mathf.Abs(horizontalDirection) < 0.4f || changingDirection) {
            rb.drag = groundLinearDrag;
        } else {
            rb.drag = 0f;
        }
    }
    private void ApplyAirLinearDrag() {
        rb.drag = airLinearDrag;
    }

    private void Jump() {

        if(!onGround) {
            extraJumpsValue--;
        }

        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void FallMultiplier() {
        if(rb.velocity.y < 0) {
            rb.gravityScale = fallMultiplier;
        } else if(rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            rb.gravityScale = lowJumpFallMultiplier;
        } else {
            rb.gravityScale = 1f;
        }
    }

    private void CheckCollisions() {
        onGround = Physics2D.Raycast(transform.position * groundRaycastLength, Vector2.down, groundRaycastLength, groundLayer);
        Debug.Log("CheckCollisions: "+onGround);
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundRaycastLength);
    }



}
