using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float maxZoom = 15.0f;

    public float minZoom = 3.0f;

    public float speed = 10.0f;

    public Rigidbody2D rb;

    public bool onGround = true;

    public bool canJump = true;

    public int jumpCooldown = 100;

    public float minJumpStrength = 500.0f;

    public float maxJumpStrength = 1500.0f;

    public float jumpStrength = 500.0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && canJump)
            jumpStrength += 5.0f;

        if (jumpCooldown > 0)
            jumpCooldown -= 1;

        if (onGround && jumpCooldown <= 0)
            canJump = true;

        float horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * speed);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * speed);
        }

    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKeyUp(KeyCode.Space) && canJump) || jumpStrength >= maxJumpStrength)
        {
            JumpWithSpacebar();
        }

        if ((Input.GetMouseButtonUp(0) && canJump) || jumpStrength >= maxJumpStrength)
        {
            JumpToCursor();
        }
    }

    private void ResetAfterJump()
    {
        //Do resets after jump.
        jumpStrength = minJumpStrength;
        jumpCooldown = 100;
        canJump = false;
    }

    private void JumpWithSpacebar()
    {
        rb.AddForce(new Vector2(0, jumpStrength));
        ResetAfterJump();
    }

    private void JumpToCursor()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mouseDir = mousePos - gameObject.transform.position;
        mouseDir.z = 0.0f;
        mouseDir = mouseDir.normalized;

        rb.AddForce(mouseDir * jumpStrength);

        ResetAfterJump();
    }

    public void OnTriggerStay2D(Collider2D collider) =>
        onGround = collider.gameObject.CompareTag("Ground") ? true : false;


    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
        {
            onGround = false;
            canJump = false;
        }
    }
}
