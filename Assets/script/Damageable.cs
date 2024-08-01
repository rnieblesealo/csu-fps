using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
	public int currentHealth;
	public int maxHealth;
	
	private Crosshair crosshair;
	
	public void TakeDamage(int amt)
	{
		if (amt <= 0)
		{
			return;
		}

		currentHealth -= amt;
		
		if (currentHealth < 0)
		{
			currentHealth = 0;
		}
		
		crosshair.ShowHitmarker();
	}
	
	public void Heal(int amt)
	{
		if (amt <= 0)
		{
			return;
		}
		
		currentHealth += amt;
		
		if (currentHealth > maxHealth)
		{
			currentHealth = maxHealth;
		}
	}

	private void Awake()
	{
		crosshair = FindObjectOfType<Crosshair>();
	}
}
