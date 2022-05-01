using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicAI : MonoBehaviour
{
    protected float horizontalAxis;
    protected float verticalAxis;
    protected Vector2 lookDir;
    public float speed = 5f;
    public float thrustPerSpeed = 10f;
    public Rigidbody2D rb;
    public PlayerController target;
    public WeaponScript weapon;

    public GameObject cadaverPrefab;

    public float tempsAttaqueMin = 1f;

    private Damageable damageable;
    protected float comteurTempsAttaque = 0;

    // Start is called before the first frame update
    void Start()
    {
        damageable = gameObject.GetComponent<Damageable>();
        //Debug.Log(damageable);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Methode appelée quand l'entitée est morte / déspawn
    private void OnDestroy()
    {
        target.kills++; // Incrémentation du conteur de morts du joueur
        Instantiate(cadaverPrefab, transform.position, transform.rotation);
    }

    private void deplacement()
    {

        Rigidbody2D targetRb = target.gameObject.GetComponent<Rigidbody2D>();
        Vector2 targetPos = targetRb.position;
        horizontalAxis = targetPos.x - rb.position.x;
        verticalAxis = targetPos.y - rb.position.y;
        lookDir = targetPos - rb.position;
        Vector2 targetVelocity = new Vector2(horizontalAxis, verticalAxis);
        //si joueur loin, passer a travers les murs
        if (targetVelocity.magnitude > 20f)
        {
            rb.GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            rb.GetComponent<Collider2D>().enabled = true;
        }
        targetVelocity = targetVelocity.normalized;
        // Debug.Log(rb.velocity);
        targetVelocity *= speed;
        rb.AddForce((targetVelocity - rb.velocity) * thrustPerSpeed);
        rb.SetRotation(Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f);
    }

    private void attaque()
    {
        weapon.shoot(damageable); 
    }

    private void FixedUpdate()
    {
        deplacement();

        if (comteurTempsAttaque >= tempsAttaqueMin)
        {
            comteurTempsAttaque = 0;
            attaque();
        }
        comteurTempsAttaque += Time.fixedDeltaTime;

    }
}
