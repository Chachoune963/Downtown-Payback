using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{

    public Text textDette;
    public Text textMorts;
    public Text textMunitions;
    public Slider barVie;
    public Image warningIndicator;
    public PlayerController player;
    public GameObject prefabShopUIController;
    public GameObject prefabOptionUI;
    public GameObject prefabGameOverUI;
    public GameObject prefabTextArea;

    public string textMortPre = "Kills : ";
    public string textDettePre = "Debt : ";
    public string detteMonaie = "BR";
    public string textAmmoPre = "Ammo : ";

    public bool warningVisible => !warningIndicator.color.Equals(invisibleWarningColor);

    private ShopUIController shopUIController;
    private OptionsUIController optionsUIController;
    private GameOverUIController gameOverUIController;
    private TextUIController textUIController;

    private Color invisibleWarningColor = new Color(0, 0, 0, 0);
    protected float blinkCounter;

    private Queue<PlayerUIController.TextBoxContent> textToDsiplay = new Queue<PlayerUIController.TextBoxContent>();

    // Start is called before the first frame update
    void Start()
    {
        // Configuration bar de vie
        barVie.maxValue = player.damageable.baseLife;

        hideWarning();

        // Configuration shop
        shopUIController = Instantiate(prefabShopUIController, gameObject.transform).GetComponent<ShopUIController>();
        shopUIController.player = player;
        shopUIController.disableShop();

        // Configuration options
        optionsUIController = Instantiate(prefabOptionUI, gameObject.transform).GetComponent<OptionsUIController>();
        optionsUIController.dissmis();

        initTextIntro();

    }

    private void initTextIntro()
    {
        addTextToDisplay("I told you kid, this town belongs to the locals.\nDon't go deep into debt with them, they are merciless.", 6);
        addTextToDisplay("Don't even think about buying some candy!\nYou will regret it, trust me.", 6);
        addTextToDisplay("The shops here all sell to credit, you never know when you owe them cash.\nThey also sell some nasty stuff to the people they fear...", 6);
    }

    // Update is called once per frame
    void Update()
    {
        barVie.value = player.damageable.life;
        textMorts.text = textMortPre + player.kills.ToString();
        textDette.text = textDettePre + player.dette.ToString() + " " + detteMonaie;
        if (player.weapon.maxAmmo != 0f)
        {
            WeaponScript weapon = player.weapon;
            textMunitions.text = textAmmoPre + weapon.ammo + "/" + weapon.maxAmmo;
        }
        else
        {
            textMunitions.text = "";
        }

        if(player.usingAccessory && player.accessory != null) {
            // Debug.Log("Jetpack in use");
            float ratioFuel = player.accessory.charge / player.accessory.maxCharge;
            if (ratioFuel <= 0.3f)
            {

                if(blinkCounter >= 1)
                {
                    blinkCounter = 0;
                    toogleWarning();
                }

                blinkCounter += Time.fixedDeltaTime;
            }
            else
            {
                hideWarning();
            }
        }
        else
        {
            hideWarning();
        }

        if (!player.vivant && gameOverUIController == null)
        {
            showGameOver();
        }

        shopKeys();
        // settingsKeys();
    }

    private void FixedUpdate()
    {
        if (textUIController == null && textToDsiplay.Count > 0)
        {
            TextBoxContent content = textToDsiplay.Dequeue();
            displayText(content.text, content.durration);
        }
    }

    // Le texte est ajouté a une liste et serra aficher quand viendra son tour
    public void addTextToDisplay(string text, float duration)
    {
        textToDsiplay.Enqueue(new PlayerUIController.TextBoxContent(text, duration));
    }

    public void clearText()
    {
        if (textUIController != null)
        {
            Destroy(textUIController.gameObject);
        }
    }

    public void displayText(string text)
    {
        displayText(text, -1);
    }

    public void displayText(string text, float durration)
    {
        if(textUIController != null)
        {
            Destroy(textUIController.gameObject);
        }

        textUIController = Instantiate(prefabTextArea, gameObject.transform).GetComponent<TextUIController>();
        textUIController.text = text;

        if(durration > 0)
            Destroy(textUIController.gameObject, durration);
    }

    protected void settingsKeys()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if(optionsUIController.enable)
            {
                optionsUIController.dissmis();
            } else
            {
                optionsUIController.show();
            }
        }
    }

    protected void shopKeys()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            if(shopUIController.enable)
            {
                shopUIController.disableShop();
            } else if (player.canShop)
            {
                shopUIController.enableShop();
            }
        }

        if (shopUIController.enable && (Input.GetKey(KeyCode.Escape) || !player.canShop))
        {
            shopUIController.disableShop();
        }

    }

    public void showGameOver()
    {
        gameOverUIController = Instantiate(prefabGameOverUI, gameObject.transform).GetComponent<GameOverUIController>();
        gameOverUIController.player = player;
    }

    public void toogleWarning()
    {
        if(warningVisible)
        {
            hideWarning();
        } else
        {
            showWarning();
        }
    }

    public void hideWarning()
    {
        warningIndicator.color = invisibleWarningColor;
    }

    public void showWarning()
    {
        warningIndicator.color = new Color(255, 255, 0, 255);
    }

    class TextBoxContent
    {
        public string text { get; private set; }
        public float durration { get; private set; }

        public TextBoxContent(string text, float durration)
        {
            this.text = text;
            this.durration = durration;
        }
    }

}
