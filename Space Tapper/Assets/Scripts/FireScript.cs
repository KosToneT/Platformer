using UnityEngine;

public class FireScript : MonoBehaviour
{
	[Header("Components")]
	public Rigidbody2D bullet; // - ������ ����� ����
	public Transform gunPoint; // - ����� ��������

	[Header("parameters")]
	public float speed = 10; // - �������� ����
	public float fireRate = 1; // - ����������������

	private float curTimeout;

	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Fire();
		}
		else
		{
			curTimeout = 100;
		}

	}

	void Fire()
	{
		curTimeout += Time.deltaTime;
		if (curTimeout > fireRate)
		{
			curTimeout = 0;
			Rigidbody2D clone = Instantiate(bullet, gunPoint.position, Quaternion.identity) as Rigidbody2D;
			clone.velocity = transform.TransformDirection(gunPoint.right * speed);
			clone.transform.right = gunPoint.right;
		}
	}
}
