using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class PartsManager : MonoBehaviour
{
    Dictionary<string, GameObject> parts;

    public Dictionary<string, GameObject> Parts { get => parts; }

    private void OnEnable()
    {
        parts = new Dictionary<string, GameObject>();

        AssetBundle bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "Parts"));
        bundle.LoadAllAssets<GameObject>().ToList().ForEach(part =>
        {
            parts.Add(part.name, part);
        });

    }

}
