using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Bullet : MonoBehaviour
{
	[Header("time to destruction")]
	public float destroy;

	void Start()
	{
		Destroy(gameObject, destroy); // - ���������� ������ �� ��������� ���������� ������� (���), ���� ���� ������ �� ������
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (!coll.isTrigger) // - ����� ���� �� ����������� �� �������
		{
			switch (coll.tag)
			{
				case "Grou":
					break;
				case "���-�� ���":
					break;
			}

			Destroy(gameObject);
		}
	}
}