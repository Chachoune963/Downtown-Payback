using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSettings : MonoBehaviour
{
    public static bool qwertySelected = true;
    // Start is called before the first frame update

    public void qwertySelection(bool input)
    {
        qwertySelected = input;
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
