using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYERMANAGER : Photon.MonoBehaviour {


	public int ThisPlayerID, ThisViewID;

	public GAMELOGIC gamelogicref;

	public int[] HandID;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(photonView.isMine){
			if(Input.GetKeyDown (KeyCode.C)){
				gamelogicref.handevaluatorref.EvaluateHand (gamelogicref.playersData [ThisPlayerID].HandID);
			}
		}

	}

	public void GetHand ()
	{
		if (photonView.isMine) {
			switch (ThisPlayerID) {

			case 0:
				gamelogicref.playersData [ThisPlayerID].HandID [0] = gamelogicref.FULLDECK [0].ID;
				gamelogicref.playersData [ThisPlayerID].HandID [1] = gamelogicref.FULLDECK [1].ID;
				gamelogicref.playersData [ThisPlayerID].HandID [2] = gamelogicref.FULLDECK [2].ID;
				break;

			case 1:
				gamelogicref.playersData [ThisPlayerID].HandID [0] = gamelogicref.FULLDECK [3].ID;
				gamelogicref.playersData [ThisPlayerID].HandID [1] = gamelogicref.FULLDECK [4].ID;
				gamelogicref.playersData [ThisPlayerID].HandID [2] = gamelogicref.FULLDECK [5].ID;
				break;

			case 2:
				gamelogicref.playersData [ThisPlayerID].HandID [0] = gamelogicref.FULLDECK [6].ID;
				gamelogicref.playersData [ThisPlayerID].HandID [1] = gamelogicref.FULLDECK [7].ID;
				gamelogicref.playersData [ThisPlayerID].HandID [2] = gamelogicref.FULLDECK [8].ID;
				break;

			}

			SpawnHand ();
		}
	}

	void SpawnHand ()
	{
		if (photonView.isMine) {
			GameObject card1, card2, card3;
			card1 = (GameObject)Instantiate (gamelogicref.ALLCARDSPREFAB [gamelogicref.playersData [ThisPlayerID].HandID [0]], new Vector3 (gamelogicref.CARDSP [ThisPlayerID].position.x - 0.5f, 0.5f, gamelogicref.CARDSP [ThisPlayerID].position.z),
				gamelogicref.ALLCARDSPREFAB [gamelogicref.playersData [ThisPlayerID].HandID [0]].transform.rotation);


			card2 = (GameObject)Instantiate (gamelogicref.ALLCARDSPREFAB [gamelogicref.playersData [ThisPlayerID].HandID [1]], new Vector3 (gamelogicref.CARDSP [ThisPlayerID].position.x, 1, gamelogicref.CARDSP [ThisPlayerID].position.z),
				gamelogicref.ALLCARDSPREFAB [gamelogicref.playersData [ThisPlayerID].HandID [1]].transform.rotation);

			card3 = (GameObject)Instantiate (gamelogicref.ALLCARDSPREFAB [gamelogicref.playersData [ThisPlayerID].HandID [2]], new Vector3 (gamelogicref.CARDSP [ThisPlayerID].position.x + 0.5f, 1, gamelogicref.CARDSP [ThisPlayerID].position.z),
				gamelogicref.ALLCARDSPREFAB [gamelogicref.playersData [ThisPlayerID].HandID [2]].transform.rotation);


			for(int player = 0; player < gamelogicref.playersData.Length; player++){
				if (player != ThisPlayerID) {
					GameObject FakeCard1, FakeCard2, FakeCard3;
					FakeCard1 = (GameObject)Instantiate (gamelogicref.ReversCardPrefab, new Vector3 (gamelogicref.CARDSP [player].position.x - 0.5f, 1, gamelogicref.CARDSP [player].position.z),
						gamelogicref.ReversCardPrefab.transform.rotation);


					FakeCard2 = (GameObject)Instantiate (gamelogicref.ReversCardPrefab, new Vector3 (gamelogicref.CARDSP [player].position.x, 0.5f, gamelogicref.CARDSP [player].position.z),
						gamelogicref.ReversCardPrefab.transform.rotation);

					FakeCard3 = (GameObject)Instantiate (gamelogicref.ReversCardPrefab, new Vector3 (gamelogicref.CARDSP [player].position.x + 0.5f, 0.5f, gamelogicref.CARDSP [player].position.z),
						gamelogicref.ReversCardPrefab.transform.rotation);

					gamelogicref.playersData[player].FakeCards = new GameObject[3];
					gamelogicref.playersData [player].FakeCards [0] = FakeCard1;
					gamelogicref.playersData [player].FakeCards [1] = FakeCard2;
					gamelogicref.playersData [player].FakeCards [2] = FakeCard2;

				}
			}

			//gamelogicref.handevaluatorref.EvaluateHand (gamelogicref.playersData [ThisPlayerID].HandID);
		}
	}

}
