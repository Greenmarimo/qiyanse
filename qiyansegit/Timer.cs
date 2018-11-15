using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float timerTime;
    public float maxTimerTime;
    public GameController gameCntrl;
    public GameObject brainLight;
    public bool hasTime = false;
    private Image GetImage;
    

    // Use this for initialization
    private void Awake()
    {
        GetImage = GetComponent<Image>();

        maxTimerTime = 5f;
        gameCntrl = GameObject.Find("Game Controller").GetComponent<GameController>();
        brainLight = GameObject.Find("BrainLight");
    }


    // Update is called once per frame
    void Update()
    {
        if (hasTime)
        {
            Image image = GetImage;
            if(gameCntrl.score >= 6 && gameCntrl.score < 15)
            {

                maxTimerTime = 4.5f - 0.1f * gameCntrl.score;
            }
            if (gameCntrl.score >= 15 && gameCntrl.score <= 50)
            {
                maxTimerTime = maxTimerTime = 4.5f - 0.024f * gameCntrl.score; ;
            }

            image.fillAmount = timerTime / maxTimerTime;

            if (timerTime > 0)
            {
                timerTime -= Time.deltaTime;
            }
            if (timerTime <= 0)
            {
                gameCntrl.lose = true;
            }
            
        }
        

    }
}
