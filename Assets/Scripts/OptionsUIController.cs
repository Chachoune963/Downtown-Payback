using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsUIController : MonoBehaviour
{

    public bool enable { get; private set; } = false;

    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void show()
    {
        // On position l'UI en 0 0 0
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        enable = true;
    }

    public void dissmis()
    {
        // On deplace l'UI tres loin
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(2076, 0, 0);
        enable = false;
    }
}
