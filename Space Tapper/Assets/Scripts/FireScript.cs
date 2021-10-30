using UnityEngine;

public class FireScript : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] private Rigidbody2D bullet; // - ������ ����� ����
	[SerializeField] private Transform gunPoint; // - ����� ��������

	[Header("parameters")]
	[SerializeField] private float speed = 10; // - �������� ����
	[SerializeField] private float fireRate = 15; // - ����������������

	private float curTimeout;

    void Update()
	{
		float side = Controller.controller.Inputs.Main.Fire.ReadValue<float>();
		if (side > 0)
		{
			curTimeout += Time.deltaTime;
			Fire();
		}
		else
		{
			curTimeout = 100;
		}

	}

	void Fire()
	{
		
		if (curTimeout > fireRate)
		{
			curTimeout = 0;
			Rigidbody2D clone = Instantiate(bullet, gunPoint.position, Quaternion.identity) as Rigidbody2D;
			clone.velocity = transform.TransformDirection(gunPoint.right * speed);
			clone.transform.right = gunPoint.right;
		}
	}
}
