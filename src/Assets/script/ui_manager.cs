using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ui_manager : MonoBehaviour {

    public GameObject menu, click_me, title, ball, go_star;
    public Text menu1, menu2;
    public Toggle autg, subtg,coltg1, coltg2, coltg3, coltg4;
    public Slider size;
    public AudioSource aud;
    public Dropdown fontch;
    public Font[] textfont;

    bool menu_change=true;
    public GameObject[] texts;
    //GameObject[] subtitles;
    int fonssta;

    // Use this for initialization
    void Start () {
        texts = GameObject.FindGameObjectsWithTag("text");
        //subtitles = GameObject.FindGameObjectsWithTag("subtitle");
        fonssta = texts[0].GetComponent<Text>().fontSize;
        menu.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void menu_on()
    {
        if (menu_change)
        {
            menu1.text = "RETURN";
            menu2.text = "backspace";
            title.SetActive(false);
            click_me.SetActive(false);
            menu.SetActive(true);
            menu_change = false;
            ball.SetActive(false);
            go_star.SetActive(false);
            menu_refresh();
        }
        else
        {
            menu1.text = "MENU";
            menu2.text = "spacebar";
            title.SetActive(true);
            if (subtg.isOn)
                click_me.SetActive(true);
            menu.SetActive(false);
            ball.SetActive(true);
            go_star.SetActive(true);
            menu_change = true;
        }
    }

    public void menu_enter()
    {
        if(autg.isOn)
        {
            aud.mute = false;
        }
        else
        {
            aud.mute = true;
        }

        //if (subtg.isOn)


        foreach (GameObject textch in texts)
        {
            Text a = textch.GetComponent<Text>();
            a.fontSize = (int)size.value + a.fontSize;
            //size.value？？
            if (coltg1.isOn)
                a.color = Color.black;
            else if (coltg2.isOn)
                a.color = Color.yellow;
            else if (coltg3.isOn)
                a.color = Color.green;
            else if (coltg4.isOn)
                a.color = Color.red;
            switch(fontch.value)
            {
                case 0:
                    a.font = textfont[0];
                    break;
                case 1:
                    a.font = textfont[1];
                    break;
                case 2:
                    a.font = textfont[2];
                    break;
                case 3:
                    a.font = textfont[3];
                    break;
            }

        }
        menu_refresh();
    }

    public void menu_refresh()
    {
        autg.isOn = !aud.mute;
        size.value = texts[0].GetComponent<Text>().fontSize - fonssta;
        
        if (texts[0].GetComponent<Text>().font == textfont[0])
            fontch.value = 0;
        else if (texts[0].GetComponent<Text>().font == textfont[1])
            fontch.value = 1;
        else if (texts[0].GetComponent<Text>().font == textfont[2])
            fontch.value = 2;
        else if (texts[0].GetComponent<Text>().font == textfont[3])
            fontch.value = 3;
        if (texts[1].GetComponent<Text>().color == Color.black)
            coltg1.isOn = true;
        else if (texts[1].GetComponent<Text>().color == Color.yellow)
            coltg2.isOn = true;
        else if (texts[1].GetComponent<Text>().color == Color.green)
            coltg3.isOn = true;
        else if (texts[1].GetComponent<Text>().color == Color.red)
            coltg4.isOn = true;
    }
 
    public void gamexit()
    {
        Application.Quit();
    }

    public void start_game()
    {
        Invoke("sta_r",0.5f);
    }
    void sta_r()
    {
        SceneManager.LoadScene(1);
    }
}
