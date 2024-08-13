using UnityEngine;

public class MenuManager : MonoBehaviour
{
	public static MenuManager i;
	
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

	[SerializeField] Menu[] menus;

	public void OpenMenu(string name)
	{
		for (int i = 0; i < menus.Length; ++i)
		{
			if (menus[i].menuName == name)
			{
				menus[i].Open();
			}
			
			else if (menus[i].open)
			{
				menus[i].Close();
			}
		}
	}

	public void OpenMenu(Menu menu)
	{
		for (int i = 0; i < menus.Length; ++i)
		{
			if (menus[i].open)
			{
				menus[i].Close();
			}
		}
		
		menu.Open();
	}

	public void CloseMenu(Menu menu)
	{
		menu.Close();
	}
}
