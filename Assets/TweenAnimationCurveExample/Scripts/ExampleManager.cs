using UnityEngine;
using UnityEditor;

/// <summary>
/// TweenSample Scene에서 AnimationCurve 재생을 도와주는 Script
/// </summary>
public class ExampleManager : MonoBehaviour
{
    #region Field
    // 재생 버튼 창 열기 위한 변수
    bool openExampleList = false;

    // 버튼을 통한 Tween 재생 여부 결정 변수
    bool playAnimation_Position = false;
    bool playAnimation_Rotation = false;
    bool playAnimation_Scale = false;
    bool playAnimation_RGBA = false;
    // Tween 전체 재생을 위한 변수
    bool playAnimation_All = false;

    // 전체 재생 도중 다른 Animation 재생을 끄기 위한 용도
    bool isPlayAll = false;
    // 해당 Component에서 m_isStart를 제어할 수 있어 ExampleManager.cs에서의 제어 여부를 확인해는 변수
    bool isExample_Position = false;
    bool isExample_Rotation = false;
    bool isExample_Scale = false;
    bool isExample_RGBA = false;
    // Error Dialog 띄우는 개수 제한 용도
    int dialogCount = 0;

    [Header("AnimationCurve Example Components")]
    [SerializeField] TweenPosition tweenPosition;
    [SerializeField] TweenRotation tweenRotation;
    [SerializeField] TweenScale tweenScale;
    [SerializeField] TweenRGBA tweenRGBA;

    // 오른쪽 하단 Layer의 Button 선택 시 버튼들이 보이도록 Layer의 높이를 유동적으로 변경하기 위한 변수
    int m_layerHeight = 40;
    #endregion


    /// <summary>
    /// 선택된 Tween의 재생을 도와주는 함수들
    /// OnGUI()에서 GetComponent가 바로 안 되어 함수마다 재생할 Compoennt가 존재하는지 검사 후 진행
    /// </summary>
    #region Activate Method
    void PlayTweenPosition()
    {
        if (tweenPosition == null)
        {
            try { tweenPosition = GetComponent<TweenPosition>(); tweenPosition.Play(); }
            catch { if (!IsInvoking("ErrorComponent")) Invoke("ErrorComponent", 0f); }
        }
        tweenPosition.Play();
    }

    void PlayTweenRotation()
    {
        if (tweenRotation == null)
        {
            try { tweenRotation = GetComponent<TweenRotation>(); tweenRotation.Play(); }
            catch { if (!IsInvoking("ErrorComponent")) Invoke("ErrorComponent", 0f); }
        }
        tweenRotation.Play();
    }

    void PlayTweenScale()
    {
        if (tweenScale == null)
        {
            try { tweenScale = GetComponent<TweenScale>(); tweenScale.Play(); }
            catch { if (!IsInvoking("ErrorComponent")) Invoke("ErrorComponent", 0f); }
        }
        tweenScale.Play();
    }

    void PlayTweenRGBA()
    {
        if (tweenRGBA == null)
        {
            try { tweenRGBA = GetComponent<TweenRGBA>(); tweenRGBA.Play(); }
            catch { if (!IsInvoking("ErrorComponent")) Invoke("ErrorComponent", 0f); }
        } 
        tweenRGBA.Play();
    }

    void ErrorComponent()
    {
        dialogCount++;
        if(dialogCount < 2)
        {
            if (EditorUtility.DisplayDialog("Component Error!", $"{gameObject.name}에 등록된 AnimationCurve GameObject 중 일부 Component가 누락되었습니다.\n해당 GameObject의 Component 존재를 확인해주세요.", "Yes"))
                EditorApplication.isPlaying = false;
        }
    }
    #endregion


    #region Event Method
    private void OnGUI()
    {
        // 위치 값을 Screen 크기 기준으로 조정해 화면 비율 상관없이 우측 하단에 보일 수 있도록 Layer 그리기
        GUILayout.BeginArea(new Rect(Screen.width - 210, Screen.height - m_layerHeight, 200, 300), GUI.skin.box);


        //=========== 재생 버튼 목록 영역 =========== //
        openExampleList = GUILayout.Toggle(openExampleList, "재생 선택", GUI.skin.button);
        // 재생 버튼 활성화 시
        if (openExampleList)
        {
            // Layer의 높이 수정(위에서 올라와서 뜬 것처럼 설정)
            m_layerHeight = 300;
            // 아래에 표시되는 사항들의 간격을 10 pixel 벌어지도록 설정
            GUILayout.Space(10);


            //=========== Tween - 전체 재생 버튼 영역 =========== //
            playAnimation_All = GUILayout.Toggle(playAnimation_All, "Tween - 전체 재생", GUI.skin.button);
            // 전체 재생 버튼 활성화 시
            if (playAnimation_All)
            {
                // 전체 재생이 비활성화 상태라면
                if (!isPlayAll)
                {
                    // 모든 재생 버튼 활성화
                    playAnimation_Position = true;
                    playAnimation_Rotation = true;
                    playAnimation_Scale = true;
                    playAnimation_RGBA = true;
                    // 전체 재생 여부 확인
                    isPlayAll = true;
                }
            }
            // 전체 재생 버튼 비활성화 시
            else
            {
                // 전체 재생이 활성화 상태라면
                if (isPlayAll)
                {
                    // 모든 재생 버튼 비활성화
                    playAnimation_Position = false;
                    playAnimation_Rotation = false;
                    playAnimation_Scale = false;
                    playAnimation_RGBA = false;
                    // 전체 재생 취소 확인
                    isPlayAll = false;
                }
            }


            // 아래에 표시되는 사항들로부터의 간격을 20 pixel 벌어지도록 설정
            GUILayout.Space(20);


            //=========== Tween - Position 버튼 영역 =========== //
            playAnimation_Position = GUILayout.Toggle(playAnimation_Position, "Tween - Position 재생", GUI.skin.button);

            // Component 자체에서 재생되는 것이 아닌 OnGUI()를 통해 재생할 때만 실행
            if (playAnimation_Position && !isExample_Position)
            {
                PlayTweenPosition();
                isExample_Position = true;
            }
            else
            {
                // OnGUI()를 통해 재생 중일 때만 재생 취소
                if (isExample_Position)
                {
                    isExample_Position = false;
                    tweenPosition.Stop();
                }
            }




            //=========== Tween - Rotation 버튼 영역 =========== //
            playAnimation_Rotation = GUILayout.Toggle(playAnimation_Rotation, "Tween - Rotation 재생", GUI.skin.button);

            // Component 자체에서 재생되는 것이 아닌 OnGUI()를 통해 재생할 때만 실행
            if (playAnimation_Rotation && !isExample_Rotation)
            {
                PlayTweenRotation();
                isExample_Rotation = true;
            }
            else
            {
                // OnGUI()를 통해 재생 중일 때만 재생 취소
                if (isExample_Rotation)
                {
                    isExample_Rotation = false;
                    tweenRotation.Stop();
                }
            }




            //=========== Tween - Scale 버튼 영역 =========== //
            playAnimation_Scale = GUILayout.Toggle(playAnimation_Scale, "Tween - Scale 재생", GUI.skin.button);

            // Component 자체에서 재생되는 것이 아닌 OnGUI()를 통해 재생할 때만 실행
            if (playAnimation_Scale && !isExample_Scale)
            {
                PlayTweenScale();
                isExample_Scale = true;
            }
            else
            {
                // OnGUI()를 통해 재생 중일 때만 재생 취소
                if (isExample_Scale)
                {
                    isExample_Scale = false;
                    tweenScale.Stop();
                }
            }



            //=========== Tween - RGBA 버튼 영역 =========== //
            playAnimation_RGBA = GUILayout.Toggle(playAnimation_RGBA, "Tween - RGBA 재생", GUI.skin.button);

            // Component 자체에서 재생되는 것이 아닌 OnGUI()를 통해 재생할 때만 실행
            if (playAnimation_RGBA && !isExample_RGBA)
            {
                PlayTweenRGBA();
                isExample_RGBA = true;
            }
            else
            {
                // OnGUI()를 통해 재생 중일 때만 재생 취소
                if (isExample_RGBA)
                {
                    isExample_RGBA = false;
                    tweenRGBA.Stop();
                }
            }


            //=========== Tween - 전체 재생 버튼 영역 =========== //
            // 전체 재생 중 일부가 재생 중지가 되었다면 Tween - 전체 재생 버튼 효과 해제
            if (!(playAnimation_Position && playAnimation_Rotation && playAnimation_Scale && playAnimation_RGBA))
            {
                playAnimation_All = false;
                isPlayAll = false;
            }
            else
                playAnimation_All = true;

        }
        else
        {
            // 비활성화 상태이므로 Layer 원래대로 줄이기
            m_layerHeight = 40;
        }
        GUILayout.EndArea();
    }
    #endregion
}
