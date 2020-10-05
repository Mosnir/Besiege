using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    GarageManager garageManager;
    SaveManager saveManager;
    CameraController cameraController;


    private void Awake()
    {
        garageManager = Toolbox.Instance.Get<GarageManager>();
        saveManager = Toolbox.Instance.Get<SaveManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        //Delay Garage load to avoid problems on game object not instantiate.
        StartCoroutine("Delayload");
    }

    IEnumerator Delayload()
    {
        yield return new WaitForSeconds(.1f);
        saveManager.Load();
    }

}
