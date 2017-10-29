using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class MainMenuManager : MonoBehaviour {


		public string gameVersion = "1.0";


		public Text CoStateText, PlayerCountText;
		public bool checkGameStart;

		public GameObject MainButtons;

		public GameObject PlayButton, HerosParent;

	public Canvas MainCanvas, RegisterCanvas;

	public InputField LocalUsernameIF;

	public Text UsernameText, MoneyText;

	public GameObject MainUI, ShopUI, ProfileUI, RewardsUI, RateUI, SettingsUI, RoomUI;
	public GameObject cashItemsPanel, DiverItemsPanel;

		public enum MatchFilters {

			None,
			NORMAL,

		}

		public MatchFilters currentMacthFilter;

		void Start () {
		if(PlayerPrefs.HasKey ("USERNAME")){
			LoadMainUI ();
		} else {
			MainCanvas.enabled = false;
			RegisterCanvas.enabled = true;
		}
			PhotonNetwork.ConnectUsingSettings (gameVersion);
			PhotonNetwork.automaticallySyncScene = true;

		}

		void CheckGameStart ()
		{
			if(PhotonNetwork.room.PlayerCount == PhotonNetwork.room.MaxPlayers){
				PhotonNetwork.room.IsOpen = false;
				PhotonNetwork.LoadLevel (1);
			}
		}

		void OnConnectedToMaster ()
		{
			print ("connected to master");

		}

	public void ValidateLocalRegister ()
	{
		if(LocalUsernameIF.text != ""){

			PlayerPrefs.SetString ("USERNAME", LocalUsernameIF.text);
			PlayerPrefs.SetInt ("MONEY", 100000);

			LoadMainUI ();

		}
	}

	public void HOME ()
	{

		LoadMainUI ();

	}

	public void DisplayShop ()
	{
		MainUI.SetActive (false);
		ShopUI.SetActive (true);
		DisplayCashPanelItems ();
	}

	public void DisplayProfile ()
	{
		MainUI.SetActive (false);
		ProfileUI.SetActive (true);
	}

	public void DisplayRewards ()
	{
		MainUI.SetActive (false);
		RewardsUI.SetActive (true);
	}

	public void DisplayRate ()
	{
		MainUI.SetActive (false);
		RateUI.SetActive (true);
	}

	public void DisplaySettings ()
	{
		MainUI.SetActive (false);
		SettingsUI.SetActive (true);
	}

	public void DisplayCashPanelItems ()
	{

		DiverItemsPanel.SetActive (false);
		cashItemsPanel.SetActive (true);

	}

	public void DisplayDiversPanelItems ()
	{

		cashItemsPanel.SetActive (false);
		DiverItemsPanel.SetActive (true);

	}

	void LoadMainUI ()
	{
		ShopUI.SetActive (false);
		ProfileUI.SetActive (false);
		RewardsUI.SetActive (false);
		RateUI.SetActive (false);
		SettingsUI.SetActive (false);
		MainUI.SetActive (true);

		RegisterCanvas.enabled = false;
		MainCanvas.enabled = true;

		UsernameText.text = PlayerPrefs.GetString ("USERNAME");
		MoneyText.text = PlayerPrefs.GetInt ("MONEY") + " ₹";


	}

		public void PLAY_NORMAL ()
		{
		currentMacthFilter = MatchFilters.NORMAL;
			PhotonHashtable NORMAL = new PhotonHashtable () {{"NORMAL", 1}};
		PhotonNetwork.JoinRandomRoom (NORMAL, (byte)3);
		MainUI.SetActive (false);


		}
		void OnPhotonRandomJoinFailed ()
		{
			byte expectedMaxPlayers;
			RoomOptions roomOptions = new RoomOptions ();

			switch(currentMacthFilter)
			{

		case MatchFilters.NORMAL:
				expectedMaxPlayers = 3;
				roomOptions.maxPlayers = expectedMaxPlayers;
			roomOptions.customRoomProperties = new PhotonHashtable () {{"NORMAL", 1}};
			roomOptions.customRoomPropertiesForLobby = new string[] {"NORMAL"};

				PhotonNetwork.CreateRoom (null, roomOptions, TypedLobby.Default);
				break;

			}
		}

		void Update () {
		CoStateText.text = PhotonNetwork.connectionStateDetailed.ToString ();


		// TODO DELETE

		if(Input.GetKeyDown (KeyCode.R)){
			PlayerPrefs.DeleteAll ();
		}

		//TODO 
			if(checkGameStart){
				CheckGameStart ();
			}

			if(PhotonNetwork.inRoom){
				PlayerCountText.text = "Waiting for more players : " + PhotonNetwork.room.PlayerCount + " / " + PhotonNetwork.room.MaxPlayers;
			}

		}


		void OnJoinedRoom ()
		{


		RoomUI.SetActive (true);

			if (PhotonNetwork.room.PlayerCount == 1) {
				PhotonHashtable Player = new PhotonHashtable () { { "player", "P1" } };
				PhotonNetwork.player.SetCustomProperties (Player);
				PhotonNetwork.player.NickName = "Player 1";
			} else if (PhotonNetwork.room.PlayerCount == 2) {
				PhotonHashtable Player = new PhotonHashtable () { { "player", "P2" } };
				PhotonNetwork.player.SetCustomProperties (Player);
				PhotonNetwork.player.NickName = "Player 2";
			} else if (PhotonNetwork.room.PlayerCount == 3) {
				PhotonHashtable Player = new PhotonHashtable () { { "player", "P3" } };
				PhotonNetwork.player.SetCustomProperties (Player);
				PhotonNetwork.player.NickName = "Player 3";
			} 

			if(PhotonNetwork.isMasterClient){
				print ("you are master client, waiting for full amount of players to start the game");
				checkGameStart = true;
			}




	}

}
