using UnityEngine;

public class Player : MonoBehaviour
{		
	[HideInInspector] public int currentHealth;
	
	public float xLookSensitivity;
	public float yLookSensitivity;
	public float moveSpeed;
	public int maxHealth;
			
	[SerializeField] private Transform cameraRig;
	[SerializeField] private Animator body;
	
	private CharacterController controller;
	private WalkAnimation walkAnimation;
	
	private float xOffset;
	private string bodyAnim;
	
	private void SetBodyAnim(string anim)
	{
		if (anim == bodyAnim)
		{
			return;
		}
		
		body.CrossFade(anim, 0.2f, -1, 0);
		bodyAnim = anim;
	}
	
	private void Look()
	{
		float xInput = Input.GetAxis("Mouse X") * xLookSensitivity;
		float yInput = Input.GetAxis("Mouse Y") * yLookSensitivity;
		
		transform.Rotate(Time.deltaTime * xInput * Vector3.up);
	
		xOffset -= yInput * Time.deltaTime;
		xOffset = Mathf.Clamp(xOffset, -90, 90);
		
		cameraRig.localRotation = Quaternion.Euler(Vector3.right * xOffset);
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
		walkAnimation = GetComponentInChildren<WalkAnimation>();
	}

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		
		currentHealth = maxHealth;
	}

	// Update is called once per frame
	private void Update()
	{
		Look();
		Move();
		
		if (controller.velocity == Vector3.zero)
		{
			walkAnimation.apply = false;
			SetBodyAnim("idle");		
		}
		
		else
		{
			walkAnimation.apply = true;
			SetBodyAnim("move");
		}
	}
}
