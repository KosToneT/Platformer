using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerControl : MonoBehaviour
{
	[Header("����������")]
	private Rigidbody2D rb;
	private Collision coll;

	[Header("���������� �����������")]
	public bool isFacingRight = true; // - ���� �� ������ ����� �������� ������� ������, �� ���� ������� true
	[SerializeField] private float speedRun = 10;
	[SerializeField] private float timeSmooth = 0.12f;
	private Vector2 speed;
	private Vector2 acceleration;
	
	[Header("���������� ������")]
	[SerializeField] private float jumpForce;
	[SerializeField] private int maxJumpCount = 2;
	private float jumpCount;

	[Header("���������� ����")]
	[SerializeField] private float dashForce = 0.5f;
	[SerializeField] private float timeDash = 0.2f;
	private float dash = 0.0f;
	private bool hasDashed;

	[Header("������ �� ����������� ������ �����")]
	[SerializeField] private float maxRay = 0.7f;
	[SerializeField] private float minRay = 0.1f;
	[SerializeField] private float radius = 0.1f;
	[SerializeField] private LayerMask layerForCheckLevel;

	private void Awake()
    {
		rb = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collision>();

		Controller.controller.Inputs.Main.Jump.performed += _ => Jump();
		Controller.controller.Inputs.Main.Gravity.performed += _ => Ratate();
		Controller.controller.Inputs.Main.Dash.performed += _ => StartCoroutine(Dash());
	}

    private void FixedUpdate()
	{
		//rb.AddForce(direction * rb.mass * speedRun);

		//if (Mathf.Abs(rb.velocity.x) > speedRun / 100f)
		//{
		//	rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * speedRun / 100f, rb.velocity.y);
		//}

		Move();

        if (coll.onGround)
        {
			jumpCount = maxJumpCount;
		}

		if(hasDashed)
		{
			speed += speed.normalized * dash;
		}
	}

	private void Move()
    {
		float side = Controller.controller.Inputs.Main.Move.ReadValue<float>(); // ����������� ������

		speed = Vector2.SmoothDamp(speed, new Vector2(side, 0) * speedRun, ref acceleration, timeSmooth);

		transform.Translate(speed * Time.fixedDeltaTime);

		MoveRay();

		if (side > 0 && !isFacingRight) Flip(); else if (side < 0 && isFacingRight) Flip();
	}

    private void MoveRay()
    {
		float disRay = speed.magnitude;
		disRay = Mathf.Clamp(disRay, minRay, maxRay);
		Debug.DrawRay(transform.position, speed.normalized * disRay, Color.black);
		if(Physics2D.CircleCast(transform.position, radius, speed, disRay, layerForCheckLevel))
        {
			speed = Vector2.zero;
			acceleration = Vector2.zero;
        }
    }

	private void Jump()
    {
		if (jumpCount > 0)
		{ 
			rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			jumpCount--;
		}
	}

	private IEnumerator Dash()
	{
		hasDashed = true;
		dash = dashForce;
		yield return new WaitForSeconds(timeDash);
		dash = 0.0f;
		hasDashed = !hasDashed;
	}

	private void Flip() // - �������� ���������
	{
		isFacingRight = !isFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private bool top;
	void Ratate()
	{
		rb.gravityScale *= -1;

		if (top == false)
		{
			transform.eulerAngles = new Vector3(0, 0, 180);
		}
		else
		{
			transform.eulerAngles = Vector3.zero;
		}

		top = !top;
	}

}