using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class settingsMenu : MonoBehaviour {

    public GameObject menu;

    public AudioSource gameMusic, gameSoundFX;
    public bool gmToggle, gsfxToggle;
    public Text gmText, gsfxText;

    public GameObject combatLog;
    public Text clText;

    // Start is called before the first frame update
    void Start()
    {
        gmToggle = true;
        gsfxToggle = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btnShowMenu() {
        if(menu.active == true) { menu.SetActive(false); }
        else { menu.SetActive(true); }
    }

    public void btnGameMusic() {
        gmToggle = !gmToggle;
        if (gmToggle) { gameMusic.volume = 1; gmText.text = "ENABLED"; }
        else { gameMusic.volume = 0; gmText.text = "DISABLED"; }
    }

    public void btnGameSoundFX() {
        gsfxToggle = !gsfxToggle;
        if (gsfxToggle) { gameSoundFX.volume = 1; gsfxText.text = "ENABLED"; }
        else { gameSoundFX.volume = 0; gsfxText.text = "DISABLED"; }
    }

    public void btnShowLog() {
        if (combatLog.active == true) { combatLog.SetActive(false); clText.text = "DISABLED"; }
        else { combatLog.SetActive(true); clText.text = "ENABLED"; }
    }

    public void btnRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void btnExitGame() {
        Application.Quit();
    }
}
