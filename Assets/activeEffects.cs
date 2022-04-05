using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeEffects : MonoBehaviour {
    private player player;

    public GameObject ironSkin, thornmail, healingAura, hornOfTheAncients, mysticalHappiness;


    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("player").GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.ironSkin) ironSkin.SetActive(true);
        else ironSkin.SetActive(false);

        if (player.thornmail) thornmail.SetActive(true);
        else thornmail.SetActive(false);

        if (player.healingAura) healingAura.SetActive(true);
        else healingAura.SetActive(false);

        if (player.hornOfTheAncients) hornOfTheAncients.SetActive(true);
        else hornOfTheAncients.SetActive(false);

        if (player.mysticalHappiness) mysticalHappiness.SetActive(true);
        else mysticalHappiness.SetActive(false);
    }
}
