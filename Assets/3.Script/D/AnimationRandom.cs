using UnityEngine;

public class AnimationRandom : MonoBehaviour
{
    private Animator anime;

    public string[] AnimationName = new string[] { "Breakdance 1990", "SittingPose", "CongCOng", "SnakeDance", "NormalPose" };

    private const int BASE_LAYER_INDEX = 0; 
    private void Start()
    {
        TryGetComponent(out anime);
    }
    public void PlayRandomCharacterAnimation()
    {
        if (anime == null)
        {
            Debug.LogError("[오류] Animator 컴포넌트가 캐릭터 오브젝트에 없습니다. PlayRandomCharacterAnimation 실행 불가.");
            return;
        }

        // [안전 검사] 재생할 애니메이션 목록이 비어 있는지 확인합니다.
        if (AnimationName.Length == 0)
        {
            Debug.LogWarning("[경고] 재생할 애니메이션 이름 목록(animationNames)이 비어 있습니다.");
            return;
        }
        float targetYRotation;

        int randomIndex = Random.Range(0, AnimationName.Length);
        if(randomIndex == 1)
        {
            targetYRotation = 120f;
        }
        else
        {
            targetYRotation = 180f;
        }

        transform.rotation = Quaternion.Euler(0f, targetYRotation, 0f);

        string animeToPlay = AnimationName[randomIndex];

        anime.Play(animeToPlay, BASE_LAYER_INDEX);

        Debug.Log($"[Random Anim 실행 완료] 선택된 애니메이션: {animeToPlay}");
    }
}

