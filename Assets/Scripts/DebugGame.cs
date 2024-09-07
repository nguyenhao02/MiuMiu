using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGame : MonoBehaviour
{
    void Awake()
    {
         DontDestroyOnLoad(gameObject);
        
    }
    // Start is called before the first frame update
    void Update()
    {
        Debug1();
       // Debug2();
        Debug3();
    }
 
    public void Debug1()
    {
        if(Input.GetKeyUp(KeyCode.T))
        {
            DataManager.Instance.AddPlayerFish(100);
            DataManager.Instance.AddPlayerPate(10);
            // DataManager.Instance.currentLevel = "Level0";
        }
        
    }

    public void Debug2()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log(Input.mousePosition);
               
        }
    }
    
    public void Debug3()
    {
        if(Input.GetKeyUp(KeyCode.Backspace))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save(); 
            Debug.Log("Delete ALL");
            DataManager.Instance.LoadGame();
        }
    }
    
}
