using UnityEngine;
using UnityEditor;

/// <summary>
/// TweenSample Scene���� AnimationCurve ����� �����ִ� Script
/// </summary>
public class ExampleManager : MonoBehaviour
{
    #region Field
    // ��� ��ư â ���� ���� ����
    bool openExampleList = false;

    // ��ư�� ���� Tween ��� ���� ���� ����
    bool playAnimation_Position = false;
    bool playAnimation_Rotation = false;
    bool playAnimation_Scale = false;
    bool playAnimation_RGBA = false;
    // Tween ��ü ����� ���� ����
    bool playAnimation_All = false;

    // ��ü ��� ���� �ٸ� Animation ����� ���� ���� �뵵
    bool isPlayAll = false;
    // �ش� Component���� m_isStart�� ������ �� �־� ExampleManager.cs������ ���� ���θ� Ȯ���ش� ����
    bool isExample_Position = false;
    bool isExample_Rotation = false;
    bool isExample_Scale = false;
    bool isExample_RGBA = false;
    // Error Dialog ���� ���� ���� �뵵
    int dialogCount = 0;

    [Header("AnimationCurve Example Components")]
    [SerializeField] TweenPosition tweenPosition;
    [SerializeField] TweenRotation tweenRotation;
    [SerializeField] TweenScale tweenScale;
    [SerializeField] TweenRGBA tweenRGBA;

    // ������ �ϴ� Layer�� Button ���� �� ��ư���� ���̵��� Layer�� ���̸� ���������� �����ϱ� ���� ����
    int m_layerHeight = 40;
    #endregion


    /// <summary>
    /// ���õ� Tween�� ����� �����ִ� �Լ���
    /// OnGUI()���� GetComponent�� �ٷ� �� �Ǿ� �Լ����� ����� Compoennt�� �����ϴ��� �˻� �� ����
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
            if (EditorUtility.DisplayDialog("Component Error!", $"{gameObject.name}�� ��ϵ� AnimationCurve GameObject �� �Ϻ� Component�� �����Ǿ����ϴ�.\n�ش� GameObject�� Component ���縦 Ȯ�����ּ���.", "Yes"))
                EditorApplication.isPlaying = false;
        }
    }
    #endregion


    #region Event Method
    private void OnGUI()
    {
        // ��ġ ���� Screen ũ�� �������� ������ ȭ�� ���� ������� ���� �ϴܿ� ���� �� �ֵ��� Layer �׸���
        GUILayout.BeginArea(new Rect(Screen.width - 210, Screen.height - m_layerHeight, 200, 300), GUI.skin.box);


        //=========== ��� ��ư ��� ���� =========== //
        openExampleList = GUILayout.Toggle(openExampleList, "��� ����", GUI.skin.button);
        // ��� ��ư Ȱ��ȭ ��
        if (openExampleList)
        {
            // Layer�� ���� ����(������ �ö�ͼ� �� ��ó�� ����)
            m_layerHeight = 300;
            // �Ʒ��� ǥ�õǴ� ���׵��� ������ 10 pixel ���������� ����
            GUILayout.Space(10);


            //=========== Tween - ��ü ��� ��ư ���� =========== //
            playAnimation_All = GUILayout.Toggle(playAnimation_All, "Tween - ��ü ���", GUI.skin.button);
            // ��ü ��� ��ư Ȱ��ȭ ��
            if (playAnimation_All)
            {
                // ��ü ����� ��Ȱ��ȭ ���¶��
                if (!isPlayAll)
                {
                    // ��� ��� ��ư Ȱ��ȭ
                    playAnimation_Position = true;
                    playAnimation_Rotation = true;
                    playAnimation_Scale = true;
                    playAnimation_RGBA = true;
                    // ��ü ��� ���� Ȯ��
                    isPlayAll = true;
                }
            }
            // ��ü ��� ��ư ��Ȱ��ȭ ��
            else
            {
                // ��ü ����� Ȱ��ȭ ���¶��
                if (isPlayAll)
                {
                    // ��� ��� ��ư ��Ȱ��ȭ
                    playAnimation_Position = false;
                    playAnimation_Rotation = false;
                    playAnimation_Scale = false;
                    playAnimation_RGBA = false;
                    // ��ü ��� ��� Ȯ��
                    isPlayAll = false;
                }
            }


            // �Ʒ��� ǥ�õǴ� ���׵�κ����� ������ 20 pixel ���������� ����
            GUILayout.Space(20);


            //=========== Tween - Position ��ư ���� =========== //
            playAnimation_Position = GUILayout.Toggle(playAnimation_Position, "Tween - Position ���", GUI.skin.button);

            // Component ��ü���� ����Ǵ� ���� �ƴ� OnGUI()�� ���� ����� ���� ����
            if (playAnimation_Position && !isExample_Position)
            {
                PlayTweenPosition();
                isExample_Position = true;
            }
            else
            {
                // OnGUI()�� ���� ��� ���� ���� ��� ���
                if (isExample_Position)
                {
                    isExample_Position = false;
                    tweenPosition.Stop();
                }
            }




            //=========== Tween - Rotation ��ư ���� =========== //
            playAnimation_Rotation = GUILayout.Toggle(playAnimation_Rotation, "Tween - Rotation ���", GUI.skin.button);

            // Component ��ü���� ����Ǵ� ���� �ƴ� OnGUI()�� ���� ����� ���� ����
            if (playAnimation_Rotation && !isExample_Rotation)
            {
                PlayTweenRotation();
                isExample_Rotation = true;
            }
            else
            {
                // OnGUI()�� ���� ��� ���� ���� ��� ���
                if (isExample_Rotation)
                {
                    isExample_Rotation = false;
                    tweenRotation.Stop();
                }
            }




            //=========== Tween - Scale ��ư ���� =========== //
            playAnimation_Scale = GUILayout.Toggle(playAnimation_Scale, "Tween - Scale ���", GUI.skin.button);

            // Component ��ü���� ����Ǵ� ���� �ƴ� OnGUI()�� ���� ����� ���� ����
            if (playAnimation_Scale && !isExample_Scale)
            {
                PlayTweenScale();
                isExample_Scale = true;
            }
            else
            {
                // OnGUI()�� ���� ��� ���� ���� ��� ���
                if (isExample_Scale)
                {
                    isExample_Scale = false;
                    tweenScale.Stop();
                }
            }



            //=========== Tween - RGBA ��ư ���� =========== //
            playAnimation_RGBA = GUILayout.Toggle(playAnimation_RGBA, "Tween - RGBA ���", GUI.skin.button);

            // Component ��ü���� ����Ǵ� ���� �ƴ� OnGUI()�� ���� ����� ���� ����
            if (playAnimation_RGBA && !isExample_RGBA)
            {
                PlayTweenRGBA();
                isExample_RGBA = true;
            }
            else
            {
                // OnGUI()�� ���� ��� ���� ���� ��� ���
                if (isExample_RGBA)
                {
                    isExample_RGBA = false;
                    tweenRGBA.Stop();
                }
            }


            //=========== Tween - ��ü ��� ��ư ���� =========== //
            // ��ü ��� �� �Ϻΰ� ��� ������ �Ǿ��ٸ� Tween - ��ü ��� ��ư ȿ�� ����
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
            // ��Ȱ��ȭ �����̹Ƿ� Layer ������� ���̱�
            m_layerHeight = 40;
        }
        GUILayout.EndArea();
    }
    #endregion
}
