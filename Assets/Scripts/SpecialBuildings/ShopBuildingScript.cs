using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuildingScript : MonoBehaviour
{
    public GameObject buyIndicator;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            player.canShop = true;
            buyIndicator.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            player.canShop = false;
            buyIndicator.SetActive(false);
        }
    }
}
