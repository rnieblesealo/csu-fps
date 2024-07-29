using UnityEngine;

public class WalkAnimation : MonoBehaviour
{
	public bool apply;
	
	[SerializeField] private Vector3 amplitude;
	[SerializeField] private Vector3 period;
	[SerializeField] private Vector3 shift;
	[SerializeField] private float transitionSmoothness;
	
	private Vector3 initialPosition;
	private Vector3 targetPosition;
	private Vector3 offset;
	private float sineTime; // we need to reset our timer so animation restarts when re-activated
	
	private void Start()
	{
		initialPosition = transform.localPosition;
		offset = Vector3.zero;
	}

	private void Update()
	{
		sineTime = apply ? sineTime + Time.deltaTime : 0;
		
		offset.x = period.x != 0 ? amplitude.x * Mathf.Sin((((2 * Mathf.PI) / period.x) - shift.x) * sineTime) : 0;
		offset.y = period.y != 0 ? amplitude.y * Mathf.Sin((((2 * Mathf.PI) / period.y) - shift.y) * sineTime) : 0;
		offset.z = period.z != 0 ? amplitude.z * Mathf.Sin((((2 * Mathf.PI) / period.z) - shift.z) * sineTime) : 0;
		
		targetPosition = apply ? initialPosition + offset : initialPosition;
		
		transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, transitionSmoothness * Time.deltaTime);
	}
}