using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Player : MonoBehaviour
{		
	public float xLookSensitivity;
	public float yLookSensitivity;
	
	public float moveSpeed;
		
	private Transform tCamera;
	
	private CharacterController controller;
	
	private float xOffset;
	
	private void Look()
	{
		float xInput = Input.GetAxis("Mouse X") * xLookSensitivity;
		float yInput = Input.GetAxis("Mouse Y") * yLookSensitivity;
		
		transform.Rotate(Vector3.up * xInput * Time.deltaTime);
	
		xOffset -= yInput * Time.deltaTime;
		xOffset = Mathf.Clamp(xOffset, -90, 90);
		
		tCamera.localRotation = Quaternion.Euler(Vector3.right * xOffset);
	}

	private void Move()
	{
		float xInput = Input.GetAxisRaw("Horizontal");
		float yInput = Input.GetAxisRaw("Vertical");
		
		Vector3 move = xInput * transform.right + yInput * transform.forward;
		
		controller.Move(move * moveSpeed * Time.deltaTime);
	}

	private void Awake()
	{
		controller = GetComponent<CharacterController>();
		tCamera = GetComponentInChildren<Camera>().transform;
	}

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	// Update is called once per frame
	private void Update()
	{
		Look();
		Move();
	}
}
