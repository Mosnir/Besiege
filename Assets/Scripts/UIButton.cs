using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Button button;

    public Text Text { get => text; set => text = value; }
    public Button Button { get => button; set => button = value; }
}
