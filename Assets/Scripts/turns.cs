using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turns : MonoBehaviour {
    public bool enemyTurn, playerTurn;

    private player player;
    private enemy enemy;


    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("player").GetComponent<player>();
        enemy = GameObject.Find("enemy").GetComponent<enemy>();

        playerTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void endTurn() {
        if (playerTurn) {
            playerTurn = false;
            enemyTurn = true;
            enemy.attack();
        }
    }
}
