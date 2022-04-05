using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawCard : MonoBehaviour {
    //attack cards
    public GameObject poisonSlice, bladeOfInferno, icicleSpear;
    public GameObject RAREpoisonSlice, RAREbladeOfInferno, RAREicicleSpear;
    //defence cards
    public GameObject ironSkin, thornmail, iceBarrier;
    //utility
    public GameObject healingAura, mysticalHappiness, hornOfTheAncients;

    public GameObject playerArea;

    List<GameObject> cards = new List<GameObject>();

    private player player;
    private turns turns;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("player").GetComponent<player>();
        turns = GameObject.Find("End Turn").GetComponent<turns>();

        cards.Add(RAREpoisonSlice);
        cards.Add(RAREbladeOfInferno);
        cards.Add(RAREicicleSpear);

        for (int i = 0; i < 3; i++) {
            cards.Add(poisonSlice);
            cards.Add(bladeOfInferno);
            cards.Add(icicleSpear);
        }

        for (int i = 0; i < 2; i++) {
            cards.Add(ironSkin);
            cards.Add(thornmail);
            cards.Add(iceBarrier);
            cards.Add(healingAura);
            cards.Add(mysticalHappiness);
            cards.Add(hornOfTheAncients);
        }

        GameObject playerCard = Instantiate(poisonSlice, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject playerCard2 = Instantiate(iceBarrier, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject playerCard3 = Instantiate(healingAura, new Vector3(0, 0, 0), Quaternion.identity);
        playerCard.transform.SetParent(playerArea.transform, false);
        playerCard2.transform.SetParent(playerArea.transform, false);
        playerCard3.transform.SetParent(playerArea.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createCard() {  
        if (player.playerMana >= 1 && turns.playerTurn == true) {
            player.playerMana = player.playerMana - 1;
            GameObject playerCard = Instantiate(cards[Random.Range(0, cards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
            playerCard.transform.SetParent(playerArea.transform, false);
        } 
    }
}
