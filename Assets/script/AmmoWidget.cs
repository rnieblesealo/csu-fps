using TMPro;
using UnityEngine;

public class AmmoWidget : MonoBehaviour
{
	public Gun playerWeapon; // make this auto-findable!
	
	[SerializeField] private TextMeshProUGUI ammoText;

	private void Update()
	{
		ammoText.text = playerWeapon.currentAmmo.ToString() + "/" + playerWeapon.currentReserve.ToString();
	}
}
