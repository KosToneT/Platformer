using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerControl : MonoBehaviour
{

	public enum ProjectAxis { onlyX = 0, xAndY = 1 };
	public ProjectAxis projectAxis = ProjectAxis.onlyX;
	public float speed = 150; // - скорость движения
	public float addForce = 7; // - если Оnly X, будет использовано для прыжка. Во втором режиме, значение addForce будет прибавлено к speed
	private KeyCode leftButton = KeyCode.A; 
	private KeyCode rightButton = KeyCode.D; 
	//private KeyCode upButton = KeyCode.W;
	//private KeyCode downButton = KeyCode.S;
	private KeyCode addForceButton = KeyCode.Space; 
	public bool isFacingRight = true; // - если на старте сцены персонаж смотрит вправо, то надо ставить true.
	private Vector3 direction; 
	private float vertical; 
	private float horizontal; 
	private Rigidbody2D body; 
	private bool jump; 

	void Start()
	{
		body = GetComponent<Rigidbody2D>();
		body.constraints = RigidbodyConstraints2D.FreezeRotation;

		if (projectAxis == ProjectAxis.xAndY)
		{
			body.gravityScale = 0;
			body.drag = 10;
		}
	}

	void OnCollisionStay2D(Collision2D coll)
	{
		if (coll.transform.tag == "Ground")
		{
			body.drag = 10;
			jump = true;
		}
	}

	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.transform.tag == "Ground")
		{
			body.drag = 0;
			jump = false;
		}
	}

	void FixedUpdate()
	{
		body.AddForce(direction * body.mass * speed);

		if (Mathf.Abs(body.velocity.x) > speed / 100f)
		{
			body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * speed / 100f, body.velocity.y);
		}

		if (projectAxis == ProjectAxis.xAndY)
		{
			if (Mathf.Abs(body.velocity.y) > speed / 100f)
			{
				body.velocity = new Vector2(body.velocity.x, Mathf.Sign(body.velocity.y) * speed / 100f);
			}
		}
		else
		{
			if (Input.GetKey(addForceButton) && jump)
			{
				body.velocity = new Vector2(0, addForce);
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
				speed += addForce;
			}
			else if (Input.GetKeyUp(addForceButton))
			{
				speed -= addForce;
			}
			direction = new Vector2(horizontal, vertical);
		}

		if (horizontal > 0 && !isFacingRight) Flip(); else if (horizontal < 0 && isFacingRight) Flip();
	}
}