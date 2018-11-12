using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {
	
	private IEnemyState currentState;

	public GameObject Target { get; set;}

	public override void Start () 
	{
		base.Start ();

		ChangeState(new IdleState());
	}
	

	void Update ()
	{
		currentState.Execute ();
	}

	public void ChangeState(IEnemyState newState)
	{
		if (currentState != null)
		{
			currentState.Exit();
		}

		currentState = newState;

		currentState.Enter(this);
	}

	public void Move()
	{
		MyAnimator.SetFloat ("speed", 1);

		transform.Translate (GetDirection () * (movementSpeed * Time.deltaTime));

	}
	public Vector2 GetDirection()
	{
		return facingRight ? Vector2.right : Vector2.left;
	}

	void OntriggerEnter2D(Collider2D other)
	{
		currentState.OnTriggerEnter (other);
	}
}