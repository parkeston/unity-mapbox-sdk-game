using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Look : MonoBehaviour {
	public enum RotAxes
	{
		MouseX=1,
		MouseY=2
	}
	public RotAxes axes = RotAxes.MouseX;
    public float sensitivity = 200.0f, dgr;
    private float rotY;
	// Use this for initialization

	private void Awake()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Start () {
		dgr = 45.0f;
		rotY=0;
	}

	void Update () {
		if (axes == RotAxes.MouseX)
			transform.Rotate (0, sensitivity * Input.GetAxis ("Mouse X") * Time.deltaTime, 0); 
		else 
		{
			rotY -= sensitivity * Input.GetAxis ("Mouse Y") * Time.deltaTime;
			rotY = Mathf.Clamp (rotY, -dgr, dgr);
			//transform.localEulerAngles = new Vector3(rotY,0,0);
			transform.localRotation= Quaternion.Euler(rotY,0,0); 
		}
	}
}
