using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUIController : MonoBehaviour
{

    public Text textDebt;
    public Text textKills;
    public Text textScore;

    public string preTextDebt = "Debt : ";
    public string preTextKills = "Kills : ";
    public string preTextScore = "Score : ";

    public string unitDebt = "BR";

    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        textDebt.text = preTextDebt + player.dette.ToString() + " " + unitDebt;
        textKills.text = preTextKills + player.kills.ToString();
        textScore.text = preTextScore + player.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public void buttonRetryCallback()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
