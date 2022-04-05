using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;

public class drag : MonoBehaviour, IDragHandler, IEndDragHandler {
    private GameObject currentObject;
    private Vector3 defaultPosition;

    private turns turns;
    private enemy enemy;
    private player player;

    public int damage,defence,heal,manaCost;
    public int cardType;

    public GameObject effect;
    public float enemyPosX, enemyPosY;
    public float playerPosX, playerPosY;
    public float enemyRotX, enemyRotY, enemyRotZ;
    public float playerRotX, playerRotY;

    private GameObject outsideCardslots,insideCardslots;

    private AudioSource soundEffects;
    public AudioClip thisCardSound;

    void Start() {
        currentObject = this.gameObject;
        defaultPosition = this.gameObject.transform.position;

        turns = GameObject.Find("End Turn").GetComponent<turns>();
        enemy = GameObject.Find("enemy").GetComponent<enemy>();
        player = GameObject.Find("player").GetComponent<player>();

        outsideCardslots = GameObject.FindGameObjectWithTag("canvas");
        insideCardslots = GameObject.FindGameObjectWithTag("cardslots");

        soundEffects = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioSource>();
    }

    void Update() {
        //manually activate cards, for enemy turn, on player side
        if (turns.enemyTurn == true) {
            if (this.transform.localPosition.x == -715 & this.transform.localPosition.y == 130) {
                if (this.gameObject.tag == "attackCard") { player.attackPlayer(damage); }
                if (this.gameObject.tag == "defenceCard") { player.defendPlayer(defence,cardType); }
                if (this.gameObject.tag == "healCard") { player.healPlayer(heal, cardType); }
                Instantiate(effect, new Vector3(playerPosX, playerPosY, 0), Quaternion.Euler(playerRotX, playerRotY, enemyRotZ));
                soundEffects.PlayOneShot(thisCardSound);
                Destroy(this.gameObject);
            }
            if (this.transform.localPosition.x == 715 & this.transform.localPosition.y == 130) {
                if (this.gameObject.tag == "healCard") { enemy.healEnemy(15,cardType); }
                Instantiate(effect, new Vector3(enemyPosX, enemyPosY, 0), Quaternion.Euler(enemyRotX, enemyRotY, enemyRotZ));
                soundEffects.PlayOneShot(thisCardSound);
                Destroy(this.gameObject);
            }
        }

    }

    public void OnEndDrag(PointerEventData eventData) {
        //Player side
        if(this.transform.localPosition.x <= -450 & this.transform.localPosition.y >= -75 & player.playerMana >= manaCost) {
            if (this.gameObject.tag == "attackCard") { player.attackPlayer(damage); }
            if(this.gameObject.tag == "defenceCard") { player.defendPlayer(defence, cardType); }
            if (this.gameObject.tag == "healCard") { player.healPlayer(heal, cardType); }
            Instantiate(effect, new Vector3(playerPosX, playerPosY, 0), Quaternion.Euler(playerRotX, playerRotY, enemyRotZ));
            soundEffects.PlayOneShot(thisCardSound);
            player.playerMana = player.playerMana - manaCost;
            Destroy(this.gameObject);
        }
        //enemy side
        else if (this.transform.localPosition.x >= 450 & this.transform.localPosition.y >= -75 & player.playerMana >= manaCost) {
            if (this.gameObject.tag == "attackCard") { enemy.attackEnemy(damage); }
            if (this.gameObject.tag == "defenceCard") { enemy.defendEnemy(defence); }
            if (this.gameObject.tag == "healCard") { enemy.healEnemy(heal,cardType); }
            Instantiate(effect, new Vector3(enemyPosX, enemyPosY, 0), Quaternion.Euler(enemyRotX,enemyRotY,enemyRotZ));
            soundEffects.PlayOneShot(thisCardSound);
            player.playerMana = player.playerMana - manaCost;
            Destroy(this.gameObject);
        }
        //if not dragged to either side
        else {
            currentObject.transform.position = defaultPosition;
            currentObject.transform.SetParent(insideCardslots.transform,false);
        }
    }

    public void OnDrag(PointerEventData eventData) {
        //drag and move card
        currentObject.transform.SetParent(outsideCardslots.transform, false);
        currentObject.transform.position = Input.mousePosition;
        
    }

}

