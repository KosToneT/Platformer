using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Bullet : MonoBehaviour
{

	void Start()
	{
		Destroy(gameObject, 1); // - ���������� ������ �� ��������� ���������� ������� (���), ���� ���� ������ �� ������
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