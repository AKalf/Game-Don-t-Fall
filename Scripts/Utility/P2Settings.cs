using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class P2Settings : MonoBehaviour {
    /*
     * Stores if second player will play
     * Value is set from main menu
     */ 
    [SerializeField]
    GameObject p2Camera;
    [SerializeField]
    GameObject PlayerGirl;
    static bool twoPlayers = false;
    static P2Settings inst = null;

    public static P2Settings GetInstance() {
        return inst;
    }
    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this);
        }
        
    }
    

    // Update is called once per frame
    void Update () {
        if (twoPlayers == true && SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1)) {
            Camera p1Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            p1Camera.rect = new Rect(p1Camera.rect.x, 0.5f, p1Camera.rect.width, p1Camera.rect.height);          
            Instantiate(PlayerGirl, PlayerGirl.transform.position, PlayerGirl.transform.rotation);
            twoPlayers = false;
        }
	}
   
    public static void SetNumberOfPlayers(bool two)
    {
        twoPlayers = two;
    }
    public static bool GetPlayers() {
        return twoPlayers;
    }
}
