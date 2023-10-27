using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TweenRGBA : MonoBehaviour
{
    #region Field
    // Easing �׷��� : 0���� 1���� �׷�������
    // from - to ��ġ���� duration ���� Linear �׷��� ���� ���� �̵� 
    [Header("Animation ����")][SerializeField] AnimationCurve animationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    [Header("�ʱ� ���� ����")][SerializeField] Color32 from;
    [Header("��ǥ ���� ����")][SerializeField] Color32 to;
    [Header("��� �ð�")][SerializeField] float duration = 1f;

    // 3D/2D/UI�� ���� ������ �����ִ� Component
    // UI�� Image�� Text Component�� ����
    MeshRenderer meshRendererComponent;
    SpriteRenderer spriteRendererComponent;
    Image imageComponent;
    Text textComponent;
    // ������ ������ Material
    Material changeMaterial;


    // true �� Animation �۵�
    // ====== �ٸ� Script���� �ش� ������ �����Ϸ��� Play() �Լ� ��� ====== //
    [Header("Animation ��� ����")][SerializeField] bool startAnimation;

    // duration�� Ȯ���� ����
    float checkTime = 0f;
    #endregion


    #region Activate Method
    /// <summary>
    /// Animation�� ��� �������ִ� �Լ�
    /// </summary>
    public void Play()
    {
        startAnimation = true;
    }

    /// <summary>
    /// Overloading, �ڵ带 ���� ������ ������ �������ִ� �Լ� 
    /// </summary>
    /// <param name="from">���� ��</param>
    /// <param name="to">�� ��</param>
    /// <param name="duration">��� �Ⱓ</param>
    public void Play(Color32 from, Color32 to, float duration)
    {
        this.from = from;
        this.to = to;
        this.duration = duration;
        Play();
    }

    /// <summary>
    /// Animation�� �����ϴ� �Լ�
    /// </summary>
    public void Stop()
    {
        startAnimation = false;
    }

    /// <summary>
    /// ���� ���������� ���� ��� �� �� ����
    /// </summary>
    /// <param name="curveTime">�׷����� ���� �ð�</param>
    void SetValue(float curveTime)
    {
        // Evaluate() : AnimationCurve�� �Ű������� ��ȯ��
        // (�׷����� ����)�ð��� ���������� �ش� �ð��� ���� (�׷����� ����)���� ����Ͽ� ��ȯ
        // �ð��� 0�� ����� ���� from, 1�� ����� ���� to(0.5�� ����)
        var value = animationCurve.Evaluate(curveTime);
        // �ش� �׷����� ��ǥ �� = ��ȭ�ϴ� ����� �� ��(float�� ���� �ؾ� �ؼ� �и�)
        var resultR = (byte)((from.r * (1f - value)) + (to.r * value));
        var resultG = (byte)((from.g * (1f - value)) + (to.g * value));
        var resultB = (byte)((from.b * (1f - value)) + (to.b * value));
        var resultA = (byte)((from.a * (1f - value)) + (to.a * value));
        // �ش� ���� ���� ���� ����(float ������ �����Ƿ� �ٽ� ���� ���� byte�� ����)
        changeMaterial.color = new Color32(resultR, resultG, resultB, resultA);
    }
    #endregion


    #region Event Method
    void Start()
    {
        // ���� ������ Component �ҷ�����
        meshRendererComponent = GetComponent<MeshRenderer>();
        spriteRendererComponent = GetComponent<SpriteRenderer>();
        imageComponent = GetComponent<Image>();
        textComponent = GetComponent<Text>();
        // ������ �����ؾ� �� Component�� �������� Ȯ���ϱ�
        if(meshRendererComponent != null)               changeMaterial = meshRendererComponent.material;
        else if(spriteRendererComponent != null)        changeMaterial = spriteRendererComponent.material;
        else if(imageComponent != null)                 changeMaterial = imageComponent.material;
        else if(textComponent != null)                  changeMaterial = textComponent.material;
        else
        {
#if UNITY_EDITOR
            bool endUnity = EditorUtility.DisplayDialog
                ("������ ���� Unity Editor ����", $"�ش� GameObjet : {gameObject.name}�� AnimationCurve�� ���� ���� ������ Component�� MeshRenderer �Ǵ� SpriteRenderer�� �������� �ʽ��ϴ�.\nGameObject�� Component�� Ȯ�����ּ���.", "OK");
            if (endUnity)
                EditorApplication.isPlaying = false;
#endif
        }

    }

    void Update()
    {
        // Animation ��� ��
        if (startAnimation)
        {
            // �׷����� 0�� 1 �����̹Ƿ� duration�� 1�� ��ȯ
            checkTime += Time.deltaTime / duration;
            // �׷����� ���� ������ ������ �׷����� �� ����ϱ�
            SetValue(checkTime);

            // �ð��� 1 ��� ��
            if (checkTime >= 1)
            {
                // checkTime�� ���� 1�� �ٻ�ġ���� �����ϴ� �����Ƿ� ������ ����(to�� �ð� = 1)������ �� ����
                SetValue(1f);
                // �ٽ� �ʱ�ȭ(�׷����� ��������)
                checkTime = 0f;
                // Animation ����
                startAnimation = false;
                // ���⼭ break�� �� �ϸ� �Ʒ��� ���� �ٽ� ��� �Ǿ ��ġ�� �������� ���ƿ��� ��
                return;
            }
        }
    }
    #endregion
}
