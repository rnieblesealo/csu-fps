using Photon.Pun;
using UnityEngine;
using TMPro;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
	[SerializeField] TMP_Text text;
	
	private Photon.Realtime.Player player;

	public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
	{
		if (player == otherPlayer)
		{
			Destroy(gameObject);
		}
	}

	public override void OnLeftRoom()
	{
		Destroy(gameObject);
	}

	public void SetUp(Photon.Realtime.Player player)
	{
		this.player = player;
		text.text = player.NickName;
	}
}
