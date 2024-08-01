using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
	[Header("Traits")]
	[SerializeField] private LayerMask hittable;
	[SerializeField] private float fireRate;
	[SerializeField] private float reloadDuration;
	[SerializeField] private float hitscanLength;
	[SerializeField] private int damage;
	[SerializeField] private int maxAmmo;
	[SerializeField] private int maxReserve;

	[HideInInspector] public int currentAmmo;
	[HideInInspector] public int currentReserve;

	[Header("Components")]
	[SerializeField] private Transform cam;
	[SerializeField] private Animator armsAnim;
	[SerializeField] private Animator gunAnim;
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private ParticleSystem[] muzzleFlash;
	[SerializeField] private AudioClip[] soundEffects;
	
	private Coroutine activeState;

	private void CastShot()
	{
		Ray hitscan = new(cam.transform.position, cam.transform.forward);
		
		if (Physics.Raycast(hitscan, out RaycastHit hitInfo, hitscanLength, hittable))
		{			
			Damageable d = hitInfo.transform.GetComponent<Damageable>();
			
			if (d)
			{
				d.TakeDamage(damage);
			}
		}
	}

	private void PlaySoundEffect(int index)
	{
		if (soundEffects.Length == 0 ||index >= soundEffects.Length || index < 0)
		{
			return;
		}
		
		audioSource.PlayOneShot(soundEffects[index]);
	}

	private IEnumerator Shoot()
	{
		currentAmmo--;
		
		CastShot();
		
		gunAnim.Play("shoot", -1, 0);
		armsAnim.Play("shoot", -1, 0);
		
		foreach (ParticleSystem s in muzzleFlash)
		{
			s.Play();
		}
		
		PlaySoundEffect(0);
		
		yield return new WaitForSeconds(60 / fireRate);
		
		foreach (ParticleSystem s in muzzleFlash)
		{
			s.Stop();
		}
		
		activeState = null;
		yield break;
	}
	
	private IEnumerator Reload()
	{
		gunAnim.Play("reload", -1, 0);
		armsAnim.Play("reload", -1, 0);
				
		yield return new WaitForSeconds(reloadDuration);
		
		int neededAmmo = maxAmmo - currentAmmo;
		
		if (currentReserve >= neededAmmo)
		{
			currentReserve -= neededAmmo;
			currentAmmo += neededAmmo;
		}
		
		else
		{
			currentAmmo += currentReserve;
			currentReserve = 0;
		}
		
		activeState = null;
		yield break;
	}
	
	private void Start()
	{
		currentAmmo = maxAmmo;
		currentReserve = maxReserve;
	}

	private void Update()
	{
		Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.red, 0, false);
		
		if (Input.GetKey(KeyCode.Mouse0) && activeState == null && currentAmmo > 0)
			activeState = StartCoroutine(Shoot());
		
		if (Input.GetKeyDown(KeyCode.R) && activeState == null && currentReserve > 0 && currentAmmo < maxAmmo)
			activeState = StartCoroutine(Reload());
	}
}
