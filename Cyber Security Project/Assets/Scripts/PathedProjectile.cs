using UnityEngine;
using System.Collections;

public class PathedProjectile : MonoBehaviour , ITakeDamage
{
	private Transform _destination;
	private float _speed;

	public GameObject destroyEffect;
	public int pointsToGivePlayer;

	public void Initialize(Transform destination , float speed)
	{
		_destination = destination;
		_speed 		 = speed;
	}

	public void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position , _destination.position, Time.deltaTime * _speed);

		var distanceSquared = (_destination.transform.position - transform.position).sqrMagnitude;

		if(distanceSquared > .01f * .01f)
			return;

		Destroy (gameObject);
	}

	public void TakeDamage(int damage, GameObject instigator)
	{
		if(destroyEffect != null)
			Instantiate(destroyEffect , transform.position,transform.rotation);

		Destroy(gameObject);

		var projectile = instigator.GetComponent<Projectile>();

		if(projectile != null && projectile.GetComponent<Player>() != null && pointsToGivePlayer != 0)
		{
			GameManager.Instance.AddPoints(pointsToGivePlayer);
		}

	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			GameManager.Instance.AddPoints(pointsToGivePlayer);
			Destroy(gameObject);
		}
	}
}
