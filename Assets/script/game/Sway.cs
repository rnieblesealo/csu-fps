using UnityEngine;

public class Sway : MonoBehaviour
{
	[SerializeField] private float smoothness;
	[SerializeField] private float multiplier;
	
	private void Update()
	{
		float mouseX = Input.GetAxisRaw("Mouse X") * multiplier;
		float mouseY = Input.GetAxisRaw("Mouse Y") * multiplier;
		
		// create x and y rotations based on mouse input
		Quaternion rotX = Quaternion.AngleAxis(mouseY, Vector3.right);
		Quaternion rotY = Quaternion.AngleAxis(mouseX, Vector3.up);
		
		// compose both rotations (is this what quat multiplication does?)
		Quaternion composite = rotX * rotY;
		
		// lerp towards that rotation
		transform.localRotation = Quaternion.Slerp(transform.localRotation, composite, smoothness * Time.deltaTime);
	}
}
