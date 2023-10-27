using UnityEngine;

public class TweenRotation : MonoBehaviour
{
    #region Field
    // Easing 그래프 : 0부터 1구간 그래프에서
    // from - to 위치들을 duration 동안 Linear 그래프 값을 통해 이동 
    [Header("Animation 조정")][SerializeField] AnimationCurve animationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    [Header("초기 기준 각도")][SerializeField] Vector3 from;
    [Header("목표 기준 각도")][SerializeField] Vector3 to;
    [Header("재생 시간")][SerializeField] float duration = 1f;

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
    public void Play(Vector3 from, Vector3 to, float duration)
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
    /// 현재 시점에서의 회전 각도 계산 및 회전
    /// </summary>
    /// <param name="curveTime">그래프의 현재 시간</param>
    void SetValue(float curveTime)
    {
        // Evaluate() : AnimationCurve의 매개변수를 반환함
        // (그래프의 가로)시간을 대입했으니 해당 시간에 대한 (그래프의 세로)값을 계산하여 변환
        // 시간이 0에 가까울 수록 from, 1에 가까울 수록 to(0.5면 절반)
        var value = animationCurve.Evaluate(curveTime);
        // 해당 그래프의 좌표 값 = 대상의 회전 각
        var result = (from * (1f - value)) + (to * value);
        // 해당 값을 현재 회전 각도에 대입
        transform.rotation = Quaternion.Euler(result);
    }
    #endregion


    #region Event Method
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
