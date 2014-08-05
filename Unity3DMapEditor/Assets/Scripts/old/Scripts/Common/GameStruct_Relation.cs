using System;
using Network;
using Network.Packets;
using System.Runtime.InteropServices;
// ��ϵ������
[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class _RELATION : ClassCanbeSerialized
{
    public uint m_GUID;								// GUID
    public byte m_uNameSize;
    public byte[] m_szName = new byte[GAMEDEFINE.MAX_CHARACTER_NAME];		// ����
    public int m_nLevel;							// ��ɫ�ȼ�
    public int m_nMenPai;							// ���� MENPAI_ATTRIBUTE
    public int m_nPortrait;						// ͷ��
    public short m_GuildID;							// ���ID�����ڷ�������ˣ�
    public byte m_uGuildNameSize;					// 
    public byte[] m_szGuildName = new byte[GAMEDEFINE.MAX_GUILD_NAME_SIZE];	// ������ƣ����� Server �����ͻ��ˣ�
    public byte m_uOnlineFlag;						// �Ƿ����ߣ����²���ֻ�����߲ŷ����ͻ��ˣ�
    public byte m_uMoodSize;						// ���鳤��
    public byte[] m_szMood = new byte[GAMEDEFINE.MOOD_DATA_SIZE];			// ����
    public byte m_uTitleSize;						// �ƺ�����
    public byte[] m_szTitle = new byte[NET_DEFINE.MAX_CHARACTER_TITLE];		// �ƺ�
    public short m_SceneID;							// ���ڳ��������²����� Server ���ã�
    public byte m_uTeamSize;						// ����������0 ��ʾδ��ӣ�
    public static void copy(_RELATION src, _RELATION dest)
    {
        dest.m_GUID = src.m_GUID;
        dest.m_uNameSize = src.m_uNameSize;
        Array.Copy(src.m_szName, dest.m_szName, src.m_szName.Length);
        dest.m_nLevel = src.m_nLevel;
        dest.m_nMenPai = src.m_nMenPai;
        dest.m_nPortrait = src.m_nPortrait;
        dest.m_GuildID = src.m_GuildID;
        dest.m_uGuildNameSize = src.m_uGuildNameSize;
        Array.Copy(src.m_szGuildName, dest.m_szGuildName, src.m_szGuildName.Length);
        dest.m_uOnlineFlag = src.m_uOnlineFlag;
        dest.m_uMoodSize = src.m_uMoodSize;
        Array.Copy(src.m_szMood, dest.m_szMood, src.m_szMood.Length);
        dest.m_uTitleSize = src.m_uTitleSize;
        Array.Copy(src.m_szTitle, dest.m_szTitle, src.m_szTitle.Length);
        dest.m_SceneID = src.m_SceneID;
        dest.m_uTeamSize = src.m_uTeamSize;
    }
    public void CleanUp()
    {
        m_GUID = MacroDefine.UINT_MAX;
        m_uNameSize = 0;
        m_nLevel = 0;
        m_nMenPai = (int)MENPAI_ATTRIBUTE.MATTRIBUTE_WUMENPAI;
        m_nPortrait = -1;
        m_GuildID = -1;
        m_uGuildNameSize = 0;
        m_uOnlineFlag = 0;
        m_uMoodSize = 0;
        m_uTitleSize = 0;
        m_SceneID = -1;
        m_uTeamSize = 0;
    }
    public Boolean readFromBuff(ref Network.NetInputBuffer buff)
    {
       	buff.ReadUint( ref m_GUID );
	    buff.ReadByte( ref m_uNameSize );

	    if( m_uNameSize > 0 && m_uNameSize < m_szName.Length)
	    {
            for(byte i=0; i<m_uNameSize; ++i)
            {
                buff.ReadByte(ref m_szName[i] );
            }
	    }
        buff.ReadInt(ref m_nLevel);
	    buff.ReadInt(ref m_nMenPai);
        buff.ReadInt(ref m_nPortrait);
        buff.ReadShort(ref m_GuildID);
	    buff.ReadByte(ref m_uGuildNameSize);

	    if( m_uGuildNameSize > 0 && m_uGuildNameSize < m_szGuildName.Length )
	    {
             for(byte i=0; i<m_uGuildNameSize; ++i)
             {
                 buff.ReadByte(ref m_szGuildName[i]);
             }
	    }
        buff.ReadByte(ref m_uOnlineFlag);
	
	    if( m_uOnlineFlag > 0 )
	    {
            buff.ReadByte(ref m_uMoodSize);

		    if( m_uMoodSize > 0 && m_uMoodSize < m_szMood.Length )
		    {
                for(byte i=0; i<m_uMoodSize; ++i)
                {
                    buff.ReadByte(ref m_szMood[i]);
                }
		    }

            buff.ReadByte(ref m_uTitleSize);

		    if( m_uTitleSize > 0 && m_uTitleSize < (m_szTitle.Length) )
		    {
                for(byte i=0; i<m_uTitleSize; ++i)
                {
                    buff.ReadByte(ref m_szTitle[i]);
                }
		    }
            buff.ReadShort(ref m_SceneID);
            buff.ReadByte(ref m_uTeamSize);
	    }
        return true;
    }

    public Int32 getSize()
    {
        int uSize;

        uSize = sizeof(uint) + sizeof(byte) + m_uNameSize*sizeof(byte)
              + sizeof(int) + sizeof(int) + sizeof(int) + sizeof(short)
              + sizeof(byte) + m_uGuildNameSize*sizeof(byte) + sizeof(byte);

        if (m_uOnlineFlag > 0)
        {
            uSize += sizeof(byte) + m_uMoodSize*sizeof(byte) + sizeof(byte)
                   + m_uTitleSize*sizeof(byte) + sizeof(short) + sizeof(byte);
        }

        return uSize;
    }
    public static int getMaxSize()
    {
        int uSize;

        uSize = sizeof(uint) + sizeof(byte) + GAMEDEFINE.MAX_CHARACTER_NAME * sizeof(byte)
              + sizeof(int) + sizeof(int) + sizeof(int) + sizeof(short)
              + sizeof(byte) + GAMEDEFINE.MAX_GUILD_NAME_SIZE * sizeof(byte) + sizeof(byte);



        uSize += sizeof(byte) + GAMEDEFINE.MOOD_DATA_SIZE * sizeof(byte) + sizeof(byte)
                   + NET_DEFINE.MAX_CHARACTER_TITLE * sizeof(byte) + sizeof(short) + sizeof(byte);
        

        return uSize;
    }
    public Int32 writeToBuff(ref Network.NetOutputBuffer buff)
    {
        buff.WriteUint(m_GUID);
	    buff.WriteByte( m_uNameSize );

	    if( m_uNameSize > 0 && m_uNameSize < m_szName.Length )
	    {
            buff.Write(ref m_szName, m_uNameSize);
	    }
        buff.WriteInt(m_nLevel);
	    buff.WriteInt(m_nMenPai);
	    buff.WriteInt(m_nPortrait);
	    buff.WriteShort(m_GuildID);
	    buff.WriteByte(m_uGuildNameSize);
	    if( m_uGuildNameSize > 0 && m_uGuildNameSize < (m_szGuildName.Length) )
	    {
		    buff.Write(ref m_szGuildName, m_uGuildNameSize);
	    }

	    buff.WriteByte(m_uOnlineFlag);

	    if( m_uOnlineFlag > 0 )
	    {
		    buff.WriteByte( m_uMoodSize);

		    if( m_uMoodSize > 0 && m_uMoodSize < (m_szMood.Length) )
		    {
			    buff.Write(ref m_szMood, m_uMoodSize);
		    }

		    buff.WriteByte( m_uTitleSize );

		    if( m_uTitleSize > 0 && m_uTitleSize < m_szTitle.Length )
		    {
			    buff.Write(ref m_szTitle, m_uTitleSize);
		    }

		    buff.WriteShort( m_SceneID );
		    buff.WriteByte( m_uTeamSize );
	    }
        return getSize();
    }
    

	//����Ӧ�ýӿ�
	public uint								GetGUID( ) { return m_GUID; }
	public void								SetGUID( uint guid ) { m_GUID = guid; }

	public byte[]						GetName( ) { return m_szName; }
	public void							SetName( byte[] pName )
	{
		Array.Copy(pName, m_szName, GAMEDEFINE.MAX_CHARACTER_NAME -1);
		m_uNameSize = (byte)GameUtil.Utility.getStringLength(m_szName);
	}

	public int							GetLevel( ) { return m_nLevel; }
	public void							SetLevel( int level ) { m_nLevel = level; }

	public int							GetMenPai( ) { return m_nMenPai; }
	public void							SetMenPai( int menpai ) { m_nMenPai = menpai; }

	public int							GetPortrait( ) { return m_nPortrait; }
	public void							SetPortrait( int nPortrait ) { m_nPortrait = nPortrait; }

	public short						GetGuildID( ) { return m_GuildID; }
	public void							SetGuildID( short gid ) { m_GuildID = gid; }

	public byte[]						GetGuildName( ) { return m_szGuildName; }
	public void							SetGuildName( byte[] pName )
	{
        Array.Copy(pName, m_szGuildName, GAMEDEFINE.MAX_GUILD_NAME_SIZE - 1);
		
		m_uGuildNameSize = (byte)GameUtil.Utility.getStringLength(m_szGuildName);
	}

	public byte								GetOnlineFlag( ) { return m_uOnlineFlag; }
	public void								SetOnlineFlag( byte flag ) { m_uOnlineFlag = flag; }

	public byte[]							GetMood( ) { return m_szMood; }
	public void								SetMood( byte[] pMood )
	{
        Array.Copy(pMood, m_szMood, GAMEDEFINE.MOOD_DATA_SIZE - 1);
		m_uMoodSize =(byte)GameUtil.Utility.getStringLength(m_szMood);
	}

	public byte[]						GetTitle( ) { return m_szTitle; }
	public void							SetTitle( byte[] pTitle )
	{
        Array.Copy(pTitle, m_szTitle, GAMEDEFINE.MAX_TITLE_SIZE - 1);
		m_uTitleSize = (byte)GameUtil.Utility.getStringLength(m_szTitle);
	}

	public short						GetSceneID( ) { return m_SceneID; }
	public void							SetSceneID( short sid ) { m_SceneID = sid; }

	public byte							GetTeamSize( ) { return m_uTeamSize; }
	public void							SetTeamSize( byte TeamSize ) { m_uTeamSize = TeamSize; }

    
}

public enum RELATION_REQUEST_TYPE
{
	REQ_NONE							= 0,
	REQ_RELATIONLIST,
	REQ_RELATIONINFO,
	REQ_VIEWPLAYER,										// �鿴���
	REQ_ADDFRIEND,
	REQ_ADDTOBLACKLIST,
	REQ_TEMPFRIEND_TO_FRIEND,
	REQ_TEMPFRIEND_ADDTO_BLACKLIST,
	REQ_TRANSITION,
	REQ_DELFRIEND,
	REQ_DELFROMBLACKLIST,
	REQ_NEWGOODFRIEND,									// ����һ�����ܺ���
	REQ_RELATIONONLINE,									// ������������б�
	REQ_MODIFYMOOD,										// �޸��Լ�������
	REQ_MODIFYSETTINGS,									// �޸���ϵ������
	REQ_NOTIFY_ADDTEMPFRIEND,							// ֪ͨ�Է�����Ϊ��ʱ����
};

public enum RELATION_RETURN_TYPE
{
	RET_NONE							= 0,
	RET_RELATIONLIST,
	RET_RELATIONINFO,
	RET_VIEWPLAYER,										// �鿴���
	RET_TARGETNOTONLINE,								// Ŀ�겻���ߣ������� World ѯ��ĳ��ϵ�˵���Ϣû���ҵ�ʱ�ķ�����
	RET_ADDFRIEND,
	RET_ADDTOBLACKLIST,
	RET_TEMPFRIEND_TO_FRIEND,
	RET_TEMPFRIEND_ADDTO_BLACKLIST,
	RET_TRANSITION,
	RET_DELFRIEND,
	RET_DELFROMBLACKLIST,
	RET_ADDFRIENDNOTIFY,								// ֪ͨ�����Ѿ�������
	RET_ONLINELIST,										// ���ߺ����б�
	RET_RELATIONONLINE,									// ��������
	RET_RELATIONOFFLINE,								// ��������
	RET_NEWMOOD,										// �µ�����
	RET_NOTIFY_ADDTEMPFRIEND,							// ֪ͨ�Է�����Ϊ��ʱ����

	RET_ERR_START,
	RET_ERR_TARGETNOTEXIST,								// Ŀ�겻���ڻ�����
	RET_ERR_GROUPISFULL,								// ����������
	RET_ERR_ISFRIEND,									// �Ѿ��Ǻ���
	RET_ERR_ISBLACKNAME,								// �Ѿ������������
	RET_ERR_CANNOTTRANSITION,							// ����ת��
	RET_ERR_ISNOTFRIEND,								// ���Ǻ���
	RET_ERR_ISNOTINBLACKLIST,							// ���ں�����
	RET_ERR_SPOUSETOBLACKLIST,							// ����ż���������
	RET_ERR_MASTERTOBLACKLIST,							// ��ʦ�����������
	RET_ERR_PRENTICETOBLACKLIST,						// ��ͽ�ܼ��������
	RET_ERR_BROTHERTOBLACKLIST,							// ������ֵܼ��������
	RET_ERR_DELSPOUSE,									// ɾ����ż
	RET_ERR_DELMASTER,									// ɾ��ʦ��
	RET_ERR_DELPRENTICE,								// ɾ��ͽ��
	RET_ERR_DELBROTHER,									// ɾ�������ֵ�
	RET_ERR_PASSWDMISMATCH,								// ���벻ƥ��
	RET_ERR_CANNOT_ADDFRIEND,							// �ܾ���Ϊ����
	RET_ERR_CANNOTRECEIVEMAIL,							// �ܾ������κ��ʼ�
	RET_ERR_NOTRECVSTRANGEMAIL,							// ����İ�����ʼ�
	RET_ERR_ISENEMY,									// ����ͬ��Ӫ

	RET_ERR_RELATIONUNKNOWN,							// δ֪����
};

// ����Ϊ GUID
[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class RELATION_GUID: ClassCanbeSerialized
{
	//����
	public uint								m_TargetGUID;		// ĳ��ҵ� GUID

	//�����ӿ�
	public void								CleanUp( )
    {
        m_TargetGUID = MacroDefine.UINT_MAX;
    }
    public    int getSize()
    {
        return sizeof(uint);
    }
    public static int getMaxSize()
    {
        return sizeof(uint);
    }
    public    bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadUint(ref m_TargetGUID);
        return true;
    }
    public    int writeToBuff(ref NetOutputBuffer buff)
    {
        buff.WriteUint(m_TargetGUID);
        return getSize();
    }

	//����Ӧ�ýӿ�
	public uint								GetTargetGUID( ) { return m_TargetGUID; }
	public void								SetTargetGUID( uint guid ) { m_TargetGUID = guid; }
};

// ɾ������
[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class REQUEST_DEL_FRIEND :  RELATION_GUID
{
	//����

	//�����ӿ�
	new void								CleanUp( )
    {
        base.CleanUp();
    }
	 public    int getSize()
     {
         return base.getSize();
     }
     new public static int getMaxSize()
     {
         return RELATION_GUID.getMaxSize();
     }
	public    bool readFromBuff(ref NetInputBuffer buff)
    {
        return base.readFromBuff(ref buff);
    }
	public    int writeToBuff(ref NetOutputBuffer buff)
    {
         return base.writeToBuff(ref buff);
    }

	//����Ӧ�ýӿ�
};

// �޸�����
[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class REQUEST_MODIFY_MOOD: ClassCanbeSerialized
{
	//����
	byte	 m_uMoodSize;						// ���鳤��
	byte[]	 m_szMood= new byte[GAMEDEFINE.MOOD_DATA_SIZE];			// ����

	public void								CleanUp( )
    {
        m_uMoodSize = 0;
        for(int i=0; i<GAMEDEFINE.MOOD_DATA_SIZE; ++i)
        {
            m_szMood[i] = 0;
        }
    }
	public    int getSize()
    {
        return sizeof(byte) + m_uMoodSize*sizeof(byte);
    }
    public static int getMaxSize()
    {
        return sizeof(byte) + GAMEDEFINE.MOOD_DATA_SIZE * sizeof(byte);
    }
	public    bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadByte(ref m_uMoodSize);
        if(m_uMoodSize > 0 && m_uMoodSize<m_szMood.Length)
        {
            buff.Read(ref m_szMood, m_uMoodSize);
        }
        return true;
    }
	public    int writeToBuff(ref NetOutputBuffer buff)
    {
        buff.WriteByte(m_uMoodSize);
        if(m_uMoodSize > 0)
        {
            buff.Write(ref m_szMood, m_uMoodSize);
        }
        return getSize();
    }

	//����Ӧ�ýӿ�
	public byte[]							GetMood( ) { return m_szMood; }
	public void								SetMood(   byte[] pMood )
	{
        Array.Copy(pMood, m_szMood, GAMEDEFINE.MOOD_DATA_SIZE-1);
		m_uMoodSize = (byte)GameUtil.Utility.getStringLength(m_szMood);
	}
};

// ��ѯ��ϵ����ϸ��Ϣ
[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class REQUEST_RELATION_INFO: ClassCanbeSerialized
{
	//����
	uint								m_TargetGUID;		// ĳ��ҵ� GUID
	byte								m_uNameSize;
	byte[]								m_szTargetName = new byte[GAMEDEFINE.MAX_CHARACTER_NAME];

	//�����ӿ�
	public void								CleanUp( )
    {
        m_TargetGUID = MacroDefine.UINT_MAX;
	    m_uNameSize = 0;
        for(int i=0; i<m_szTargetName.Length; ++i)
        {
            m_szTargetName[i] = 0;
        }
    }
	public    int getSize()
    {
        return sizeof(uint) + sizeof(byte) + m_uNameSize*sizeof(byte);
    }
    public static int getMaxSize()
    {
        return sizeof(uint) + sizeof(byte) + GAMEDEFINE.MAX_CHARACTER_NAME * sizeof(byte);
    }
	public    bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadUint(ref m_TargetGUID);
        buff.ReadByte(ref m_uNameSize);
	    if( m_uNameSize > 0 && m_uNameSize < (m_szTargetName.Length) )
	    {
		    buff.Read( ref m_szTargetName, m_uNameSize );
	    }
        return true;
    }
	public    int writeToBuff(ref NetOutputBuffer buff)
    {
        buff.WriteUint(m_TargetGUID);
        buff.WriteByte(m_uNameSize);

	    if( m_uNameSize > 0 && m_uNameSize < (m_szTargetName.Length) )
	    {
		    buff.Write( ref m_szTargetName, m_uNameSize );
	    }
        return getSize();
    }
	//����Ӧ�ýӿ�
	public uint								GetTargetGUID( ) { return m_TargetGUID; }
	public void								SetTargetGUID( uint guid ) { m_TargetGUID = guid; }

	public void								SetTargetName(   byte[] pName )
	{
		Array.Copy(pName,m_szTargetName, GAMEDEFINE.MAX_CHARACTER_NAME-1 );
		m_uNameSize = (byte)GameUtil.Utility.getStringLength(m_szTargetName);
	}
    public byte[]							GetTargetName( ) { return m_szTargetName; }
};

// �ҽ��鿴���
[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class REQUEST_VIEW_PLAYER:ClassCanbeSerialized 
{
	//����
	byte								m_uNameSize;
	byte[]								m_szTargetName = new byte[GAMEDEFINE.MAX_CHARACTER_NAME];

	//�����ӿ�
	public void								CleanUp( )
    {
        m_uNameSize = 0;
        for(int i=0; i<m_szTargetName.Length;++i)
        {
            m_szTargetName[i] = 0;
        }
    }
	public    int getSize()
    {
        return sizeof(byte) + m_uNameSize*sizeof(byte);
    }
    public static int getMaxSize()
    {
        return sizeof(byte) + GAMEDEFINE.MAX_CHARACTER_NAME * sizeof(byte);
    }
	public    bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadByte(ref m_uNameSize);
	    if( m_uNameSize > 0 && m_uNameSize < (m_szTargetName.Length) )
	    {
		    buff.Read( ref m_szTargetName, m_uNameSize );
	    }
        return true;
    }
	public    int writeToBuff(ref NetOutputBuffer buff)
    {
        buff.WriteByte(m_uNameSize);

	    if( m_uNameSize > 0 && m_uNameSize < (m_szTargetName.Length) )
	    {
		    buff.Write( ref m_szTargetName, m_uNameSize );
	    }
        return getSize();
    }
	//����Ӧ�ýӿ�
	public void								SetTargetName(   byte[] pName )
	{
		Array.Copy(pName,  m_szTargetName,  GAMEDEFINE.MAX_CHARACTER_NAME-1 );
		m_uNameSize = (byte)GameUtil.Utility.getStringLength(m_szTargetName);
	}
	public byte[]							GetTargetName( ) { return m_szTargetName; }
};

// ����һ����ϵ��
[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class REQUEST_ADD_RELATION:ClassCanbeSerialized 
{
	//����
	uint								m_TargetGUID;		// ĳ��ҵ� GUID
	byte								m_uNameSize;
	byte[]								m_szTargetName = new byte[GAMEDEFINE.MAX_CHARACTER_NAME];
	byte								m_uRelationType;	// ��ϵ����

	//�����ӿ�
	public void								CleanUp( )
    {
        m_TargetGUID = MacroDefine.UINT_MAX;
	    m_uNameSize = 0;
        for(int i=0; i<m_szTargetName.Length; ++i)
        {
            m_szTargetName[i] = 0;
        }
	    m_uRelationType = 0;
    }

    public    int getSize()
    {
        return sizeof(uint) + sizeof(byte) + m_uNameSize*sizeof(byte) + sizeof(byte);
    }
    public static int getMaxSize()
    {
        return sizeof(uint) + sizeof(byte) + GAMEDEFINE.MAX_CHARACTER_NAME * sizeof(byte) + sizeof(byte);
    }
	public    bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadUint(ref m_TargetGUID);
        buff.ReadByte(ref m_uNameSize);
	    if( m_uNameSize > 0 && m_uNameSize < (m_szTargetName.Length) )
	    {
		    buff.Read( ref m_szTargetName, m_uNameSize );
	    }
        buff.ReadByte(ref m_uRelationType);
        return true;
    }
	public    int writeToBuff(ref NetOutputBuffer buff)
    {
        buff.WriteUint(m_TargetGUID);
        buff.WriteByte(m_uNameSize);

	    if( m_uNameSize > 0 && m_uNameSize < (m_szTargetName.Length) )
	    {
		    buff.Write( ref m_szTargetName, m_uNameSize );
	    }
        buff.WriteByte(m_uRelationType);
        return getSize();
    }

	//����Ӧ�ýӿ�
	public uint								GetTargetGUID( ) { return m_TargetGUID; }
	public void								SetTargetGUID( uint guid ) { m_TargetGUID = guid; }

	public void								SetTargetName(   byte[] pName )
	{
		Array.Copy( pName, m_szTargetName, pName.Length );
		m_uNameSize = (byte)GameUtil.Utility.getStringLength(m_szTargetName);
	}
    public byte[] GetTargetName() { return m_szTargetName; }

    public byte GetRelationType() { return m_uRelationType; }
    public void SetRelationType(byte uRelationType) { m_uRelationType = uRelationType; }
};

// ����һ����ϵ�ˣ����Ҵ������
[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class REQUEST_ADD_RELATION_WITH_GROUP :  REQUEST_ADD_RELATION
{
	//����
	byte								m_uGroup;			// ��

	//�����ӿ�
	void								CleanUp( )
    {
        base.CleanUp( );
        m_uGroup = 0;
    }
	public    int getSize() 
    {
        return base.getSize() + sizeof(byte);
    }
    public static int getMaxSize()
    {
        return REQUEST_ADD_RELATION.getMaxSize() + sizeof(byte); 
    }
	public    bool readFromBuff(ref NetInputBuffer buff)
    {
        base.readFromBuff(ref buff);
        buff.ReadByte(ref m_uGroup);
        return true;
    }
	public    int writeToBuff(ref NetOutputBuffer buff)
    {
        base.writeToBuff(ref buff);
        buff.WriteByte(m_uGroup);
        return getSize();
    }
	//����Ӧ�ýӿ�
	public byte								GetGroup( ) { return m_uGroup; }
	public void								SetGroup( byte uGroup ) { m_uGroup = uGroup; }
};

// ����Ϊ GUID �� char��char
[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class RELATION_GUID_UCHAR_UCHAR:ClassCanbeSerialized
{
	//����
	uint								m_TargetGUID;		// ĳ��ҵ� GUID
	byte								m_uRelationType;	// ��ϵ����
	byte								m_uGroup;			// ��

	//�����ӿ�
	public void								CleanUp( )
    {
        m_TargetGUID = MacroDefine.UINT_MAX;
        m_uRelationType = 0;
        m_uGroup = 0;
    }
    public    int getSize()
    {
        return sizeof(uint) + 2*sizeof(byte);
    }
    public static int getMaxSize()
    {
        return sizeof(uint) + 2 * sizeof(byte);
    }
    public    bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadUint(ref m_TargetGUID);
        buff.ReadByte(ref m_uRelationType);
        buff.ReadByte(ref m_uGroup);
        return true;
    }
    public    int writeToBuff(ref NetOutputBuffer buff)
    {
        buff.WriteUint(m_TargetGUID);
        buff.WriteByte(m_uRelationType);
        buff.WriteByte(m_uGroup);
        return getSize();
    }

	//����Ӧ�ýӿ�
	public uint								GetTargetGUID( ) { return m_TargetGUID; }
	public void								SetTargetGUID( uint guid ) { m_TargetGUID = guid; }

	public byte								GetRelationType( ) { return m_uRelationType; }
	public void								SetRelationType( byte uRelationType ) { m_uRelationType = uRelationType; }

	public byte								GetGroup( ) { return m_uGroup; }
	public void								SetGroup( byte uGroup ) { m_uGroup = uGroup; }
};
[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class CG_RELATION: ClassCanbeSerialized
{
	public byte								m_Type;			//public enum CG_TYPE

	/*union
	{
		RELATION_GUID					m_RelationGUID;
		REQUEST_RELATION_INFO			m_RequestInfo;
		REQUEST_VIEW_PLAYER				m_ViewPlayer;
		REQUEST_ADD_RELATION			m_AddRelation;
		REQUEST_ADD_RELATION_WITH_GROUP	m_AddRelationWithGroup;
		RELATION_GUID_UCHAR_UCHAR		m_RelationGUIDcharchar;
		REQUEST_DEL_FRIEND				m_DelFriend;
		REQUEST_MODIFY_MOOD				m_ModifyMood;
	};*/
    public  System.Object                   mRelation;
	public void								CleanUp( )
    {
        m_Type = (byte)RELATION_REQUEST_TYPE.REQ_NONE;
    }
    public    int getSize()
    {
        int uSize = sizeof(byte);

	    switch( (RELATION_REQUEST_TYPE)m_Type )
	    {
	    case RELATION_REQUEST_TYPE.REQ_RELATIONLIST:
		    break;
	    case RELATION_REQUEST_TYPE.REQ_RELATIONINFO:
		    uSize += ((REQUEST_RELATION_INFO)(mRelation)).getSize();
		    break;
	    case RELATION_REQUEST_TYPE.REQ_VIEWPLAYER:
	    case RELATION_REQUEST_TYPE.REQ_NOTIFY_ADDTEMPFRIEND:
		    uSize += ((REQUEST_VIEW_PLAYER)mRelation).getSize();
		    break;
	    case RELATION_REQUEST_TYPE.REQ_DELFROMBLACKLIST:
		    uSize += ((RELATION_GUID)mRelation).getSize();
		    break;
	    case RELATION_REQUEST_TYPE.REQ_DELFRIEND:
		    uSize += ( (REQUEST_DEL_FRIEND)mRelation).getSize();
		    break;
	    case RELATION_REQUEST_TYPE.REQ_ADDFRIEND:
	    case RELATION_REQUEST_TYPE.REQ_TEMPFRIEND_TO_FRIEND:
		    uSize +=(( REQUEST_ADD_RELATION_WITH_GROUP)mRelation).getSize();
		    break;
	    case RELATION_REQUEST_TYPE.REQ_ADDTOBLACKLIST:
	    case RELATION_REQUEST_TYPE.REQ_TEMPFRIEND_ADDTO_BLACKLIST:
		    uSize += ((REQUEST_ADD_RELATION)mRelation).getSize();
		    break;
	    case RELATION_REQUEST_TYPE.REQ_TRANSITION:
		    uSize += ((RELATION_GUID_UCHAR_UCHAR )mRelation).getSize();
		    break;
	    case RELATION_REQUEST_TYPE.REQ_MODIFYMOOD:
		    uSize += ((REQUEST_MODIFY_MOOD)mRelation).getSize();
		    break;
	    default:
		    return 0;
	    }

	    return uSize;

    }
    public static int getMaxSize()
    {
        int max1 = Math.Max(RELATION_GUID.getMaxSize(), REQUEST_RELATION_INFO.getMaxSize());
        int max2 = Math.Max(REQUEST_VIEW_PLAYER.getMaxSize(), REQUEST_ADD_RELATION.getMaxSize());
        int max3 = Math.Max(REQUEST_ADD_RELATION_WITH_GROUP.getMaxSize(), RELATION_GUID_UCHAR_UCHAR.getMaxSize());
        int max4 = Math.Max(REQUEST_DEL_FRIEND.getMaxSize(), REQUEST_MODIFY_MOOD.getMaxSize());
        return Math.Max( Math.Max(max1,max2), Math.Max(max3,max4));
    }
    public    bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadByte( ref m_Type);

	    switch( (RELATION_REQUEST_TYPE)m_Type )
	    {
	    case RELATION_REQUEST_TYPE.REQ_RELATIONLIST:
		    break;
	    case RELATION_REQUEST_TYPE.REQ_RELATIONINFO:
            REQUEST_RELATION_INFO m_RequestInfo = new REQUEST_RELATION_INFO();
            mRelation = m_RequestInfo;
		    m_RequestInfo.CleanUp();
		    m_RequestInfo.readFromBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_VIEWPLAYER:
	    case RELATION_REQUEST_TYPE.REQ_NOTIFY_ADDTEMPFRIEND:
            REQUEST_VIEW_PLAYER m_ViewPlayer = new REQUEST_VIEW_PLAYER();
            mRelation = m_ViewPlayer;
		    m_ViewPlayer.CleanUp();
		    m_ViewPlayer.readFromBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_DELFROMBLACKLIST:
            RELATION_GUID m_RelationGUID = new RELATION_GUID();
            mRelation = m_RelationGUID;
            m_RelationGUID.CleanUp();
		    m_RelationGUID.readFromBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_DELFRIEND:
            REQUEST_DEL_FRIEND m_DelFriend = new REQUEST_DEL_FRIEND();
		    m_DelFriend.CleanUp();
		    m_DelFriend.readFromBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_ADDFRIEND:
	    case RELATION_REQUEST_TYPE.REQ_TEMPFRIEND_TO_FRIEND:
            REQUEST_ADD_RELATION_WITH_GROUP m_AddRelationWithGroup = new REQUEST_ADD_RELATION_WITH_GROUP();
		    mRelation = m_AddRelationWithGroup;
            m_AddRelationWithGroup.CleanUp();
		    m_AddRelationWithGroup.readFromBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_ADDTOBLACKLIST:
	    case RELATION_REQUEST_TYPE.REQ_TEMPFRIEND_ADDTO_BLACKLIST:
            REQUEST_ADD_RELATION m_AddRelation = new REQUEST_ADD_RELATION();
            mRelation = m_AddRelation;
		    m_AddRelation.CleanUp();
		    m_AddRelation.readFromBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_TRANSITION:
            RELATION_GUID_UCHAR_UCHAR m_RelationGUIDUCHARUCHAR = new RELATION_GUID_UCHAR_UCHAR();
            mRelation = m_RelationGUIDUCHARUCHAR;
		    m_RelationGUIDUCHARUCHAR.CleanUp();
		    m_RelationGUIDUCHARUCHAR.readFromBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_MODIFYMOOD:
            REQUEST_MODIFY_MOOD m_ModifyMood = new REQUEST_MODIFY_MOOD();
            mRelation = m_ModifyMood;
		    m_ModifyMood.CleanUp();
		    m_ModifyMood.readFromBuff(ref buff);
		    break;
	    default:
		    return false;
	    }
        return true;
    }
    public    int writeToBuff(ref NetOutputBuffer buff)
    {
        buff.WriteByte(m_Type);

	    switch( (RELATION_REQUEST_TYPE)m_Type )
	    {
	    case RELATION_REQUEST_TYPE.REQ_RELATIONLIST:
		    break;
	    case RELATION_REQUEST_TYPE.REQ_RELATIONINFO:
		    ((REQUEST_RELATION_INFO )mRelation).writeToBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_VIEWPLAYER:
	    case RELATION_REQUEST_TYPE.REQ_NOTIFY_ADDTEMPFRIEND:
		    ((REQUEST_VIEW_PLAYER)mRelation) .writeToBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_DELFROMBLACKLIST:
		    ((RELATION_GUID )mRelation).writeToBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_DELFRIEND:
		    ((REQUEST_DEL_FRIEND)mRelation) .writeToBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_ADDFRIEND:
	    case RELATION_REQUEST_TYPE.REQ_TEMPFRIEND_TO_FRIEND:
		    ((REQUEST_ADD_RELATION_WITH_GROUP)mRelation).writeToBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_ADDTOBLACKLIST:
	    case RELATION_REQUEST_TYPE.REQ_TEMPFRIEND_ADDTO_BLACKLIST:
		    ((REQUEST_ADD_RELATION )mRelation).writeToBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_TRANSITION:
		    ((RELATION_GUID_UCHAR_UCHAR )mRelation).writeToBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_MODIFYMOOD:
		    ((REQUEST_MODIFY_MOOD )mRelation).writeToBuff(ref buff);
		    break;
	    default:
		    return 0;
	    }
        return getSize();
    }

};

/////////////////////////////////////////////////////////////////////////////////

// ����Ϊ GUID �� char
[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class RELATION_GUID_UCHAR:ClassCanbeSerialized
{
	//����
	uint								m_TargetGUID;		// ĳ��ҵ� GUID
	byte								m_uRelationType;	// ��ϵ����

	public void								CleanUp( )
    {
        m_TargetGUID = MacroDefine.UINT_MAX;
        m_uRelationType = 0;
    }
    public    int getSize()
    {
        return sizeof(uint) + sizeof(byte);
    }
    public static int getMaxSize()
    {
        return sizeof(uint) + sizeof(byte);
    }
    public    bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadUint(ref m_TargetGUID);
        buff.ReadByte(ref m_uRelationType);
        return true;
    }
    public    int writeToBuff(ref NetOutputBuffer buff)
    {
        buff.WriteUint(m_TargetGUID);
        buff.WriteByte(m_uRelationType);
        return getSize();
    }

	//����Ӧ�ýӿ�
	uint								GetTargetGUID( ) { return m_TargetGUID; }
	void								SetTargetGUID( uint guid ) { m_TargetGUID = guid; }

	byte								GetRelationType( ) { return m_uRelationType; }
	void								SetRelationType( byte uRelationType ) { m_uRelationType = uRelationType; }
};

// �޸���ϵ������
[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class REQUEST_MODIFY_SETTINGS:ClassCanbeSerialized
{
	//����
	ushort								m_uSettings;	// ��ϵ����

	//�����ӿ�
	public void								CleanUp( )
    {
        m_uSettings = 0;
    }
     public static int getMaxSize()
     {
        return sizeof(ushort);
     }
    public    int getSize()
    {
        return sizeof(ushort);
    }
    public    bool readFromBuff(ref NetInputBuffer buff)
    {

        return true;
    }
    public    int writeToBuff(ref NetOutputBuffer buff)
    {
        return getSize();
    }

	//����Ӧ�ýӿ�
	ushort								GetSettings( ) { return m_uSettings; }
	void								SetSettings( ushort uSettings ) { m_uSettings = uSettings; }
};

//////////////////////////////////////////////////////////////////////////
[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class GW_RELATION:ClassCanbeSerialized
{
	byte								m_Type;
	uint								m_GUID;		// �Լ��� GUID

	/*union
	{
		RELATION_GUID					m_RelationGUID;
		REQUEST_RELATION_INFO			m_RequestInfo;
		REQUEST_VIEW_PLAYER				m_ViewPlayer;
		REQUEST_ADD_RELATION_WITH_GROUP	m_AddRelationWithGroup;
		REQUEST_ADD_RELATION			m_AddRelation;
		RELATION_GUID_UCHAR				m_TransitionRelation;
		REQUEST_MODIFY_SETTINGS			m_Settings;
		REQUEST_MODIFY_MOOD				m_ModifyMood;
	};*/
     System.Object  mRelation;

	public void								CleanUp( )
    {
        m_Type = (byte)RELATION_REQUEST_TYPE.REQ_NONE;
        m_GUID = MacroDefine.UINT_MAX;
    }
    public    int getSize()
    {
        int uSize = sizeof(byte) + sizeof(uint);

	    switch( (RELATION_REQUEST_TYPE)m_Type )
	    {
	    case RELATION_REQUEST_TYPE.REQ_RELATIONINFO:
		    uSize += ((REQUEST_RELATION_INFO )mRelation).getSize();
		    break;
	    case RELATION_REQUEST_TYPE.REQ_VIEWPLAYER:
	    case RELATION_REQUEST_TYPE.REQ_NOTIFY_ADDTEMPFRIEND:
		    uSize += ((REQUEST_VIEW_PLAYER  )mRelation).getSize();
		    break;
	    case RELATION_REQUEST_TYPE.REQ_DELFRIEND:
	    case RELATION_REQUEST_TYPE.REQ_DELFROMBLACKLIST:
	    case RELATION_REQUEST_TYPE.REQ_NEWGOODFRIEND:
		    uSize += ((RELATION_GUID)mRelation).getSize();
		    break;
	    case RELATION_REQUEST_TYPE.REQ_ADDFRIEND:
	    case RELATION_REQUEST_TYPE.REQ_TEMPFRIEND_TO_FRIEND:
		    uSize += ((REQUEST_ADD_RELATION_WITH_GROUP)mRelation).getSize();
		    break;
	    case RELATION_REQUEST_TYPE.REQ_ADDTOBLACKLIST:
	    case RELATION_REQUEST_TYPE.REQ_TEMPFRIEND_ADDTO_BLACKLIST:
		    uSize += ((REQUEST_ADD_RELATION  )mRelation).getSize();
		    break;
	    case RELATION_REQUEST_TYPE.REQ_TRANSITION:
		    uSize += ((RELATION_GUID_UCHAR  )mRelation).getSize();
		    break;
	    case RELATION_REQUEST_TYPE.REQ_RELATIONONLINE:
		    break;
	    case RELATION_REQUEST_TYPE.REQ_MODIFYMOOD:
		    uSize += ((REQUEST_MODIFY_MOOD  )mRelation).getSize();
		    break;
	    case RELATION_REQUEST_TYPE.REQ_MODIFYSETTINGS:
		    uSize += ((REQUEST_MODIFY_SETTINGS  )mRelation).getSize();
		    break;
	    default:
		    return 0;
	    }

	    return uSize;
    }
    public static int getMaxSize()
    {
        int max1 = Math.Max(RELATION_GUID.getMaxSize(), REQUEST_RELATION_INFO.getMaxSize());
        int max2 = Math.Max(REQUEST_VIEW_PLAYER.getMaxSize(), REQUEST_ADD_RELATION_WITH_GROUP.getMaxSize());
        int max3 = Math.Max(REQUEST_ADD_RELATION.getMaxSize(), RELATION_GUID_UCHAR.getMaxSize());
        int max4 = Math.Max(REQUEST_MODIFY_SETTINGS.getMaxSize(), REQUEST_MODIFY_MOOD.getMaxSize());
        return Math.Max(Math.Max(max1,max2), Math.Max(max3,max4));
    }
    public    bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadByte( ref m_Type);
	    buff.ReadUint( ref m_GUID );

	    switch( (RELATION_REQUEST_TYPE)m_Type )
	    {
	    case RELATION_REQUEST_TYPE.REQ_RELATIONINFO:
            mRelation = new REQUEST_RELATION_INFO();
		    ((REQUEST_RELATION_INFO )mRelation).CleanUp();
		    ((REQUEST_RELATION_INFO )mRelation).readFromBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_VIEWPLAYER:
	    case RELATION_REQUEST_TYPE.REQ_NOTIFY_ADDTEMPFRIEND:
            mRelation = new REQUEST_VIEW_PLAYER(); 
		    ((REQUEST_VIEW_PLAYER  )mRelation).CleanUp();
		    ((REQUEST_VIEW_PLAYER  )mRelation).readFromBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_DELFRIEND:
	    case RELATION_REQUEST_TYPE.REQ_DELFROMBLACKLIST:
	    case RELATION_REQUEST_TYPE.REQ_NEWGOODFRIEND:
            mRelation = new RELATION_GUID();
		   ((RELATION_GUID)mRelation).CleanUp();
		    ((RELATION_GUID)mRelation).readFromBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_ADDFRIEND:
	    case RELATION_REQUEST_TYPE.REQ_TEMPFRIEND_TO_FRIEND:
            mRelation = new REQUEST_ADD_RELATION_WITH_GROUP();
		    ((REQUEST_ADD_RELATION_WITH_GROUP)mRelation).CleanUp();
		    ((REQUEST_ADD_RELATION_WITH_GROUP)mRelation).readFromBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_ADDTOBLACKLIST:
	    case RELATION_REQUEST_TYPE.REQ_TEMPFRIEND_ADDTO_BLACKLIST:
            mRelation = new REQUEST_ADD_RELATION();
		    ((REQUEST_ADD_RELATION  )mRelation).CleanUp();
		    ((REQUEST_ADD_RELATION  )mRelation).readFromBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_TRANSITION:
             mRelation = new RELATION_GUID_UCHAR();
		    ((RELATION_GUID_UCHAR  )mRelation).CleanUp();
		    ((RELATION_GUID_UCHAR  )mRelation).readFromBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_RELATIONONLINE:
		    break;
	    case RELATION_REQUEST_TYPE.REQ_MODIFYMOOD:
            mRelation = new REQUEST_MODIFY_MOOD();
		     ((REQUEST_MODIFY_MOOD  )mRelation).CleanUp();
		     ((REQUEST_MODIFY_MOOD  )mRelation).readFromBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_MODIFYSETTINGS:
            mRelation = new REQUEST_MODIFY_SETTINGS();
		    ((REQUEST_MODIFY_SETTINGS  )mRelation).CleanUp();
		    ((REQUEST_MODIFY_SETTINGS  )mRelation).readFromBuff(ref buff);
		    break;
	    default:
		    return false;
	    }
        return true;
    }
    public    int writeToBuff(ref NetOutputBuffer buff)
    {
        buff.WriteByte(m_Type);
	    buff.WriteUint(m_GUID);

	    switch( (RELATION_REQUEST_TYPE)m_Type )
	    {
	    case RELATION_REQUEST_TYPE.REQ_RELATIONINFO:
		    ((REQUEST_RELATION_INFO )mRelation).writeToBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_VIEWPLAYER:
	    case RELATION_REQUEST_TYPE.REQ_NOTIFY_ADDTEMPFRIEND:
		     ((REQUEST_VIEW_PLAYER  )mRelation).writeToBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_DELFRIEND:
	    case RELATION_REQUEST_TYPE.REQ_DELFROMBLACKLIST:
	    case RELATION_REQUEST_TYPE.REQ_NEWGOODFRIEND:
		    ((RELATION_GUID)mRelation).writeToBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_ADDFRIEND:
	    case RELATION_REQUEST_TYPE.REQ_TEMPFRIEND_TO_FRIEND:
		     ((REQUEST_ADD_RELATION_WITH_GROUP)mRelation).writeToBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_ADDTOBLACKLIST:
	    case RELATION_REQUEST_TYPE.REQ_TEMPFRIEND_ADDTO_BLACKLIST:
		   ((REQUEST_ADD_RELATION  )mRelation).writeToBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_TRANSITION:
		    ((RELATION_GUID_UCHAR  )mRelation).writeToBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_RELATIONONLINE:
		    break;
	    case RELATION_REQUEST_TYPE.REQ_MODIFYMOOD:
		    ((REQUEST_MODIFY_MOOD  )mRelation).writeToBuff(ref buff);
		    break;
	    case RELATION_REQUEST_TYPE.REQ_MODIFYSETTINGS:
		    ((REQUEST_MODIFY_SETTINGS  )mRelation).writeToBuff(ref buff);
		    break;
	    default:
		    return 0;
	    }
        return getSize();
    }

	//����Ӧ�ýӿ�
	uint								GetGUID( ) { return m_GUID; }
	void								SetGUID( uint guid ) { m_GUID = guid; }
};

/////////////////////////////////////////////////////////////////////////////////
[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class RETURN_ADD_RELATION:ClassCanbeSerialized
{
	byte								m_uRelationType;	// ��ϵ����
	byte								m_uGroup;			// ��

	_RELATION							m_Relation = new _RELATION();			// ��ϵ����ϸ��Ϣ���������ɹ���Я��

	public void								CleanUp( )
    {
        m_uRelationType = 0;
        m_uGroup = 0;
        m_Relation.CleanUp();
    }
    public    int getSize()
    {
        return 2*sizeof(byte) + m_Relation.getSize();
    }
    public static int getMaxSize()
    {
        return 2 * sizeof(byte) + _RELATION.getMaxSize();
    }
    public    bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadByte(ref m_uRelationType);
        buff.ReadByte(ref m_uGroup);
        m_Relation.readFromBuff(ref buff);
        return true;
    }
    public    int writeToBuff(ref NetOutputBuffer buff)
    {
        buff.WriteByte(m_uRelationType);
        buff.WriteByte(m_uGroup);
        m_Relation.writeToBuff(ref buff);
        return getSize();
    }

	//����Ӧ�ýӿ�
    public byte GetRelationType() { return m_uRelationType; }
    public void SetRelationType(byte uRelationType) { m_uRelationType = uRelationType; }

    public byte GetGroup() { return m_uGroup; }
    public void SetGroup(byte uGroup) { m_uGroup = uGroup; }

    public _RELATION GetRelationData() { return m_Relation; }
    public void SetRelationData(_RELATION pRelationData)
	{
        _RELATION.copy(pRelationData, m_Relation);
	}
};
[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class RETURN_RELATION_INFO:ClassCanbeSerialized
{
	byte								m_uRelationType;	// ��ϵ����
	byte								m_uGroup;			// ��
	int									m_nFriendpoint;		// ��ϵֵ

	_RELATION							m_Relation = new _RELATION();			// ��ϵ����ϸ��Ϣ

	public void								CleanUp( )
    {
        m_uRelationType = 0;
        m_uGroup = 0;
        m_nFriendpoint = 0;
        m_Relation.CleanUp();
    }
    public    int getSize()
    {
	    int uSize = sizeof(byte) + m_Relation.getSize();

	    switch( (RELATION_TYPE)m_uRelationType )
	    {
	        case RELATION_TYPE.RELATION_TYPE_BLACKNAME:
                break;
	        case RELATION_TYPE.RELATION_TYPE_TEMPFRIEND:
		        break;
	        default:
		        uSize += sizeof(byte) + sizeof(int);
                break;
	    }

	    return uSize;
    }
    public static int getMaxSize()
    {
        int uSize = sizeof(byte) + _RELATION.getMaxSize() + sizeof(byte) + sizeof(int);
        return uSize;
    }
    public    bool readFromBuff(ref NetInputBuffer buff)
    {
 	    buff.ReadByte( ref m_uRelationType );

	    switch( (RELATION_TYPE)m_uRelationType )
	    {
	        case RELATION_TYPE.RELATION_TYPE_BLACKNAME:
	        case RELATION_TYPE.RELATION_TYPE_TEMPFRIEND:
		        break;
	        default:
		        buff.ReadByte( ref m_uGroup);
		        buff.ReadInt( ref m_nFriendpoint);
                break;
	    }

	    m_Relation.readFromBuff( ref buff );
        return true;
    }
    public    int writeToBuff(ref NetOutputBuffer buff)
    {
  	    buff.WriteByte( m_uRelationType );

	    switch( (RELATION_TYPE)m_uRelationType )
	    {
	    case RELATION_TYPE.RELATION_TYPE_BLACKNAME:
	    case RELATION_TYPE.RELATION_TYPE_TEMPFRIEND:
		    break;
	    default:
		    buff.WriteByte(m_uGroup );
		    buff.WriteInt( m_nFriendpoint );
                break;
	    }

	    m_Relation.writeToBuff( ref buff );
        return getSize();
    }

	//����Ӧ�ýӿ�
	public byte								GetRelationType( ) { return m_uRelationType; }
    public void SetRelationType(byte uRelationType) { m_uRelationType = uRelationType; }

    public byte GetGroup() { return m_uGroup; }
    public void SetGroup(byte uGroup) { m_uGroup = uGroup; }

    public int GetFriendPoint() { return m_nFriendpoint; }
    public void SetFriendPoint(int nFp) { m_nFriendpoint = nFp; }

    public _RELATION GetRelationData() { return m_Relation; }
    public void SetRelationData(_RELATION pRelationData)
	{
		_RELATION.copy(pRelationData, m_Relation);
	}
};

// �Ҽ��鿴�����Ϣ
[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class RETURN_VIEW_PLAYER:ClassCanbeSerialized
{
	uint								m_GUID;								// GUID
	byte								m_uNameSize;
	byte[]								m_szName = new byte[GAMEDEFINE.MAX_CHARACTER_NAME];		// 1.����
	//char								m_uTitleSize;
	//char								m_szTitle[MAX_charACTER_TITLE];		// 2.�ƺ�
	//int									m_nLevel;							// 3.��ɫ�ȼ�
	//int									m_nMenPai;							// 4.���� MENPAI_ATTRIBUTE
	//char								m_uGuildNameSize;
	//char								m_szGuildName[MAX_GUILD_NAME_SIZE];	// 5.������ƣ����� Server �����ͻ��ˣ�
	//char								m_uSpouseNameSize;
	//char								m_szSpouseName[MAX_charACTER_NAME];	// 6.��ż����
	//int									m_nModelID;							// 7.ģ��
	//uint								m_uHairColor;						// 8.ͷ����ɫ

	//uint								m_WeaponID;							// 9.����
	//uint								m_CapID;							// 10.ñ��
	//uint								m_ArmourID;							// 11.�·�
	//uint								m_CuffID;							// 12.����
	//uint								m_FootID;							// 13.ѥ��

	public void								CleanUp( )
    {
        m_GUID = MacroDefine.UINT_MAX;
	    m_uNameSize = 0;
	    for(int i=0; i<m_szName.Length; ++i)
        {
            m_szName[i] = 0;
        }
    }
    public    int getSize()
    {
        return sizeof(uint) + sizeof(byte) + sizeof(byte)*m_uNameSize;
    }
     public static int getMaxSize()
     {
        return sizeof(uint) + sizeof(byte) + sizeof(byte)*GAMEDEFINE.MAX_CHARACTER_NAME;
     }
    public    bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadUint(ref m_GUID);
        buff.ReadByte(ref m_uNameSize);
        if(m_uNameSize > 0 && m_uNameSize<GAMEDEFINE.MAX_CHARACTER_NAME)
        {
            buff.Read(ref m_szName, m_uNameSize);
        }
        return true;
    }
    public    int writeToBuff(ref NetOutputBuffer buff)
    {
        buff.WriteUint(m_GUID);
        buff.WriteByte(m_uNameSize);
        if(m_uNameSize > 0 && m_uNameSize<GAMEDEFINE.MAX_CHARACTER_NAME)
        {
            buff.Write(ref m_szName, m_uNameSize);
        }
        
        return getSize();
    }

	//����Ӧ�ýӿ�
	void								FillData(   RETURN_VIEW_PLAYER pViewPlayer )
	{
		m_GUID = pViewPlayer.m_GUID;
        m_uNameSize = pViewPlayer.m_uNameSize;
        Array.Copy(pViewPlayer.m_szName, m_szName, pViewPlayer.m_szName.Length);
	}

	uint								GetGUID( ) { return m_GUID; }
	void								SetGUID( uint guid ) { m_GUID = guid; }

	byte[]							GetName( ) { return m_szName; }
	void								SetName(   byte[] pName )
	{
        Array.Copy(pName, m_szName, pName.Length);
		m_uNameSize = (byte)GameUtil.Utility.getStringLength(m_szName);
	}
};

[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class RETURN_NOTIFY_FRIEND:ClassCanbeSerialized
{
	uint								m_GUID;								// GUID
	byte								m_uNameSize;
	byte[]								m_szName = new byte [GAMEDEFINE.MAX_CHARACTER_NAME];		// ����

	public void								CleanUp( )
    {
        m_GUID = MacroDefine.UINT_MAX;
	    m_uNameSize = 0;
	    for(int i=0; i<m_szName.Length; ++i)
        {
            m_szName[i] = 0;
        }
    }
    public    int getSize()
    {
        return sizeof(uint) + sizeof(byte) + sizeof(byte)*m_uNameSize;
    }
     public static int getMaxSize()
     {
        return sizeof(uint) + sizeof(byte) + sizeof(byte)*GAMEDEFINE.MAX_CHARACTER_NAME;
     }
    public    bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadUint(ref m_GUID);
        buff.ReadByte(ref m_uNameSize);
        if(m_uNameSize > 0 && m_uNameSize<GAMEDEFINE.MAX_CHARACTER_NAME)
        {
            buff.Read(ref m_szName, m_uNameSize);
        }
        return true;
    }
    public    int writeToBuff(ref NetOutputBuffer buff)
    {
        buff.WriteUint(m_GUID);
        buff.WriteByte(m_uNameSize);
        if(m_uNameSize > 0 && m_uNameSize<GAMEDEFINE.MAX_CHARACTER_NAME)
        {
            buff.Write(ref m_szName, m_uNameSize);
        }
        
        return getSize();
    }

	//����Ӧ�ýӿ�
	uint								GetGUID( ) { return m_GUID; }
	void								SetGUID( uint guid ) { m_GUID = guid; }

	byte[]							GetName( ) { return m_szName; }
	void								SetName(   byte[] pName )
	{
        Array.Copy(pName, m_szName, pName.Length);
		m_uNameSize = (byte)GameUtil.Utility.getStringLength(m_szName);
	}
};

[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class _RELATION_ONLINE:ClassCanbeSerialized
{
	uint								m_GUID;								// ���ߵ����ܺ���
	byte								m_uMoodSize;						// ���鳤��
	byte[]								m_szMood = new byte[GAMEDEFINE.MOOD_DATA_SIZE];			// ����

	public void								CleanUp( )
    {
        m_GUID = MacroDefine.UINT_MAX;
	    m_uMoodSize = 0;
	    for(int i=0; i<m_szMood.Length; ++i)
        {
            m_szMood[i] = 0;
        }
    }
    public    int getSize()
    {
        return sizeof(uint) + sizeof(byte) + sizeof(byte)*m_uMoodSize;
    }

    public static int getMaxSize()
    {
        return sizeof(uint) + sizeof(byte) + sizeof(byte)*GAMEDEFINE.MOOD_DATA_SIZE;
    }
    public    bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadUint(ref m_GUID);
        buff.ReadByte(ref m_uMoodSize);
        if(m_uMoodSize > 0 && m_uMoodSize<GAMEDEFINE.MAX_CHARACTER_NAME)
        {
            buff.Read(ref m_szMood, m_uMoodSize);
        }
        return true;
    }
    public    int writeToBuff(ref NetOutputBuffer buff)
    {
        buff.WriteUint(m_GUID);
        buff.WriteByte(m_uMoodSize);
        if(m_uMoodSize > 0 && m_uMoodSize<GAMEDEFINE.MAX_CHARACTER_NAME)
        {
            buff.Write(ref m_szMood, m_uMoodSize);
        }
        
        return getSize();
    }

	//����Ӧ�ýӿ�
	public uint								GetGUID( ) { return m_GUID; }
    public void SetGUID(uint guid) { m_GUID = guid; }

    public byte[] GetMood() { return m_szMood; }
    public void SetMood(byte[] pMood)
	{
        Array.Copy(pMood, m_szMood, pMood.Length);
		m_uMoodSize = (byte)GameUtil.Utility.getStringLength(m_szMood);
	}
};

// ���ڸ�������������б�
[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class RETURN_ONLINE_LIST:ClassCanbeSerialized
{
	byte								m_uOnlineCount;						// ��������
	_RELATION_ONLINE[]	m_OnlineInfo = new _RELATION_ONLINE[(int)(RELATION_DEFINE.RELATION_BLACKNAME_OFFSET-RELATION_DEFINE.RELATION_FRIEND_OFFSET)];

	public void								CleanUp( )
    {
        m_uOnlineCount = 0;

	    for( int i=0; i<RELATION_DEFINE.RELATION_BLACKNAME_OFFSET-RELATION_DEFINE.RELATION_FRIEND_OFFSET; ++i )
	    {
            if(m_OnlineInfo[i] != null)
		        m_OnlineInfo[i].CleanUp();
	    }
    }
    public    int getSize()
    {
        int uSize = sizeof(byte);

	    if( m_uOnlineCount > 0 && m_uOnlineCount < RELATION_DEFINE.RELATION_BLACKNAME_OFFSET-RELATION_DEFINE.RELATION_FRIEND_OFFSET )
	    {
		    for( int i=0; i<m_uOnlineCount; ++i )
		    {
			    uSize += m_OnlineInfo[i].getSize();
		    }
	    }

	    return uSize;
    }
    public static int getMaxSize()
    {
        int uSize = sizeof(byte) + (RELATION_DEFINE.RELATION_BLACKNAME_OFFSET-RELATION_DEFINE.RELATION_FRIEND_OFFSET)*_RELATION_ONLINE.getMaxSize();
        return uSize;
    }
    public    bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadByte( ref m_uOnlineCount);

	    if( m_uOnlineCount > 0 && m_uOnlineCount < RELATION_DEFINE.RELATION_BLACKNAME_OFFSET-RELATION_DEFINE.RELATION_FRIEND_OFFSET )
	    {
		    for( int i=0; i<m_uOnlineCount; ++i )
		    {
			    m_OnlineInfo[i].readFromBuff(ref buff);
		    }
	    }
        return true;
    }
    public    int writeToBuff(ref NetOutputBuffer buff)
    {
        buff.WriteByte( m_uOnlineCount );
        if( m_uOnlineCount > 0 && m_uOnlineCount < RELATION_DEFINE.RELATION_BLACKNAME_OFFSET-RELATION_DEFINE.RELATION_FRIEND_OFFSET )
	    {
		    for( int i=0; i<m_uOnlineCount; ++i )
		    {
			    m_OnlineInfo[i].writeToBuff(ref buff);
		    }
	    }
        return getSize();
    }

	//����Ӧ�ýӿ�
	public byte								GetOnlineCount() { return m_uOnlineCount; }

	public _RELATION_ONLINE				GetOnlineRelation( byte uIndex )
	{
		if( uIndex >= m_uOnlineCount )
		{
			return null;
		}

		return m_OnlineInfo[uIndex];
	}
    public void AddOnlineRelation(_RELATION_ONLINE pOnlineInfo)
	{
		if( m_uOnlineCount >= RELATION_DEFINE.RELATION_BLACKNAME_OFFSET-RELATION_DEFINE.RELATION_FRIEND_OFFSET )
		{
			return;
		}

		m_OnlineInfo[m_uOnlineCount++] = pOnlineInfo;
	}
};

// ����֪ͨ����������
[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class RETURN_NOTIFY_ONLINE:ClassCanbeSerialized
{
	byte								m_uNameSize;
	byte[]		m_szTargetName = new byte[GAMEDEFINE.MAX_CHARACTER_NAME];
	byte								m_uMoodSize;						// ���鳤��
	byte[]		m_szMood = new byte[GAMEDEFINE.MOOD_DATA_SIZE];			// ����

	public void								CleanUp( )
    {
        m_uNameSize = 0;
        for(int i=0; i<m_szTargetName.Length; ++i)
        {
            m_szTargetName[i] = 0;
        }
        m_uMoodSize = 0;
        for(int i=0; i<m_szMood.Length ;++i)
        {
            m_szMood[i] = 0;
        }
    }
    public    int getSize()
    {
        return   sizeof(byte) + sizeof(byte)*m_uNameSize +
                 sizeof(byte) + sizeof(byte)*m_uMoodSize;
    }
    public static int getMaxSize()
    {
         return  sizeof(byte) + sizeof(byte)*GAMEDEFINE.MAX_CHARACTER_NAME +
                 sizeof(byte) + sizeof(byte)*GAMEDEFINE.MOOD_DATA_SIZE;
    }
    public    bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadByte( ref m_uNameSize);

	    if( m_uNameSize > 0 )
	    {
		    buff.Read( ref m_szTargetName, m_uNameSize );
	    }

	    buff.ReadByte( ref m_uMoodSize);

	    if( m_uMoodSize > 0 )
	    {
		    buff.Read( ref m_szMood, m_uMoodSize );
	    }
        return true;
    }
    public    int writeToBuff(ref NetOutputBuffer buff)
    {
        buff.WriteByte(m_uNameSize );

	    if( m_uNameSize > 0 )
	    {
		    buff.Write( ref m_szTargetName, m_uNameSize );
	    }

	    buff.WriteByte( m_uMoodSize );

	    if( m_uMoodSize > 0 )
	    {
		    buff.Write( ref m_szMood, m_uMoodSize );
	    }
        return getSize();
    }

	//����Ӧ�ýӿ�
    public byte[] GetTargetName() { return m_szTargetName; }
    public void SetTargetName(byte[] pName)
	{
		Array.Copy(pName, m_szTargetName, GAMEDEFINE.MAX_CHARACTER_NAME-1 );
		m_uNameSize = (byte)GameUtil.Utility.getStringLength(m_szTargetName);
	}

	public byte[]							GetMood( ) { return m_szMood; }
    public void								SetMood(   byte[] pMood )
	{
		Array.Copy(pMood, m_szMood , GAMEDEFINE.MOOD_DATA_SIZE-1 );
		m_uMoodSize = (byte)GameUtil.Utility.getStringLength(m_szMood);
	}
};


[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class WG_RELATION:ClassCanbeSerialized
{
	byte								m_Type;
	short							m_PlayerID;			// ���˵� PlayerID

	/*union
	{
		RETURN_ADD_RELATION				m_AddRelation;
		RETURN_RELATION_INFO			m_RelationInfo;
		RETURN_VIEW_PLAYER				m_ViewPlayer;
		RELATION_GUID					m_RelationGUID;
		RETURN_NOTIFY_FRIEND			m_NotifyFriend;
		RETURN_ONLINE_LIST				m_RelationOnline;
		RETURN_NOTIFY_ONLINE			m_NotifyOnline;
		REQUEST_VIEW_PLAYER				m_PlayerName;
	};*/
    System.Object mRelation;
	public void								CleanUp( )
    {
        m_PlayerID = MacroDefine.INVALID_ID;
	    m_Type =  (byte)RELATION_RETURN_TYPE.RET_NONE;
    }
    public    int getSize()
    {
        int uSize = sizeof(byte) + sizeof(short);

	    switch( (RELATION_RETURN_TYPE)m_Type )
	    {
	        case RELATION_RETURN_TYPE.RET_RELATIONINFO:
                RETURN_RELATION_INFO m_RelationInfo = new RETURN_RELATION_INFO();
                mRelation = m_RelationInfo;
		        uSize += m_RelationInfo.getSize();
		        break;
	        case RELATION_RETURN_TYPE.RET_VIEWPLAYER:
                RETURN_VIEW_PLAYER m_ViewPlayer = new RETURN_VIEW_PLAYER();
                mRelation = m_ViewPlayer;
		        uSize += m_ViewPlayer.getSize();
		        break;
	        case RELATION_RETURN_TYPE.RET_TARGETNOTONLINE:
	        case RELATION_RETURN_TYPE.RET_RELATIONOFFLINE:
                RELATION_GUID  m_RelationGUID = new RELATION_GUID();
                mRelation = m_RelationGUID;
		        uSize += m_RelationGUID.getSize();
		        break;
	        case RELATION_RETURN_TYPE.RET_ADDFRIEND:
	        case RELATION_RETURN_TYPE.RET_ADDTOBLACKLIST:
	        case RELATION_RETURN_TYPE.RET_TEMPFRIEND_TO_FRIEND:
	        case RELATION_RETURN_TYPE.RET_TEMPFRIEND_ADDTO_BLACKLIST:
                RETURN_ADD_RELATION m_AddRelation = new RETURN_ADD_RELATION();
                mRelation = m_AddRelation;
		        uSize += m_AddRelation.getSize();
		        break;
	        case RELATION_RETURN_TYPE.RET_ADDFRIENDNOTIFY:
                RETURN_NOTIFY_FRIEND m_NotifyFriend = new RETURN_NOTIFY_FRIEND();
                mRelation = m_NotifyFriend;
		        uSize += m_NotifyFriend.getSize();
		        break;
	        case RELATION_RETURN_TYPE.RET_ONLINELIST:
                RETURN_ONLINE_LIST  m_RelationOnline = new RETURN_ONLINE_LIST();
                mRelation = m_RelationOnline;
		        uSize += m_RelationOnline.getSize();
		        break;
	        case RELATION_RETURN_TYPE.RET_RELATIONONLINE:
                RETURN_NOTIFY_ONLINE m_NotifyOnline = new RETURN_NOTIFY_ONLINE();
                mRelation = m_NotifyOnline;
		        uSize += m_NotifyOnline.getSize();
		        break;
	        case RELATION_RETURN_TYPE.RET_NOTIFY_ADDTEMPFRIEND:
                REQUEST_VIEW_PLAYER  m_PlayerName = new REQUEST_VIEW_PLAYER();
                mRelation =  m_PlayerName;
		        uSize += m_PlayerName.getSize();
		        break;
	        case RELATION_RETURN_TYPE.RET_ERR_TARGETNOTEXIST:
	        case RELATION_RETURN_TYPE.RET_ERR_ISFRIEND:
	        case RELATION_RETURN_TYPE.RET_ERR_ISBLACKNAME:
	        case RELATION_RETURN_TYPE.RET_ERR_SPOUSETOBLACKLIST:
	        case RELATION_RETURN_TYPE.RET_ERR_MASTERTOBLACKLIST:
	        case RELATION_RETURN_TYPE.RET_ERR_PRENTICETOBLACKLIST:
	        case RELATION_RETURN_TYPE.RET_ERR_BROTHERTOBLACKLIST:
	        case RELATION_RETURN_TYPE.RET_ERR_CANNOT_ADDFRIEND:
	        case RELATION_RETURN_TYPE.RET_ERR_CANNOTRECEIVEMAIL:
	        case RELATION_RETURN_TYPE.RET_ERR_NOTRECVSTRANGEMAIL:
		        break;
	        default:
		        return 0;
	        }

	    return uSize;
    }
    public static int getMaxSize()
    {
        int max1 = Math.Max(RETURN_ADD_RELATION.getMaxSize(), RETURN_RELATION_INFO.getMaxSize());
        int max2 = Math.Max(RETURN_VIEW_PLAYER.getMaxSize(), RELATION_GUID.getMaxSize());
        int max3 = Math.Max(RETURN_NOTIFY_FRIEND.getMaxSize(), RETURN_ONLINE_LIST.getMaxSize());
        int max4 = Math.Max(RETURN_NOTIFY_ONLINE.getMaxSize(), REQUEST_VIEW_PLAYER.getMaxSize());
        return Math.Max(Math.Max(max1, max2), Math.Max(max3, max4));
    }
    public    bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadShort( ref m_PlayerID );
	    buff.ReadByte( ref m_Type);

	    switch((RELATION_RETURN_TYPE) m_Type )
	    {
	    case RELATION_RETURN_TYPE.RET_RELATIONINFO:
             mRelation = new RETURN_RELATION_INFO();
		    ((RETURN_RELATION_INFO)mRelation).CleanUp();
		    ((RETURN_RELATION_INFO)mRelation).readFromBuff(ref buff);
		    break;
	    case RELATION_RETURN_TYPE.RET_VIEWPLAYER:
            mRelation = new RETURN_VIEW_PLAYER();
		    ((RETURN_VIEW_PLAYER)mRelation).CleanUp();
		    ((RETURN_VIEW_PLAYER)mRelation).readFromBuff(ref buff);
		    break;
	    case RELATION_RETURN_TYPE.RET_TARGETNOTONLINE:
	    case RELATION_RETURN_TYPE.RET_RELATIONOFFLINE:
            mRelation = new RELATION_GUID();
		    ((RELATION_GUID)mRelation).CleanUp();
		    ((RELATION_GUID)mRelation).readFromBuff(ref buff);
		    break;
	    case RELATION_RETURN_TYPE.RET_ADDFRIEND:
	    case RELATION_RETURN_TYPE.RET_ADDTOBLACKLIST:
	    case RELATION_RETURN_TYPE.RET_TEMPFRIEND_TO_FRIEND:
	    case RELATION_RETURN_TYPE.RET_TEMPFRIEND_ADDTO_BLACKLIST:
            mRelation = new RETURN_ADD_RELATION();
		   ((RETURN_ADD_RELATION)mRelation).CleanUp();
		    ((RETURN_ADD_RELATION)mRelation).readFromBuff(ref buff);
		    break;
	    case RELATION_RETURN_TYPE.RET_ADDFRIENDNOTIFY:
            mRelation = new RETURN_NOTIFY_FRIEND();
		    ((RETURN_NOTIFY_FRIEND )mRelation).CleanUp();
		   ((RETURN_NOTIFY_FRIEND )mRelation).readFromBuff(ref buff);
		    break;
	    case RELATION_RETURN_TYPE.RET_ONLINELIST:
            mRelation = new RETURN_ONLINE_LIST();
		    ((RETURN_ONLINE_LIST  )mRelation).CleanUp();
		    ((RETURN_ONLINE_LIST  )mRelation).readFromBuff(ref buff);
		    break;
	    case RELATION_RETURN_TYPE.RET_RELATIONONLINE:
            mRelation = new RETURN_NOTIFY_ONLINE();
		    ((RETURN_NOTIFY_ONLINE)mRelation).CleanUp();
		    ((RETURN_NOTIFY_ONLINE)mRelation).readFromBuff(ref buff);
		    break;
	    case RELATION_RETURN_TYPE.RET_NOTIFY_ADDTEMPFRIEND:
            mRelation = new REQUEST_VIEW_PLAYER();
		    ((REQUEST_VIEW_PLAYER )mRelation).CleanUp();
		    ((REQUEST_VIEW_PLAYER )mRelation).readFromBuff(ref buff);
		    break;
	    case RELATION_RETURN_TYPE.RET_ERR_TARGETNOTEXIST:
	    case RELATION_RETURN_TYPE.RET_ERR_ISFRIEND:
	    case RELATION_RETURN_TYPE.RET_ERR_ISBLACKNAME:
	    case RELATION_RETURN_TYPE.RET_ERR_SPOUSETOBLACKLIST:
	    case RELATION_RETURN_TYPE.RET_ERR_MASTERTOBLACKLIST:
	    case RELATION_RETURN_TYPE.RET_ERR_PRENTICETOBLACKLIST:
	    case RELATION_RETURN_TYPE.RET_ERR_BROTHERTOBLACKLIST:
	    case RELATION_RETURN_TYPE.RET_ERR_CANNOT_ADDFRIEND:
	    case RELATION_RETURN_TYPE.RET_ERR_CANNOTRECEIVEMAIL:
	    case RELATION_RETURN_TYPE.RET_ERR_NOTRECVSTRANGEMAIL:
		    break;
	    default:
		    return false;
	    }
        return true;
    }
    public    int writeToBuff(ref NetOutputBuffer buff)
    {
        buff.WriteShort( m_PlayerID );
	    buff.WriteByte( m_Type );

	    switch( (RELATION_RETURN_TYPE)m_Type )
	    {
	    case RELATION_RETURN_TYPE.RET_RELATIONINFO:
		    ( (RETURN_RELATION_INFO) mRelation).writeToBuff( ref buff );
		    break;
	    case RELATION_RETURN_TYPE.RET_VIEWPLAYER:
		    ((RETURN_VIEW_PLAYER )mRelation).writeToBuff( ref buff );
		    break;
	    case RELATION_RETURN_TYPE.RET_TARGETNOTONLINE:
	    case RELATION_RETURN_TYPE.RET_RELATIONOFFLINE:
		    ((RELATION_GUID )mRelation).writeToBuff( ref buff );
		    break;
	    case RELATION_RETURN_TYPE.RET_ADDFRIEND:
	    case RELATION_RETURN_TYPE.RET_ADDTOBLACKLIST:
	    case RELATION_RETURN_TYPE.RET_TEMPFRIEND_TO_FRIEND:
	    case RELATION_RETURN_TYPE.RET_TEMPFRIEND_ADDTO_BLACKLIST:
		    ((RETURN_ADD_RELATION )mRelation).writeToBuff( ref buff );
		    break;
	    case RELATION_RETURN_TYPE.RET_ADDFRIENDNOTIFY:
		    ((RETURN_NOTIFY_FRIEND )mRelation).writeToBuff( ref buff );
		    break;
	    case RELATION_RETURN_TYPE.RET_ONLINELIST:
		    ((RETURN_ONLINE_LIST )mRelation).writeToBuff( ref buff );
		    break;
	    case RELATION_RETURN_TYPE.RET_RELATIONONLINE:
		    ((RETURN_NOTIFY_ONLINE )mRelation).writeToBuff( ref buff );
		    break;
	    case RELATION_RETURN_TYPE.RET_NOTIFY_ADDTEMPFRIEND:
		    ((REQUEST_VIEW_PLAYER )mRelation).writeToBuff( ref buff );
		    break;
	    case RELATION_RETURN_TYPE.RET_ERR_TARGETNOTEXIST:
	    case RELATION_RETURN_TYPE.RET_ERR_ISFRIEND:
	    case RELATION_RETURN_TYPE.RET_ERR_ISBLACKNAME:
	    case RELATION_RETURN_TYPE.RET_ERR_SPOUSETOBLACKLIST:
	    case RELATION_RETURN_TYPE.RET_ERR_MASTERTOBLACKLIST:
	    case RELATION_RETURN_TYPE.RET_ERR_PRENTICETOBLACKLIST:
	    case RELATION_RETURN_TYPE.RET_ERR_BROTHERTOBLACKLIST:
	    case RELATION_RETURN_TYPE.RET_ERR_CANNOT_ADDFRIEND:
	    case RELATION_RETURN_TYPE.RET_ERR_CANNOTRECEIVEMAIL:
	    case RELATION_RETURN_TYPE.RET_ERR_NOTRECVSTRANGEMAIL:
		    break;
	    default:
		    return 0;
	    }
        return getSize();
    }

	//����Ӧ�ýӿ�
	short							GetPlayerID( ) { return m_PlayerID; }
	void								SetPlayerID( short pid ) { m_PlayerID = pid; }
};

/////////////////////////////////////////////////////////////////////////////////
[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class _FRIEND_INFO:ClassCanbeSerialized
{
	public uint								m_GUID;				// GUID
	public byte								m_uNameSize;
	public byte[]		                    m_szTargetName = new byte[GAMEDEFINE.MAX_CHARACTER_NAME];
	public byte								m_uRelationType;	// ��ϵ����
	public byte								m_uGroup;			// ��
	public int									m_nFriendpoint;		// ��ϵֵ
	public void								CleanUp()
	{
		m_GUID							= MacroDefine.UINT_MAX;
		m_uNameSize						= 0;
		for(int i=0; i<m_szTargetName.Length; ++i)
        {
            m_szTargetName[i] = 0;
        }
		m_uGroup						= 0;
		m_nFriendpoint					= 0;
	}
    public    int getSize()
    {
        int							uSize;
		uSize							= sizeof(uint) + sizeof(byte) + m_uNameSize*sizeof(byte)
                						+ sizeof(byte) + sizeof(byte) + sizeof(int);
		return uSize;
    }
    public static int getMaxSize()
    {
        int							uSize;
		uSize							= sizeof(uint) + sizeof(byte) + GAMEDEFINE.MAX_CHARACTER_NAME*sizeof(byte)
                						+ sizeof(byte) + sizeof(byte) + sizeof(int);
		return uSize;

    }
    public    bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadUint( ref m_GUID );
	    buff.ReadByte( ref m_uNameSize );

	    if( m_uNameSize > 0 && m_uNameSize < (m_szTargetName.Length) )
	    {
		    buff.Read(ref m_szTargetName, m_uNameSize);
	    }

	    buff.ReadByte( ref m_uRelationType );
	    buff.ReadByte( ref m_uGroup);
	    buff.ReadInt( ref m_nFriendpoint );
        return true;
    }
    public    int writeToBuff(ref NetOutputBuffer buff)
    {
        buff.WriteUint(m_GUID);
	    buff.WriteByte( m_uNameSize );

	    if( m_uNameSize > 0 && m_uNameSize < (m_szTargetName.Length) )
	    {
		    buff.Write(ref m_szTargetName, m_uNameSize);
	    }

	    buff.WriteByte( m_uRelationType );
	    buff.WriteByte( m_uGroup );
	    buff.WriteInt(m_nFriendpoint);
        return getSize();
    }
};


[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class _BLACKNAME_INFO:ClassCanbeSerialized
{
	public uint								m_GUID;				// GUID
    public byte m_uNameSize;
    public byte[] m_szTargetName = new byte[GAMEDEFINE.MAX_CHARACTER_NAME];
	public void								CleanUp()
	{
		m_GUID							= MacroDefine.UINT_MAX;
		m_uNameSize						= 0;
		for(int i=0; i<m_szTargetName.Length; ++i)
        {
            m_szTargetName[i] = 0;
        }
	}

    public    int getSize()
    {
        int	uSize;
		uSize		= sizeof(uint) + sizeof(byte) + m_uNameSize*sizeof(byte);
		return uSize;

    }
     public static int getMaxSize()
     {
        int	uSize;
		uSize		= sizeof(uint) + sizeof(byte) + GAMEDEFINE.MAX_CHARACTER_NAME*sizeof(byte);
		return uSize;
     }
    public    bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadUint(ref m_GUID);
        buff.ReadByte(ref m_uNameSize);
        if(m_uNameSize > 0 && m_uNameSize<m_szTargetName.Length)
        {
            buff.Read(ref m_szTargetName, m_uNameSize);
        }
        return true;
    }
    public    int writeToBuff(ref NetOutputBuffer buff)
    {
        buff.WriteUint(m_GUID);
        buff.WriteByte(m_uNameSize);
        if(m_uNameSize > 0 && m_uNameSize<m_szTargetName.Length)
        {
            buff.Write(ref m_szTargetName, m_uNameSize);
        }
        return getSize();
    }
};



// �����б��Լ�������
[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class GC_RELATIONLIST:ClassCanbeSerialized
{
	//����
	byte								m_uFriendCount;				// ������
	byte								m_uBlackCount;				// ��������
	byte								m_uMoodSize;
	byte[]								m_szMood = new byte[GAMEDEFINE.MOOD_DATA_SIZE];	// �Լ�������
	_FRIEND_INFO[]	m_FriendInfo = new _FRIEND_INFO[RELATION_DEFINE.RELATION_BLACKNAME_OFFSET - RELATION_DEFINE.RELATION_FRIEND_OFFSET];
	_BLACKNAME_INFO[]	m_BlackNameInfo = new _BLACKNAME_INFO[GAMEDEFINE.MAX_RELATION_SIZE - (int)RELATION_DEFINE.RELATION_BLACKNAME_OFFSET];
    public GC_RELATIONLIST()
    {
        for (int i = 0; i < m_FriendInfo.Length; ++i)
        {
            m_FriendInfo[i] = new _FRIEND_INFO();
        }

        for (int i = 0; i < m_BlackNameInfo.Length; ++i)
        {
            m_BlackNameInfo[i] = new _BLACKNAME_INFO();
        }
        CleanUp();
    }
	//�����ӿ�
	public void								CleanUp( )
    {
        m_uFriendCount = 0;
	    m_uBlackCount = 0;
	    m_uMoodSize = 0;
	    for(int i=0; i<m_szMood.Length; ++i)
        {
            m_szMood[i] = 0;
        }
	    for( int i=0; i<RELATION_DEFINE.RELATION_BLACKNAME_OFFSET - RELATION_DEFINE.RELATION_FRIEND_OFFSET; ++i )
	    {
		    m_FriendInfo[i].CleanUp();
	    }

	    for( int i=0; i<GAMEDEFINE.MAX_RELATION_SIZE - (int)RELATION_DEFINE.RELATION_BLACKNAME_OFFSET; ++i )
	    {
		    m_BlackNameInfo[i].CleanUp();
	    }
    }
    public    int getSize()
    {
        int uSize;

        uSize = sizeof(byte) + sizeof(byte) + sizeof(byte) + m_uMoodSize*sizeof(byte);

	    for( byte i=0; i<m_uFriendCount; ++i )
	    {
		    uSize += m_FriendInfo[i].getSize();
	    }

	    for( byte i=0; i<m_uBlackCount; ++i )
	    {
		    uSize += m_BlackNameInfo[i].getSize();
	    }

	    return uSize;
    }
    public static int getMaxSize()
    {
        
       int  uSize = sizeof(byte) + sizeof(byte) + sizeof(byte) + GAMEDEFINE.MOOD_DATA_SIZE*sizeof(byte);


		uSize += _FRIEND_INFO.getMaxSize() *(int)( RELATION_DEFINE.RELATION_BLACKNAME_OFFSET - RELATION_DEFINE.RELATION_FRIEND_OFFSET);
	    


		uSize += _BLACKNAME_INFO.getMaxSize()*(int)(GAMEDEFINE.MAX_RELATION_SIZE - (int)RELATION_DEFINE.RELATION_BLACKNAME_OFFSET);
	    
        return uSize;
    }
    public    bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadByte(ref m_uFriendCount);
        buff.ReadByte(ref m_uBlackCount);
        buff.ReadByte(ref m_uMoodSize);

	    if( m_uMoodSize > 0 && m_uMoodSize < GAMEDEFINE.MOOD_DATA_SIZE )
	    {
            buff.Read(ref m_szMood, m_uMoodSize);
	    }

	    for( byte i=0; i<m_uFriendCount; ++i )
	    {
            m_FriendInfo[i].readFromBuff(ref buff);
	    }

	    for( byte i=0; i<m_uBlackCount; ++i )
	    {
            m_BlackNameInfo[i].readFromBuff(ref buff);
	    }
        return true;
    }
    public    int writeToBuff(ref NetOutputBuffer buff)
    {
        buff.WriteByte(m_uFriendCount);
        buff.WriteByte(m_uBlackCount);
        buff.WriteByte(m_uMoodSize);

        if (m_uMoodSize > 0 && m_uMoodSize < GAMEDEFINE.MOOD_DATA_SIZE)
	    {
            buff.Write(ref m_szMood, m_uMoodSize);
	    }

	    for( byte i=0; i<m_uFriendCount; ++i )
	    {
            m_FriendInfo[i].writeToBuff(ref buff);
	    }

	    for( byte i=0; i<m_uBlackCount; ++i )
	    {
            m_BlackNameInfo[i].writeToBuff(ref buff);
	    }
        return getSize();
    }

	//����Ӧ�ýӿ�
    public byte GetFriendCount() { return m_uFriendCount; }

    public byte GetBlackCount() { return m_uBlackCount; }

    public byte[] GetMood() { return m_szMood; }
    public void SetMood(byte[] pMood)
	{
        Array.Copy(pMood, m_szMood, GAMEDEFINE.MOOD_DATA_SIZE - 1);
        m_uMoodSize = (byte)GameUtil.Utility.getStringLength(m_szMood);
	}

    public void AddFriend(_OWN_RELATION pRelation)
    { 
        if(pRelation == null) return;
	    m_FriendInfo[m_uFriendCount].m_GUID = pRelation.m_Member.m_MemberGUID;
        Array.Copy(pRelation.m_Member.m_szMemberName, m_FriendInfo[m_uFriendCount].m_szTargetName, m_FriendInfo[m_uFriendCount].m_szTargetName.Length);
	    m_FriendInfo[m_uFriendCount].m_uNameSize = (byte)GameUtil.Utility.getStringLength(pRelation.m_Member.m_szMemberName);
	    m_FriendInfo[m_uFriendCount].m_uRelationType = pRelation.m_Type;
	    m_FriendInfo[m_uFriendCount].m_uGroup = pRelation.m_Group;
	    m_FriendInfo[m_uFriendCount].m_nFriendpoint = pRelation.m_FriendPoint;

	    ++m_uFriendCount;
    }
    public _FRIEND_INFO GetFriend(int index)
    {
        return m_FriendInfo[index];
    }

    public void AddBlackName(_OWN_RELATION pRelation)
    { 
        if(pRelation == null) return;

	    m_BlackNameInfo[m_uBlackCount].m_GUID = pRelation.m_Member.m_MemberGUID;
        Array.Copy(pRelation.m_Member.m_szMemberName, m_BlackNameInfo[m_uBlackCount].m_szTargetName, m_BlackNameInfo[m_uBlackCount].m_szTargetName.Length);
	    m_BlackNameInfo[m_uBlackCount].m_uNameSize = (byte)GameUtil.Utility.getStringLength(pRelation.m_Member.m_szMemberName);
	    ++m_uBlackCount;
    }
    public _BLACKNAME_INFO GetBlackName(int index)
    {
        return m_BlackNameInfo[index];
    }
};

[Serializable] //�����л�
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class GC_RELATION:ClassCanbeSerialized
{
	public byte								m_Type;
    /*
    union
	{
		GC_RELATIONLIST					m_RelationList;
		RETURN_ADD_RELATION				m_NewRelation;
		RETURN_RELATION_INFO			m_RelationInfo;
		RETURN_VIEW_PLAYER				m_ViewPlayer;
		RELATION_GUID_UCHAR_UCHAR		m_RelationGUIDUCHARUCHAR;
		RELATION_GUID					m_RelationGUID;
		RETURN_NOTIFY_FRIEND			m_NotifyFriend;
		RETURN_ONLINE_LIST				m_RelationOnline;
		RETURN_NOTIFY_ONLINE			m_NotifyOnline;
		REQUEST_MODIFY_MOOD				m_NewMood;
		REQUEST_VIEW_PLAYER				m_PlayerName;
	};
     */
    public System.Object      mRelation = null;
	public void								CleanUp( )
    {
        m_Type = (byte)RELATION_RETURN_TYPE.RET_NONE;
    }
    public static int getMaxSize()
    {
        int max1 = Math.Max(GC_RELATIONLIST.getMaxSize(), RETURN_ADD_RELATION.getMaxSize());
        int max2 = Math.Max(RETURN_RELATION_INFO.getMaxSize(), RETURN_VIEW_PLAYER.getMaxSize());
        int max3 = Math.Max(RELATION_GUID_UCHAR_UCHAR.getMaxSize(), RELATION_GUID.getMaxSize());
        int max4 = Math.Max(RETURN_NOTIFY_FRIEND.getMaxSize(), RETURN_ONLINE_LIST.getMaxSize());
        int max5 = Math.Max(RETURN_NOTIFY_ONLINE.getMaxSize(), REQUEST_MODIFY_MOOD.getMaxSize());
        int max6 = REQUEST_VIEW_PLAYER.getMaxSize();
        int temp = Math.Max(Math.Max(max1, max2), Math.Max(max3, max4));
        return Math.Max(Math.Max(max5, max6), temp);
    }
    public    int getSize()
    {
        int uSize = sizeof(byte);

        switch ((RELATION_RETURN_TYPE )m_Type)
        {
            case RELATION_RETURN_TYPE.RET_RELATIONLIST:
                uSize += ((GC_RELATIONLIST)mRelation).getSize();
                break;
            case RELATION_RETURN_TYPE.RET_RELATIONINFO:
                uSize += ((RETURN_RELATION_INFO )mRelation).getSize();
                break;
            case RELATION_RETURN_TYPE.RET_VIEWPLAYER:
                uSize += ((RETURN_VIEW_PLAYER )mRelation).getSize();
                break;
            case RELATION_RETURN_TYPE.RET_ADDFRIEND:
            case RELATION_RETURN_TYPE.RET_ADDTOBLACKLIST:
            case RELATION_RETURN_TYPE.RET_TEMPFRIEND_TO_FRIEND:
            case RELATION_RETURN_TYPE.RET_TEMPFRIEND_ADDTO_BLACKLIST:
                uSize += ((RETURN_ADD_RELATION )mRelation).getSize();
                break;
            case RELATION_RETURN_TYPE.RET_TRANSITION:
                uSize += ((RELATION_GUID_UCHAR_UCHAR )mRelation).getSize();
                break;
            case RELATION_RETURN_TYPE.RET_DELFRIEND:
            case RELATION_RETURN_TYPE.RET_DELFROMBLACKLIST:
            case RELATION_RETURN_TYPE.RET_RELATIONOFFLINE:
                uSize += ((RELATION_GUID )mRelation).getSize();
                break;
            case RELATION_RETURN_TYPE.RET_ADDFRIENDNOTIFY:
                uSize += ((RETURN_NOTIFY_FRIEND )mRelation).getSize();
                break;
            case RELATION_RETURN_TYPE.RET_ONLINELIST:
                uSize += ((RETURN_ONLINE_LIST )mRelation).getSize();
                break;
            case RELATION_RETURN_TYPE.RET_RELATIONONLINE:
                uSize += ((RETURN_NOTIFY_ONLINE )mRelation).getSize();
                break;
            case RELATION_RETURN_TYPE.RET_NEWMOOD:
                uSize += ((REQUEST_MODIFY_MOOD )mRelation).getSize();
                break;
            case RELATION_RETURN_TYPE.RET_NOTIFY_ADDTEMPFRIEND:
                uSize += ((REQUEST_VIEW_PLAYER )mRelation).getSize();
                break;
            case RELATION_RETURN_TYPE.RET_ERR_TARGETNOTEXIST:
            case RELATION_RETURN_TYPE.RET_ERR_GROUPISFULL:
            case RELATION_RETURN_TYPE.RET_ERR_ISFRIEND:
            case RELATION_RETURN_TYPE.RET_ERR_ISBLACKNAME:
            case RELATION_RETURN_TYPE.RET_ERR_CANNOTTRANSITION:
            case RELATION_RETURN_TYPE.RET_ERR_ISNOTFRIEND:
            case RELATION_RETURN_TYPE.RET_ERR_ISNOTINBLACKLIST:
            case RELATION_RETURN_TYPE.RET_ERR_SPOUSETOBLACKLIST:
            case RELATION_RETURN_TYPE.RET_ERR_MASTERTOBLACKLIST:
            case RELATION_RETURN_TYPE.RET_ERR_PRENTICETOBLACKLIST:
            case RELATION_RETURN_TYPE.RET_ERR_BROTHERTOBLACKLIST:
            case RELATION_RETURN_TYPE.RET_ERR_DELSPOUSE:
            case RELATION_RETURN_TYPE.RET_ERR_DELMASTER:
            case RELATION_RETURN_TYPE.RET_ERR_DELPRENTICE:
            case RELATION_RETURN_TYPE.RET_ERR_DELBROTHER:
            case RELATION_RETURN_TYPE.RET_ERR_PASSWDMISMATCH:
            case RELATION_RETURN_TYPE.RET_ERR_CANNOT_ADDFRIEND:
            case RELATION_RETURN_TYPE.RET_ERR_CANNOTRECEIVEMAIL:
            case RELATION_RETURN_TYPE.RET_ERR_NOTRECVSTRANGEMAIL:
            case RELATION_RETURN_TYPE.RET_ERR_RELATIONUNKNOWN:
                break;
            default:
                return 0;
        };

        return uSize;
    }
    public    bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadByte( ref m_Type );

	    switch( (RELATION_RETURN_TYPE )m_Type )
	    {
	        case RELATION_RETURN_TYPE.RET_RELATIONLIST:
                mRelation = new GC_RELATIONLIST();
                ((GC_RELATIONLIST)mRelation).CleanUp();
                ((GC_RELATIONLIST)mRelation).readFromBuff(ref buff);
		        break;
	        case RELATION_RETURN_TYPE.RET_RELATIONINFO:
                mRelation = new RETURN_RELATION_INFO();
                ((RETURN_RELATION_INFO)mRelation).CleanUp();
                ((RETURN_RELATION_INFO)mRelation).readFromBuff(ref buff);
		        break;
	        case RELATION_RETURN_TYPE.RET_VIEWPLAYER:
                mRelation = new RETURN_VIEW_PLAYER();
                ((RETURN_VIEW_PLAYER)mRelation).CleanUp();
                ((RETURN_VIEW_PLAYER)mRelation).readFromBuff(ref buff);
		        break;
	        case RELATION_RETURN_TYPE.RET_ADDFRIEND:
	        case RELATION_RETURN_TYPE.RET_ADDTOBLACKLIST:
	        case RELATION_RETURN_TYPE.RET_TEMPFRIEND_TO_FRIEND:
	        case RELATION_RETURN_TYPE.RET_TEMPFRIEND_ADDTO_BLACKLIST:
                mRelation = new RETURN_ADD_RELATION();
                ((RETURN_ADD_RELATION)mRelation).CleanUp();
                ((RETURN_ADD_RELATION)mRelation).readFromBuff(ref buff);
		        break;
	        case RELATION_RETURN_TYPE.RET_TRANSITION:
                mRelation = new RELATION_GUID_UCHAR_UCHAR();
                ((RELATION_GUID_UCHAR_UCHAR)mRelation).CleanUp();
                ((RELATION_GUID_UCHAR_UCHAR)mRelation).readFromBuff(ref buff);
		        break;
	        case RELATION_RETURN_TYPE.RET_DELFRIEND:
	        case RELATION_RETURN_TYPE.RET_DELFROMBLACKLIST:
	        case RELATION_RETURN_TYPE.RET_RELATIONOFFLINE:
                mRelation = new RELATION_GUID();
                ((RELATION_GUID)mRelation).CleanUp();
                ((RELATION_GUID)mRelation).readFromBuff(ref buff);
		        break;
	        case RELATION_RETURN_TYPE.RET_ADDFRIENDNOTIFY:
                mRelation = new RETURN_NOTIFY_FRIEND();
                ((RETURN_NOTIFY_FRIEND)mRelation).CleanUp();
                ((RETURN_NOTIFY_FRIEND)mRelation).readFromBuff(ref buff);
		        break;
	        case RELATION_RETURN_TYPE.RET_ONLINELIST:
                mRelation = new RETURN_ONLINE_LIST();
                ((RETURN_ONLINE_LIST)mRelation).CleanUp();
                ((RETURN_ONLINE_LIST)mRelation).readFromBuff(ref buff);
		        break;
	        case RELATION_RETURN_TYPE.RET_RELATIONONLINE:
                mRelation = new RETURN_NOTIFY_ONLINE();
                ((RETURN_NOTIFY_ONLINE)mRelation).CleanUp();
                ((RETURN_NOTIFY_ONLINE)mRelation).readFromBuff(ref buff);
		        break;
	        case RELATION_RETURN_TYPE.RET_NEWMOOD:
                mRelation = new REQUEST_MODIFY_MOOD();
                ((REQUEST_MODIFY_MOOD)mRelation).CleanUp();
                ((REQUEST_MODIFY_MOOD)mRelation).readFromBuff(ref buff);
		        break;
	        case RELATION_RETURN_TYPE.RET_NOTIFY_ADDTEMPFRIEND:
                mRelation = new REQUEST_VIEW_PLAYER();
                ((REQUEST_VIEW_PLAYER)mRelation).CleanUp();
                ((REQUEST_VIEW_PLAYER)mRelation).readFromBuff(ref buff);
		        break;
	        case RELATION_RETURN_TYPE.RET_ERR_TARGETNOTEXIST:
	        case RELATION_RETURN_TYPE.RET_ERR_GROUPISFULL:
	        case RELATION_RETURN_TYPE.RET_ERR_ISFRIEND:
	        case RELATION_RETURN_TYPE.RET_ERR_ISBLACKNAME:
	        case RELATION_RETURN_TYPE.RET_ERR_CANNOTTRANSITION:
	        case RELATION_RETURN_TYPE.RET_ERR_ISNOTFRIEND:
	        case RELATION_RETURN_TYPE.RET_ERR_ISNOTINBLACKLIST:
	        case RELATION_RETURN_TYPE.RET_ERR_SPOUSETOBLACKLIST:
	        case RELATION_RETURN_TYPE.RET_ERR_MASTERTOBLACKLIST:
	        case RELATION_RETURN_TYPE.RET_ERR_PRENTICETOBLACKLIST:
	        case RELATION_RETURN_TYPE.RET_ERR_BROTHERTOBLACKLIST:
	        case RELATION_RETURN_TYPE.RET_ERR_DELSPOUSE:
	        case RELATION_RETURN_TYPE.RET_ERR_DELMASTER:
	        case RELATION_RETURN_TYPE.RET_ERR_DELPRENTICE:
	        case RELATION_RETURN_TYPE.RET_ERR_DELBROTHER:
	        case RELATION_RETURN_TYPE.RET_ERR_PASSWDMISMATCH:
	        case RELATION_RETURN_TYPE.RET_ERR_CANNOT_ADDFRIEND:
	        case RELATION_RETURN_TYPE.RET_ERR_CANNOTRECEIVEMAIL:
	        case RELATION_RETURN_TYPE.RET_ERR_NOTRECVSTRANGEMAIL:
	        case RELATION_RETURN_TYPE.RET_ERR_RELATIONUNKNOWN:
		        break;
	    }
        return true;
    }
    public    int writeToBuff(ref NetOutputBuffer buff)
    {
        buff.WriteByte(m_Type);

        switch ((RELATION_RETURN_TYPE)m_Type)
	    {
	        case RELATION_RETURN_TYPE.RET_RELATIONLIST:
                ((GC_RELATIONLIST)mRelation).writeToBuff(ref buff);
		        break;
	        case RELATION_RETURN_TYPE.RET_RELATIONINFO:
                ((RETURN_RELATION_INFO)mRelation).writeToBuff(ref buff);
		        break;
	        case RELATION_RETURN_TYPE.RET_VIEWPLAYER:
                ((RETURN_VIEW_PLAYER)mRelation).writeToBuff(ref buff);
		        break;
	        case RELATION_RETURN_TYPE.RET_ADDFRIEND:
	        case RELATION_RETURN_TYPE.RET_ADDTOBLACKLIST:
	        case RELATION_RETURN_TYPE.RET_TEMPFRIEND_TO_FRIEND:
	        case RELATION_RETURN_TYPE.RET_TEMPFRIEND_ADDTO_BLACKLIST:
                ((RETURN_ADD_RELATION)mRelation).writeToBuff(ref buff);
		        break;
	        case RELATION_RETURN_TYPE.RET_TRANSITION:
                ((RELATION_GUID_UCHAR_UCHAR)mRelation).writeToBuff(ref buff);
		        break;
	        case RELATION_RETURN_TYPE.RET_DELFRIEND:
	        case RELATION_RETURN_TYPE.RET_DELFROMBLACKLIST:
	        case RELATION_RETURN_TYPE.RET_RELATIONOFFLINE:
                ((RELATION_GUID)mRelation).writeToBuff(ref buff);
		        break;
	        case RELATION_RETURN_TYPE.RET_ADDFRIENDNOTIFY:
                ((RETURN_NOTIFY_FRIEND)mRelation).writeToBuff(ref buff);
		        break;
	        case RELATION_RETURN_TYPE.RET_ONLINELIST:
                ((RETURN_ONLINE_LIST)mRelation).writeToBuff(ref buff);
		        break;
	        case RELATION_RETURN_TYPE.RET_RELATIONONLINE:
                ((RETURN_NOTIFY_ONLINE)mRelation).writeToBuff(ref buff);
		        break;
	        case RELATION_RETURN_TYPE.RET_NEWMOOD:
                ((REQUEST_MODIFY_MOOD)mRelation).writeToBuff(ref buff);
		        break;
	        case RELATION_RETURN_TYPE.RET_NOTIFY_ADDTEMPFRIEND:
                ((REQUEST_VIEW_PLAYER)mRelation).writeToBuff(ref buff);
		        break;
	        case RELATION_RETURN_TYPE.RET_ERR_TARGETNOTEXIST:
	        case RELATION_RETURN_TYPE.RET_ERR_GROUPISFULL:
	        case RELATION_RETURN_TYPE.RET_ERR_ISFRIEND:
	        case RELATION_RETURN_TYPE.RET_ERR_ISBLACKNAME:
	        case RELATION_RETURN_TYPE.RET_ERR_CANNOTTRANSITION:
	        case RELATION_RETURN_TYPE.RET_ERR_ISNOTFRIEND:
	        case RELATION_RETURN_TYPE.RET_ERR_ISNOTINBLACKLIST:
	        case RELATION_RETURN_TYPE.RET_ERR_SPOUSETOBLACKLIST:
	        case RELATION_RETURN_TYPE.RET_ERR_MASTERTOBLACKLIST:
	        case RELATION_RETURN_TYPE.RET_ERR_PRENTICETOBLACKLIST:
	        case RELATION_RETURN_TYPE.RET_ERR_BROTHERTOBLACKLIST:
	        case RELATION_RETURN_TYPE.RET_ERR_DELSPOUSE:
	        case RELATION_RETURN_TYPE.RET_ERR_DELMASTER:
	        case RELATION_RETURN_TYPE.RET_ERR_DELPRENTICE:
	        case RELATION_RETURN_TYPE.RET_ERR_DELBROTHER:
	        case RELATION_RETURN_TYPE.RET_ERR_PASSWDMISMATCH:
	        case RELATION_RETURN_TYPE.RET_ERR_CANNOT_ADDFRIEND:
	        case RELATION_RETURN_TYPE.RET_ERR_CANNOTRECEIVEMAIL:
	        case RELATION_RETURN_TYPE.RET_ERR_NOTRECVSTRANGEMAIL:
	        case RELATION_RETURN_TYPE.RET_ERR_RELATIONUNKNOWN:
		        break;
	    }
        return getSize();
    }
}
