using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
	[Header("Traits")]
	public float fireRate;
	[Tooltip("Ideally use the duration of the anim!")] public float reloadDuration;

	private Arms arms;
	private Animator anim;	

	private Coroutine activeState;

	private IEnumerator Shoot()
	{
		anim.Play("shoot", -1, 0);
		arms.anim.Play("shoot", -1, 0);
		
		yield return new WaitForSeconds(60 / fireRate);
		
		activeState = null;
		yield break;
	}
	
	private IEnumerator Reload()
	{
		anim.Play("reload", -1, 0);
		arms.anim.Play("reload", -1, 0);
		
		yield return new WaitForSeconds(reloadDuration);
		
		activeState = null;
		yield break;
	}
	
	private void Awake()
	{
		arms = GetComponentInParent<Arms>();
		anim = GetComponentInChildren<Animator>();
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.Mouse0) && activeState == null)
			activeState = StartCoroutine(Shoot());
		
		if (Input.GetKeyDown(KeyCode.R) && activeState == null)
			activeState = StartCoroutine(Reload());
	}
}
