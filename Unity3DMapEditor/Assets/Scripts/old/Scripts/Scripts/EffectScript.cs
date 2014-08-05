using UnityEngine;
using System.Collections;
//������Ч��������[2012/2/16 ZZY]
public class EffectScript : MonoBehaviour
{
    public bool UseLifeTime = false;
    public float LifeTime = 0.5f;
    public float startTime = 0.0f;
    float mTimeElapsed = 0.0f;
    bool mHasStart = true;
    // Use this for initialization
    void Start()
    {
        mTimeElapsed = 0;
        if (startTime > 0.0001f && gameObject.particleEmitter != null)//�����start time��ôһ��ʼ����ͣ��Ч
        {
            gameObject.particleEmitter.enabled = false;
            mHasStart = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (UseLifeTime)
        {
            if (mTimeElapsed > LifeTime)
            {
                GFX.GfxUtility.setGameObjectVisible(gameObject, false);
                mTimeElapsed = 0;
                return;
            }
        }
        if (mHasStart == false && mTimeElapsed > startTime)
        {
            gameObject.particleEmitter.enabled = true;
            mHasStart = true;
        }
 
        mTimeElapsed += Time.deltaTime;
    }

}
