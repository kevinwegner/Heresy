﻿using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

[System.Serializable]
//DeckManager, probably have to build the fucking inventory again and modify
public class DeckManager : MonoBehaviour {

    public List<Card> deck = new List<Card>();
   
    int libCount;
    int deckCount;
    [SerializeField]
    int maxDeckCount;
    string deckName;
    string deckLocation;
 
    CardLibrary cardLibrary;

    [NonSerialized]
    public GameObject card;
    

    //Start 
    void Start()
    {
        //Don't know what the fuck I'm doing here, but works. #coding101
        cardLibrary = GameObject.Find("CardLibary").GetComponent<CardLibrary>();
        libCount = cardLibrary.cardList.Count;
        deckLocation = (Application.dataPath + "/Resources/deck.txt");

        //Debugging, sets up deck with one copy of each card in the CardLibrary
        /*for (int i = 0; i < cardCount; i++)
        {
            deck.Add(new Card());

            //used to add Cards individually; handle through buttons via PlayerController
            //if (deck[i].GetID() == -1)
            //{
                deck[i] = cardLibrary.cardList[i];
                Debug.Log(deck[i].GetName());
            //}            
        }*/

        SpawnCard();
        Debug.Log(deck.Count);

        //SaveDeck();
        //LoadDeck();
    }

    public void AddCard(string name) {

        Cultist card;

        if (deckCount == maxDeckCount)
            return;

        for(int i = 0;i < libCount;i++) {
            if(cardLibrary.cardList[i].GetName().Equals(name)) {

                card = (Cultist)cardLibrary.cardList[i];
                deck.Add(card);
                deckCount++;
                Debug.Log(deck[deck.Count - 1].GetName());
            }
        }          
    }

    public void SpawnCard() {
        Vector3 spawnPos;
        float x = -1.5f;
        float y = -1.5f;
        int counter = -1;

        GameObject card;

        for(int i = 0; i < 5;i++){
            for(int j = 0;j < 3;j++) {

                counter++;

                card = (GameObject)Resources.Load("Prefabs/" + cardLibrary.cardList[counter].GetName());
                spawnPos = new Vector3(x+(i * 2f),y+(j* 2.5f), 0);
                GameObject cardSpawn = (GameObject)Instantiate(card, spawnPos, Quaternion.identity);

                if (counter >= (cardLibrary.cardList.Count - 1))
                {
                    counter = -1;
                }
            }
        }
    }

    public void SaveDeck()
    {
        StringBuilder builder = new StringBuilder();

        Debug.Log(deckLocation);

        for(int i = 0; i < deck.Count; i++)
        {
            builder.Append(deck[i].GetID() + ",");
        }

        File.WriteAllText(deckLocation, builder.ToString());
        builder.Remove(0, builder.Length);
       
        if (File.Exists(deckLocation))
        {
            Debug.Log("File saved");
        }
    }

    //Not working anymore, changed SaveDeck() method
    /*public void LoadDeck()
    {
        StringBuilder builder = new StringBuilder();
        TextAsset textFile = (TextAsset)Resources.Load("deck", typeof(TextAsset));
        builder.Append(textFile.text);

        int index = 0;
        int start = 0;
        int counter = 0;
        //Uncomment to check for file content
        Debug.Log(builder);

        for(int i = 0; i < builder.Length; i++)
        {
            index++;

            string id = "";
            string name = "";
            string textureID = "";

            if ((builder[i]).Equals(','))
            {
                Debug.Log(counter);

                if (counter == 0)
                {
                    id = builder.ToString(start, index);
                    Debug.Log(id);
                }

                if (counter == 1)
                {
                    name = builder.ToString(start, index);
                    Debug.Log(name);
                }

                if (counter == 2)
                {
                    textureID = builder.ToString(start, index);
                    Debug.Log(textureID);
                }

                counter++;
                start = index;
                //Debug.Log(",");
            }

            if ((builder[i]).Equals(';'))
            {
                builder.Remove(0, index);

                counter = 0;
                start = 0;
                index = 0;
                //Debug.Log("end");
                //Debug.Log(";");
            }
        }

    }*/

    public void DeleteDeck()
    {
        File.Delete(deckLocation);
    }
}
