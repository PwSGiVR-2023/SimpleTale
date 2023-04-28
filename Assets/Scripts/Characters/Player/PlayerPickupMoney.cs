using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerPickupMoney : MonoBehaviour
{
    //[SerializeField] private MoneyScriptable playerMoney;

    [SerializeField]
    private int money;


    void Start()
    {
        //money = playerMoney.Money;
    }

    void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.gameObject.CompareTag("Coin"))
            {
                money++;
                Destroy(collision.gameObject);
            }
    }


}