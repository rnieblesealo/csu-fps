using TMPro;
using UnityEngine;

public class HealthWidget : MonoBehaviour
{
	public Player player;
	
	[SerializeField] private TextMeshProUGUI healthText; // make this auto-findable!
	
	private void Update()
	{
		healthText.text = player.currentHealth.ToString();
	}
}
