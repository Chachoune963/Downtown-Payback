using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUIController : MonoBehaviour
{
    
    public Text textBox;
    public string text;

    void Start()
    {
        textBox.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        textBox.text = text;
    }
}
