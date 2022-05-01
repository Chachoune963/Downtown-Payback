using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected float horizontalAxis;
	protected float verticalAxis;
    protected Vector2 mousePosition;
    protected bool shootOrder;
    protected bool accessoryOrder;
    protected bool upKey;
    protected bool leftKey;
    protected bool downKey;
    protected bool rightKey;

    public WeaponScript weapon { get; private set; }
    public AccessoryScript accessory { get; private set; }
    private ArmorPrefab armor;

    public Rigidbody2D rb2D;
    public Camera cam;
    public AudioSource playerAudio;
    public Damageable damageable;
    public GameObject prefabArmeBase;
    public GameObject prefabAccessoryBase;
    public GameObject prefabArmorBase;
    public float speed = 8f;
    public float thrustPerSpeed = 10f;

    public bool canShop = false;
    
    public int kills { get; set; }
    public bool vivant { get; private set; } = true;
    public int score => dette + kills * 100;
    public int dette { get; private set; } = 0;
    public bool usingAccessory { get; private set; } = false;


    // Start is called before the first frame update
    void Start()
    {
        changementArme(prefabArmeBase);
        changementAccessory(prefabAccessoryBase);

        if(prefabArmorBase != null)
            changementArmure(prefabArmorBase);


    }

    // Update is called once per frame
    void Update()
    {
        //récupérer les entrées clavier
        //horizontalAxis = Input.GetAxisRaw("Horizontal");
        //verticalAxis = Input.GetAxisRaw("Vertical");

        getAxis();

        shootOrder = Input.GetMouseButton(0);
        accessoryOrder = Input.GetKey(KeyCode.X);



        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

    }


    private void getAxis()
    {
        if (ControlSettings.qwertySelected)
        {
            upKey = Input.GetKey(KeyCode.W);
            leftKey = Input.GetKey(KeyCode.A);
        }
        else //Supposé Azerty
        {
            upKey = Input.GetKey(KeyCode.Z);
            leftKey = Input.GetKey(KeyCode.Q);
        }
        

        //Pareil ZQSD et WASD
        downKey = Input.GetKey(KeyCode.S);
        rightKey = Input.GetKey(KeyCode.D);

        if(rightKey && leftKey)
        {
            horizontalAxis = 0;
        }
        else if (rightKey)
        {
            horizontalAxis = 1;
        }
        else if (leftKey)
        {
            horizontalAxis = -1;
        }
        else
        {
            horizontalAxis = 0;
        }

        if(downKey && upKey)
        {
            verticalAxis = 0;
        }
        if (upKey)
        {
            verticalAxis = 1;
        }
        else if (downKey)
        {
            verticalAxis = -1;
        }
        else
        {
            verticalAxis = 0;
        }

    }



    public void changementArme(GameObject prefabArme)
    {
        if (weapon != null)
        {
            Destroy(weapon.gameObject);
        }
        GameObject instance = Instantiate(prefabArme, transform);
        weapon = instance.GetComponent<WeaponScript>();
    }

    public void changementAccessory(GameObject prefabAccessory)
    {
        if (accessory != null)
        {
            Destroy(accessory.gameObject);
        }
        GameObject instance = Instantiate(prefabAccessory, transform);
        accessory = instance.GetComponent<AccessoryScript>();
    }

    public void changementArmure(GameObject prefabArmure)
    {
        if (armor != null)
        {
            armor.dequip();
            Destroy(armor.gameObject);
        }
        GameObject instance = Instantiate(prefabArmure, transform);
        armor = instance.GetComponent<ArmorPrefab>();
        armor.equip(this);

        damageable.damageListener = armor.damageListener;
    }

    public void augmanterDette(int montant)
    {
        dette += montant;
    }

    public void tuer()
    {
        vivant = false;
    }

    private void FixedUpdate()
    {

        if(vivant)
        {
            //rb2D.velocity = new Vector2(horizontalAxis * speed, verticalAxis * speed);
            Vector2 targetVelocity = new Vector2(horizontalAxis * speed, verticalAxis * speed);
            rb2D.AddForce((targetVelocity - rb2D.velocity) * thrustPerSpeed);

            Vector2 lookDir = mousePosition - rb2D.position;
            rb2D.SetRotation(Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f);

            if (shootOrder && !ShopUIController.isShopping)
            {
                weapon.shoot(damageable);
            }
            if (accessoryOrder)
            {
                // print("activating accessory");
                accessory.activate(this);
                usingAccessory = true;
            } else
            {
                usingAccessory = false;
            }
        } else
        {
            // Si mort, le joueur ne bouge plus
            rb2D.velocity = new Vector2(0, 0);
            rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}