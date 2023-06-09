using UnityEngine;
using UnityEngine.Tilemaps;

public class BossArea : MonoBehaviour
{
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject nextShop;
    private CharacterDeath bossDeath;

    private void Start()
    {
        GetComponent<TilemapRenderer>().enabled = false;
        GetComponent<TilemapCollider2D>().enabled = false;
        bossDeath = GetComponentInChildren<CharacterDeath>();
        bossDeath.BossDeathEvent += OpenPath;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 && GetComponent<TilemapRenderer>().enabled == false)
        {
            GetComponent<TilemapRenderer>().enabled = true;
            GetComponent<TilemapCollider2D>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void OpenPath()
    {
        if (GetComponent<TilemapRenderer>().enabled == true)
        {
            gameObject.SetActive(false);
            if(shop != null)
            {
                shop.SetActive(false);
                if(nextShop != null)
                    nextShop.SetActive(true);
            }
                
        }
    }

    private void OnDisable()
    {
        bossDeath.BossDeathEvent -= OpenPath;
    }
}
