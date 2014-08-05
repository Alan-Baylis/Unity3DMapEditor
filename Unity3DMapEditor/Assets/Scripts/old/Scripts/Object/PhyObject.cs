using UnityEngine;
public class CObject_Phy :  CObject
{
    public enum PHY_EVENT_ID
    {
        PE_NONE = -1,									//���¼�
        PE_COLLISION_WITH_GROUND,						//������ػ�����淢���Ӵ�
        PE_OBJECT_BEGIN_MOVE,							//��ʼ�ƶ�
        PE_OBJECT_TURN_AROUND,							//��ת
    };

    private const int       MAX_REGISTER_EVENTS_NUM = 20;
    private PHY_EVENT_ID[]  m_aEventList;
    private uint            m_nEventListNum;
    private bool            m_bIsInAir;
    private bool            m_bIsEnable;
    private Vector3         m_fvLinearSpeed;
    private Vector3         m_fvRotSpeed;
    private uint            m_nLastTickTime;
    public CObject_Phy()
    {
	   m_aEventList     = new PHY_EVENT_ID[MAX_REGISTER_EVENTS_NUM];
       m_bIsInAir       = false;
       m_bIsEnable      = false;
       m_fvLinearSpeed  = Vector3.zero;
       m_fvRotSpeed     = Vector3.zero;
       m_nLastTickTime  = 0;
    }

    public override void SetMapPosition(float x, float z)//���ݵ���ĸ߶����������λ��
    {
        float y = 0;
        y = GFX.GfxUtility.getSceneHeight(x, z);

        Vector3 fvCurObjPos = GetPosition();
        float fInAirHeight  = fvCurObjPos.y;

        if (m_bIsEnable)
        {
            if (m_fvLinearSpeed.y < 0)
            {//�½�����
                //---------------------------------------------------
                if (y > fInAirHeight)
                {//�Ѿ������
                    SetPosition(new Vector3(x, y, z));		//����������
                    DispatchPhyEvent(PHY_EVENT_ID.PE_COLLISION_WITH_GROUND,null);
                    m_bIsInAir = false;
                }
                else
                {//�ڿ������ÿ��еĸ߶�
                    SetPosition(new Vector3(x, fInAirHeight, z));	//�ڿ���
                    m_bIsInAir = true;
                }
            }
            else
            {//�������̣�һ���ڿ���
                if (y > fInAirHeight)
                {
                    SetPosition(new Vector3(x, y, z));	//�ڿ���
                }
                else
                {
                    SetPosition(new Vector3(x, fInAirHeight,z));	//�ڿ���
                }
                m_bIsInAir = true;
            }
        }
        else
        {
            SetPosition(new Vector3(x, y, z));		//����������
        }
        SetFootPosition(new Vector3(x, y, z));
    }

    public override void Initial(object pInit)
    {
        base.Initial(pInit);
	    for(uint i =0; i< MAX_REGISTER_EVENTS_NUM; i++)
	    {
		    m_aEventList[i] = PHY_EVENT_ID.PE_NONE;
	    }
	    m_nEventListNum = 0;
	    //  [8/19/2010 Sun]
	    RegisterPhyEvent(PHY_EVENT_ID.PE_OBJECT_BEGIN_MOVE);
        
	    return;
    }

    public virtual void NotifyPhyEvent(PHY_EVENT_ID eventid, object pParam)
    {
        return;
    }
    public void RegisterPhyEvent(PHY_EVENT_ID eventid)
    {
	    if( m_nEventListNum == MAX_REGISTER_EVENTS_NUM )
	    {
		    return;
	    }
	    for(uint i = 0; i < m_nEventListNum; i++)
	    {
		    if(m_aEventList[i] == eventid)
			    return;
	    }
	    m_aEventList[m_nEventListNum++] = eventid;
    }

    //ע��һ�������¼�
    public void UnRegisterPhyEvent(PHY_EVENT_ID eventid)
    {
	    if( m_nEventListNum == 0)
	    {
		    return;
	    }

	    for(uint i = 0; i < m_nEventListNum; i++)
	    {
		    if(m_aEventList[i] == eventid)
		    {
			    m_aEventList[i] = m_aEventList[m_nEventListNum-1];
			    m_nEventListNum--;
			    break;
		    }
	    }
    }

    public void AddLinearSpeed(Vector3 vSpeed)
    {
	    if(m_bIsInAir == true)
		    return;
	    m_fvLinearSpeed = m_fvLinearSpeed+vSpeed;
    }

    public void PhyEnable(bool bFlag)
    {
	    if(bFlag == false)
	    {
		    m_fvLinearSpeed = Vector3.zero;
            m_fvRotSpeed    = Vector3.zero;
	    }
	    m_nLastTickTime	=	0;
	    m_bIsEnable		=	bFlag;
    }
    
    public override void Tick()
    {
        //��������ϵͳ������λ�ý��н���
	    if(m_bIsEnable == false)
	    {
		    return;
	    }

	    //��ǰλ��
	    Vector3	fvTempPos	= GetPosition();
	    Vector3	fvCurPos	= fvTempPos;
	    //����ÿ�� 50����
	    uint	nCurTime	= GameProcedure.s_pTimeSystem.GetTimeNow();

	    //��һ�β���tick
	    if(m_nLastTickTime == 0)
	    {
		    //��¼�ϴ�ʱ���
		    m_nLastTickTime	=	nCurTime;
		    return;
	    }
        const uint  PHY_MACRO_MSECONDS_PER_FRAME = 10;
        const float PHY_MACRO_GRAVITY = 9.8f;
        const float	SF_Factor = (float)((float)PHY_MACRO_MSECONDS_PER_FRAME/(float)1000);
	    uint	nDeltaTime	=	GameProcedure.s_pTimeSystem.CalSubTime(m_nLastTickTime, nCurTime);
	    if(nDeltaTime < PHY_MACRO_MSECONDS_PER_FRAME)
	    {
		    return;
	    }

	    uint	nStridTimes	    = (uint)(nDeltaTime/PHY_MACRO_MSECONDS_PER_FRAME);
	    float	fUsedGravity	=	PHY_MACRO_GRAVITY;

	    while(nStridTimes != 0)
	    {
		    //�����ٶ�Ҫ�и���������ʵ�����Ե�����̫���ˣ�
		    if(m_fvLinearSpeed.y > 0)
		    {
			    fUsedGravity = PHY_MACRO_GRAVITY + 25.0f;
		    }
		    else
		    {
			    fUsedGravity = PHY_MACRO_GRAVITY + 70.0f;
		    }

		    //���㴹ֱ���ٶȱ仯
		    m_fvLinearSpeed.y -= fUsedGravity*SF_Factor;

		    //����λ��
		    fvCurPos = fvCurPos+m_fvLinearSpeed*SF_Factor;

		    //
		    nStridTimes--;
	    }
    	
	    //��¼�ϴ�ʱ���
	    m_nLastTickTime	=	nCurTime;

	    //����λ��
        SetPosition(fvCurPos);

	    //����λ��
	    SetMapPosition(fvCurPos.x, fvCurPos.z);
	    base.Tick();
    }

    void DispatchPhyEvent(PHY_EVENT_ID eventid, object pParam)
    {
	    for(uint i = 0; i<m_nEventListNum; i++)
	    {
		    if(m_aEventList[i] == eventid)
		    {//�ѱ�ע��
			    NotifyPhyEvent(eventid, pParam);
			    return;
		    }
	    }
    }
}