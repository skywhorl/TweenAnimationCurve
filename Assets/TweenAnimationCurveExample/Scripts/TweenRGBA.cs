using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TweenRGBA : MonoBehaviour
{
    #region Field
    // Easing 그래프 : 0부터 1구간 그래프에서
    // from - to 위치들을 duration 동안 Linear 그래프 값을 통해 이동 
    [Header("Animation 조정")][SerializeField] AnimationCurve animationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    [Header("초기 기준 색상")][SerializeField] Color32 from;
    [Header("목표 기준 색상")][SerializeField] Color32 to;
    [Header("재생 시간")][SerializeField] float duration = 1f;

    // 3D/2D/UI에 색상 적용을 도와주는 Component
    // UI는 Image와 Text Component만 지원
    MeshRenderer meshRendererComponent;
    SpriteRenderer spriteRendererComponent;
    Image imageComponent;
    Text textComponent;
    // 색상을 적용할 Material
    Material changeMaterial;


    // true 시 Animation 작동
    // ====== 다른 Script에서 해당 변수를 제어하려면 Play() 함수 사용 ====== //
    [Header("Animation 재생 여부")][SerializeField] bool startAnimation;

    // duration을 확인할 변수
    float checkTime = 0f;
    #endregion


    #region Activate Method
    /// <summary>
    /// Animation을 재생 설정해주는 함수
    /// </summary>
    public void Play()
    {
        startAnimation = true;
    }

    /// <summary>
    /// Overloading, 코드를 통해 설정된 내용을 적용해주는 함수 
    /// </summary>
    /// <param name="from">시작 점</param>
    /// <param name="to">끝 점</param>
    /// <param name="duration">재생 기간</param>
    public void Play(Color32 from, Color32 to, float duration)
    {
        this.from = from;
        this.to = to;
        this.duration = duration;
        Play();
    }

    /// <summary>
    /// Animation을 중지하는 함수
    /// </summary>
    public void Stop()
    {
        startAnimation = false;
    }

    /// <summary>
    /// 현재 시점에서의 색상 계산 및 색 변경
    /// </summary>
    /// <param name="curveTime">그래프의 현재 시간</param>
    void SetValue(float curveTime)
    {
        // Evaluate() : AnimationCurve의 매개변수를 반환함
        // (그래프의 가로)시간을 대입했으니 해당 시간에 대한 (그래프의 세로)값을 계산하여 변환
        // 시간이 0에 가까울 수록 from, 1에 가까울 수록 to(0.5면 절반)
        var value = animationCurve.Evaluate(curveTime);
        // 해당 그래프의 좌표 값 = 변화하는 대상의 색 값(float로 연산 해야 해서 분리)
        var resultR = (byte)((from.r * (1f - value)) + (to.r * value));
        var resultG = (byte)((from.g * (1f - value)) + (to.g * value));
        var resultB = (byte)((from.b * (1f - value)) + (to.b * value));
        var resultA = (byte)((from.a * (1f - value)) + (to.a * value));
        // 해당 값을 현재 색에 대입(float 연산을 했으므로 다시 색의 값인 byte로 적용)
        changeMaterial.color = new Color32(resultR, resultG, resultB, resultA);
    }
    #endregion


    #region Event Method
    void Start()
    {
        // 색상 적용할 Component 불러오기
        meshRendererComponent = GetComponent<MeshRenderer>();
        spriteRendererComponent = GetComponent<SpriteRenderer>();
        imageComponent = GetComponent<Image>();
        textComponent = GetComponent<Text>();
        // 색상을 적용해야 할 Component가 무엇인지 확인하기
        if(meshRendererComponent != null)               changeMaterial = meshRendererComponent.material;
        else if(spriteRendererComponent != null)        changeMaterial = spriteRendererComponent.material;
        else if(imageComponent != null)                 changeMaterial = imageComponent.material;
        else if(textComponent != null)                  changeMaterial = textComponent.material;
        else
        {
#if UNITY_EDITOR
            bool endUnity = EditorUtility.DisplayDialog
                ("오류로 인한 Unity Editor 종료", $"해당 GameObjet : {gameObject.name}의 AnimationCurve를 통해 색을 적용할 Component인 MeshRenderer 또는 SpriteRenderer가 존재하지 않습니다.\nGameObject의 Component를 확인해주세요.", "OK");
            if (endUnity)
                EditorApplication.isPlaying = false;
#endif
        }

    }

    void Update()
    {
        // Animation 재생 시
        if (startAnimation)
        {
            // 그래프가 0과 1 사이이므로 duration을 1로 변환
            checkTime += Time.deltaTime / duration;
            // 그래프의 현재 시점을 가지고 그래프의 값 계산하기
            SetValue(checkTime);

            // 시간이 1 경과 시
            if (checkTime >= 1)
            {
                // checkTime의 값이 1의 근사치에서 연산하다 끝나므로 마지막 시점(to의 시간 = 1)에서의 값 적용
                SetValue(1f);
                // 다시 초기화(그래프의 원점으로)
                checkTime = 0f;
                // Animation 중지
                startAnimation = false;
                // 여기서 break를 안 하면 아래의 값이 다시 계산 되어서 위치가 원점으로 돌아오게 됨
                return;
            }
        }
    }
    #endregion
}
