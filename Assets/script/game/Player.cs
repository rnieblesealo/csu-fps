using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{		
	[HideInInspector] public int currentHealth;
	
	public float xLookSensitivity;
	public float yLookSensitivity;
	public float moveSpeed;
	public float gravity;
	public float groundCheckRadius;
	public float jumpHeight;
	public int maxHealth;
	
	public LayerMask whatIsGround;
			
	[SerializeField] private Transform cameraRig;
	[SerializeField] private Transform torso;
	[SerializeField] private Transform groundChecker;
	[SerializeField] private Animator body;
	
	private CharacterController controller;
	private WalkAnimation walkAnimation;
	
	private bool isGrounded;
	private bool isMoving;
	private float xOffset;
	private float xOffsetInitial; 
	private float yVel;
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
		
		// due to imported model properties, default rotation of cam rig might not be 0
		xOffset = Mathf.Clamp(xOffset, xOffsetInitial - 90, xOffsetInitial + 90);
		
		cameraRig.localRotation = Quaternion.Euler(Vector3.right * xOffset);
		
		// rotate torso bone independently; parent constraint disables animator influence entirely which we don't want
		// this allows torso to rotate while not giving the arms themselves the camera shake caused by the animation
		torso.localRotation = cameraRig.localRotation;
	}

	private void Move()
	{
		float xInput = Input.GetAxisRaw("Horizontal");
		float yInput = Input.GetAxisRaw("Vertical");
		
		Vector3 move = xInput * transform.right + yInput * transform.forward;
		
		isMoving = move.magnitude > 0;
		
		controller.Move(move * moveSpeed * Time.deltaTime);

		isGrounded = Physics.CheckSphere(groundChecker.position, groundCheckRadius, whatIsGround);

		if (isGrounded && yVel < 0)
		{
			yVel = -2f;
		}
		
		if (isGrounded && Input.GetKeyDown(KeyCode.Space))
		{
			yVel = Mathf.Sqrt(jumpHeight * -2f * gravity);
		}
		
		yVel += gravity * Time.deltaTime;
		
		// del y = (1/2)g * t^2
		controller.Move(Time.deltaTime * yVel * Vector3.up);
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
		
		xOffsetInitial = cameraRig.transform.localRotation.eulerAngles.x;
		currentHealth = maxHealth;
	}

	private void Update()
	{
		Look();
		Move();
		
		if (isMoving && isGrounded)
		{
			walkAnimation.apply = true;
			SetBodyAnim("move");
		}
		
		else
		{
			walkAnimation.apply = false;
			SetBodyAnim("idle");	
		}
	}
}
