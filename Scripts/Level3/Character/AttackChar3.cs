using UnityEngine;

public class AttackChar3 : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;

	float speed = 10; // скорость пули
	Transform gunPoint; // точка рождения
	float fireRate = 1; // скорострельность
	[SerializeField]
	Transform zRotate; // объект для вращения по оси Z

	// ограничение вращения
	float minAngle = -90;
	float maxAngle = 90;

	private float curTimeout;

	void Start()
	{
		gunPoint = zRotate.transform;
	}

	void SetRotation()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = 10;
		Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePosition)-transform.position;
		float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
		//angle = Mathf.Clamp(angle, minAngle, maxAngle);
		zRotate.rotation = Quaternion.Euler(0,0,angle);
	}

	void Update()
	{
		if (PlatformBuble.varTemp != null)
		{
			if (Input.GetMouseButton(0))
			{
				Fire();
			}
			else
			{
				curTimeout = 100;
			}
			if (zRotate) SetRotation();
		}
	}

	void Fire()
	{
		curTimeout += Time.deltaTime;
		if (curTimeout > fireRate)
		{
			curTimeout = 0;
			CanvasMod.charMov.animator.Play("Throw");
			GameObject go = Instantiate(bullet, gunPoint.position, Quaternion.identity);
			Rigidbody2D clone = go.GetComponent<Rigidbody2D>();
			clone.velocity = transform.TransformDirection(gunPoint.right * speed);
			clone.transform.right = gunPoint.right;
		}
	}
}

