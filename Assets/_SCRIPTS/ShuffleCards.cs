using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleCards : MonoBehaviour {

	public GAMELOGIC gamelogicref;

	public void ShuffleFullDeck ()
	{

		Random rdm = new Random ();

		/*
		for (int shuffletimes = 0; shuffletimes < 1000; shuffletimes++){

			for(int i = 0; i < 52; i++){

				int secondchardindex = i + 1;
				tempcard = gamelogicref.FULLDECK [i];
				gamelogicref.FULLDECK [i] = gamelogicref.FULLDECK [secondchardindex];
				gamelogicref.FULLDECK [secondchardindex] = tempcard;

			}
		}
*/


		for(int i = 0; i < 52; i++){


			int rdmindex = Random.Range (0, 51);
			GAMELOGIC.CARD thiscard;
			GAMELOGIC.CARD rdmpickedcard;

			thiscard = gamelogicref.FULLDECK [i];
			rdmpickedcard = gamelogicref.FULLDECK [rdmindex];


			gamelogicref.FULLDECK [i] = rdmpickedcard;
			gamelogicref.FULLDECK [rdmindex] = thiscard;
		}

		for (int player = 0; player < gamelogicref.playersData.Length; player++){

			gamelogicref.playersData [player].PlayerRef.GetHand ();
		}

	}


	public void MakePair ()
	{
		GAMELOGIC.CARD secondcard;
		GAMELOGIC.CARD wantedcard;

		secondcard = gamelogicref.FULLDECK [1];
		wantedcard = gamelogicref.FULLDECK [13];


		gamelogicref.FULLDECK [1] = wantedcard;
		gamelogicref.FULLDECK [13] = secondcard;
	}
}
