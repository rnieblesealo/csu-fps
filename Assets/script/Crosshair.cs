using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
	[SerializeField] private Image hMarker;
	[SerializeField] private float hMarkTransitionLength;
	[SerializeField] private float hMarkVisibleDuration;
	[SerializeField] [Range(0, 1)] private float hMarkAlpha;
	[SerializeField] private AudioClip hMarkSound;

	private float hMarkShowTimer;	

	public void ShowHitmarker()
	{
		hMarkAlpha = 1;
		hMarkShowTimer = hMarkVisibleDuration;
		
		SceneController.i.globalAudio.PlayOneShot(hMarkSound);
	}

	private void Update()
	{
		if (hMarkShowTimer > 0)
		{
			hMarkShowTimer -= Time.deltaTime;
		}
		
		else
		{
			hMarkAlpha = 0;
		}
		
		hMarker.CrossFadeAlpha(hMarkAlpha, hMarkTransitionLength, false);
	}
}
