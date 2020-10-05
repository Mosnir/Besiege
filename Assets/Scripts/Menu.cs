using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    LevelsManager levelsmanager;

    [SerializeField] GameObject prefabButton;
    [SerializeField] Transform levels;

    // Start is called before the first frame update
    void Awake()
    {
        levelsmanager = Toolbox.Instance.Get<LevelsManager>();
    }

    private void Start()
    {
        int i = 0;

        foreach (string s in levelsmanager.ScenePath)
        {

            GameObject go = Instantiate(prefabButton, levels);
            RectTransform rectTransform = go.GetComponent<RectTransform>();
            UIButton uiButton = go.GetComponent<UIButton>();
            uiButton.GetComponent<RectTransform>().sizeDelta = Vector2.one * 100;
            rectTransform.anchoredPosition = new Vector3((i+0.5f - levelsmanager.ScenePath.Length/2.0f) * 2.0f * rectTransform.sizeDelta.x, 0, 0);

            uiButton.Button.onClick.AddListener(() => LoadScene(s));
            uiButton.name = prefabButton.name;
            uiButton.Text.text = "level " + (i+1).ToString();
            uiButton.Text.fontSize = 28;
            i++;
        }
    }

    void LoadScene(string s)
    {
        SceneManager.LoadScene(s);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
