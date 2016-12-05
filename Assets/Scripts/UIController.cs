using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public Transform gameUI;
    public Transform pauseUI;
    public Text muteText;
    public Text exitText;
    public AudioSource pauseMusic2nd;
    public GameObject victory;

    private bool exit = false;

    void Start()
    {
        Resume();
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameUI.gameObject.activeInHierarchy == true)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
	}

    public void Pause()
    {
        exit = false;
        gameUI.gameObject.SetActive(false);
        pauseUI.gameObject.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMusic2nd.Play();
        victory.gameObject.SetActive(false);
    }

    public void Resume()
    {
        gameUI.gameObject.SetActive(true);
        pauseUI.gameObject.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMusic2nd.Stop();
        victory.gameObject.SetActive(true);
        exitText.text = "Exit game";
    }

    public void Restart()
    {
        SceneManager.LoadScene("MiniGame", LoadSceneMode.Single);
    }

    public void Mute()
    {
        if (AudioListener.volume == 0)
        {
            muteText.text = "Mute";
            AudioListener.volume = 1;
        } else
        {
            muteText.text = "Unmute";
            AudioListener.volume = 0;
        }
    }

    public void ExitApplication()
    {
        if(!exit)
        {
            exitText.text = "Are you sure?";
            exit = true;
            return;
        } else
        {
            Application.Quit();
        }
    }
}
