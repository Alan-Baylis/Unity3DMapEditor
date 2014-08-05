/**
	Tripper Object
*/

public class CTripperObject :	 CObject_Static
{
	// ���ָ��ķ���
	public override void FillMouseCommand_Left(ref SCommand_Mouse pOutCmd, tActionItem pActiveSkill)
    {
        if(Tripper_CanOperation())
	    {
            pOutCmd.m_typeMouse = MOUSE_CMD_TYPE.MCT_HIT_TRIPPEROBJ;
		    pOutCmd.SetValue<int>(0,ServerID);
	    }
	    else
	    {
            pOutCmd.m_typeMouse = MOUSE_CMD_TYPE.MCT_NULL;
	    }
    }
	// �Ҽ�ָ��ķ���
    public override void FillMouseCommand_Right(ref SCommand_Mouse pOutCmd, tActionItem pActiveSkill)
    {
        if(Tripper_CanOperation())
	    {
            pOutCmd.m_typeMouse = MOUSE_CMD_TYPE.MCT_CONTEXMENU;
		    pOutCmd.SetValue<int>(0,ServerID);
	    }
	    else
	    {
            pOutCmd.m_typeMouse = MOUSE_CMD_TYPE.MCT_NULL;
	    }
    }
	//����Ϊ��������ɲɼ���Ʒ���õ���  [7/4/2011 zzy]
	public void					SetTransparent(float fTransparency, float fTime)
    {
        //todo
    }
};