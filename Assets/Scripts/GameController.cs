using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public PlayerController player;

    public GameObject enemiPrefab;

    public string prefabArmesDossier = "Prefabs/Armes";

    public float tempsSpawnEnemiMin = 15f;

    public int nombreMinEnemiSpawnVague = 2;

    public GameObject prefabUICanevas;

    public static GameObject[] armes;

    protected float conteurSpawnEnemi = 0f;
    protected float tempsAvantProchaineVague = 15f;
    protected float maxAllowedWeaponStrenght = 1f;

    public PlayerUIController playerUIController { get; private set; }
    private bool canStart = false;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(6, 6, true);

        // Initialise le dictionaire d'items pour le shop
        if(ShopItem.items.Count == 0)
            ShopItem.initItemsDictionary();

        armes = Resources.LoadAll<GameObject>(prefabArmesDossier);

        playerUIController = Instantiate(prefabUICanevas, gameObject.transform).GetComponent<PlayerUIController>();
        playerUIController.player = player;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canStart)
        {
            spawnEnemis();
        }
        else
        {
            if (player.dette > 0)
                canStart = true;
        }
    }

    private void spawnEnemis()
    {
        if (conteurSpawnEnemi >= tempsAvantProchaineVague)
        {
            tempsAvantProchaineVague = Mathf.Max(30f - (player.dette / 1000), tempsSpawnEnemiMin);

            maxAllowedWeaponStrenght = 30 + (player.dette / 500) + player.kills;

            int nombreEnemis = nombreMinEnemiSpawnVague + (int)Mathf.Pow((player.dette / 100), 0.5f);

            Debug.Log("nouvelle vague avec " + nombreEnemis + " ennemis, de force max " + maxAllowedWeaponStrenght + " et " + tempsAvantProchaineVague+" secondes avant prochaine vague");

            for (int i = 0; i < nombreEnemis; i++)
            {
                Vector3 position = new Vector3(Random.Range(-45, 50), Random.Range(-15, 25));

                GameObject instance = Instantiate(enemiPrefab, position, new Quaternion());
                basicAI enemi = instance.GetComponent<basicAI>();

                // Definit la cible
                enemi.target = player;

                // Choix de l'arme
                GameObject prefabArme = armes[Random.Range(0, armes.Length)];
                float weaponPower = prefabArme.GetComponent<WeaponScript>().overallPower;
                while (weaponPower > maxAllowedWeaponStrenght)
                {
                    prefabArme = armes[Random.Range(0, armes.Length)];
                    weaponPower = prefabArme.GetComponent<WeaponScript>().overallPower;
                }
                instance = Instantiate(prefabArme, enemi.GetComponent<Transform>());
                WeaponScript arme = instance.GetComponent<WeaponScript>();
                if (arme is HandgunScript)
                {
                    enemi.tempsAttaqueMin = ((HandgunScript)arme).cooldown*2;
                }
                enemi.weapon = arme;
            }

            conteurSpawnEnemi = 0;
        }
        conteurSpawnEnemi += Time.fixedDeltaTime;
    }
}
