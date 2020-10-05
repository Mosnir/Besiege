using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Editor : MonoBehaviour
{

    PartsManager partsManager;
    SaveManager saveManager;
    GarageManager garageManager;
    Animator animator;
    Camera mainCamera;

    public RaycastHit hit;
    public bool isHit;
    public Ray ray;

    [SerializeField] GameObject prefabUIButton;
    public GameObject parts;

    GameObject selectedPart;

    //an instance used for preview
    GameObject instanceSelected;
    [SerializeField] Material transparent;

    [SerializeField] GameObject previous;
    [SerializeField] GameObject next;

    //overwrite setter for updating preview
    public GameObject SelectedPart
    {
        get => selectedPart;
        set
        {
            selectedPart = value;
            Destroy(instanceSelected);
            instanceSelected = Instantiate(selectedPart);

            foreach (Collider collider in instanceSelected.GetComponentsInChildren<Collider>())
            {
                collider.enabled = false;
            };

            foreach (MeshRenderer mr in instanceSelected.GetComponentsInChildren<MeshRenderer>())
            {
                mr.material = transparent;
            }

        }
    }

    public GameObject InstanceSelected { get => instanceSelected; set => instanceSelected = value; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

        mainCamera = Camera.main;
        partsManager = Toolbox.Instance.Get<PartsManager>();
        saveManager = Toolbox.Instance.Get<SaveManager>();
        garageManager = Toolbox.Instance.Get<GarageManager>();

        int i = 0;
        partsManager.Parts.ToList().ForEach(part =>
        {
            GameObject go = Instantiate(prefabUIButton, parts.transform);
            RectTransform rectTransform = go.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector3(i * rectTransform.sizeDelta.x, 0, 0);
            UIButton uiButton = go.GetComponent<UIButton>();
            uiButton.Button.onClick.AddListener(() => SetSelected(part.Value));
            uiButton.name = prefabUIButton.name;
            uiButton.Text.text = part.Key;
            i++;
        });
    }

    private void Update()
    {
        if (garageManager.HasNext())
        {
            next.SetActive(true);
        }
        else next.SetActive(false);

        if (garageManager.HasPrevious()) previous.SetActive(true);
        else previous.SetActive(false);
    }

    //A common Raycast who will be use by our behaviors
    public void LaunchRaycast()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        isHit = Physics.Raycast(ray, out hit);

        //Debug for seeing ray
        //Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.red, 5.0f);
    }

    public void SetSelected(GameObject prefab)
    {
        SelectedPart = prefab;
    }

    public void Play()
    {
        garageManager.ActivateCurrentVehicle();
        gameObject.SetActive(false);
    }

    public void Next()
    {
        garageManager.Next();
    }

    public void Previous()
    {
        garageManager.Previous();
    }

    public void AddVehicle()
    {
        garageManager.AddVehicle();
    }

    public void DeleteVehicle()
    {
        garageManager.DeleteCurrent();
    }

    public void Save()
    {
        saveManager.Save();
    }

    public void SetMode(string mode)
    {
        animator.SetTrigger(mode);
    }

}
