using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 3f;
    [SerializeField] private float distance = 4f;
	[SerializeField] private float rotationDelayTime = 0.12f;
	[SerializeField] private Vector2 pitchMinMax = new Vector2(-30,70);
	[SerializeField] private Transform target;

	private float yaw;  //rotation around y-axis (left & right)
    private float pitch; //rotation around x-axis (up & down)

	private Vector3 currentRotation;
	private Vector3 currentVelocity;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void LateUpdate() //called after all updates, ensure that everything needed is set
    {
        yaw += Input.GetAxisRaw("Mouse X") * sensitivity;
        pitch -= Input.GetAxisRaw("Mouse Y") * sensitivity; // mouse y is inverted by default
		pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

		//why curr rotation instead of transform euler angles?
		currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref currentVelocity, rotationDelayTime);
		transform.rotation = Quaternion.Euler(currentRotation);

		transform.position = target.position - transform.forward * distance; //remember
    }
}
