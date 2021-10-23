using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Bullet : MonoBehaviour
{
	[Header("time to destruction")]
	public float destroy;

	void Start()
	{
		Destroy(gameObject, destroy); // - уничтожить объект по истечению указанного времени (сек), если пуля никуда не попала
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (!coll.isTrigger) // - чтобы пуля не реагировала на триггер
		{
			switch (coll.tag)
			{
				case "Grou":
					break;
				case "что-то ещё":
					break;
			}

			Destroy(gameObject);
		}
	}
}