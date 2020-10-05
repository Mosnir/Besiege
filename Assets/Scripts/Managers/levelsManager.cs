using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{

    string[] scenePath;

    public string[] ScenePath { get => scenePath;}

    private void OnEnable()
    {
        AssetBundle bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "levels"));
        scenePath = bundle.GetAllScenePaths();
    }

}
