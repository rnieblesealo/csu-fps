using UnityEngine;

public class SceneController : MonoBehaviour
{
	public static SceneController i;
	
	private void Awake()
	{
		if (i != null && i != this)
		{
			Destroy(this);
		}
		
		else
		{
			i = this;
		}
	}
	
	public AudioSource globalAudio;
}
