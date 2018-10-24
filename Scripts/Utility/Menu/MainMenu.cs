using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour {

    [SerializeField]
    GameObject blackPanel;

    [SerializeField]
    GameObject levelDescriptiom;

    AudioSource source;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().buildIndex != 2) {
            Cursor.visible = true;
        }
        Cursor.visible = true;
        P2Settings.SetNumberOfPlayers(false);
    }
	
	// Update is called once per frame
	void Update () {

    }
    public void StartGame() {
        if (levelDescriptiom != null) {
            levelDescriptiom.SetActive(true);
        }
        source.Play();
        StartCoroutine(LoadNewScene(1));
    }
    public void GoToCredits() {
        source.Play();
        StartCoroutine(LoadNewScene(2));
    }
    public void ReturnToMainMenu() {
        source.Play();
        StartCoroutine(LoadNewScene(0));
    }
  
    IEnumerator LoadNewScene(int scene) {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        blackPanel.SetActive(true);
        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone) {
           
            yield return null;
        }
    }
    public void QuitToDesktop() {
        Application.Quit();
    }
}
