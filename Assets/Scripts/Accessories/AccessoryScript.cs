using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AccessoryScript : MonoBehaviour
{

    public float maxCharge;
    public float charge;
    public abstract void activate(PlayerController player);
}
