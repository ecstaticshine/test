using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private Sprite emptyHeartSprite;

    private List<Image> heartList = new List<Image>();

    private void OnEnable()
    {
        if (B_GameManager.instance != null)
        {
            B_GameManager.instance.OnHealthChanged += UpdateHearts;
        }
    }

    private void OnDisable()
    {
        if (B_GameManager.instance != null)
        {
            B_GameManager.instance.OnHealthChanged -= UpdateHearts;
        }
    }

    public void CreateHearts(int maxHealth)
    {
        foreach (Image heart in heartList)
        {
            Destroy(heart.gameObject);
        }
        heartList.Clear();

        for (int i = 0; i < maxHealth; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, transform);

            Image heartImage = newHeart.GetComponent<Image>();

            heartImage.sprite = fullHeartSprite;

            heartList.Add(heartImage);
        }
    }

    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < heartList.Count; i++)
        {
            if (i < currentHealth)
            {
                heartList[i].sprite = fullHeartSprite;
            }
            else
            {
                heartList[i].sprite = emptyHeartSprite;
            }
        }
    }
}