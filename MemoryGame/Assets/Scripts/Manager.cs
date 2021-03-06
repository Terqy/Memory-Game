﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
	public GameObject[] cards;
	public Sprite[] cardFront;
	public int[] values = new int[12];

	public Sprite cardBack;

	public GameObject restartButton;
	public GameObject exitButton;

	public AudioSource matchedCards;

	public Text numberOfPairsText;
	public Text winnerText;
	public Text timerText;

	private int numberOfPairs = 6;
	private int flippedCards;

	private float timeLeft = 60.0f;

	private bool isPlaying;

	void Awake () {
		flippedCards = 0;
		cards = GameObject.FindGameObjectsWithTag("Card");	
		shuffle();
		restartButton.SetActive(false);
		winnerText.gameObject.SetActive(false);
		numberOfPairsText.text = "Number of pairs left: " + numberOfPairs;
		isPlaying = true;
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
		if (isPlaying) {
			timer ();
		}
	}

	void timer() 
	{
		timeLeft -= Time.deltaTime;

		int timeLeftInt = (int)Mathf.RoundToInt(timeLeft);
		timerText.text = "Time left: " + timeLeftInt;

		if (timeLeft < 0) {
			winnerText.text = "You lost try again!";
			winnerText.gameObject.SetActive(true);
			restartButton.SetActive(true);
			isPlaying = false;
		}
	}

	public void checkCards ()
	{
		List<int> cardFace = new List<int> ();

		for (int i = 0; i < cards.Length; i++) {
			if (cards [i].GetComponent<Card> ().FaceUp == true) {
				cardFace.Add (i);
				Debug.Log(cardFace.Count);
			}
		}
		if (cardFace.Count == 2) {
			cardComparison(cardFace);
			numberOfPairsText.text = "Number of pairs left: " + numberOfPairs;
		}
	}

	void cardComparison (List<int> cardFace)
	{
		Card.cantTurn = true;

		if (cards [cardFace [0]].GetComponent<Card> ().CardValue == cards [cardFace [1]].GetComponent<Card> ().CardValue) {
			numberOfPairs--;
			cards[cardFace[0]].GetComponent<Card>().Matched = true;
			cards[cardFace[1]].GetComponent<Card>().Matched = true;
			matchedCards.Play();
			if (numberOfPairs == 0) {
				winnerText.gameObject.SetActive(true);
				restartButton.SetActive(true);
				exitButton.SetActive(true);
				isPlaying = false;
			}
		}

		for (int i = 0; i < cardFace.Count; i++) {
			cards[cardFace[i]].GetComponent<Card>().falseCheck();
		}
	}

	void shuffle ()
	{
		for (int i = values.Length - 1; i > 0; i--) {
			int rnd = Random.Range (0, i);
			int temp = values[i];
			values [i] = values [rnd];
			values [rnd] = temp;
		}

		for (int i = 0; i < cards.Length; i++) {
			cards[i].GetComponent<Card>().CardValue = values[i];
		}
	}

	public void restart ()
	{
		for (int i = 0; i < cards.Length; i++) {
			cards [i].GetComponent<Card> ().Matched = false;
			cards [i].GetComponent<Image> ().sprite = cardBack;
			cards [i].GetComponent<Button>().interactable = true;
			winnerText.gameObject.SetActive(false);
			restartButton.SetActive (false);
			exitButton.SetActive (false);
			numberOfPairs = 6;
			numberOfPairsText.text = "Number of pairs: " + numberOfPairs;
			isPlaying = true;
			timeLeft = 60.0f;
		}
		shuffle();
	}

	public void exit() {
		Application.Quit();
	}

	public int FlippedCards {
		get{return flippedCards;}
		set{flippedCards = value;}
	}
}
