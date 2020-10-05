using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{

    bool victory = false;

    private void Start()
    {
        
    }

    bool IsVictory()
    {
        return victory;
    }

    private void Update()
    {
        if(IsVictory())
        {
            SceneManager.LoadScene("Victory");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.name == "Core")
        {
            victory = true;
        }
    }

}
