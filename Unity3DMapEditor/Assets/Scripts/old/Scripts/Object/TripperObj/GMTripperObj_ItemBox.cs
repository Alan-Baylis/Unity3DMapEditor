
/**
	���ڵ��ϵı���
*/

public class CTripperObject_ItemBox :	  CTripperObject
{
	//Tripper ����
	public override TRIPPER_OBJECT_TYPE		Tripper_GetType() 	{ return TRIPPER_OBJECT_TYPE.TOT_ITEMBOX; }
	//�ܷ�������
    public override bool Tripper_CanOperation() 
    {
        if(CObjectManager.Instance.getPlayerMySelf().GetServerGUID() == (int)m_idOwner)
 		    return true;
 	    else
 		    return false;
    }
	//�������
    public override ENUM_CURSOR_TYPE Tripper_GetCursor() { return ENUM_CURSOR_TYPE.CURSOR_PICKUP; }
	//���뼤��
    public override void Tripper_Active()
    {
        Network.Packets.CGOpenItemBox msg = new Network.Packets.CGOpenItemBox();
 	    msg.setObjID((uint)ServerID);
 	    NetManager.GetNetManager().SendPacket(msg);
    }

	//-----------------------------------------------------
	///���ݳ�ʼ�����壬��ͬ������Ⱦ��
    public override void Initial(object pInit)
    { 
        if ( mRenderInterface == null )
	    {
		    string itemBoxAsset = "ItemBox";//���������Դ��
            //������Ⱦ��ʵ��
            mRenderInterface = GFX.GFXObjectManager.Instance.createObject(itemBoxAsset,  GFX.GFXObjectType.ACTOR);
		    //����ѡ�����ȼ�
		    mRenderInterface.RayQuery_SetLevel(RAYQUERY_LEVEL.RL_TRIPPEROBJ);
		    mRenderInterface.SetData(ID);
	    }

        m_uBirthTime = GameProcedure.s_pTimeSystem.GetTimeNow();

	    //����  [7/4/2011 zzy]
	    SetTransparent(1,0);
	    SetTransparent(0,1);
        base.Initial(pInit);
        base.CreateRenderInterface();
    }
    ///�߼�����
    public override void Tick()
    {
        //�Զ�ʰȡ
        if (!m_bPickUp)
        {
            if (m_uBirthTime + 2000 < GameProcedure.s_pTimeSystem.GetTimeNow())
            {
                if (Tripper_CanOperation())
                    Tripper_Active();

                m_bPickUp = true;
            }
        }
        SetMapPosition(mPosition.x, mPosition.z);
        base.Tick();
    }
	public override void Release()
    {
        base.Release();
    }
	//���õ�����Ĺ���
	public void						SetOwnerGUID(uint nID) { m_idOwner = nID; }

	protected uint			m_idOwner;		//��Ʒ���˵�ObjID,������ӵ�ID

    bool m_bPickUp = false;

    uint m_uBirthTime = 0;

};
