using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerControl : MonoBehaviour
{
	[Header("Components")]
	private Rigidbody2D rb;
	private Vector3 direction;

	[Header("Movement Variables")]
	public float speed; // - скорость движения
	public KeyCode leftButton = KeyCode.A;
	public KeyCode rightButton = KeyCode.D;
	public KeyCode jumpForceButton = KeyCode.Space;
	public bool isFacingRight = true; // - если на старте сцены персонаж смотрит вправо, то надо ставить true
	private float horizontal;

	[Header("Jump Variables")]
	public float jumpForce;
	private bool isGrounded;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	void FixedUpdate()
	{
		rb.AddForce(direction * rb.mass * speed);

		if (Input.GetKey(leftButton))
		{
			horizontal = -1;
		}
		else if (Input.GetKey(rightButton))
		{
			horizontal = 1;
		}
		else
		{
			horizontal = 0;
		}

		direction = new Vector2(horizontal * speed, 0);

		if (horizontal > 0 && !isFacingRight) Flip(); else if (horizontal < 0 && isFacingRight) Flip();

		if (Mathf.Abs(rb.velocity.x) > speed / 100f)
		{
			rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * speed / 100f, rb.velocity.y);
		}

		if (Input.GetKey(jumpForceButton) && isGrounded)
		{
			rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
		}
	}

	void OnCollisionStay2D(Collision2D coll)
	{
		if (coll.transform.tag == "Ground")
		{
			rb.drag = 10;
			isGrounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.transform.tag == "Ground")
		{
			rb.drag = 0;
			isGrounded = false;
		}
	}

	void Flip() // - Разворот персонажа
	{
		isFacingRight = !isFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}