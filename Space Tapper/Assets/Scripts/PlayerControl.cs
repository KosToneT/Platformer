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
	[SerializeField] private float speedRun = 10;
	[SerializeField] private float timeSmooth = 0.12f;
	private Vector2 speed;
	private Vector2 acceleration;
	public bool isFacingRight = true; // - если на старте сцены персонаж смотрит вправо, то надо ставить true

	[Header("Jump Variables")]
	[SerializeField] private float jumpForce;
	[SerializeField] private bool isGrounded;

    private void Awake()
    {
		rb = GetComponent<Rigidbody2D>();
	}

    private void Start()
    {
		Controller.controller.Inputs.Main.Jump.performed += _ => Jump();
    }

	private void FixedUpdate()
	{
		Move();
	}

	private void Move()
    {
		float side = Controller.controller.Inputs.Main.Move.ReadValue<float>(); // направление игрока

		speed = Vector2.SmoothDamp(speed, new Vector2(side, 0), ref acceleration, timeSmooth);

		transform.Translate(speed * speedRun * Time.fixedDeltaTime);

		if (side > 0 && !isFacingRight) Flip(); else if (side < 0 && isFacingRight) Flip();
	}

	private void Jump()
    {
		if (isGrounded)
		{
			rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}
	}

	private void OnCollisionStay2D(Collision2D coll)
	{
		if (coll.transform.tag == "Ground")
		{
			rb.drag = 10;
			isGrounded = true;
		}
	}

	private void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.transform.tag == "Ground")
		{
			rb.drag = 0;
			isGrounded = false;
		}
	}
	private void OnCollisionEnter2D(Collision2D coll)
    {
		if (coll.transform.tag == "Ground")
		{
			rb.drag = 10;
			isGrounded = true;
		}
	}

	private void Flip() // - Разворот персонажа
	{
		isFacingRight = !isFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}