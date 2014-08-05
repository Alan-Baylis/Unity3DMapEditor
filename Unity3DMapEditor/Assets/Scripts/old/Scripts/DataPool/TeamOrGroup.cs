using System;
using System.Collections;
using System.Collections.Generic;
using Interface;
using Network.Packets;
//--------------------------------------------------------------------------------------------------------------------------
//
// �ڽ�������ʾ��ģ��.
//
public class CModelShowInUI
{
    //-----------------------------------------------------------------------------------------------------------------------
    //
    // ������ui��������ʾ����Ϣ.
    //
    public static uint m_TeamNumCount = 0;					// ģ�ͼ���
    public CObject_PlayerOther m_pAvatar;						// ����UI��ʾ���߼�����.
    public string m_szBufModel;				// ģ������.


    // �����µ�ģ��
    public bool CreateNewUIModel()
    {
        return true;
    }

    // ɾ��uiģ��
    public bool DestroyUIModel()
    {
        return true;
    }

    // ����ģ����Ϣ
    public void SetUIModelInfo(HUMAN_EQUIP point, int nID)
    {

    }

    // �õ�uiģ������
    public string GetUIModelName()
    {
        return "";
    }

    // ����ģ��
    public void SetFaceMeshId(int nID)
    {
    }

    // ͷ��ģ��
    public void SetFaceHairId(int nID)
    {
    }
    // ͷ����ɫ
    public void SetHairColor(uint nID)
    {

    }
}
// �ͻ��˶���|�Ŷ����ݽṹ
public class TeamMemberInfo
{
    public uint m_GUID;
    public int m_OjbID;						//
    public short m_SceneID;
    public string m_szNick;	// 1.�ǳ�
    public int m_uFamily;						// 2.����
    public int m_uDataID;						// 3.�Ա�
    public int m_uLevel;						// 4.�ȼ�
    public int m_nPortrait;					// 5.ͷ��
    public WORLD_POS m_WorldPos;						// 6.λ�ã����꣩
    public int m_nHP;							// 7.HP
    public int m_dwMaxHP;						// 8.HP����
    public int m_nMP;							// 9.MP
    public int m_dwMaxMP;						// 10.MP����
    public int m_nAnger;						// 11.ŭ��
    public int m_WeaponID;						// 12.����
    public int m_CapID;						// 13.ñ��
    public int m_ArmourID;						// 14.�·�
    public int m_CuffID;						// 15.����
    public int m_FootID;						// 16.ѥ��
    // 17.buff����ʱ������
    public SimpleImpactList m_SimpleImpactList;				// Buff �б�
    public bool m_bDeadLink;					// 18.����
    public bool m_bDead;						// 19.����
    public int m_uFaceMeshID;					// 20.�沿ģ��
    public int m_uHairMeshID;					// 21.ͷ��ģ��
    public uint m_uHairColor;					// 22.ͷ����ɫ
    public int m_uBackID;						// 23.���� [8/30/2010 Sun]

    public CModelShowInUI m_UIModel = new CModelShowInUI();						// �ڽ�����ʾ��ģ��
    public TeamMemberInfo()
    {
        m_GUID = 0;
        m_OjbID = 0;						//
        m_SceneID = 0;
        m_uFamily = 9;						// 2.��������
        m_uDataID = 0;						// 3.�Ա�
        m_uLevel = 0;						// 4.�ȼ�
        m_nPortrait = -1;						// 5.ͷ��
        m_nHP = 0;						// 7.HP
        m_dwMaxHP = 0;						// 8.HP����
        m_nMP = 0;						// 9.MP
        m_dwMaxMP = 0;						// 10.MP����
        m_nAnger = 0;						// 11.ŭ��
        m_WeaponID = 0;						// 12.����
        m_CapID = 0;						// 13.ñ��
        m_ArmourID = 0;						// 14.�·�
        m_CuffID = 0;						// 15.����
        m_FootID = 0;						// 16.ѥ��
        m_bDeadLink = false;					// 18.����
        m_bDead = false;					// 19.����
        m_uFaceMeshID = 0;						// 20.�沿ģ��
        m_uHairMeshID = 0;						// 21.ͷ��ģ��
        m_uHairColor = 0;						// 22.ͷ����ɫ
        m_uBackID = 0;						// 23.���� [8/30/2010 Sun]
    }
}

public class TeamInfo
{
    public short m_TeamID;			// 1 or 2, 3, ...
    public List<TeamMemberInfo> m_TeamMembers = new List<TeamMemberInfo>();		// [MAX_TEAM_MEMBER]
}

public struct TeamCacheInfo // ���������Ϊ�������ʾ�����˻�������������Ϣ������
{
    public uint m_GUID;
    public string m_szNick;	// 1.�ǳ�
    public uint m_uFamily;						// 2.��������
    public short m_Scene;						// 3.����
    public uint m_uLevel;						// 4.�ȼ�
    public uint m_uDetailFlag;					// ������Ϣ�Ƿ���ʾ
    public ushort m_uDataID;						// 5.�Ա�
    public uint m_WeaponID;						// 7.����
    public uint m_CapID;						// 8.ñ��
    public uint m_ArmourID;						// 9.�·�
    public uint m_CuffID;						// 10.����
    public uint m_FootID;						// 11.ѥ��
    public uint m_uFaceMeshID;					// 12.�沿ģ��
    public uint m_uHairMeshID;					// 13.ͷ��ģ��
    public uint m_uHairColor;					// 14.ͷ����ɫ

    public CModelShowInUI m_UIModel;						// �ڽ�����ʾ��ģ��


};


public struct InviteTeam
{
    public uint m_InviterGUID;
    public List<TeamCacheInfo> m_InvitersInfo;
};

public class CTeamOrGroup
{ // when a team change to a group, the member of m_TeamMembers will be assigned to m_TeamInfo[0].

    public enum TEAM_OR_GROUP_TYPE
    {
        INVAILD_TYPE = 0,
        TEAM_TYPE = 1,
        GROUP_TYPE = 2,
    }

    public enum UI_ON_OPEN
    {
        UI_ALL_CLOSE = 0,
        UI_INVITE_ON_OPEN = 1,
        UI_APPLY_ON_OPEN = 2,
    }


    public const int MAX_INVITE_TEAM = 7;
    public const int MAX_PROPOSER = 18;


    public CTeamOrGroup()
    {
        CleanUp();
    }

    public void CleanUp()
    {
        m_Type = TEAM_OR_GROUP_TYPE.INVAILD_TYPE;
        m_ID = MacroDefine.INVALID_ID;
        m_MyTeamID = 0xFF;
        m_LeaderID = MacroDefine.UINT_MAX;
        m_TeamAllocation = 0;
    }

    public bool HasTeam()
    {
        return m_Type != TEAM_OR_GROUP_TYPE.INVAILD_TYPE;
    }

    // �齨���飬���ö���� leader�����Ҽ���Ϊ��һ����Ա
    public bool CreateTeam(TeamMemberInfo leader, short TeamID)
    {
        if (m_Type != TEAM_OR_GROUP_TYPE.INVAILD_TYPE)
        {
            throw new Exception("create team failed  type = " + m_Type.ToString());
        }

        m_Type = TEAM_OR_GROUP_TYPE.TEAM_TYPE;
        m_ID = TeamID;
        SetLeaderGUID(leader.m_GUID);

        m_TeamMembers.Add(leader);

        //֪ͨTalk Interface�����齨��
        //Talk..TeamCreate(m_ID);
        //���ý�ɫ������
        CObjectManager.Instance.getPlayerMySelf().GetCharacterData().Set_HaveTeamFlag(true);

        // ���¶�����Ϣ [9/26/2011 Ivan edit]
        CObject_Character player = CObjectManager.Instance.FindServerObject(leader.m_OjbID) as CObject_Character;
        if (player != null)
        {
            player.GetCharacterData().RefreshName();
        }

        return true;
    }

    // ��ɢ����
    public void DismissTeam()
    {
        switch (m_Type)
        {
            case TEAM_OR_GROUP_TYPE.TEAM_TYPE:
                { // ��������
                    for (int i = 0; i < m_TeamMembers.Count; i++)
                    {
                        // ���¶�Աͷ����Ϣ��ɫ [10/9/2011 Ivan edit]
                        CObject_Character player = CObjectManager.Instance.FindServerObject((int)m_TeamMembers[i].m_OjbID) as CObject_Character;
                        if (player != null)
                        {
                            player.GetCharacterData().RefreshName();
                        }
                    }
                    m_TeamMembers.Clear();
                }
                break;
            case TEAM_OR_GROUP_TYPE.GROUP_TYPE:
                {
                    m_TeamInfo.Clear();
                }
                break;
            default:
                return;
        }

        CleanUp();

        //���ý�ɫ������
        CObject_PlayerMySelf self = CObjectManager.Instance.getPlayerMySelf();
        if (self != null)
        {
            self.GetCharacterData().Set_HaveTeamFlag(false);
            self.GetCharacterData().Set_TeamLeaderFlag(false);
            self.GetCharacterData().Set_TeamFullFlag(false);
            self.GetCharacterData().Set_TeamFollowFlag(false);
        }
    }

    // ����һ����Ա��������ŶӵĻ�����Ҫ�����Ŷ����ڵ�С���
    public void AddMember(TeamMemberInfo member, short TeamID, byte TeamIndex)
    {
        TeamMemberInfo pTMInfo = new TeamMemberInfo();

        pTMInfo.m_GUID = member.m_GUID;
        pTMInfo.m_OjbID = member.m_OjbID;						//
        pTMInfo.m_SceneID = member.m_SceneID;
        pTMInfo.m_uFamily = member.m_uFamily;					// 2.��������
        pTMInfo.m_uDataID = member.m_uDataID;					// 3.�Ա�
        pTMInfo.m_nPortrait = member.m_nPortrait;					// 5.ͷ��
        pTMInfo.m_szNick = member.m_szNick;

        CObject_PlayerMySelf pObj = CObjectManager.Instance.getPlayerMySelf();
        switch (m_Type)
        {
            case TEAM_OR_GROUP_TYPE.INVAILD_TYPE:
                {
                    if (CreateTeam(pTMInfo, TeamID) == false)
                        pTMInfo = null;
                }
                break;
            case TEAM_OR_GROUP_TYPE.TEAM_TYPE:
                { // ������˵����
                    if (m_TeamMembers.Count >= GAMEDEFINE.MAX_TEAM_MEMBER)
                    {
                        pTMInfo = null;
                        CEventSystem.Instance.PushEvent(GAME_EVENT_ID.GE_INFO_SELF, "�����������޷�����");
                        return;
                    }

                    m_TeamMembers.Add(pTMInfo);
                    // �������˱�� [6/14/2011 edit by ZL]
                    if (m_TeamMembers.Count == GAMEDEFINE.MAX_TEAM_MEMBER)
                    {
                        CObjectManager.Instance.getPlayerMySelf().GetCharacterData().Set_TeamFullFlag(true);

                    }
                    // ���¶�����Ϣ [9/26/2011 Ivan edit]
                    CObject_Character player = CObjectManager.Instance.FindServerObject((int)pTMInfo.m_OjbID) as CObject_Character;
                    if (player != null)
                    {
                        player.GetCharacterData().RefreshName();
                    }
                }
                break;
            case TEAM_OR_GROUP_TYPE.GROUP_TYPE:
                { // �ŶӼ��˵����
                    if (TeamIndex < 0 || TeamIndex >= GAMEDEFINE.MAX_TEAMS_IN_GROUP)
                    {
                        pTMInfo = null;
                        return;
                    }

                    ++TeamIndex; // Ĭ�ϴ��������Ϊ 0 ~ MAX_TEAMS_IN_GROUP-1

                    TeamInfo pTeamInfo = GetTeam(TeamIndex);

                    if (pTeamInfo != null)
                    {
                        if (pTeamInfo.m_TeamMembers.Count >= GAMEDEFINE.MAX_TEAM_MEMBER)
                        {
                            pTMInfo = null;
                            return;
                        }
                    }
                    else
                    { // �����С�鲻���ڣ��򴴽�����������Ŷ�
                        pTeamInfo = new TeamInfo();
                        pTeamInfo.m_TeamID = TeamIndex;
                        m_TeamInfo.Add(pTeamInfo);

                        //������
                        if (pTeamInfo.m_TeamMembers.Count >= GAMEDEFINE.MAX_TEAM_MEMBER)
                        {
                            CObjectManager.Instance.getPlayerMySelf().GetCharacterData().Set_TeamFullFlag(true);
                        }
                    }

                    pTeamInfo.m_TeamMembers.Add(pTMInfo);

                    if (pTMInfo.m_GUID == pObj.GetServerGUID())
                    {
                        m_MyTeamID = TeamIndex;
                    }
                }
                break;
            default:
                return;
        }

        if (pTMInfo.m_GUID != pObj.GetServerGUID())
        { // ��Ҫ���ض��ѵ�ͷ�񴰿�
            // ����ö��ѵ�����
            CGAskTeamMemberInfo Msg = new CGAskTeamMemberInfo();
            Msg.SceneID = pTMInfo.m_SceneID;
            Msg.ObjID = (uint)pTMInfo.m_OjbID;
            Msg.GUID = pTMInfo.m_GUID;

            NetManager.GetNetManager().SendPacket(Msg);
        }
        else
        {
            FillMyInfo(pTMInfo);
        }

        //// ����һ�������ģ��.
        //pTMInfo.m_UIModel.CreateNewUIModel();
        //// ����uiģ��
        //// �����Ա�
        //pTMInfo.m_UIModel.m_pAvatar.GetCharacterData().Set_RaceID(pTMInfo.m_uDataID);
    }

    // һ����Ա�뿪
    public void DelMember(uint guid)
    {
        switch (m_Type)
        {
            case TEAM_OR_GROUP_TYPE.TEAM_TYPE:
                {
                    // Keep ObjId [10/9/2011 Ivan edit]
                    int teamMemberObjId = MacroDefine.INVALID_ID;

                    for (int i = 0; i < m_TeamMembers.Count; i++)
                    {
                        if (m_TeamMembers[i].m_GUID == guid)
                        {
                            teamMemberObjId = m_TeamMembers[i].m_OjbID;
                            m_TeamMembers.RemoveAt(i);
                            break;
                        }
                    }

                    //���鲻����
                    if (m_TeamMembers.Count < GAMEDEFINE.MAX_TEAM_MEMBER)
                    {
                        CObjectManager.Instance.getPlayerMySelf().GetCharacterData().Set_TeamFullFlag(false);
                    }

                    // ���¶�����Ϣ [9/26/2011 Ivan edit]
                    CObject_Character player = CObjectManager.Instance.FindServerObject(teamMemberObjId) as CObject_Character;

                    if (player != null)
                    {
                        player.GetCharacterData().RefreshName();
                    }
                }
                break;
            case TEAM_OR_GROUP_TYPE.GROUP_TYPE:
                { // �Ŷ����
                    bool bFind = false;

                    for (int i = 0; i < m_TeamInfo.Count; i++)
                    {
                        TeamInfo teamInfo = m_TeamInfo[i];
                        for (int j = 0; j < teamInfo.m_TeamMembers.Count; j++)
                        {
                            if (teamInfo.m_TeamMembers[j].m_GUID == guid)
                            {
                                teamInfo.m_TeamMembers.RemoveAt(j);
                                bFind = true;
                                if (teamInfo.m_TeamMembers.Count < 1)
                                    m_TeamInfo.RemoveAt(i);

                                break;
                            }
                        }
                        if (bFind)
                            break;
                    }

                }
                break;
            default:
                return;
        }

        if (guid == m_LeaderID)
        { // choose a new leader
            switch (m_Type)
            {
                case TEAM_OR_GROUP_TYPE.TEAM_TYPE:
                    { // �������
                        if (m_TeamMembers.Count < 1)
                        { // ���ڴ˴���������ɢ�����
                            return;
                        }

                        SetLeaderGUID(m_TeamMembers[0].m_GUID);
                    }
                    break;
                case TEAM_OR_GROUP_TYPE.GROUP_TYPE:
                    { // �Ŷ��������ʱ�������Ŷ�ѡ�����ų��Ĺ���
                        //Assert(FALSE);
                    }
                    break;
                default:
                    //Assert(FALSE);
                    return;
            }
        }
    }

    // ������Աλ��
    public void ExchangeMemberPosition(uint guid1, uint guid2, byte TeamIndex1, byte TeamIndex2)
    {
        TeamMemberInfo pTMInfo1, pTMInfo2;

        pTMInfo1 = GetMember(guid1);
        if (pTMInfo1 == null)
        {
            throw new NullReferenceException("Team member1 is null : " + guid1);
        }

        pTMInfo2 = GetMember(guid2);
        if (pTMInfo2 == null)
        {
            throw new NullReferenceException("Team member2 is null : " + guid2);
        }

        switch (m_Type)
        {
            case TEAM_OR_GROUP_TYPE.TEAM_TYPE:
                { // �������
                    for (int i = 0; i < m_TeamMembers.Count; ++i)
                    {
                        if (m_TeamMembers[i].m_GUID == pTMInfo1.m_GUID)
                        {
                            m_TeamMembers[i] = pTMInfo2;
                            continue;
                        }

                        if (m_TeamMembers[i].m_GUID == pTMInfo2.m_GUID)
                        {
                            m_TeamMembers[i] = pTMInfo1;
                            continue;
                        }
                    }
                }
                break;
            case TEAM_OR_GROUP_TYPE.GROUP_TYPE:
                { // �Ŷ��������ʱ�����ǣ�
                }
                break;
            default:
                return;
        }
    }

    // ���¶�Ա��Ϣ����ʱû���õ���
    public void UpdateMemberInfo(TeamMemberInfo member, uint guid)
    {

    }

    // �ӳ�
    public uint GetLeaderGUID() { return m_LeaderID; }

    // ���öӳ� GUID
    public void SetLeaderGUID(uint guid)
    {
        if (guid == CObjectManager.Instance.getPlayerMySelf().GetServerGUID())
        {
            CObjectManager.Instance.getPlayerMySelf().GetCharacterData().Set_TeamLeaderFlag(true);
        }
        else
        {
            CObjectManager.Instance.getPlayerMySelf().GetCharacterData().Set_TeamLeaderFlag(false);
        }

        m_LeaderID = guid;
    }

    // �����¶ӳ�
    public void Appoint(uint guid)
    {
        switch (m_Type)
        {
            case TEAM_OR_GROUP_TYPE.TEAM_TYPE:
                { // �������
                    TeamMemberInfo pTMInfo;

                    for (int i = 0; i < m_TeamMembers.Count; i++)
                    {
                        if (m_TeamMembers[i].m_GUID == guid)
                        {
                            pTMInfo = m_TeamMembers[i]; // �ͷŶ�Ա��Ϣ
                            m_TeamMembers.RemoveAt(i);
                            m_TeamMembers.Insert(0, pTMInfo);
                            SetLeaderGUID(guid);
                            break;
                        }
                    }
                }
                break;
            case TEAM_OR_GROUP_TYPE.GROUP_TYPE:
                { // �Ŷ����
                }
                break;
            default:
                return;
        }
    }
    // �õ���Ա������
    public int GetTeamMemberCount(byte TeamIndex /*= -1*/)
    {
        switch (m_Type)
        {
            case TEAM_OR_GROUP_TYPE.INVAILD_TYPE:
                return 0;
            case TEAM_OR_GROUP_TYPE.TEAM_TYPE:
                { // ��������
                    return m_TeamMembers.Count;
                }
                break;
            case TEAM_OR_GROUP_TYPE.GROUP_TYPE:
                { // �Ŷӵ����
                    if (TeamIndex < 1 || TeamIndex > GAMEDEFINE.MAX_TEAMS_IN_GROUP)
                    {
                        return 0;
                    }

                    return GetTeam(TeamIndex).m_TeamMembers.Count;
                }
                break;
            default:
                return 0;
        }
    }
    public int GetTeamMemberCount()
    {
        return GetTeamMemberCount(0);
    }

    // �õ�ĳ��������ʾ�ڶ��ѽ�������ľ���λ�� 1,2,3...
    public int GetMemberUIIndex(uint guid, byte TeamIndex/* = -1*/)
    {
        // ����� TeamIndex ����Ҫ����
        List<TeamMemberInfo> TeamMembers;
        uint myGUID = (uint)CObjectManager.Instance.getPlayerMySelf().GetServerGUID();

        if (guid == myGUID)
        { // �Լ�û�����
            return MacroDefine.INVALID_ID;
        }

        switch (m_Type)
        {
            case TEAM_OR_GROUP_TYPE.TEAM_TYPE:
                { // ��������
                    TeamMembers = m_TeamMembers;
                }
                break;
            case TEAM_OR_GROUP_TYPE.GROUP_TYPE:
                { // �Ŷӵ����
                    if (TeamIndex < 1 || TeamIndex > GAMEDEFINE.MAX_TEAMS_IN_GROUP)
                    {
                        return MacroDefine.INVALID_ID;
                    }

                    if (m_MyTeamID != TeamIndex)
                    { // ��ʱ��������ʾ�Ŷӳ�Ա��Ϣ
                        return MacroDefine.INVALID_ID;
                    }

                    TeamMembers = GetTeam(TeamIndex).m_TeamMembers;
                }
                break;
            default:
                return MacroDefine.INVALID_ID;
        }

        int i=0;
        foreach (TeamMemberInfo info in TeamMembers)
        {
            if (info.m_GUID != myGUID)
                i++;
            if (info.m_GUID == guid)
                return i;

        }
        return MacroDefine.INVALID_ID;
    }
    public int GetMemberUIIndex(uint guid)
    {
        return GetMemberUIIndex(guid, 0);
    }

    // �����������ת���ɶ��������е�����
    public TeamMemberInfo GetMemberByUIIndex(int UIIndex, byte TeamIndex /*= -1*/)
    {
        List<TeamMemberInfo> pTeamMembers;
        uint myGUID = (uint)CObjectManager.Instance.getPlayerMySelf().GetServerGUID();

        switch (m_Type)
        {
            case TEAM_OR_GROUP_TYPE.TEAM_TYPE:
                { // ��������
                    pTeamMembers = m_TeamMembers;
                }
                break;
            case TEAM_OR_GROUP_TYPE.GROUP_TYPE:
                { // �Ŷӵ����
                    if (TeamIndex < 1 || TeamIndex > GAMEDEFINE.MAX_TEAMS_IN_GROUP)
                    {
                        return null;
                    }

                    if (m_MyTeamID != TeamIndex)
                    { // ��ʱ��������ʾ�Ŷӳ�Ա��Ϣ
                        return null;
                    }

                    pTeamMembers = (GetTeam(TeamIndex).m_TeamMembers);
                }
                break;
            default:
                return null;
        }

        int i=0;
        foreach (TeamMemberInfo info in pTeamMembers)
        {

            if (i == UIIndex)
                return info;
            i++;
        }

        return null;
    }

    // ͨ����������, �õ�server id
    public int GetMemberServerIdByUIIndex(int UIIndex, byte TeamIndex /*= -1*/)
    {

        List<TeamMemberInfo> pTeamMembers;
        uint myGUID = (uint)CObjectManager.Instance.getPlayerMySelf().GetServerGUID();

        switch (m_Type)
        {
            case TEAM_OR_GROUP_TYPE.TEAM_TYPE:
                { // ��������
                    pTeamMembers = m_TeamMembers;
                }
                break;
            case TEAM_OR_GROUP_TYPE.GROUP_TYPE:
                { // �Ŷӵ����
                    if (TeamIndex < 1 || TeamIndex > GAMEDEFINE.MAX_TEAMS_IN_GROUP)
                    {
                        return -1;
                    }

                    if (m_MyTeamID != TeamIndex)
                    { // ��ʱ��������ʾ�Ŷӳ�Ա��Ϣ
                        return -1;
                    }

                    pTeamMembers = (GetTeam(TeamIndex).m_TeamMembers);
                }
                break;
            default:
                return -1;
        }

        int i=0;
        foreach (TeamMemberInfo info in pTeamMembers)
        {
            if (info.m_GUID != myGUID)
                i++;
            if (i == UIIndex)
                return info.m_OjbID;
        }

        return -1;
    }

    // ͨ�����������õ�ѡ�ж�Ա��guid
    public uint GetMemberGUIDByUIIndex(int UIIndex, byte TeamIndex /*= -1*/)
    {
        TeamMemberInfo pInfo = GetMemberByUIIndex(UIIndex, TeamIndex);
        if (pInfo != null)
        {
            return pInfo.m_GUID;
        }
        return MacroDefine.UINT_MAX;
    }

    // �õ��Լ�������
    public int GetSelfIndex(byte TeamIndex /*= -1*/)
    {
        return 0;
    }

    // ����ĳ�� guid �ҵ��������
    public TeamMemberInfo GetMember(uint guid)
    {
        switch (m_Type)
        {
            case TEAM_OR_GROUP_TYPE.TEAM_TYPE:
                { // �������

                    foreach (TeamMemberInfo info in m_TeamMembers)
                    {
                        if (info.m_GUID == guid)
                            return info;
                    }
                }
                break;
            case TEAM_OR_GROUP_TYPE.GROUP_TYPE:
                { // �Ŷ����
                    foreach (TeamInfo team in m_TeamInfo)
                    {
                        foreach (TeamMemberInfo info in team.m_TeamMembers)
                        {
                            if (info.m_GUID == guid)
                                return info;
                        }
                    }
                }
                break;
            default:
                //assert(FALSE);
                return null;
        }

        return null;
    }

    // ����server id �ҵ��������
    public TeamMemberInfo GetMemberByServerId(int iServerId)
    {
        switch(m_Type)
	{
	case TEAM_OR_GROUP_TYPE.TEAM_TYPE:
		{ // �������

            foreach (TeamMemberInfo info in m_TeamMembers)
            {
                if(info.m_OjbID == iServerId)
                    return info;
            }
		}
		break;
	case TEAM_OR_GROUP_TYPE.GROUP_TYPE:
		{ // �Ŷ����
            foreach (TeamInfo team in m_TeamInfo)
            {
                foreach (TeamMemberInfo info in team.m_TeamMembers)
                {
                    if (info.m_OjbID == iServerId)
                        return info;
                }
            }
		}
		break;
	default:
		//assert(FALSE);
		return null;
	}

	return null;
    }

    // �õ������е� idx ����Ա
    public TeamMemberInfo GetMemberByIndex(int idx, byte TeamIndex /*= -1*/)
    {
        switch (m_Type)
        {
            case TEAM_OR_GROUP_TYPE.TEAM_TYPE:
                { // ��������
                    if (m_TeamMembers.Count > idx)
                    {
                        return m_TeamMembers[idx];
                    }
                }
                break;
            case TEAM_OR_GROUP_TYPE.GROUP_TYPE:
                { // �Ŷӵ����
                    if (TeamIndex < 1 || TeamIndex > GAMEDEFINE.MAX_TEAMS_IN_GROUP)
                    {
                        return null;
                    }

                    if (GetTeam(TeamIndex).m_TeamMembers.Count > idx)
                    {
                        return GetTeam(TeamIndex).m_TeamMembers[idx];
                    }
                }
                break;
            default:
                return null;
        }

        return null;
    }
    public TeamMemberInfo GetMemberByIndex(int idx)
    {
        return GetMemberByIndex(idx, 0);
    }

    // �ı����롢�������Ĵ�״̬
    public void SetUIFlag(UI_ON_OPEN flag)
    {
        if (flag == 0)
        {
            // ˢ�����ݽṹ���ر����루���룩�����ʱ��һЩ�����������
            switch (m_UIFlag)
            {
                case UI_ON_OPEN.UI_INVITE_ON_OPEN:
                    {
                        while (m_InviteTeams.Count > MAX_INVITE_TEAM)
                        {
                            EraseInviteTeam(0);
                        }
                    }
                    break;
                case UI_ON_OPEN.UI_APPLY_ON_OPEN:
                    {
                        while (m_Proposers.Count > MAX_PROPOSER)
                        {
                            EraseProposer(0);
                        }
                    }
                    break;
                default:
                    return;
            }
        }

        m_UIFlag = flag;
    }

    // ȡ�����롢�������Ĵ�״̬
    public UI_ON_OPEN GetUIFlag() { return m_UIFlag; }

    // ����һ��������飬TRUE �ɹ�����֮ʧ��
    public bool AddInviteTeam(InviteTeam pTeam)
    {
        for (int i = 0; i < m_InviteTeams.Count; ++i)
        {
            if (m_InviteTeams[i].m_InviterGUID == pTeam.m_InviterGUID)
            { // ͬ���������˾Ͳ�������
                return false;
            }
        }

        m_InviteTeams.Add(pTeam);

        if (m_InviteTeams.Count > MAX_INVITE_TEAM)
        { // ֻ�����������ر�ʱ������ʱ����
            if (m_UIFlag == UI_ON_OPEN.UI_ALL_CLOSE)
            {
                EraseInviteTeam(0);
            }
        }

        return true;
    }

    // �õ���ǰ������������
    public int GetInviteTeamCount() { return m_InviteTeams.Count; }

    // ���������õ�ĳ������
    public InviteTeam GetInviteTeamByIndex(int idx)
    {
        if (idx < 0 || idx >= m_InviteTeams.Count)
        {
            
            return new InviteTeam();
        }

        return m_InviteTeams[idx];
    }
    // �������ֵõ�ĳ������ [6/15/2011 edit by ZL] 
    public InviteTeam GetInviteTeamByName(string nickName)
    {
        foreach (InviteTeam inviteTeam in m_InviteTeams)
        {
            foreach (TeamCacheInfo cacheInfo in inviteTeam.m_InvitersInfo)
            {
                if (cacheInfo.m_szNick == nickName)
                    return inviteTeam;
            }
        }
 
        return new InviteTeam();
    }


    // ���ĳ���������
    public void EraseInviteTeam(int idx)
    {
        if (idx < 0 || idx >= m_InviteTeams.Count)
        {
            return;
        }
        m_InviteTeams.RemoveAt(idx);
    }

    // ͨ���������ĳ��������� [6/16/2011 edit by ZL]
    public void EraseInviteTeamByName(string nickName)
    {
        int idx = 0;
        foreach (InviteTeam inviteTeam in m_InviteTeams)
        {
            
            foreach (TeamCacheInfo cacheInfo in inviteTeam.m_InvitersInfo)
            {
                if (cacheInfo.m_szNick == nickName)
                    break;
            }
            idx++;
        }
        if (idx == m_InviteTeams.Count) return;

        EraseInviteTeam(idx);
    }

    // ����������
    public void ClearInviteTeam()
    {
        m_InviteTeams.Clear();
    }

    // ����һ��������
    public bool AddProposer(TeamCacheInfo pProposer)
    {
        foreach (TeamCacheInfo info in  m_Proposers)
        {
            if (info.m_GUID == pProposer.m_GUID)
                return false;
        }


        m_Proposers.Add(pProposer);

        if (m_Proposers.Count > MAX_PROPOSER)
        { // ֻ�����������ر�ʱ������ʱ����
            if (m_UIFlag == UI_ON_OPEN.UI_ALL_CLOSE)
            {
                EraseProposer(0);
            }
        }

        return true;
    }

    // ���һ��������
    public void EraseProposer(int idx)
    {
        if (idx < 0 || idx >= m_Proposers.Count)
        {
            return;
        }
        m_Proposers.RemoveAt(idx);
    }

    // ͨ���������һ�������� [6/15/2011 edit by ZL]
    public void EraseProposerByName(string nickName)
    {
        foreach (TeamCacheInfo info in m_Proposers)
        {
            if (info.m_szNick == nickName)
            {
                m_Proposers.Remove(info);
                return;
            }
        }
    }

    // ����������
    public void ClearProposer()
    {
        m_Proposers.Clear();
    }

    // �õ���ǰ������е�����
    public int GetProposerCount() { return (int)m_Proposers.Count; }

    // ���������õ�ĳ��������
    public TeamCacheInfo GetProposerByIndex(int idx)
    {
        if (idx < 0 || idx >= m_Proposers.Count)
        {
            return new TeamCacheInfo();
        }

        return m_Proposers[idx];
    }

    // ���������õ�ĳ�������� [6/15/2011 edit by ZL]
    public TeamCacheInfo GetProposerByName(string nickName)
    {
        for (int idx = 0; idx < m_Proposers.Count; ++idx)
        {
            if (m_Proposers[idx].m_szNick == nickName)
            {
                return m_Proposers[idx];
            }
        }

        return new TeamCacheInfo();
    }

    // �������е�ǰ��ҵ���ϸ��Ϣ
    public void FillMyInfo()
    {
        if (!HasTeam())
        { // û�ж��鲻���в���
            return;
        }

        TeamMemberInfo pMyTMInfo = null;

        uint guid = (uint)CObjectManager.Instance.getPlayerMySelf().GetServerGUID();
        pMyTMInfo = GetMember(guid);

        if (pMyTMInfo == null)
        {
            return;
        }

        FillMyInfo(pMyTMInfo);
    }

    // ����ģ����Ϣ
    public void SetModelLook()
    {
        if ( !HasTeam() )
	{ // û�ж��鲻���в���
		return;
	}

	TeamMemberInfo pMyTMInfo = null;

	uint guid = (uint)CObjectManager.Instance.getPlayerMySelf().GetServerGUID();
	pMyTMInfo = GetMember(guid);
	if ( pMyTMInfo == null )
	{
		return;
	}

	// ����uiģ��
	pMyTMInfo.m_UIModel.SetUIModelInfo(HUMAN_EQUIP.HEQUIP_WEAPON, pMyTMInfo.m_WeaponID);
	
	// ����uiģ��
    pMyTMInfo.m_UIModel.SetUIModelInfo(HUMAN_EQUIP.HEQUIP_CAP, pMyTMInfo.m_CapID);
		
	// ����uiģ��
    pMyTMInfo.m_UIModel.SetUIModelInfo(HUMAN_EQUIP.HEQUIP_ARMOR, pMyTMInfo.m_ArmourID);
		
	// ����uiģ��
    pMyTMInfo.m_UIModel.SetUIModelInfo(HUMAN_EQUIP.HEQUIP_CUFF, pMyTMInfo.m_CuffID);
	
	// ����uiģ��
    pMyTMInfo.m_UIModel.SetUIModelInfo(HUMAN_EQUIP.HEQUIP_BOOT, pMyTMInfo.m_FootID);

    pMyTMInfo.m_UIModel.SetUIModelInfo(HUMAN_EQUIP.HEQUIP_BACK, pMyTMInfo.m_uBackID);


	if(pMyTMInfo.m_uFaceMeshID < 255)
	{
		// ��������
		pMyTMInfo.m_UIModel.SetFaceMeshId(pMyTMInfo.m_uFaceMeshID);
	}
		
	if(pMyTMInfo.m_uHairMeshID < 255)
	{
		// ���÷���
		pMyTMInfo.m_UIModel.SetFaceHairId(pMyTMInfo.m_uHairMeshID);
	}
	
	//if(pMyTMInfo->m_uHairColor < 255)
	//{
	//	// ������ɫ
	//	pMyTMInfo->m_UIModel.SetHairColor(pMyTMInfo->m_uHairColor);
	//}
	//else
	//{
	//	// ������ɫ
	//	pMyTMInfo->m_UIModel.SetHairColor(0);
	//}//

	// ������ɫ
	pMyTMInfo.m_UIModel.SetHairColor(pMyTMInfo.m_uHairColor);
    }

    // �Ƿ���ͬһ��������.
    public bool IsInScene(int iIndex)
    {
        switch(m_Type)
	{
	case TEAM_OR_GROUP_TYPE.TEAM_TYPE:
		{ // ��������
			if( m_TeamMembers.Count > iIndex )
			{
				TeamMemberInfo pInfo = m_TeamMembers[iIndex];
				if(pInfo != null)
				{
					if(pInfo.m_SceneID == WorldManager.Instance.GetActiveSceneID())
					{
                        return true;
					}
				}
				else
				{
					return false;
				}
					
			}
			else
			{
				return false;
			}
		}
		break;
	case TEAM_OR_GROUP_TYPE.GROUP_TYPE:
		{ // �Ŷӵ����
			
			return false;
		}
		break;
	default:
		{
			return false;
		}
	}

	return false;
    }

    // ���� Buff �б�
    public void UpdateImpactsList(int ObjID, SimpleImpactList pSimpleImpactList)
    {
        TeamMemberInfo pTMInfo = GetMemberByServerId(ObjID);
        if (pTMInfo == null)
        {
            return;
        }

        pTMInfo.m_SimpleImpactList.SetImpactList(pSimpleImpactList);

        int idx = GetMemberUIIndex(pTMInfo.m_GUID, 0);
        if (idx != MacroDefine.INVALID_ID)
        {
            GameProcedure.s_pEventSystem.PushEvent(GAME_EVENT_ID.GE_TEAM_UPTEDATA_MEMBER_INFO, idx);
        }
    }

    // ���� Buff
    public void AddImpact(int ObjID, short ImpactID)
    {
        TeamMemberInfo pTMInfo = GetMemberByServerId(ObjID);
        if (pTMInfo == null)
        {
            return;
        }

        pTMInfo.m_SimpleImpactList.AddImpact(ImpactID);

        int idx = GetMemberUIIndex(pTMInfo.m_GUID, 0);
        if (idx != MacroDefine.INVALID_ID)
        {
            GameProcedure.s_pEventSystem.PushEvent(GAME_EVENT_ID.GE_TEAM_UPTEDATA_MEMBER_INFO, idx);
        }
    }

    // ���� Buff
    public void RemoveImpact(int ObjID, short ImpactID)
    {
        TeamMemberInfo pTMInfo = GetMemberByServerId(ObjID);
        if (pTMInfo == null)
        {
            return;
        }

        pTMInfo.m_SimpleImpactList.RemoveImpact(ImpactID);

        int idx = GetMemberUIIndex(pTMInfo.m_GUID, 0);
        if (idx != MacroDefine.INVALID_ID)
        {
            GameProcedure.s_pEventSystem.PushEvent(GAME_EVENT_ID.GE_TEAM_UPTEDATA_MEMBER_INFO, idx);
        }
    }

    // �޸���ӷ��䷽ʽ [8/24/2011 edit by ZL]
    public void SetTeamAllocation(byte ruler) { m_TeamAllocation = ruler; }

    // �õ���ӷ��䷽ʽ [8/24/2011 edit by ZL]
    public int GetTeamAllocation() { return (int)m_TeamAllocation; }

    // �õ��Լ��Ķ�����Ϣ�����Լ������ݳػ�ȡ��
    void FillMyInfo(TeamMemberInfo member)
    {
        if(member == null)
            throw new NullReferenceException("team member in FillMyInfo(member)");

	CObject_PlayerMySelf pMe = CObjectManager.Instance.getPlayerMySelf();

	CCharacterData pMyData = pMe.GetCharacterData();

	if(null == pMyData)
	{
		return ;
	}
	member.m_szNick= pMyData.Get_Name();
	member.m_uFamily = pMyData.Get_MenPai();
	member.m_uDataID = pMyData.Get_RaceID();
	member.m_uLevel = pMyData.Get_Level();
	member.m_nPortrait = pMyData.Get_PortraitID();
	member.m_WorldPos.m_fX = pMe.GetPosition().x;
	member.m_WorldPos.m_fZ = pMe.GetPosition().z;
	member.m_nHP = pMyData.Get_HP();
	member.m_dwMaxHP = pMyData.Get_MaxHP();
	member.m_nMP = pMyData.Get_MP();
	member.m_dwMaxMP = pMyData.Get_MaxMP();
	member.m_nAnger = 100; // �ͻ���û��
	member.m_WeaponID = pMyData.Get_Equip(HUMAN_EQUIP.HEQUIP_WEAPON);
    member.m_CapID = pMyData.Get_Equip(HUMAN_EQUIP.HEQUIP_CAP);
    member.m_ArmourID = pMyData.Get_Equip(HUMAN_EQUIP.HEQUIP_ARMOR);
    member.m_CuffID = pMyData.Get_Equip(HUMAN_EQUIP.HEQUIP_CUFF);
    member.m_FootID = pMyData.Get_Equip(HUMAN_EQUIP.HEQUIP_BOOT);
    member.m_uBackID = pMyData.Get_Equip(HUMAN_EQUIP.HEQUIP_BACK);//  [8/30/2010 Sun]
    member.m_bDead = (pMe.CharacterLogic_Get() == ENUM_CHARACTER_LOGIC.CHARACTER_LOGIC_DEAD);

	member.m_uFaceMeshID = pMyData.Get_FaceMesh();
	member.m_uHairMeshID = pMyData.Get_HairMesh();
	member.m_uHairColor  = pMyData.Get_HairColor();
    }

    // ����С��� N �õ��� N ��С��
    TeamInfo GetTeam(byte TeamIndex)
    {
        foreach(TeamInfo info in m_TeamInfo)
	{
		if( info.m_TeamID == TeamIndex )
		{
			return info;
		}
	}

	return null;
    }

    TEAM_OR_GROUP_TYPE m_Type;				// team or group
    short m_ID;				// the serial number of the team or group in the game world
    // it can be used to identify the empty team or group
    byte m_MyTeamID;			// the team ID of mine in my group
    uint m_LeaderID;			// guid of the team leader
    List<TeamMemberInfo> m_TeamMembers = new List<TeamMemberInfo>();		// [MAX_TEAM_MEMBER]
    List<TeamInfo> m_TeamInfo = new List<TeamInfo>();			// [MAX_TEAMS_IN_GROUP]

    byte m_TeamAllocation;	// �������ģʽ [8/24/2011 edit by ZL]

    // ����������Ϣ
    UI_ON_OPEN m_UIFlag;			// �����ж������˽�����������˽����Ƿ��Ѿ���
    List<TeamCacheInfo> m_Proposers = new List<TeamCacheInfo>();		// �������б��ӳ��ɼ�
    List<InviteTeam> m_InviteTeams = new List<InviteTeam>();		// ���������Ϣ�б��������˿ɼ�
};
