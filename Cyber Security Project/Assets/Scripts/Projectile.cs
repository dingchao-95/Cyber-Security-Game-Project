using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour 
{
	public float speed;
	public LayerMask CollisionMask;

	public GameObject Owner {get; private set;}
	public Vector2 Direction {get; private set;}
	public Vector2 InitialVelocity {get; private set;}

	public void Initialize(GameObject owner, Vector2 direction , Vector2 initialVelocity)
	{
		transform.right = direction;

		Owner = owner;
		Direction = direction;
		InitialVelocity = initialVelocity;
		OnInitialized();
	}

	protected virtual void OnInitialized()
	{

	}

	public virtual void OnTriggerEnter2D(Collider2D Other)
	{
		if((CollisionMask.value & (1 << Other.gameObject.layer)) == 0)
		{
			OnNotCollideWith(Other);
			return;
		}

		var isOwner = Other.gameObject == Owner;

		if(isOwner)
		{
			OnCollideOwner();
		}

		var takeDamage = (ITakeDamage) Other.GetComponent(typeof (ITakeDamage));
		if(takeDamage != null)
		{
			OnCollideTakeDamage(Other,takeDamage);
		}

		OnCollideOther(Other);
	}


	protected virtual void OnNotCollideWith(Collider2D Other)
	{

	}

	protected virtual void OnCollideOwner()
	{

	}

	protected virtual void OnCollideTakeDamage(Collider2D other, ITakeDamage takeDamage)
	{

	}

	protected virtual void OnCollideOther(Collider2D Other)
	{

	}











}
