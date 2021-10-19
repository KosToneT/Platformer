using UnityEngine;

public class FireScript : MonoBehaviour
{

	public float speed = 10; // - скорость пули
	public Rigidbody2D bullet; // - префаб нашей пули
	public Transform gunPoint; // - точка рождения
	public float fireRate = 1; // - скорострельность

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
            print(clone.velocity);
            if (clone.velocity.x < 0) {
                Vector3 scale = clone.transform.localScale;
                clone.transform.localScale = new Vector3(-1 * scale.x, scale.y, scale.z);
            }
			clone.transform.right = gunPoint.right;
		}
	}
}
