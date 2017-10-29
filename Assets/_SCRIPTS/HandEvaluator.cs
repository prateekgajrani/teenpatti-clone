using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEvaluator : MonoBehaviour {

	public GAMELOGIC gameLogicRef;



	public void EvaluateHand (int[] HANDID)
	{
		print (HANDID[0] + "&" + HANDID[1]);
		if(PAIR (HANDID)){
			gameLogicRef.CurrentValueText.text = "PAIR";
		} else {
			gameLogicRef.CurrentValueText.text = "NOTHING";
		}
	}

	public bool PAIR (int[] HANDID)
	{

		switch(gameLogicRef.curPhase)
		{

		case GAMELOGIC.PHASE.PREFLOP:
			int CardValue1 = GetCardValue (HANDID [0]);
			int cardValue2 = GetCardValue (HANDID [1]);
			print (CardValue1);
			print (cardValue2);
			if(CardValue1 == cardValue2){
				return true;
			} else {
				return false;
			}
			break;

		default:

			return false;

		}
	}

	int GetCardSum (int CARDID)
	{
		switch(gameLogicRef.FULLDECK[CARDID].thisSuit)
		{
		case GAMELOGIC.CARD.SUIT.HEARTS:
			return 1;
			break;

		case GAMELOGIC.CARD.SUIT.SPADES:
			return 2;
			break;

		case GAMELOGIC.CARD.SUIT.DIAMONDS:
			return 3;
			break;

		case GAMELOGIC.CARD.SUIT.CLUBS:
			return 4;
			break;

		default:
			return -1;

		}
	}

	int GetCardValue (int CARDID)
	{
		switch(gameLogicRef.FULLDECK[CARDID].thisValue)
		{
		case GAMELOGIC.CARD.VALUE.TWO:
			return 2;
			break;

		case GAMELOGIC.CARD.VALUE.THREE:
			return 3;
			break;

		case GAMELOGIC.CARD.VALUE.FOUR:
			return 4;
			break;

		case GAMELOGIC.CARD.VALUE.FIVE:
			return 5;
			break;

		case GAMELOGIC.CARD.VALUE.SIX:
			return 6;
			break;

		case GAMELOGIC.CARD.VALUE.SEVEN:
			return 7;
			break;

		case GAMELOGIC.CARD.VALUE.EIGHT:
			return 8;
			break;

		case GAMELOGIC.CARD.VALUE.NINE:
			return 9;
			break;

		case GAMELOGIC.CARD.VALUE.TEN:
			return 10;
			break;

		case GAMELOGIC.CARD.VALUE.JACK:
			return 11;
			break;

		case GAMELOGIC.CARD.VALUE.QUEEN:
			return 12;
			break;

		case GAMELOGIC.CARD.VALUE.KING:
			return 13;
			break;

		case GAMELOGIC.CARD.VALUE.ACE:
			return 14;
			break;

		default:
			return -1;
		}
	}


}
