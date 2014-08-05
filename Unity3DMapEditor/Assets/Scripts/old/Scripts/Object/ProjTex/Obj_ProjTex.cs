/**
	���ڵ��ϵı���
*/

public class CObject_ProjTex :  CObject_Static
{
	public enum TYPE
	{
		PROJTEX_NULL,
		PROJTEX_REACHTARGET,		//���ѡ���Ŀ���
		PROJTEX_AURARUNE,			//���ܷ�Χ��
		PROJTEX_MOVETRACK,			//�ƶ��켣
	}
    protected static float PROJTEX_HEIGHT =2.0f;
    protected static float PROJTEX_DEFAULT_RANGE = 1.0f;
    public virtual TYPE GetProjTexType() { return TYPE.PROJTEX_NULL; }
    public virtual void SetHeight(float fHeight)
    {

    }
	//-----------------------------------------------------
	///���ݳ�ʼ�����壬��ͬ������Ⱦ��
	public override	void				Initial(object pInit)
    {
        if ( mRenderInterface == null )
        {
            mRenderInterface = GFX.GFXObjectManager.Instance.createObject("", GFX.GFXObjectType.DUMY);
        }
        base.Initial(pInit);
    }
}

//--------------------------------------------------
//���ָ���Ŀ�ĵ�
public  class CObject_ProjTex_MouseTarget : CObject_ProjTex
{
    public override TYPE GetProjTexType() { return TYPE.PROJTEX_REACHTARGET; }

    public void SetReachAble(bool bReachAble)
    { 
        m_uEnableTime = GameProcedure.s_pTimeSystem.GetTimeNow();
	    if(bReachAble)
	    {
            mRenderInterface.Attach_ProjTexture(GFX.GfxObject.PROJTEX_TYPE.REACHABLE, true, PROJTEX_DEFAULT_RANGE, PROJTEX_HEIGHT, null);
            mRenderInterface.Attach_ProjTexture(GFX.GfxObject.PROJTEX_TYPE.UNREACHABLE, false, PROJTEX_DEFAULT_RANGE, PROJTEX_HEIGHT, null);
	    }
	    else
	    {
            mRenderInterface.Attach_ProjTexture(GFX.GfxObject.PROJTEX_TYPE.REACHABLE, false, PROJTEX_DEFAULT_RANGE, PROJTEX_HEIGHT, null);
            mRenderInterface.Attach_ProjTexture(GFX.GfxObject.PROJTEX_TYPE.UNREACHABLE, true, PROJTEX_DEFAULT_RANGE, PROJTEX_HEIGHT, null);
	    }
    }
    public void UpdateAsCursor()
    {
        UnityEngine.Vector3 point = GameProcedure.s_pInputSystem.GetMousePos();
        UnityEngine.Vector3 fvMousePos;
        CObjectManager.Instance.GetMouseOverObject(point, out fvMousePos);
	    SetMapPosition(fvMousePos.x, fvMousePos.z);
    }  
    public override void Tick()
    {
        if (CObjectManager.Instance.getPlayerMySelf() == null)
            return;
        UnityEngine.Vector3 vec = GetPosition() - CObjectManager.Instance.getPlayerMySelf().GetPosition();
	    uint uTime = GameProcedure.s_pTimeSystem.GetTimeNow();
	    //��������Ѿ��ﵽ����Visible�ı����ΪFALSE������
	    if(	(!(CObjectManager.Instance.getPlayerMySelf().IsMoving()) && (uTime - m_uEnableTime > 2000))
            || (vec.magnitude < 1.0f))
	    {
            mRenderInterface.Attach_ProjTexture(GFX.GfxObject.PROJTEX_TYPE.REACHABLE, false, PROJTEX_DEFAULT_RANGE, PROJTEX_HEIGHT, null);
            mRenderInterface.Attach_ProjTexture(GFX.GfxObject.PROJTEX_TYPE.UNREACHABLE, false, PROJTEX_DEFAULT_RANGE, PROJTEX_HEIGHT, null);
	    }
    }
	protected uint						m_uEnableTime;
};

//--------------------------------------------------
//���ܷ�Χ��
public class CObject_ProjTex_AuraDure : CObject_ProjTex
{
    public override TYPE GetProjTexType() { return TYPE.PROJTEX_AURARUNE; }

    public void SetShowEnable(bool bEnable)
    {
        if(m_bShowEnable == bEnable) return;
	    m_bShowEnable = bEnable; 

	    if(m_bShowEnable)
	    {
            mRenderInterface.Attach_ProjTexture(GFX.GfxObject.PROJTEX_TYPE.AURA_RUNE, true, m_fRingRange,PROJTEX_HEIGHT, null);
	    }
	    else
	    {
            mRenderInterface.Attach_ProjTexture(GFX.GfxObject.PROJTEX_TYPE.AURA_RUNE, false, m_fRingRange,PROJTEX_HEIGHT, null);
	    }
    }
	public bool						GetShowEnable()  { return m_bShowEnable; }

    public void SetRingRange(float nRingRange)
    {
 	    if(UnityEngine.Mathf.Approximately(m_fRingRange, nRingRange)) return;
        m_fRingRange = nRingRange; 

	    if(m_bShowEnable)
	    {
            mRenderInterface.Attach_ProjTexture(GFX.GfxObject.PROJTEX_TYPE.AURA_RUNE, true, m_fRingRange,PROJTEX_HEIGHT, null);
	    }
	    else
	    {
            mRenderInterface.Attach_ProjTexture(GFX.GfxObject.PROJTEX_TYPE.AURA_RUNE, false, m_fRingRange, PROJTEX_HEIGHT, null);
	    }
    }
	public float						GetRingRange()  { return m_fRingRange; }

	public CObject_ProjTex_AuraDure() { m_bShowEnable = false; }
    public override void Tick()
    {

    }

	protected bool						m_bShowEnable;
	protected float						m_fRingRange;
};

//-----------------------------------------------------
// Ѱ·�켣 [9/5/2011 Sun]
public class CObject_ProjTex_MoveTrack : CObject_ProjTex
{
    public override TYPE GetProjTexType() { return TYPE.PROJTEX_MOVETRACK; }

	void						Show(bool bShow)
    {
        if(m_bShow == bShow) return;

	    if(mRenderInterface != null)
            mRenderInterface.Attach_ProjTexture(GFX.GfxObject.PROJTEX_TYPE.MOVE_TRACK, bShow, PROJTEX_DEFAULT_RANGE, PROJTEX_HEIGHT, null);

	    m_bShow = bShow;
    }
    public override void Tick()
    {

    }

	public CObject_ProjTex_MoveTrack()
    {
        m_bShow = false;
    }

	protected bool			m_bShow;

};