using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour {
    public Text txtPlayerHealth;
    public int playerHealth;
    public int playerDefence;

    public int playerMana,totalPlayerMana;
    public Text txtPlayerMana;

    public Transform healthbar,defencebar;
    float fPlayerHealth,fPlayerDefence;

    private enemy enemy;
    private combatlog combatlog;
    public GameObject gameLost;

    //card active affects
    public bool ironSkin;
    public int ironSkinDuration = 3;

    public bool thornmail;
    public int thornmailDuration = 5;

    public bool healingAura;
    public int healingAuraDuration = 10;

    public bool hornOfTheAncients;
    public int hotaDuration = 10;

    public bool mysticalHappiness;
    public int mysticalHappinessDuration = 2;

    // Start is called before the first frame update
    void Start() {
        enemy = GameObject.Find("enemy").GetComponent<enemy>();
        combatlog = GameObject.Find("combatlog").GetComponent<combatlog>();

        playerHealth = 100;
        playerDefence = 0;
        playerMana = 3;
        totalPlayerMana = playerMana;
    }

    // Update is called once per frame
    void Update() {
        //playerStats.text = "health: " + playerHealth +"\nshield: "+playerDefence;
        txtPlayerHealth.text = "" + playerHealth;
        //txtPlayerDefence.text = "" + playerDefence;

        fPlayerHealth = playerHealth / 1;
        fPlayerDefence = playerDefence / 1;
        healthbar.localScale = new Vector3(fPlayerHealth / 100 * 1, 1f);
        defencebar.localScale = new Vector3(fPlayerDefence / 100 * 1, 1f);

        if (playerHealth > 100) playerHealth = 100; if (playerHealth <= 0) { gameLost.SetActive(true); combatlog.gameObject.SetActive(false); }
        if (playerDefence > 100) playerDefence = 100; if (playerDefence < 0) playerDefence = 0;
        if (playerMana > totalPlayerMana) playerMana = totalPlayerMana;

        txtPlayerMana.text = "MANA:\n" + playerMana + "/"+totalPlayerMana;

        if (ironSkinDuration <= 0) ironSkin = false;
        if (thornmailDuration <= 0) thornmail = false;
        if (healingAuraDuration <= 0) { healingAura = false; }
        if (hotaDuration <= 0) hornOfTheAncients = false;
        if (mysticalHappinessDuration <= 0) mysticalHappiness = false;
    }

    public void attackPlayer(int damage) {

        //thornmail, reflects 20% of dmg taken
        float reflectedDmg;
        if (thornmail) {
            reflectedDmg = damage * 0.2f;
            int reflectDmgAmount = (int)reflectedDmg;
            enemy.attackEnemy(reflectDmgAmount);
            combatlog.SendMessageToChat("reflected "+reflectDmgAmount+"dmg!");
        }

        //iron skin, reducing damage by 20%
        float reducedDmg;
        if (ironSkin) {
            reducedDmg = damage * 0.2f;
            int reduceDmgBy = (int)reducedDmg;
            damage = damage - reduceDmgBy;
            combatlog.SendMessageToChat("reduced damage by " + reduceDmgBy);
        }

        //shield / armour mechanic, dmg is done to armour (if any) first, then remainding damage is done to health
        int remainingDmg = 0;
        if (playerDefence > 0) {
            remainingDmg = damage - playerDefence;
            playerDefence = playerDefence - damage;
            if(remainingDmg > 0) playerHealth = playerHealth - remainingDmg;
        }

        //if no shield / armour, damage health normally
        else { playerHealth = playerHealth - damage; }
        
        Debug.Log("damage done " + damage);
        combatlog.SendMessageToChat("("+damage+"dmg) <- enemy");
    }

    public void defendPlayer(int shield, int type) {
        if (type == 1) { playerDefence = playerDefence + shield; combatlog.SendMessageToChat("+" + shield + " defence"); }
        if (type == 2) { ironSkin = true; ironSkinDuration = 3; combatlog.SendMessageToChat("+20% dmg reduction"); }
        if (type == 3) { thornmail = true; thornmailDuration = 5; combatlog.SendMessageToChat("+20% reflection"); }
    }

    public void healPlayer(int healing, int type) {
        if (type == 4) { healingAura = true; healingAuraDuration = 10; }
        if(type == 5) { hornOfTheAncients = true; hotaDuration = 5; combatlog.SendMessageToChat("+5 dmg output"); }
        if(type == 6) { mysticalHappiness = true; mysticalHappinessDuration = 2; }
        //playerHealth = playerHealth + healing;
    }
}
