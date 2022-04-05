using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour{
    public Text txtEnemyHealth;
    public int enemyHealth;
    public int enemyDefence;

    public Transform healthbar, defencebar;
    float fEnemyHealth, fEnemyDefence;

    private turns turns;
    private player player;
    private combatlog combatlog;
    public GameObject gameWon;

    public GameObject poisonSlice, bladeOfInferno, icicleSpear;
    public GameObject ironSkin, thornmail, iceBarrier;
    public GameObject healingAura, mysticalHappiness, hornOfTheAncients;
    public List<GameObject> cards = new List<GameObject>();

    private GameObject outsideCardslots;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("player").GetComponent<player>();
        outsideCardslots = GameObject.FindGameObjectWithTag("canvas");
        turns = GameObject.Find("End Turn").GetComponent<turns>();
        combatlog = GameObject.Find("combatlog").GetComponent<combatlog>();

        enemyHealth = 100;
        enemyDefence = 0;

        //make poison slice item more common than others, least damaging card
        for (int i = 0; i < 2; i++) { cards.Add(poisonSlice); }
        //make icicle spear 2nd most common, 2nd most damaging card
        for (int i = 0; i < 2; i++) { cards.Add(icicleSpear); }
        //make the most damaging item the least common
        cards.Add(bladeOfInferno);
        
        cards.Add(healingAura);
        //Debug.Log(cards[6]);
        Debug.Log(cards[5]);
        //cards.Add(ironSkin);
        //cards.Add(thornmail);
        //cards.Add(healingAura);
    }

    // Update is called once per frame
    void Update() {
        txtEnemyHealth.text = " " + enemyHealth;

        fEnemyHealth = enemyHealth / 1;
        fEnemyDefence = enemyDefence / 1;
        healthbar.localScale = new Vector3(fEnemyHealth / 100 * 1, 1f);
        defencebar.localScale = new Vector3(fEnemyDefence / 100 * 1, 1f);

        if (enemyHealth > 100) enemyHealth = 100; if (enemyHealth <= 0) { gameWon.SetActive(true); combatlog.gameObject.SetActive(false); }
        if (enemyDefence > 100) enemyDefence = 100; if (enemyDefence < 0) enemyDefence = 0;

    }

    public void attack() {

        StartCoroutine(beginAttack());
    }

    IEnumerator beginAttack() {
        yield return new WaitForSeconds(1);

        //if under the effects of mystical happiness
        if (player.mysticalHappiness) {
            GameObject useHeal = Instantiate(cards[5], new Vector3(0, 0, 0), Quaternion.identity);
            useHeal.GetComponent<Image>().enabled = false;
            useHeal.transform.SetParent(outsideCardslots.transform, false);
            useHeal.transform.localPosition = new Vector3(715, 130);
        }

        //if not
        else {
            GameObject useCard = Instantiate(cards[Random.Range(0, 4)], new Vector3(0, 0, 0), Quaternion.identity);
            useCard.GetComponent<Image>().enabled = false;
            useCard.transform.SetParent(outsideCardslots.transform, false);
            useCard.transform.localPosition = new Vector3(-715, 130);
        }
        yield return new WaitForSeconds(2);

        playerTurn();
    }

    void playerTurn() {
        //increases player's mana cap and current mana
        if (player.totalPlayerMana <= 9) player.totalPlayerMana++;
        if (player.playerMana < player.totalPlayerMana) player.playerMana = player.playerMana + 3;

        //decreses round duration for active effects
        if (player.ironSkin) { player.ironSkinDuration--; /*combatlog.SendMessageToChat("iron skin("+player.ironSkinDuration+")");*/ }
        if (player.thornmail) player.thornmailDuration--;
        if (player.healingAura) {
            player.healingAuraDuration--;
            player.playerHealth += 5;
            combatlog.SendMessageToChat("+5hp");
        }
        if (player.hornOfTheAncients) player.hotaDuration--;
        if (player.mysticalHappiness) player.mysticalHappinessDuration--;

        turns.playerTurn = true;
        turns.enemyTurn = false;
    }

    public void attackEnemy(int damage) {
        if (player.hornOfTheAncients) damage += 5;
        enemyHealth = enemyHealth - damage;

        Debug.Log("damage done " + damage);
        combatlog.SendMessageToChat("player -> (" + damage + "dmg)");
    }

    public void defendEnemy(int shield) {
        enemyDefence = enemyDefence + shield;
    }

    public void healEnemy(int healing, int type) {
        if (type == 6) { player.mysticalHappiness = true; player.mysticalHappinessDuration = 2; }
        if (enemyHealth < 100) {
            enemyHealth = enemyHealth + healing;
        }
    }
}
