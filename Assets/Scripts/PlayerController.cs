using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text timerText;
    public Text recordText;
    public Text gameText;
    public Text winText;
    public GameObject pickups;
    public GameObject restartPick;
    public GameObject exitPick;
    public AudioSource victory;

    AudioSource blop;

    private Rigidbody rb;
    private short count;
    private short game;
    private int objectCount;
    private float startTime;
    private float t = 0;
    private float rec = 999999999999; // 12 numbers = 31708 days
    private int minutes = 0;
    private float seconds = 0;
    private bool finished = false;

    private void Start() // On start
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        game = 1;
        startTime = Time.time;
        recordText.text = "Record: ---";
        gameText.text = "Game: " + game.ToString();
        objectCount = pickups.transform.childCount;
        SetCountText();
        winText.text = "";
        restartPick.SetActive(false);
        exitPick.SetActive(false);
        blop = GetComponent<AudioSource>();
    }

    private void FixedUpdate() // Physics
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    private void Update()
    {
        if (finished)
        {
            if(t < rec)
            {
                rec = t;
                recordText.text = "Record: " + minutes + ":" + seconds.ToString("f2") + " (Game: " + game + ")";
                winText.text = "You Win! NEW RECORD!";
            }
            return;
        }
        else
        {
            t = Time.time - startTime;
            minutes = (int)t / 60;
            seconds = t % 60;
            timerText.text = "Timer: " + minutes + ":" + seconds.ToString("f2");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            blop.Play();
            count++; // +1
            SetCountText();
        }
        if (other.gameObject.CompareTag("Restart Pick"))
        {
            resetAfterWin();
        }
        if (other.gameObject.CompareTag("Exit Pick"))
        {
            Application.Quit();
        }
    }

    void resetAfterWin()
    {
        exitPick.SetActive(false);
        restartPick.SetActive(false);
        count = 0;
        game++;
        SetGameText();
        SetCountText();
        winText.text = "";
        startTime = Time.time;
        finished = false;
        foreach (Transform child in pickups.transform)
        {
            child.gameObject.SetActive(true);
            child.gameObject.transform.rotation = Quaternion.Euler(45, 45, 45);
        }
    }

    void SetGameText()
    {
        gameText.text = "Game: " + game.ToString();
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString() + " / " + objectCount;
        if (count >= objectCount)
        {
            winText.text = "You win!";
            finished = true;
            victory.Play();
            restartPick.SetActive(true);
            exitPick.SetActive(true);
        }
    }
}