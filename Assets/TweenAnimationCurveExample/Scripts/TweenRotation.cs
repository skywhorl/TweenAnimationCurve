using UnityEngine;

public class TweenRotation : MonoBehaviour
{
    #region Field
    // Easing �׷��� : 0���� 1���� �׷�������
    // from - to ��ġ���� duration ���� Linear �׷��� ���� ���� �̵� 
    [Header("Animation ����")][SerializeField] AnimationCurve animationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    [Header("�ʱ� ���� ����")][SerializeField] Vector3 from;
    [Header("��ǥ ���� ����")][SerializeField] Vector3 to;
    [Header("��� �ð�")][SerializeField] float duration = 1f;

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
    public void Play(Vector3 from, Vector3 to, float duration)
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
    /// ���� ���������� ȸ�� ���� ��� �� ȸ��
    /// </summary>
    /// <param name="curveTime">�׷����� ���� �ð�</param>
    void SetValue(float curveTime)
    {
        // Evaluate() : AnimationCurve�� �Ű������� ��ȯ��
        // (�׷����� ����)�ð��� ���������� �ش� �ð��� ���� (�׷����� ����)���� ����Ͽ� ��ȯ
        // �ð��� 0�� ����� ���� from, 1�� ����� ���� to(0.5�� ����)
        var value = animationCurve.Evaluate(curveTime);
        // �ش� �׷����� ��ǥ �� = ����� ȸ�� ��
        var result = (from * (1f - value)) + (to * value);
        // �ش� ���� ���� ȸ�� ������ ����
        transform.rotation = Quaternion.Euler(result);
    }
    #endregion


    #region Event Method
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
