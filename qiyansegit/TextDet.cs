using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDet : MonoBehaviour {

    public Sprite[] textColorName = new Sprite[7];
    public Sprite[] colorEdgeSprites = new Sprite[7];
    public Sprite colorEdgeSprite;
    public Color[] colorArray = new Color[7];
    public int idColorName;
    public int idColorFont;
    public bool hasNext = false;
    public GameController gameCntrl;
    
   
    public SpriteRenderer GetText;
    // Use this for initialization
    private void Awake()
    {
        gameCntrl = GameObject.Find("Game Controller").GetComponent<GameController>();

        GetText = GetComponent<SpriteRenderer>();
        /*
        colorArray[0] = GetComponent<Text>().color = new Color(255, 0, 0);
        colorArray[1] = GetComponent<Text>().color = new Color(255, 255, 0);
        colorArray[2] = GetComponent<Text>().color = new Color(0, 255, 0);
        colorArray[3] = GetComponent<Text>().color = new Color(0, 0, 255);
        colorArray[4] = GetComponent<Text>().color = new Color(255, 127, 0);
        colorArray[5] = GetComponent<Text>().color = new Color(75, 0, 130);
        colorArray[6] = GetComponent<Text>().color = new Color(228, 35, 157); 
        */
    }
    
    IEnumerator NextColor()
    {

        if (gameCntrl.score < 6)
        {
            GetText.sprite = textColorName[idColorName];
            GetText.color = colorArray[6];
        }
        else if (gameCntrl.score >= 6 )
        {
            while (true)
            {
                GetText.sprite = textColorName[idColorName];
                GetText.color = colorArray[6];
                colorEdgeSprite = colorEdgeSprites[idColorName];
                yield return new WaitForSeconds(0.18f);
                GetText.sprite = textColorName[idColorName+1];
                GetText.color = colorArray[5];
                colorEdgeSprite =  colorEdgeSprites[idColorName+1];
                yield return new WaitForSeconds(0.18f);
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (hasNext)
        {
            
            NextText();
        }
    }
    void NextText()
    {

        hasNext = false;

        

        var rand = new System.Random();
        for (int i = textColorName.Length - 1; i >= 0; i--)
        {
            int j = rand.Next(i);
            var temp = textColorName[i];
            var temp1 = colorArray[i];
            var temp2 = colorEdgeSprites[i];
            textColorName[i] = textColorName[j];
            colorEdgeSprites[i] = colorEdgeSprites[j];
            colorArray[i] = colorArray[j];
            textColorName[j] = temp;
            colorArray[j] = temp1;
            colorEdgeSprites[j] = temp2;
        }
        idColorName = 0;
        
        gameCntrl.hasNext = true;
        StartCoroutine(NextColor());
       

    }
   
}
