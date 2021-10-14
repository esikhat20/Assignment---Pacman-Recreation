using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Loads the Level 1 scene
    public void LoadLevel1() {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(1);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.buildIndex == 1) {
            Button exitButton = GameObject.Find("HUD/Exit Button").GetComponent<Button>();
            exitButton.onClick.AddListener(ExitGame);
        }
    }

    public void ExitGame() {
        // UnityEditor.EditorApplication.isPlaying = false;
        SceneManager.LoadScene(0);
    }
}
