using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.EditorTools;

public class SceneManagement : MonoBehaviour
{
    private List<string> scenes = new List<string>
    {
        "Scene2",
        "Scene3"
    };

    private string currentScene;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = "Scene1";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
