using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerControl : MonoBehaviour
{
	[Header("Components")]
	private Rigidbody2D rb;
	public enum ProjectAxis { onlyX = 0, xAndY = 1 };
	public ProjectAxis projectAxis = ProjectAxis.onlyX;
	private Vector3 direction;

	[Header("Movement Variables")]
	public float speed = 150; // - скорость движения
	public KeyCode leftButton = KeyCode.A;
	public KeyCode rightButton = KeyCode.D;
	//private KeyCode upButton = KeyCode.W;
	//private KeyCode downButton = KeyCode.S;
	public KeyCode addForceButton = KeyCode.Space;
	public bool isFacingRight = true; // - если на старте сцены персонаж смотрит вправо, то надо ставить true.
	private float vertical;
	private float horizontal;

	[Header("Jump Variables")]
	public float AddForce = 7; // - если Оnly X, будет использовано для прыжка. Во втором режиме, значение addForce будет прибавлено к speed
	private bool jump;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;

		if (projectAxis == ProjectAxis.xAndY)
		{
			rb.gravityScale = 0;
			rb.drag = 10;
		}
	}

	void OnCollisionStay2D(Collision2D coll)
	{
		if (coll.transform.tag == "Ground")
		{
			rb.drag = 10;
			jump = true;
		}
	}

	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.transform.tag == "Ground")
		{
			rb.drag = 0;
			jump = false;
		}
	}

	void FixedUpdate()
	{
		rb.AddForce(direction * rb.mass * speed);

		if (Mathf.Abs(rb.velocity.x) > speed / 100f)
		{
			rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * speed / 100f, rb.velocity.y);
		}

		if (projectAxis == ProjectAxis.xAndY)
		{
			if (Mathf.Abs(rb.velocity.y) > speed / 100f)
			{
				rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * speed / 100f);
			}
		}
		else
		{
			if (Input.GetKey(addForceButton) && jump)
			{
				rb.velocity = new Vector2(0, AddForce);
			}
		}
	}

	void Flip() // - Разворот персонажа. При движении только по горизонтали
	{
		if (projectAxis == ProjectAxis.onlyX)
		{
			isFacingRight = !isFacingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}

	void Update()
	{

		//if (Input.GetKey(upButton))
		//{
		//	vertical = 1;
		//}
		//else if (Input.GetKey(downButton))
		//{
		//	vertical = -1;
		//}
		//else
		//{
		//	vertical = 0;
		//}

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

		if (projectAxis == ProjectAxis.onlyX)
		{
			direction = new Vector2(horizontal, 0);
		}
		else
		{
			if (Input.GetKeyDown(addForceButton))
			{
				speed += AddForce;
			}
			else if (Input.GetKeyUp(addForceButton))
			{
				speed -= AddForce;
			}
			direction = new Vector2(horizontal, vertical);
		}

		if (horizontal > 0 && !isFacingRight) Flip(); else if (horizontal < 0 && isFacingRight) Flip();
	}
}