using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private EnemyHealth health;

    void Start()
    {
        health = GetComponent<EnemyHealth>();
        health.DeathEvent += Die;
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        health.DeathEvent -= Die;
    }
}
