using UnityEngine;


public class ScoreInputSceneAnimation : MonoBehaviour
{
    private Animator animator;

    
    public string[] animationNames = new string[] { "Idle_Anim", "Run_Anim", "Jump_Anim" };

    
    private const int BASE_LAYER_INDEX = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        PlayRandomCharacterAnimation();

    }

    public void PlayRandomCharacterAnimation()
    {

        float TargetRotation;
        if (animator == null)
        {
            //Debug.LogError("[오류] Animator 컴포넌트가 캐릭터 오브젝트에 없습니다. PlayRandomCharacterAnimation 실행 불가.");
            return;
        }

        if (animationNames.Length == 0)
        {
            //Debug.LogWarning("[경고] 재생할 애니메이션 이름 목록(animationNames)이 비어 있습니다.");
            return;
        }

        int randomIndex = Random.Range(0, animationNames.Length);

        if(randomIndex == 2)
        {
            TargetRotation = 82f;
        }
        else
        {
            TargetRotation = 139f;
        }

        transform.rotation = Quaternion.Euler(0f, TargetRotation, 0f);

        string animToPlay = animationNames[randomIndex];

        animator.Play(animToPlay, BASE_LAYER_INDEX);

        //Debug.Log($"[Random Anim 실행 완료] 선택된 애니메이션: {animToPlay}");
    }
}