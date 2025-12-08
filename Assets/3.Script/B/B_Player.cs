using System.Collections;
using UnityEngine;

public class B_Player : MonoBehaviour
{
    [SerializeField] private HeartUI heartUI;
    [SerializeField] private float invincibleTime = 2f;
    [SerializeField] private float blinkSpeed = 0.1f;
    [SerializeField] private int maxHealth = 3;
    //private MeshRenderer playerMR;
    private Color originalColor;
    private bool isInvincible = false;
    private int currentHealth;

    private void Awake()
    {
        //TryGetComponent(out playerMR);

        //originalColor = playerMR.material.color;
    }

    void Start()
    {
        currentHealth = maxHealth;

        heartUI.CreateHearts(maxHealth);
    }

    public void OnDamaged(int damage)
    {
        if (isInvincible || currentHealth <= 0) return;

        currentHealth -= damage;

        heartUI.UpdateHearts(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            //StartCoroutine(Invincible_co());
        }
    }

    //private IEnumerator Invincible_co()
    //{
    //    isInvincible = true;
    //
    //    float timer = 0f;
    //
    //    while (timer < invincibleTime)
    //    {
    //        if (playerMR != null) playerMR.material.color = Color.red;
    //        yield return new WaitForSeconds(blinkSpeed);
    //
    //        if (playerMR != null) playerMR.material.color = originalColor;
    //        yield return new WaitForSeconds(blinkSpeed);
    //
    //        timer += (blinkSpeed * 2);
    //    }
    //
    //    if (playerMR != null) playerMR.material.color = originalColor;
    //    isInvincible = false;
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            OnDamaged(1);
        }
    }

    private void Die()
    {
        B_GameManager.instance.isLive = false;
    }
}
