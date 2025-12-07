using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WarningZone : MonoBehaviour
{
    [SerializeField] private Sprite imageA;
    [SerializeField] private Sprite imageB;

    [SerializeField] private float changeSpeed = 0.2f;
    [SerializeField] private float duration = 2f;

    private Image targetImage;

    private void Awake()
    {
        targetImage = GetComponentInChildren<Image>();
    }

    private void OnEnable()
    {
        StartCoroutine(TwoSpriteRoutine());
    }

    private IEnumerator TwoSpriteRoutine()
    {
        float timer = 0f;
        bool isTurnA = true;

        while (timer < duration)
        {
            targetImage.sprite = isTurnA ? imageA : imageB;

            isTurnA = !isTurnA;

            yield return new WaitForSeconds(changeSpeed);
            timer += changeSpeed;
        }

        gameObject.SetActive(false);
    }
}