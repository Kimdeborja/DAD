using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour  {


	[SerializeField]
	public float movementSpeed;

	protected bool facingRight;

	public Animator MyAnimator { get; private set; }

	public virtual void Start () {
		
		facingRight = true;

		MyAnimator = GetComponent<Animator>();
	}


	void Update () {
		
	}
	public void ChangeDirection()
	{
		facingRight = !facingRight;
		transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
	}
}
