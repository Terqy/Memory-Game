using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {
	public static bool cantTurn = false;

	private int cardValue;

	private bool faceUp;
	private bool matched = false;

	private GameObject manager;

	void Start() {
		cantTurn = false;
		manager = GameObject.FindGameObjectWithTag("Manager");
	}

	public void flipCard ()
	{
		if (!faceUp && !cantTurn) {
			GetComponent<Image>().sprite = manager.GetComponent<Manager>().cardFront[cardValue - 1];
			faceUp = true;
		}
	}

	public void falseCheck() {
		StartCoroutine(pause());
	}

	IEnumerator pause ()
	{
		yield return new WaitForSeconds (.5f);
		if (faceUp && !matched) {
			GetComponent<Image>().sprite = manager.GetComponent<Manager>().cardBack;
		}
		faceUp = false;
		cantTurn = false;
	}

	public int CardValue {
		get{ return cardValue; }
		set{ cardValue = value; }
	}

	public bool FaceUp {
		get{ return faceUp; }	
	}

	public bool Matched {
		set{ matched = value; }
	}
}
