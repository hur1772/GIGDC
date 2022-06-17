using UnityEngine;

public class BossSizeController : MonoBehaviour
{

    public enum BossType
    {
        Stage_1_1,
        Stage_1_2,
        Stage_1_3
    }

    public BossType bossType;
    float maxScaleX = 0.0f, maxScaleY = 0.0f;
    Vector3 NowScale;


    private void Start() => StartFunc();

    private void StartFunc()
    {
        if(bossType == BossType.Stage_1_1)
        {
            maxScaleX = 1.6f;
            maxScaleY = 1.6f;
        }

        NowScale = this.transform.localScale;
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        SizeUpdate();
    }

    void SizeUpdate()
    {
        if(NowScale.x <= maxScaleX)
        {
            NowScale.x += Time.deltaTime * 0.75f;
        }

        if (NowScale.y <= maxScaleY)
        {
            NowScale.y += Time.deltaTime * 0.75f;
        }

        this.transform.localScale = NowScale;
    }
}