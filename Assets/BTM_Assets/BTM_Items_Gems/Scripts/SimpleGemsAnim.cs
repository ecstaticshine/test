using UnityEngine;
using System.Collections;

namespace Benjathemaker
{
    public class SimpleGemsAnim : MonoBehaviour
    {
        public bool isRotating = false;
        public bool rotateX = false;
        public bool rotateY = false;
        public bool rotateZ = false;
        public float rotationSpeed = 90f; // Degrees per second

        public bool isFloating = false;
        public bool useEasingForFloating = false; // Separate toggle for floating ease
        public float floatHeight = 1f; // Max height displacement
        public float floatSpeed = 1f;
        private Vector3 initialPosition;
        private float floatTimer;

        private Vector3 initialScale;
        public Vector3 startScale;
        public Vector3 endScale;

        public bool isScaling = false;
        public bool useEasingForScaling = false; // Separate toggle for scaling ease
        public float scaleLerpSpeed = 1f; // Speed of scaling transition
        private float scaleTimer;

        void Start()
        {
            initialScale = transform.localScale;
            // [유니 수정 1] 월드 좌표(position) 대신 로컬 좌표(localPosition)를 기억하게 해!
            initialPosition = transform.localPosition;

            // Adjust start and end scale based on initial scale
            startScale = initialScale;
            // [유니] magnitude 계산할 때 null 체크나 0 체크가 있으면 더 완벽하지만 일단 오빠 코드 존중!
            if (startScale.magnitude > 0)
                endScale = initialScale * (endScale.magnitude / startScale.magnitude);
        
        }
        

        void Update()
        {
            if (isRotating)
            {
                Vector3 rotationVector = new Vector3(
                    rotateX ? 1 : 0,
                    rotateY ? 1 : 0,
                    rotateZ ? 1 : 0
                );
                transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime);
            }

            if (isFloating)
            {
                floatTimer += Time.deltaTime * floatSpeed;
                float t = Mathf.PingPong(floatTimer, 1f);
                if (useEasingForFloating) t = EaseInOutQuad(t);

                // [유니 수정 2] transform.position 대신 transform.localPosition을 써!
                // 이제 부모 오브젝트가 움직이면, 얘는 부모를 따라서 같이 움직이면서 위아래로만 둥둥 떠!
                transform.localPosition = initialPosition + new Vector3(0, t * floatHeight, 0);
            }
            

            if (isScaling)
            {
                scaleTimer += Time.deltaTime * scaleLerpSpeed;
                float t = Mathf.PingPong(scaleTimer, 1f); // Oscillates between 0 and 1

                if (useEasingForScaling)
                {
                    t = EaseInOutQuad(t);
                }

                transform.localScale = Vector3.Lerp(startScale, endScale, t);
            }
        }

        float EaseInOutQuad(float t)
        {
            return t < 0.5f ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) / 2;
        }
    }
}

