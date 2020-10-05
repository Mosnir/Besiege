using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{


    PartsManager partsManager;
    GarageManager garageManager;
    
    Save m_save;

    public delegate void OnEventDelegate();
    public static event OnEventDelegate OnSave = () => { };
    public static event OnEventDelegate OnLoad = () => { };

    private void Start()
    {
        partsManager = Toolbox.Instance.Get<PartsManager>();
        garageManager = Toolbox.Instance.Get<GarageManager>();

    }

    public void Save()
    {

        m_save = CreateSave();

        string json = JsonUtility.ToJson(m_save);

        using (StreamWriter writer = new StreamWriter(Application.streamingAssetsPath + "/garage.save", false))
        {
            writer.Write(json);
            writer.Close();

            OnSave();
        }

    }

    //Create a json from our garage
    private Save CreateSave()
    {

        Save save = new Save();

        foreach (Transform vehiclesGo in garageManager.GetRoot())
        {

            Vehicle vehicle = new Vehicle();

            foreach(Transform part in vehiclesGo)
            {

                Part savePart = new Part();
                savePart.position = part.position;
                savePart.rotation = part.rotation;
                savePart.id = part.name;

                vehicle.parts.Add(savePart);
            }

            save.vehicles.Add(vehicle);

        }

        return save;

    }

    //Recreate our garage from json data
    public void Load()
    {

        if(m_save == null)
        {
            if (!File.Exists(Application.streamingAssetsPath + "/garage.save")) return;

            using (StreamReader reader = new StreamReader(Application.streamingAssetsPath + "/garage.save", true))
            {

                string json = reader.ReadToEnd();
                reader.Close();

                m_save = JsonUtility.FromJson<Save>(json);


            }
        }

        Reload();

        OnLoad();

    }

    public void Reload()
    {

        garageManager.Clear();

        int i = 0;

        foreach (Vehicle vehicle in m_save.vehicles)
        {

            GameObject vehicleGo = garageManager.AddVehicle();

            if (i != 0) vehicleGo.SetActive(false);

            foreach (Part part in vehicle.parts)
            {
                GameObject partPrefab;
                if (partsManager.Parts.TryGetValue(part.id, out partPrefab))
                {
                    GameObject partGo = Instantiate(partPrefab, vehicleGo.transform);
                    partGo.name = part.id;
                    partGo.transform.position = part.position;
                    partGo.transform.rotation = part.rotation;
                }
            }

            i++;
        }
    }

}
