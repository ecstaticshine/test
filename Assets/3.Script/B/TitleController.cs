using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float minAlpha = 0.1f;
    [SerializeField] private float maxAlpha = 1.0f;
    [SerializeField] private Text targetText;
    private Color originalTextColor;
    private bool isInputLocked = false;

    private void Awake()
    {
        originalTextColor = targetText.color;
    }

    private void Update()
    {
        float time = Mathf.PingPong(Time.time * speed, 1f);

        float alpha = Mathf.Lerp(minAlpha, maxAlpha, time);

        targetText.color = new Color(originalTextColor.r, originalTextColor.g, originalTextColor.b, alpha);

        if (Input.anyKeyDown && !isInputLocked)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            {
                return;
            }

            isInputLocked = true;

            if (SceneLoader.Instance != null)
            {
                SceneLoader.Instance.LoadSelectScene();
            }
        }
    }
}