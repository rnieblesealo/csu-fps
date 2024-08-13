using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{
	public static Launcher i;
	
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
		
	[SerializeField] private TMP_InputField roomNameInputField;
	[SerializeField] private TMP_Text errorText;
	[SerializeField] private TMP_Text roomText;
	[SerializeField] private Transform roomListContent;
	[SerializeField] private GameObject roomListItemPrefab;
	[SerializeField] private Transform playerListContent;
	[SerializeField] private GameObject playerListItemPrefab;
	[SerializeField] private GameObject startGameButton;
	
	private void Start()
	{
		PhotonNetwork.ConnectUsingSettings();
		MenuManager.i.OpenMenu("loading");
	}

	public override void OnConnectedToMaster()
	{
		Debug.Log("Connected to master");
		PhotonNetwork.JoinLobby();
		
		// sync scene loading between clients
		PhotonNetwork.AutomaticallySyncScene = true;
	}

	public override void OnJoinedLobby()
	{
		Debug.Log("Joined lobby");
		MenuManager.i.OpenMenu("title");
		
		PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString();
	}

	public void CreateLobby()
	{
		if (string.IsNullOrEmpty(roomNameInputField.text))
		{
			return;
		}
		
		// this calls back onjoinedroom if successful, oncreateroomfailed if not
		PhotonNetwork.CreateRoom(roomNameInputField.text);
		
		MenuManager.i.OpenMenu("loading");
	}
	
	public override void OnJoinedRoom()
	{
		MenuManager.i.OpenMenu("room");
		roomText.text = roomNameInputField.text;
		
		Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;
		
		// clear prev. playerlist
		foreach (Transform child in playerListContent)
		{	
			Destroy(child.gameObject);
		}
		
		// make playerlist
		for (int i = 0; i < players.Count(); ++i)
		{
			Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
		}
		
		// room starter is master client
		startGameButton.SetActive(PhotonNetwork.IsMasterClient);
	}

	public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
	{
		// photon has host migration; someone else will become client if curr. one leaves
		startGameButton.SetActive(PhotonNetwork.IsMasterClient);
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		MenuManager.i.OpenMenu("error");
		errorText.text = "Room Creation Failed: " + message;
	}
	
	public void StartGame()
	{
		PhotonNetwork.LoadLevel(1);
	}

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
		MenuManager.i.OpenMenu("loading");
	}

	public void JoinRoom(RoomInfo info)
	{
		PhotonNetwork.JoinRoom(info.Name);
		MenuManager.i.OpenMenu("loading");
	}

	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		// remake list on every update
		foreach (Transform t in roomListContent)
		{
			Destroy(t.gameObject);
		}
		
		for (int i = 0; i < roomList.Count; ++i)
		{
			// if any players are removed, they aren't cleared from list; this bool updates instead
			if (roomList[i].RemovedFromList)
				continue;
			
			Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
		}
	}

	public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
	{
		Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
	}
}
