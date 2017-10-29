using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GAMELOGIC : Photon.MonoBehaviour {


	public enum PHASE{

		PREFLOP, 
		FLOP,
		TURN,
		RIVER


	}

	public PHASE curPhase;

	public ShuffleCards shufflecardref;
	public HandEvaluator handevaluatorref;

	const int CARDS_AMOUNT = 52;
	public int TAPIS;
	public Transform FullDeckSP;
	public GameObject ReversCardPrefab;
	public float MargeLeftPile;
	public float DelayBetweenSpawnCard;

	public Transform[] CARDSP;
	public GameObject[] ALLCARDSPREFAB;

	[System.Serializable]
	public class CARD {

		public enum SUIT {

			HEARTS,
			SPADES,
			DIAMONDS,
			CLUBS

		}

		public enum VALUE {

			TWO = 2,
			THREE,
			FOUR, 
			FIVE, 
			SIX, 
			SEVEN, 
			EIGHT, 
			NINE, 
			TEN, 
			JACK, 
			QUEEN, 
			KING, 
			ACE

		}


		public SUIT thisSuit;
		public VALUE thisValue;

		public int ID;
	}

	public CARD[] FULLDECK;

	public GameObject PlayerPrefab;

	public int LocalPlayerID, LocalViewID;

	[System.Serializable]
	public class PLAYERSDATA {

		public string Name;

		public GameObject PlayerGO;
		public PLAYERMANAGER PlayerRef;

		public GameObject SideSlot;
		public Text UsernameText, TapisText;

		public int curTapis;

		public int[] HandID;

		public GameObject[] FakeCards;

	}

	public PLAYERSDATA[] playersData;

	public GameObject[] Sideslots;
	public Text[] UsernameText, TapisText;

	public Text CurrentValueText;

	void Start () {
		TAPIS = PlayerPrefs.GetInt ("MONEY");

		if(PhotonNetwork.isMasterClient){
			photonView.RPC ("InitializeAllCards", PhotonTargets.AllBuffered);
		}

		InitializePlayer ();
	}

	void Update () {
		
	}

	void InitializePlayer ()
	{
		if(PhotonNetwork.player.CustomProperties.ContainsValue ("P1")){
			GameObject thisplayer;
			thisplayer = (GameObject)PhotonNetwork.Instantiate ("Prefabs/" + PlayerPrefab.name, Vector3.zero, PlayerPrefab.transform.rotation, 0);
			PhotonNetwork.player.NickName = "Player1";
			LocalPlayerID = 0;
			LocalViewID = 1001;
			photonView.RPC ("InitializePlayerToServer", PhotonTargets.All, LocalPlayerID, LocalViewID, PhotonNetwork.player.NickName);
		} else if(PhotonNetwork.player.CustomProperties.ContainsValue ("P2")){
			GameObject thisplayer;
			thisplayer = (GameObject)PhotonNetwork.Instantiate ("Prefabs/" + PlayerPrefab.name,Vector3.zero, PlayerPrefab.transform.rotation, 0);
			PhotonNetwork.player.NickName = "Player2";
			LocalPlayerID = 1;
			LocalViewID = 2001;
			photonView.RPC ("InitializePlayerToServer", PhotonTargets.All, LocalPlayerID, LocalViewID, PhotonNetwork.player.NickName);
		} else if(PhotonNetwork.player.CustomProperties.ContainsValue ("P3")){
			GameObject thisplayer;
			thisplayer = (GameObject)PhotonNetwork.Instantiate ("Prefabs/" + PlayerPrefab.name, Vector3.zero, PlayerPrefab.transform.rotation, 0);
			PhotonNetwork.player.NickName = "Player3";
			LocalPlayerID = 2;
			LocalViewID = 3001;
			photonView.RPC ("InitializePlayerToServer", PhotonTargets.All, LocalPlayerID, LocalViewID, PhotonNetwork.player.NickName);
		}
	}

	[PunRPC]
	void InitializePlayerToServer (int PlayerID, int PlayerViewID, string Username)
	{
		GameObject thisplayer = PhotonView.Find (PlayerViewID).gameObject;
		playersData [PlayerID].PlayerGO = thisplayer;
		playersData [PlayerID].Name = Username;
		playersData [PlayerID].curTapis = TAPIS;
		playersData [PlayerID].SideSlot = Sideslots [PlayerID];
		playersData [PlayerID].UsernameText = UsernameText [PlayerID];
		playersData [PlayerID].TapisText = TapisText [PlayerID];

		PLAYERMANAGER playerref = thisplayer.GetComponent <PLAYERMANAGER> ();
		playersData [PlayerID].PlayerRef = playerref;
		playerref.ThisPlayerID = PlayerID;
		playerref.ThisViewID = PlayerViewID;
		playerref.gamelogicref = this;

		playersData [PlayerID].UsernameText.text = Username;
		playersData [PlayerID].TapisText.text = "" + playersData [PlayerID].curTapis;

		if(PhotonView.Find (PlayerViewID).isMine){

			playersData [PlayerID].SideSlot.GetComponent <Image> ().color = Color.cyan;

		}

	}


	[PunRPC]
	IEnumerator InitializeAllCards ()
	{
		FULLDECK = new CARD[CARDS_AMOUNT];

		int curcard = 0;

		foreach(CARD.SUIT suit in Enum.GetValues (typeof(CARD.SUIT))){

			foreach(CARD.VALUE value in Enum.GetValues(typeof(CARD.VALUE))){

				FULLDECK [curcard] = new CARD { thisSuit = suit, thisValue = value };
				FULLDECK [curcard].ID = curcard;
				curcard++;
			}
		}
		yield return new WaitForSeconds (1);
		shufflecardref.ShuffleFullDeck ();
		//shufflecardref.MakePair ();

		float leftmarge = 0;
		for(int i = 0; i < 52; i++){
			GameObject Cardgo;
			Cardgo = (GameObject)Instantiate (ReversCardPrefab, new Vector3(FullDeckSP.transform.position.x-leftmarge, FullDeckSP.transform.position.y, FullDeckSP.transform.position.z+leftmarge), FullDeckSP.transform.rotation);
			leftmarge += MargeLeftPile;

			yield return new WaitForSeconds (DelayBetweenSpawnCard); 
		}


	}

	void SpawnFakeCards ()
	{

		foreach(Transform sp in CARDSP){

			GameObject card1, card2;
			card1 = (GameObject)Instantiate (ReversCardPrefab, new Vector3(sp.position.x-0.2f, 1, sp.position.z), ReversCardPrefab.transform.rotation);

			card2 = (GameObject)Instantiate (ReversCardPrefab, new Vector3(sp.position.x+0.2f, 0.5f, sp.position.z), ReversCardPrefab.transform.rotation);
		}

	}


}
