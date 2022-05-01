using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JacketController : ArmorPrefab
{

    public float probabiliteAbsorbtionDegats = 0.3f;
    public float reducteurDegatsConstant = 0; // degats mutlipliées par (1-reducteurDegatsConstant) quand non absorbées

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override
    public float damageListener(float baseDamage)
    {
        print("dégats entrants" + baseDamage);
        if(baseDamage>0 && Random.value <= probabiliteAbsorbtionDegats)
        {
            Debug.Log("degats absorbées");
            return 0;
        }
        if (baseDamage < 0)
        {
            return baseDamage;
        }

        return Mathf.Max(baseDamage * (1 - reducteurDegatsConstant),0f);
    }
}
