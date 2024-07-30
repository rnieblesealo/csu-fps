using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
	[Header("Traits")]
	[SerializeField] private float fireRate;
	[Tooltip("Ideally use the duration of the anim!")] [SerializeField] private float reloadDuration;
	[SerializeField] private int maxAmmo;
	[SerializeField] private int maxReserve;
	[HideInInspector] public int currentAmmo;
	[HideInInspector] public int currentReserve;

	[Header("Components")]
	[SerializeField] private Animator armsAnim;
	[SerializeField] private Animator gunAnim;
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private ParticleSystem[] muzzleFlash;
	[SerializeField] private AudioClip[] soundEffects;
	
	private Coroutine activeState;

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
		if (Input.GetKey(KeyCode.Mouse0) && activeState == null && currentAmmo > 0)
			activeState = StartCoroutine(Shoot());
		
		if (Input.GetKeyDown(KeyCode.R) && activeState == null && currentReserve > 0 && currentAmmo < maxAmmo)
			activeState = StartCoroutine(Reload());
	}
}
