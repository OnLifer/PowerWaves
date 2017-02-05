using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan : MonoBehaviour {

    public int levelto;
    public int x;
    public GameObject[] Passplane;
    public GameObject[] MCube;
    private bool win = false;
    private bool win0 = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Invoke("Dowin", 2f);
        for (int i=0;i<x;i++)
        {
            Vector3 a = Passplane[i].GetComponent<Transform>().position;
            Vector3 b = MCube[i].GetComponent<Transform>().position;
            float dir = Vector3.Distance(a, b);
            Debug.Log(dir);

            if (dir <= 1.6f)
            {
                Debug.Log("It's OK");
                Dowin();
            }
            else
            {
                Debug.Log("OK");
                //CancelInvoke();
            }
                
        }
	}

    void Dowin()
    {
        SceneManager.LoadScene(levelto);
    }
}
