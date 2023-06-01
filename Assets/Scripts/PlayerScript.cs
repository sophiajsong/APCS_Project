using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
private float horizontal;
[SerializeField] private float speed=8f;
[SerializeField] private float jumpingPower=8f;
public Animator animator;
float horizontalMove = 0f;
private SpriteRenderer sprite;
[SerializeField] private Rigidbody2D rb;
private CapsuleCollider2D coll;
private bool isJumping;

[SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      horizontalMove = Input.GetAxisRaw("Horizontal")*speed;

      animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

      horizontal = Input.GetAxisRaw("Horizontal");

      if(Input.GetButtonDown("Jump") && !isJumping)
      {
          rb.velocity=new Vector2(rb.velocity.x, jumpingPower);
          isJumping = true;
          animator.SetBool("isJumping", true);
      }

      if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
      {
        rb.velocity=new Vector2(rb.velocity.x, rb.velocity.y*0.5f);
      }

      UpdateAnimation();



    }

    private void UpdateAnimation() {
      if (horizontal > 0f) {
        animator.SetBool("isMoving", true);
        sprite.flipX = false;
      } else if (horizontal < 0f) {
        animator.SetBool("isMoving", true);
        sprite.flipX = true;
      } else {
        animator.SetBool("isMoving", false);
      }
    }

    private void FixedUpdate()
    {
      if (rb.bodyType != RigidbodyType2D.Static) {
        rb.velocity=new Vector2(horizontal*speed, rb.velocity.y);
      }
    }

    private bool IsGrounded()
    {
      return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }

    void OnCollisionEnter2D(Collision2D other) {
      if (other.gameObject.CompareTag("Ground")) {
        isJumping = false;
        animator.SetBool("isJumping", false);
      }
    }



}
