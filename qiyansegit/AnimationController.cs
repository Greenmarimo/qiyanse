using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    private Animator[] anim = new Animator[7];
    private int[] array = { 0, 1, 2, 3, 4, 5, 6 };
    public bool lose = false;
    private bool oneTime = true;
    public GameObject brainElectric;
    public GameController gameController;
    public GameObject colorTextObject;
    public GameObject backGround;
    private Vector4 nsvVector = new Vector4(0,0,0,0);
    private bool blueTransition = true;
    private bool violetTransition = true;
    private bool pinkTransition = true;
    private bool redTransition = true;
    private bool orangeTransition = true;
    private bool yellowTransition = true;
    private Timer timer;
    private Animator leftTalisman;
    private Animator rightTalisman;
    public float speed;
    // Use this for initialization
    private void Awake()
    {
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        colorTextObject = GameObject.Find("ColorText");
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
        anim[0] = GameObject.Find("Winking0").GetComponent<Animator>();
        anim[1] = GameObject.Find("Winking1").GetComponent<Animator>();
        anim[2] = GameObject.Find("Winking2").GetComponent<Animator>();
        anim[3] = GameObject.Find("Winking3").GetComponent<Animator>();
        anim[4] = GameObject.Find("Winking4").GetComponent<Animator>();
        anim[5] = GameObject.Find("Winking5").GetComponent<Animator>();
        anim[6] = GameObject.Find("Winking6").GetComponent<Animator>();
        leftTalisman = GameObject.Find("LeftTalisman").GetComponent<Animator>();
        rightTalisman = GameObject.Find("RightTalisman").GetComponent<Animator>(); 

        
    }
    // Use this for initialization
    void Start () {
        StartCoroutine(JustWaitCorr());
        StartCoroutine(BrainElectricCorr());
    }
    IEnumerator WinkingScreenPlay()
    {
        for (int i = 0; i < 7; i++)
        {
            
            anim[i].SetBool("WinkingAnim_In_CloseAnim", false);
            anim[i].SetBool("OpenIdle_In_CloseAnim", false);
            anim[i].SetBool("CloseAnim_In_CloseIdle", false);
            anim[i].SetBool("OpenIdle_In_WinkingAnim", false);
            anim[i].SetBool("CloseIdle_In_OpenAnim", true);
            anim[i].SetBool("Open_In_OpenIdle", true);
        }
        yield return new WaitForSeconds(2f);
        while (true)
        {
            var rand = new System.Random();
            for (int i = array.Length - 1; i >= 0; i--)
            {
                int j = rand.Next(i);
                var temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
            for (int i = 0; i < 7; i++)
            {
                anim[array[i]].SetBool("OpenIdle_In_WinkingAnim", true);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < 7; i++)
            {
                anim[array[i]].SetBool("OpenIdle_In_WinkingAnim", false);
            }


        }


        //Open_In_OpenIdle
    }
    IEnumerator JustWaitCorr()
    {
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(WinkingScreenPlay());
    }
    IEnumerator TransitionCorr()
    {
        gameController.hasTransition = true;
        colorTextObject.SetActive(false);
        StopCoroutine(WinkingScreenPlay());
        timer.timerTime = timer.maxTimerTime;
        timer.hasTime = false;
       

        for (int i = 0; i < 7; i++)
        {
            anim[i].SetBool("OpenIdle_In_WinkingAnim", false);
            anim[i].SetBool("CloseIdle_In_OpenAnim", false);
            anim[i].SetBool("Open_In_OpenIdle", false);
            anim[i].SetBool("WinkingAnim_In_CloseAnim", true);
            anim[i].SetBool("OpenIdle_In_CloseAnim", true);
            anim[i].SetBool("CloseAnim_In_CloseIdle", true);
        }
        yield return new WaitForSeconds(2.5f);
        for (int i = 0; i < 7; i++)
        {
            anim[i].SetBool("OpenIdle_In_WinkingAnim", false);
            anim[i].SetBool("CloseIdle_In_OpenAnim", false);
            anim[i].SetBool("Open_In_OpenIdle", false);
            anim[i].SetBool("WinkingAnim_In_CloseAnim", false);
            anim[i].SetBool("OpenIdle_In_CloseAnim", false);
            anim[i].SetBool("CloseAnim_In_CloseIdle", false);
        }
        yield return new WaitForSeconds(0.5f);
        gameController.hasTransition = false;
        colorTextObject.SetActive(true);
        StartCoroutine(WinkingScreenPlay());
        timer.hasTime = true;
        StopCoroutine(TransitionCorr());
    }

    IEnumerator BrainElectricCorr()
    {
        yield return new WaitForSeconds(0.8f);
        brainElectric.SetActive(true);
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            brainElectric.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            brainElectric.SetActive(true);
        }

    }
    // Update is called once per frame
    void Update () {
        if (lose && oneTime)
        {
            oneTime = false;
            StopCoroutine(WinkingScreenPlay());
            for (int i = 0; i < 7; i++)
            {
                anim[i].SetBool("OpenIdle_In_WinkingAnim", false);
                anim[i].SetBool("CloseIdle_In_OpenAnim", false);
                anim[i].SetBool("Open_In_OpenIdle", false);
                anim[i].SetBool("WinkingAnim_In_CloseAnim", true);
                anim[i].SetBool("OpenIdle_In_CloseAnim", true);
                anim[i].SetBool("CloseAnim_In_CloseIdle", true);
            }
        }
        if (blueTransition && gameController.score == 6 )
        {
            blueTransition = false;
            ThemeTransition("HumanCutInFishAnim");
        }
        if (violetTransition && gameController.score == 10)
        {
            violetTransition = false;
            ThemeTransition("FishCutInSnakeAnim");
        }
        if (pinkTransition && gameController.score == 16)
        {
            pinkTransition = false;
            ThemeTransition("SnakeCutInButterflyAnim");
        }
        if (orangeTransition && gameController.score == 20)
        {
            orangeTransition = false;
            ThemeTransition("ButterflyCutInTigerAnim");
        }
        if (redTransition && gameController.score == 26)
        {
            redTransition = false;
            ThemeTransition("TigerCutInDemonAnim");
        }
        if (yellowTransition && gameController.score == 30)
        {
            yellowTransition = false;
            ThemeTransition("DemonCutInWaspAnim");
        }
        if (gameController.score == 6)
        {
            HSVSwap(new Vector4(0.35f, 0, 0, 0));          
        }
        if (gameController.score == 10)
        {   
            HSVSwap(new Vector4(0.55f, 0, 0, 0));
        }
        if (gameController.score == 16)
        {
            HSVSwap(new Vector4(0.7f, 0.1f, 0, 0));
        }
        if (gameController.score == 20)
        {
            HSVSwap(new Vector4(0.8f, 0.2f, 0, 0));
        }
        if (gameController.score == 26)
        {
            HSVSwap(new Vector4(0.9f, 0.38f, 0, 0));
        }
        if (gameController.score == 30)
        {
            HSVSwap(new Vector4(0.07f, 0.3f, 0.02f, 0));
        }
        
    }
    private void HSVSwap(Vector4 vector4){
        float step = speed * Time.deltaTime;
        nsvVector = Vector4.MoveTowards(nsvVector, vector4, step);
        backGround.GetComponent<Renderer>().sharedMaterial.SetVector("_HSVAAdjust", nsvVector);
    }
    private void ThemeTransition(string str){
        StartCoroutine(TransitionCorr());
        rightTalisman.SetBool(str, true);
        leftTalisman.SetBool(str, true);
    }

}

/* WinkingAnim_In_CloseAnim
 * OpenIdle_In_CloseAnim
 * CloseAnim_In_CloseIdle
*/
