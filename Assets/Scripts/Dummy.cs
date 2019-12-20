using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Dummy : MonoBehaviour
{
	[SerializeField] protected float maxSpeed = 7f;
	[SerializeField] protected float maxAngularSpeed = 90f;
	[SerializeField] protected float gravityScale = 2.5f;
	[SerializeField] protected float fallMultiplier = 1.5f;
	[SerializeField] protected float health;
	[SerializeField] protected float attackTimer = 2f;

	[SerializeField] private Color baseColor;

	public float Health { get => health; set { health = value; } }
	public Animator DummyAnimator { get; private set; }

	protected CharacterController controller;
	protected float gravity;
	protected float velocityY;

	protected virtual void Awake()
	{
		controller = GetComponent<CharacterController>();
		//DummyAnimator = GetComponent<Animator>();

		gravity = -Physics.gravity.y * gravityScale;
	}
}
