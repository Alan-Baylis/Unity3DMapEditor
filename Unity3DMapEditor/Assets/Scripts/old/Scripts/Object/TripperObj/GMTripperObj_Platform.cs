
/**
	�����
	����ƽ̨
*/
using UnityEngine;
public class CTripperObject_Platform :	 CTripperObject
{
	//Tripper ����
	public override TRIPPER_OBJECT_TYPE		Tripper_GetType() 	{ return TRIPPER_OBJECT_TYPE.TOT_PLATFORM; }
	//�ܷ�������
	public override bool Tripper_CanOperation() 
    {
        return false;
    }
	//�������
	public override ENUM_CURSOR_TYPE Tripper_GetCursor() { return ENUM_CURSOR_TYPE.CURSOR_SPEAK; }
	//���뼤��
	public override void Tripper_Active()
    {
        //todo
//         Packets::CGCharDefaultEvent Msg; 
// 	    Msg.setObjID( GetServerID() );
// 
// 	    CNetManager::GetMe()->SendPacket(&Msg);
    }
	//����ƽ̨������
	public bool							SetPlatformID(int nID)
    {
        return true;
    }
	//ȡ��ƽ̨������
	public int								GetPlatformID()
    {
        return MacroDefine.INVALID_ID;
    }

	//-----------------------------------------------------
	///���ݳ�ʼ�����壬��ͬ������Ⱦ��
	public override void Initial(object pInit)
    {

    }
	public override void Release()
    {
        base.Release();
    }
	public override void SetPosition(Vector3 pos)
    {

    }
	GFX.GfxObject			mRenderInterface2;
};
