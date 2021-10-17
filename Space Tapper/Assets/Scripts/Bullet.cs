using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Bullet : MonoBehaviour
{

	void Start()
	{
		Destroy(gameObject, 1); // - уничтожить объект по истечению указанного времени (сек), если пуля никуда не попала
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (!coll.isTrigger) // - чтобы пуля не реагировала на триггер
		{
			switch (coll.tag)
			{
				case "что-то":
					break;
				case "что-то ещё":
					break;
			}

			Destroy(gameObject);
		}
	}
}