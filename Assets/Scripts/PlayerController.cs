using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : Dummy
{
	[SerializeField] private float jumpHeight = 2;
	[SerializeField] private float heightPadding = 0.2f;
	[SerializeField] private float reloadTime;
	
	private float jumpSpeed;
	private float timer;

	private Vector3 inputDir;

	private bool isJumping;
	private bool isGrounded;

	private int counter;
	
	Quaternion targetRotation;

	private MeshRenderer renderer;

	protected override void Awake()
	{
		base.Awake();
		jumpSpeed = Mathf.Sqrt(2 * gravity * jumpHeight);
		renderer = GetComponent<MeshRenderer>();
	}

	//---- Problems to solve ----
	//check prev cam & input state for  rotation optimizing!
	//input manager settings, eliminating slippery feel & etc. (GetAxisRaw do not affected by input manager settings!)
	//split input logic from controller logic?

	private void Update()
	{
//		if (DummyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
//			return;

		inputDir = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical")) * maxSpeed;
		inputDir = Vector3.ClampMagnitude(inputDir, maxSpeed); //clamping on air & ground, not affecting jump height (inputDir is separate from jump value)

		isGrounded = CheckGround();
		if (isGrounded)
		{
			isJumping = false;
			velocityY = 0;

			if (Input.GetButtonDown("Jump"))
			{
				velocityY = jumpSpeed;
				isJumping = true;
			}
		}

		velocityY -= gravity * Time.deltaTime * ((velocityY < 0) ? fallMultiplier : 1); //gravity applied per second
		Vector3 targetMove = inputDir + Vector3.up * velocityY;
		targetMove = transform.TransformDirection(targetMove);
		controller.Move(targetMove * Time.deltaTime); //movement per second

		PlayAnimation();

		if (Input.GetKey(KeyCode.Q) && Time.time>timer)
		{
			timer = Time.time + reloadTime;
			StartCoroutine(ShiftOut(2));

		}
	}
	private bool CheckGround()
	{
		if (controller.isGrounded)
			return true;

		if (isJumping)
			return false;

		//slope check
		if (Physics.Raycast(transform.position,Vector3.down,out RaycastHit hit, heightPadding))
		{
			//delegate to final movement vector
			controller.Move(Vector3.down * hit.distance);
			return true;
		}

		return false;
	}

	private void PlayAnimation()
	{
//		DummyAnimator.SetBool("IsGrounded", isGrounded);
//		DummyAnimator.SetFloat("HSpeed", inputDir.magnitude);
//		DummyAnimator.SetFloat("VSpeed", velocityY);
	}

	private void OnTriggerEnter(Collider other)
	{
		print(other.tag);
		counter++;
		
		Destroy(other.gameObject);

		if (counter >= 5)
		{
			UIManager.Instance.Win("Player win!");
		}
	}

	IEnumerator ShiftOut(float delay)
	{
		renderer.enabled = false;
		
		yield return new WaitForSeconds(delay);

		renderer.enabled = true;
	}
}
