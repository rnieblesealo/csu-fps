using Photon.Realtime;
using UnityEngine;
using TMPro;

public class RoomListItem : MonoBehaviour
{
	[SerializeField] TMP_Text text;
	
	public RoomInfo info;
	
	public void SetUp(RoomInfo info)
	{
		this.info = info;
		text.text = info.Name;
	}
	
	public void OnClick()
	{
		Launcher.i.JoinRoom(info);
	}
}
