/***********************************\
*									*
*		�����ؽӿ�				*
*		2006-03-24					*
*									*
\***********************************/
using System.Collections;
using System.Collections.Generic;


// 	//only for tolua++
// 	// Ϊ�˵����ṹ��lua����Ҫ����һ��ԭ���ݽṹ��ӳ�䣬���ڵ���
// 	// ���������ͬʱ��Ҫ�޸�lua�ű��Ĵ���[6/8/2010 Sun]
// 	// ������Ϣ����[struct for tolua++]
// 	struct Lua_GuildInfo{//tolua_export
// 	
// 		//tolua_begin
// 		int					m_GuildID;
// 		std::string			m_ChiefName;
// 		std::string			m_GuildName;
// 		std::string			m_GuildDesc;
// 		std::string			m_CityName;
// 		int					m_PortSceneID;
// 		UCHAR				m_uGuildStatus;
// 		int					m_uGuildUserCount;
// 		BYTE				m_bGuildLevel;
// 		std::string			m_FoundedTime;
// 		//tolua_end
// 
// 		Lua_GuildInfo()
// 		{
// 			CleanUp();
// 		}
// 
// 		void CleanUp()
// 		{
// 			m_GuildID			= INVALID_ID;
// 			m_ChiefName			=	"";
// 			m_GuildName			=	"";
// 			m_GuildDesc			=	"";
// 			m_CityName			=	"";
// 			m_uGuildStatus		=	0;
// 			m_uGuildUserCount	=	0;
// 			m_bGuildLevel		=	0;
// 			m_PortSceneID		=	-1;
// 			m_FoundedTime		=	"";
// 		}
// 
// 	};//tolua_export
// 
// 	//only for tolua++
// 	// ������Ϣ����[struct for tolua++]
// 	struct Lua_GuildMemberInfo{//tolua_export
// 	
// 		//tolua_begin
// 		std::string	m_szName;
// 		UINT		m_Guid;
// 		BYTE		m_bLevel;
// 		BYTE		m_bMenPaiID;
// 		int			m_iCurContribute;
// 		int			m_iMaxContribute;
// 		std::string	m_JoinTime;
// 		std::string	m_LogOutTime;
// 		BYTE		m_bIsOnline;
// 		BYTE		m_bPosition;
// 		std::string	m_ShowColour;
// 		//tolua_end
// 
// 		Lua_GuildMemberInfo()
// 		{
// 			CleanUp();
// 		}
// 		void CleanUp()
// 		{
// 			m_szName			=	"";
// 			m_Guid				=	INVALID_ID;
// 			m_bLevel			=	0;
// 			m_bMenPaiID			=	0;
// 			m_iCurContribute	=	0;
// 			m_iMaxContribute	=	0;
// 			m_JoinTime			=	"";
// 			m_LogOutTime		=	"";
// 			m_bIsOnline			=	0;
// 			m_bPosition			=	0;
// 			m_ShowColour		=   "";
// 		}
// 
// 	};//tolua_export
// 
// 	//���ɵ���ϸ��Ϣ
// 	//only for tolua++
// 	// ������Ϣ����[struct for tolua++]
// 	struct Lua_GuildDetailInfo{//tolua_export
// 
// 		//tolua_begin
// 		std::string			m_GuildName;
// 		std::string			m_GuildCreator;
// 		std::string			m_GuildChairMan;
// 		std::string			m_CityName;
// 		BYTE			m_nLevel;
// 		int				m_nPortSceneID;				//��ڳ���
// 		int				m_MemNum;					//����
// 		int				m_Longevity;				//���� 
// 		int				m_Contribute;				//���׶�
// 		int				m_Honor;					//����
// 		int				m_FoundedMoney;				//�����ʽ�
// 		int				m_nIndustryLevel;			//��ҵ��
// 		int				m_nAgrLevel;				//ũҵ��
// 		int				m_nComLevel;				//��ҵ��
// 		int				m_nDefLevel;				//������
// 		int				m_nTechLevel;				//�Ƽ���
// 		int				m_nAmbiLevel;				//���Ŷ�
// 		//tolua_end
// 
// 		Lua_GuildDetailInfo()
// 		{
// 			CleanUp();
// 		}
// 
// 		void CleanUp()
// 		{
// 			m_GuildName			= "";
// 			m_GuildCreator		= "";
// 			m_GuildChairMan		= "";
// 			m_CityName			= "";
// 			m_nLevel			=	0;
// 			m_nPortSceneID		=	0;			//��ڳ���
// 			m_MemNum			=	0;			//����
// 			m_Longevity			=	0;			//���� 
// 			m_Contribute		=	0;			//���׶�
// 			m_Honor				=	0;			//����
// 			m_FoundedMoney		=	0;			//�����ʽ�
// 			m_nIndustryLevel	=	0;			//��ҵ��
// 			m_nAgrLevel			=	0;			//ũҵ��
// 			m_nComLevel			=	0;			//��ҵ��
// 			m_nDefLevel			=	0;			//������
// 			m_nTechLevel		=	0;			//�Ƽ���
// 			m_nAmbiLevel		=	0;			//���Ŷ�
// 		}
// 
// 	};//tolua_export
    public enum ERR_GUILD
	{
		ERR_GUILD_ALREADYIN_MSG = 0,	//����Ѿ���һ�������
		ERR_GUILD_NOTIN_MSG,			//��Ҳ��ڰ����
		ERR_GUILD_NOPOW_MSG,			//���Ȩ�޲���
		ERR_GUILD_NOTHAVEASSCHIEF_MSG,	//û�и�����

		ERR_GUILD_CREATE_LEVEL_TOO_LOW,	//�����ȼ�����
		ERR_GUILD_NAME_EMPTY,			//�����Ϊ��
		ERR_GUILD_NAME_INVALID,			//�����ﺬ�����д�
		ERR_GUILD_NAME_CANTUSE,			//��������ȫ���˱��еĴ�
		ERR_GUILD_DESC_EMPTY,			//����Ϊ��
		ERR_GUILD_DESC_INVALID,			//�����ﺬ�����д�
		ERR_GUILD_MONEY_NOT_ENOUGH,		//��������Ǯ����

		ERR_GUILD_JOIN_LEVEL_TOO_LOW,	//����ȼ�����
		ERR_GUILD_POW_NORECRUIT,		//û��Ȩ������
		ERR_GUILD_POW_NOEXPEL,			//û��Ȩ������
	};

 	public class Guild
    {
 		//�������
		public int	CreateGuild(string lpGuildName, string lpGuildDesc)
        {
            const int E_FALSE = -1;
		    string szGuildName = lpGuildName;
		    string szGuildDesc = lpGuildDesc;

		    if(szGuildName.Length>0 && szGuildDesc.Length>0)
		    {
			    //��ᴴ���ʸ���
			    if( CObjectManager.Instance.getPlayerMySelf().GetCharacterData().Get_Level() < 40 )
			    {
                    ShowMsg(ERR_GUILD.ERR_GUILD_CREATE_LEVEL_TOO_LOW, false, true);
				    return E_FALSE;
			    }

			    if(MacroDefine.INVALID_ID == CObjectManager.Instance.getPlayerMySelf().GetCharacterData().Get_Guild())
			    {
				    //��ȫƥ�����
// 				    if(!GameProcedure.s_pUISystem.CheckStringFullCompare(szGuildName, "guild"))
// 				    {
// 					    ShowMsg(ERR_GUILD.ERR_GUILD_NAME_CANTUSE);
// 					    return E_FALSE;
// 				    }
                    
				    //�����ַ�����
// 				    if(!GameProcedure.s_pUISystem.CheckStringFilter(szGuildName))
// 				    {
// 					    ShowMsg(ERR_GUILD.ERR_GUILD_NAME_INVALID);
// 					    return E_FALSE;
// 				    }

// 				    //�����ַ�����
// 				    if(!GameProcedure.s_pUISystem.CheckStringFilter(szGuildDesc))
// 				    {
// 					    ShowMsg(ERR_GUILD.ERR_GUILD_DESC_INVALID);
// 					    return E_FALSE;
// 				    }

// 				    //���ƷǷ��ַ�����
// 				    if(!TDU_CheckStringValid(szGuildName.c_str()))
// 				    {
// 					    ShowMsg(ERR_GUILD.ERR_GUILD_NAME_INVALID);
// 					    return E_FALSE;
// 				    }

				    m_MsgArray.Add(szGuildName);
				    m_MsgArray.Add(szGuildDesc);

				    //��ʾȷ�Ͽ�
				    CEventSystem.Instance.PushEvent(GAME_EVENT_ID.GE_GUILD_CREATE_CONFIRM, szGuildName);

				    return 1;
			    }
			    else
			    {
				    //�Ѿ�ӵ��һ�����MSG
                    ShowMsg(ERR_GUILD.ERR_GUILD_ALREADYIN_MSG, false, true);
				    return	-1;
			    }
		    }
		    else
		    {
			    if(szGuildName.Length == 0)
			    {
                    ShowMsg(ERR_GUILD.ERR_GUILD_NAME_EMPTY, false, true);
			    }

                if (szGuildDesc.Length == 0)
			    {
                    ShowMsg(ERR_GUILD.ERR_GUILD_DESC_EMPTY, false, true);
			    }
		    }

		    return E_FALSE;
        }
// 
// 		//�������ȷ��
 		public void CreateGuildConfirm(int nConfirmId)
        {
         /*   if(1 == nConfirmId && m_MsgArray.Count == 2) //create
		    {
    			
			    if(CObjectManager.Instance.getPlayerMySelf().GetCharacterData().Get_Money()<500000)
			    {
				    ShowMsg(ERR_GUILD.ERR_GUILD_MONEY_NOT_ENOUGH);
				    return ;
			    }
			    //���ʹ��������Ϣ��
                CGGuildApply pk = new CGGuildApply();
			    pk.SetGuildNameSize((BYTE)m_MsgArray[0].size());
			    pk.SetGuildName((CHAR*)m_MsgArray[0].c_str());
			    pk.SetGuildDescSize((BYTE)m_MsgArray[1].size());
			    pk.SetGuildDesc((CHAR*)m_MsgArray[1].c_str());
    			
			    NetManager.GetNetManager().SendPacket(pk);
		    }
		    else if(nConfirmId == 2)	//destory
		    {
			    //���Ͱ��ɾ����
                CGGuild ck = new CGGuild();
			    ck.GetGuildPacket().m_uPacketType = GUILD_PACKET_TYPE.GUILD_PACKET_CG_DISMISS;

			    NetManager.GetNetManager().SendPacket(&ck);
		    }
		    else if(nConfirmId == 3) //quit
		    {
			    //�����˳�����
                CGGuild dk = new CGGuild();
    			
			    dk.GetGuildPacket().m_uPacketType = GUILD_PACKET_TYPE.GUILD_PACKET_CG_LEAVE;
			    GUILD_CGW_LEAVE pLeave = (GUILD_CGW_LEAVE)dk.GetGuildPacket().GetPacket(GUILD_PACKET_TYPE.GUILD_PACKET_CG_LEAVE);

			    if(pLeave)
			    {
                    NetManager.GetNetManager().SendPacket(&dk);
			    }
		    }

		    m_MsgArray.clear();*/
        }
// 
// 		//��World��������ϸ��Ϣ
// 		public void AskGuildDetailInfo();
// 
// 		//��World�������Ա��Ϣ
// 		public void AskGuildMembersInfo();
// 
// 		//��World������ְλ��Ϣ
// 		public void AskGuildAppointPosInfo(int nMemberBak);
// 
// 		//������а�������
// 		public int	GetGuildNum();
// 
// 		//�������������Ϣ
// 		// �޸��˷���ֵ�Ͳ��������lua�ű�����Ҫ�޸� [6/8/2010 Sun]
// 		public Lua_GuildInfo GetGuildInfo(int nIndex);
// 
// 		//������
// 		public void JoinGuild(int nIndex);
// 
// 		//�˳����
// 		public void	QuitGuild();
// 
// 		//�߳����,�ܾ�����
// 		public void KickGuild(int nIndex);
// 
// 		//���ɻ�Ա
// 		public void RecruitGuild(int nIndex);
// 
// 		//����Լ��İ�����Ϣ
// 		public int GetMembersNum(int nType);
// 
// 		//Lua��ʾ��list�ؼ���λ��[4/16/2006]
// 		public int GetShowMembersIdx(int nShowIdx);
// 		public int GetShowTraineesIdx(int nShowIdx);
// 
// 		//����Լ��İ�����ϸ��Ϣ
// 		public Lua_GuildMemberInfo	GetMembersInfo(int nIndex);
// 
// 		//����Լ��İ�����Ϣ
// 		public string GetMyGuildInfo(string lpOp, int nIndex /*= 0*/);
// 
// 		//����Լ����ɵ���ϸ��Ϣ
// 		public Lua_GuildDetailInfo GetMyGuildDetailInfo();
// 
// 		//����Լ��İ���Ȩ��
// 		public string GetMyGuildPower();
// 
//         //�޸����а���ְλ
// 		public void	AdjustMemberAuth(int nIndex);
// 
// 		//�����λ������
// 		public void ChangeGuildLeader();
// 
// 		//���ɾ��
// 		public void	DestoryGuild();
// 
// 		//�޸İ����Ϣ
// 		public void FixGuildInfo();
// 
// 		//�������
// 		public void DemisGuild();
// 
// 		//׼������Ա����[4/16/2006]
// 		public void PrepareMembersInfomation();
// 
// 		// ��ʾ������ [2/27/2010 Sun]
// 		public void ToggleGuildDetailInfo();
// 		//tolua_end
// 
// 		//��ʾ��ʾ��Ϣ
// 		// msgType		��Ϣ�ţ��������ֵ����ȡ��Ӧ������
// 		// bTalk		��Ҫ��ʾ�����촰��
// 		// bTip			��Ҫ��ʾ����Ļ�м����ʾ
        public void ShowMsg(ERR_GUILD msgType, bool bTalk /*= FALSE*/, bool bTip/* = TRUE*/)
        {

        }
// 
// 		public struct Name_Idx
// 		{
// 			string	m_MemberName;		//��ӦDataPool��GuildMemberInfo_t�ṹ���m_szName
// 			int		m_MemberIdx;		//��ӦDataPool��GuildMemberInfo_t������ֵ
// 			int		m_Position;			//�ڰ��е�ְλ
// 
// 			Name_Idx()
// 			{
// 				m_MemberName.erase();
// 				m_MemberIdx = -1;
// 				m_Position = GUILD_POSITION.GUILD_POSITION_INVALID;
// 			}
// 
// 			void	CleanUp()
// 			{
// 				m_MemberName.erase();
// 				m_MemberIdx = -1;
// 				m_Position = GUILD_POSITION.GUILD_POSITION_INVALID;
// 			}
// 		};
// 
// 		public class ShowColor
// 		{
// 			public string	m_OnlineLeaderColor;		//�����쵼����ʾ��ɫ
// 			public string	m_LeaveLeaderColor;			//�����쵼����ʾ��ɫ
// 			public string	m_OnlineMemberColor;		//���߰�����ʾ��ɫ
// 			public string	m_LeaveMemberColor;			//���߰�����ʾ��ɫ
// 			public string	m_OnlineTraineeColor;		//������������ʾ��ɫ
// 			public string	m_LeaveTraineeColor;		//������������ʾ��ɫ
// 
// 			ShowColor()
// 			{
// 				m_OnlineLeaderColor = "#B";			//��ɫ
// 				m_OnlineMemberColor = "#W";			//��ɫ
// 				m_OnlineTraineeColor = "#W";
// 
// 				m_LeaveLeaderColor = "#c808080";	//��ɫ
// 				m_LeaveMemberColor = "#c808080";
// 				m_LeaveTraineeColor = "#c808080";
// 			}
// 		};
// 		//������ʱ��ת��
// 		void ConvertServerTime(int dTime, string  strTime);
// 
// 		//���°����Ӧ����ʾ��Ϣ
// 		void PerpareMembersBeforeShow();
// 
// 		//ѡ����ʾ��ɫ
// 		string GetShowColor_For_Lua(int idx);
// 
		List<string>		m_MsgArray = new List<string>();			//���ShowMsg����ʹ��
		int						m_iMemberBak;		//�ı�ְλʱ�ı�����Ҫ�޸��ĸ���ҵ�ְλ��Ϣ

		//��Ա�б�
		List<string>						m_AllMembers = new List<string>();			//���г�Ա
		List<string>						m_AllTrainees = new List<string>();			//����Ԥ����Ա
//		ShowColor								m_LuaShowColors = new ShowColor();		//Lua����ʾ����ɫ
// 		public Guild();
// 		public ~Guild();
// 
 		public static Guild s_Guild;
 	}

