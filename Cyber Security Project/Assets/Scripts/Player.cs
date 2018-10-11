using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour , ITakeDamage
{
	private bool _isFacingRight;
	private CharacterController2D _controller;
	private float _normalizedHorizontalSpeed;

	public float MaxSpeed = 8;
	public float SpeedAccelerationOnGround = 10f;
	public float SpeedAccelerationInAir = 5f;
	public int MaxHealth = 100;
	public GameObject OuchEffect;
	public Projectile Projectile;
	public float FireRate;
	public Transform ProjectileFireLocation;
	public Animator animator;

	public int Health {get; private set;}
	public bool isDead {get; private set;}

	private float _canFireIn;

	public void Awake()
	{
		_controller = GetComponent<CharacterController2D>();
		_isFacingRight = transform.localScale.x > 0;
		Health = MaxHealth;

	}

	public void Update()
	{
		_canFireIn -= Time.deltaTime;

		if(!isDead)
		HandleInput();

		var movementFactor = _controller.State.isGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;

		if(isDead)
			_controller.setHorizontalForce(0);
		else
		_controller.setHorizontalForce(Mathf.Lerp(_controller.Velocity.x , _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movementFactor) );


		animator.SetBool("IsGrounded" , _controller.State.isGrounded);
		animator.SetBool("IsDead" , isDead);
		animator.SetFloat("Speed" , Mathf.Abs(_controller.Velocity.x) / MaxSpeed);
	}

	public void FinishLevel()
	{
		enabled = false;
		_controller.enabled = false;
		collider2D.enabled = false;
	}

	public void Kill()
	{
		_controller.HandleCollisions = false;
		collider2D.enabled = false;
		isDead = true;
		Health = 0;

		_controller.SetForce(new Vector2(0,20));
	}

	public void RespawnAt(Transform SpawnPoint)
	{
		if(!_isFacingRight)
			Flip();

		isDead = false;
		collider2D.enabled = true;
		_controller.HandleCollisions = true;
		Health = MaxHealth;

		transform.position = SpawnPoint.position;


	}

	public void TakeDamage(int damage, GameObject instigator)
	{
		Instantiate(OuchEffect,transform.position,transform.rotation);
		Health -= damage;

		if(Health <= 0)
			LevelManager.Instance.KillPlayer();
	}

	public void GiveHealth(int health, GameObject instigator)
	{
		Health = Mathf.Min(Health + health, MaxHealth);
	}

	private void HandleInput()
	{
		if(Input.GetKey(KeyCode.D))
		{
			_normalizedHorizontalSpeed = 1;
			if(!_isFacingRight)
			{
				Flip();
			}
	
		}
		else if(Input.GetKey(KeyCode.A))
		{
			_normalizedHorizontalSpeed = -1;
			if(_isFacingRight)
			{
				Flip();
			}
		}
		else
		{
			_normalizedHorizontalSpeed = 0;
		}

		if(_controller.CanJump && Input.GetKeyDown(KeyCode.Space))
		{
			_controller.Jump();
		}

		if(Input.GetMouseButtonDown(0))
			FireProjectile();

	}

	private void FireProjectile()
	{
		if(_canFireIn > 0)
			return;

		var direction = _isFacingRight ? Vector2.right : -Vector2.right;

		var projectile = (Projectile)Instantiate(Projectile, ProjectileFireLocation.position, ProjectileFireLocation.rotation);
		projectile.Initialize(gameObject,direction, _controller.Velocity);

		_canFireIn = FireRate;
		animator.SetTrigger("Fire");
	}

	private void Flip()
	{
		transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		_isFacingRight = transform.localScale.x > 0;
	}


}
