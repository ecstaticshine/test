using System.Collections;
using UnityEngine;

public class B_Player : MonoBehaviour
{
    [SerializeField] private HeartUI heartUI;
    [SerializeField] private float invincibleTime = 2f;
    [SerializeField] private float blinkSpeed = 0.1f;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private SkinnedMeshRenderer[] playerMRs;
    [SerializeField] private Color[] originalColors;
    private Animator playerAni;
    private bool isInvincible = false;
    private bool isDie = false;
    private int currentHealth;

    private void Awake()
    {
        playerMRs = GetComponentsInChildren<SkinnedMeshRenderer>();

        if (playerMRs != null && playerMRs.Length > 0)
        {
            originalColors = new Color[playerMRs.Length];

            for (int i = 0; i < playerMRs.Length; i++)
            {
                if (playerMRs[i].material.HasProperty("_BaseColor"))
                {

                    originalColors[i] = playerMRs[i].material.GetColor("_Color");
                }
                else
                {
                    originalColors[i] = playerMRs[i].material.color;
                }
            }
        }

        TryGetComponent(out playerAni);
    }

    void Start()
    {
        currentHealth = maxHealth;

        heartUI.CreateHearts(maxHealth);
    }

    private void Update()
    {
        if (currentHealth <= 0 && B_GameManager.instance.isLive)
        {
            //B_GameManager.instance.isClear = true;
            //playerAni.SetBool("IsWin", true);
            //isClear = true;
            //isInvincible = true;

            Die();
        }
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
            StartCoroutine(OnDamaged_co());
        }
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    public void healHealth()
    {
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth++;
        }
    }

    private IEnumerator OnDamaged_co()
    {
        isInvincible = true;

        float timer = 0f;

        while (timer < invincibleTime)
        {
            foreach (var mr in playerMRs)
            {
                if (mr == null) continue;
                mr.material.color = Color.red;
            }

            yield return new WaitForSeconds(blinkSpeed);

            for (int i = 0; i < playerMRs.Length; i++)
            {
                if (playerMRs[i] == null) continue;

                playerMRs[i].material.color = originalColors[i];
            }

            yield return new WaitForSeconds(blinkSpeed);

            timer += (blinkSpeed * 2);
        }

        for (int i = 0; i < playerMRs.Length; i++)
        {
            if (playerMRs[i] == null) continue;

            playerMRs[i].material.color = originalColors[i];
        }

        isInvincible = false;
    }

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

        playerAni.SetTrigger("IsLose");
    }

    // 추가 예정
    private void Urararara()
    {
        if (B_GameManager.instance.character != 2) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isInvincible = true;
        }
    }
}
