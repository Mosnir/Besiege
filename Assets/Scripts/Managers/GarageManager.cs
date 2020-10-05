using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GarageManager : MonoBehaviour
{

    int currentVehicle = 0;
    int nbVehicles = 0;

    GameObject corePrefab;

    SaveManager saveManager;

    private void OnEnable()
    {

        GameObject empty = new GameObject("0");
        empty.transform.parent = transform;

        AssetBundle bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "core"));
        corePrefab = bundle.LoadAsset<GameObject>("Core");

        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }



    }


    public Transform GetRoot()
    {
        return transform;
    }

    public void ActivateCurrentVehicle()
    {
        Transform current = transform.Find(currentVehicle.ToString());

        foreach(Rigidbody rb in current.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
        }

        foreach(MyJoint joint in current.GetComponentsInChildren<MyJoint>())
        {
            joint.Join();
        }

    }

    public void Clear()
    {
        currentVehicle = 0;
        nbVehicles = 0;

        //Clean our garage before reading json
        foreach (Transform vehiclesGo in transform)
        {
            vehiclesGo.gameObject.SetActive(false);
            Destroy(vehiclesGo.gameObject);
        }
    }

    public bool HasNext()
    {
        bool result = currentVehicle < nbVehicles-1;
        return result;
    }

    public bool HasPrevious()
    {
        bool result = currentVehicle > 0;
        return result;
    }

    public void Next()
    {
        if (!HasNext()) return;
        currentVehicle++;
        ShowCurrent();

    }

    public void Previous()
    {
        if (!HasPrevious()) return;
        currentVehicle--;
        ShowCurrent();
    }

    public void ShowCurrent()
    {
        int i = 0;

        foreach (Transform t in transform)
        {
            if (i == currentVehicle)
            {
                t.gameObject.SetActive(true);
            }
            else
            {
                t.gameObject.SetActive(false);
            }
            i++;
        }
    }

    public GameObject AddVehicle()
    {

        GameObject vehicleGo = new GameObject(nbVehicles.ToString());
        vehicleGo.AddComponent<Root>();
        vehicleGo.transform.parent = transform;

        GameObject coreGo = Instantiate(corePrefab, vehicleGo.transform);
        coreGo.name = "Core";

        nbVehicles++;
        return vehicleGo;
    }

    public void DeleteCurrent()
    {

        int i = 0;

        foreach (Transform t in transform)
        {
            if (i == currentVehicle)
            {
                Destroy(t.gameObject);
            }
            i++;
        }
        
        nbVehicles--;

        if (nbVehicles == 0)
        {
            AddVehicle();
        }

        if (currentVehicle > nbVehicles - 1)
        {
            currentVehicle--;
        }

        ShowCurrent();

    }

    public Transform getCurrentVehicle()
    {

        int i = 0;

        foreach (Transform t in transform)
        {
            if (i == currentVehicle)
            {
                return t.Find("Core");
            }
            i++;
        }

        return null;
    }

    public GameObject getCore()
    {
        return corePrefab;
    }

    public void Update()
    {
        if(getCurrentVehicle())
        {
            if (getCurrentVehicle().position.y < -20)
            {
                Clear();
                SceneManager.LoadScene("Defeat");
            }
        }

    }

}