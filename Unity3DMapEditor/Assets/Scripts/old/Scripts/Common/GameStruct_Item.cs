using System;
using System.Runtime.InteropServices;
using Network.Packets;
using Network;

// 
// #ifndef __GAMESTRUCT_ITEM_H__
// #define __GAMESTRUCT_ITEM_H__
// 
// #include "Type.h"
// 
// class SocketInputStream;
// class SocketOutputStream;
// 
// 
// BYTE		GetSerialType(UINT Serial);
// BYTE		GetSerialClass(UINT Serial);
// BYTE		GetSerialQual(UINT Serial);
// UINT		GetSerialIndex(UINT Serial);
// 
// //��Χ
// public struct _RANGE_VALUE
// {
// 	WORD				m_MinValue ;
// 	WORD				m_MaxValue ;
// };
// 
// public struct _ITEM_SKILL 
// {
// 	BOOL				m_bActive;
// 	WORD				m_SkillID;
// 	WORD				m_SkillRate;
// 
// 	BOOL				IsActive()
// 	{
// 		return m_bActive;
// 	}
// 
// 	VOID				SetActive(BOOL bActive)
// 	{	
// 		m_bActive = bActive;
// 	}
// 
// 	VOID	CleanUp()
// 	{
// 		memset(this,0,sizeof(_ITEM_SKILL));
// 	}
// 
// 	_ITEM_SKILL()
// 	{
// 		CleanUp();
// 	}
// };
// 
 //��Ʒ����ֵ
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
 public struct _ITEM_VALUE: ClassCanbeSerialized
 {
 	public short			m_Value ;
 	public void	CleanUp( )
 	{
 		m_Value = 0;
 	}
 
 	public static bool	operator == ( _ITEM_VALUE iV, _ITEM_VALUE rV)
 	{
 		return iV.m_Value == rV.m_Value;
 	}
    public static bool operator != (_ITEM_VALUE iV, _ITEM_VALUE rV)
    {
        return iV.m_Value != rV.m_Value;
    }
    public override bool Equals(object obj)
    {
        if(obj is _ITEM_VALUE)
        {
            _ITEM_VALUE iV = (_ITEM_VALUE) obj;
            return this == iV;
        }
        return base.Equals(obj);
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public static int getMaxSize()
    {
        return sizeof(short);
    }
#region ClassCanbeSerialized
    public int getSize()
    {
        return sizeof(short);
    }
    public bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadShort(ref m_Value);
        return true;
    }
    public int writeToBuff(ref NetOutputBuffer buff)
    {
        buff.WriteShort(m_Value);
        return sizeof(short);
    }

#endregion
 };
 
 //��Ʒ����
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
 public struct _ITEM_ATTR:ClassCanbeSerialized
 {
 	public byte				m_AttrType ;//�������ͨװ������m_AttrType��enum ITEM_ATTRIBUTE
 									//    ����ʾ�������ͣ�m_Value��ʾ��������ֵ
 									//�������ɫ����װ����m_AttrType��ʾ��ǰ��װ��
 									//    ������ϵ�װ��������m_Value��ʾ��װ��������
 	public _ITEM_VALUE			m_Value ;
 
 	void	CleanUp( )
 	{
 		m_AttrType = 0;
 		m_Value.CleanUp();
 	}
 	
 	public static bool	operator==( _ITEM_ATTR iA, _ITEM_ATTR rA)
 	{
 		return	(iA.m_AttrType == rA.m_AttrType)&&(iA.m_Value == rA.m_Value);
 
 	}
    public static bool operator !=(_ITEM_ATTR iA, _ITEM_ATTR rA)
    {
        return !((iA.m_AttrType == rA.m_AttrType)&& (iA.m_Value == rA.m_Value));
    }
    public override bool Equals(object obj)
    {
        if(obj is _ITEM_ATTR)
        {
            _ITEM_ATTR attri = (_ITEM_ATTR) obj;
            return this == attri;
        }
        return base.Equals(obj);
    }
    public static int getMaxSize()
    {
        return sizeof(byte) + _ITEM_VALUE.getMaxSize();
    }
#region ClassCanbeSerialized
     public int getSize()
    {
        return sizeof(byte) + m_Value.getSize();
    }
    public bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadByte(ref m_AttrType);
        m_Value.readFromBuff(ref buff);
        return true;
    }
    public int writeToBuff(ref NetOutputBuffer buff)
    {
        buff.WriteByte(m_AttrType);
        m_Value.writeToBuff(ref buff);
        return sizeof(byte) + m_Value.getSize();
    }
#endregion
 };
// 
// public struct _EQUIP_SET_ATTR                      // Allan 4/1/2011 
// {
//     byte                m_ActivityNum;          //������ǰ������Ҫ�Ļ�Ծװ������
// 
//     _ITEM_ATTR          m_ItemAttr;             //����
// 
//     void    CleanUp( )
//     {
//        m_ActivityNum = 0;
// 		m_ItemAttr.CleanUp();
//     }
// 
//     bool    operator== (ref _EQUIP_SET_ATTR iA)
//     {
//         return (iA.m_ActivityNum == m_ActivityNum)
//             && (iA.m_ItemAttr == m_ItemAttr);
//     }
// };
// 
// 
// 
//��ƷΨһID

public struct _ITEM_GUID : ClassCanbeSerialized
{
	public Byte	m_World ;		//�����: (��)101
    public Byte m_Server;		//����˳���ţ�(��)5
    public int m_Serial;		//��Ʒ���кţ�(��)123429
	
// 	public _ITEM_GUID()
// 	{
// 		m_Serial = 0;
// 		m_Server = 0;
// 		m_World	 = 0;	
// 	}
	void Reset()
	{
		m_Serial = 0;
		m_Server = 0;
		m_World	 = 0;	
	}
    //C#��ṹ����ֵ���ͣ���ֵ�������
// 	public _ITEM_GUID operator =(_ITEM_GUID  rhs)
// 	{
// 		m_Serial = rhs.m_Serial;
// 		m_Server = rhs.m_Server;
// 		m_World  = rhs.m_World;
// 		return this;
// 	}
    public static bool operator ==(_ITEM_GUID lhs, _ITEM_GUID rhs)
    {
        return (lhs.m_Serial == rhs.m_Serial) && (lhs.m_Server == rhs.m_Server) && (lhs.m_World == rhs.m_World);
    }

    public static bool operator !=(_ITEM_GUID lhs, _ITEM_GUID rhs)
    {
        return !(lhs == rhs);
    }


    bool isNull()
    {
        return (m_World == 0) && (m_Serial == 0) && (m_Server == 0);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }


    #region ClassCanbeSerialized Members

    public int getSize()
    {
        return sizeof(Byte) * 2 + sizeof(int);
    }

    public static int GetMaxSize()
    {
        return sizeof(Byte) * 2 + sizeof(int);
    }

    public bool readFromBuff(ref Network.NetInputBuffer buff)
    {
        buff.ReadByte(ref m_World);
        buff.ReadByte(ref m_Server);
        buff.ReadInt(ref m_Serial);
        return true;
    }

    public int writeToBuff(ref Network.NetOutputBuffer buff)
    {
        buff.WriteByte(m_World);
        buff.WriteByte(m_Server);
        buff.WriteInt(m_Serial);
        return getSize();
    }
    public override bool Equals(object obj)
    {
        _ITEM_GUID rhs = (_ITEM_GUID)obj;
        return this == rhs;
    }
    #endregion
};


public class _ITEM_TYPE
{
    public uint uTemp;          //��ŴӰ��ж�ȡ�ͽ�Ҫ���͵���������
    public uint m_Class;		//�������ͣ����磺װ���ࡢ����Ʒ����
    public uint m_Quality;		//���Σ����磺��ɫװ������ɫװ������
    public uint m_Type;		    //��𣬱��磺�󵶡���ǹ����
    public uint m_Index;		//��Ʒ�ţ����磺��Ҷ����ԧ�쵶����

    public void OneToFour()
    {
        m_Class = (uint)(uTemp & 0x0000007f);
        m_Quality = (uint)((uTemp & 0x00003f80) >> 7);
        m_Type = (uint)((uTemp & 0x001fc000) >> 14);
        m_Index = (uint)((uTemp & 0xffe00000) >> 21);
    }

    //�����ͽ������ϵ�һ��uint���Ա㷢��
    public uint FourToOne()
    {
        uTemp = (uint)((m_Class & 0x0000007f) | ((m_Quality << 7) & 0x00003f80) | ((m_Type << 14) & 0x001fc000) | ((m_Index << 21) & 0xffe00000));
        return uTemp;
    }

    private uint ToSerial()
    {
        uint Serial;
        Serial = m_Class;
        Serial = Serial * 100 + m_Quality;
        Serial = Serial * 100 + m_Type;
        Serial = Serial * 1000 + m_Index;
        return Serial;
    }

    public bool isNull()
    {
        return (m_Class == 0) && (m_Quality == 0) && (m_Type == 0) && (m_Index == 0);
    }

    public static bool operator ==(_ITEM_TYPE lhs, _ITEM_TYPE rhs)
    {
        return (lhs.m_Class == rhs.m_Class) && (lhs.m_Quality == rhs.m_Quality) && (lhs.m_Type == rhs.m_Type) && (lhs.m_Index == rhs.m_Index);
    }

    public static bool operator !=(_ITEM_TYPE lhs, _ITEM_TYPE rhs)
    {
        return !(lhs == rhs);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public static bool operator >(_ITEM_TYPE lhs, _ITEM_TYPE rhs)
    {
        return lhs.ToSerial() > rhs.ToSerial();
    }

    public static bool operator <(_ITEM_TYPE lhs, _ITEM_TYPE rhs)
    {
        return lhs.ToSerial() < rhs.ToSerial();
    }

    public void CleanUp()
    {
        m_Class = 0;
        m_Quality = 0;
        m_Type = 0;
        m_Index = 0;
    }
}
// 
// 
// _ITEM_TYPE	ConvertSerial2ItemType(UINT Serial);
// 
// 
// public struct _ITEM_RULER
// {
// 	BOOL	m_Discard;		//����
// 	BOOL	m_Tile;			//�ص�
// 	BOOL	m_ShortCut;		//���
// 	BOOL	m_CanSell;		//����
// 	BOOL	m_CanExchange;	//����
// 	BOOL	m_CanUse;		//ʹ��
// 	BOOL	m_PickBind;		//ʰȡ��
// 	BOOL	m_EquipBind;	//װ����
// 	BOOL	m_Unique;		//�Ƿ�Ψһ
// 
// 
// 	BOOL	CanDiscard()	{return m_Discard;}		//����
// 	BOOL	CanTile()		{return m_Tile;}		//�ص�
// 	BOOL	CanShortCut()	{return m_ShortCut;}	//���
// 	BOOL	CanSell()		{return m_CanSell;}		//����
// 	BOOL	CanExchange()	{return m_CanExchange;}	//����
// 	BOOL	CanUse()		{return m_CanUse;}		//ʹ��
// 	BOOL	isPickBind()	{return m_PickBind;}	//ʰȡ��
// 	BOOL	isEquipBind()	{return m_EquipBind;}	//װ����
// 	BOOL	isUnique()		{return m_Unique;}		//�Ƿ�Ψһ
// };
// 
// 
// enum	ITEM_RULER_LIST
// {
// 	IRL_DISCARD,			//����
// 	IRL_TILE,				//�ص�
// 	IRL_SHORTCUT,			//���
// 	IRL_CANSELL,			//����
// 	IRL_CANEXCHANGE,		//����
// 	IRL_CANUSE,				//ʹ��
// 	IRL_PICKBIND,			//ʰȡ��
// 	IRL_EQUIPBIND,			//װ����
// 	IRL_UNIQUE				//�Ƿ�Ψһ
// };
// 
// 
// 
 //��Ƕ��ʯ��Ϣ
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
 public struct _ITEM_GEMINFO : ClassCanbeSerialized
 {
 	public uint	m_GemType;
 
     //תΪ�����ƣ�ǰ2λ�洢������Ƕ���״̬����ͨ�����������£�
     //��30λ�洢������Ƕ�ϵı�ʯ�ı��
     //��ʯ��� [2011-9-20] by: cfp+
     uint GetGemSerialNum()
     {
         return m_GemType;
     }
     void SetGemSerialNum(uint uIndex)
     {
         m_GemType = uIndex;
     }
 
     ////��Ƕ���״̬
     //byte GetGemState()
     //{
     //    return (byte)(m_GemType>>30);
     //}
     //void SetGemState(byte ucState)
     //{
     //    //ucState & 0x3 ���� �ضϣ�������3
     //    uint state =(uint)( ucState & 0x3);
     //    m_GemType = ( m_GemType & 0x3FFFFFFF ) | ( state << 30 ) ;
     //}
    public static int getMaxSize()
    {
        return sizeof(uint);
    }

#region ClassCanbeSerialized
    public int getSize()
    {
        return sizeof(uint);
    }
    public  bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadUint(ref m_GemType);
        return true;
    }
     public  int writeToBuff(ref NetOutputBuffer buff)
     {
         buff.WriteUint(m_GemType);
         return sizeof(uint);
     }
#endregion
 };
// 
// 
// 
// 
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
 public class GEM_INFO:ClassCanbeSerialized
 {
 	
 	public _ITEM_ATTR	m_Attr;
 	public uint		m_nPrice;
 	

#region ClassCanbeSerialized
     public int getSize()
     {
         return sizeof(uint) + m_Attr.getSize();
     }
     public bool readFromBuff(ref NetInputBuffer buff)
     {
         m_Attr.readFromBuff(ref buff);
         buff.ReadUint(ref m_nPrice);
         return true;
     }
     public int writeToBuff(ref NetOutputBuffer buff)
     {
         m_Attr.writeToBuff(ref buff);
         buff.WriteUint(m_nPrice);
         return getSize();
     }
#endregion
 };
// 
// 
// public struct STORE_MAP_INFO 
// {
// 	INT						m_nLevel;
// 	FLOAT					m_xPos;
// 	FLOAT					m_zPos;
// 	INT						m_SceneID;
// 	INT						m_GrowType;
// 	
// };
// 
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
 public class MEDIC_INFO : ClassCanbeSerialized
 {
 	
 	public byte					m_nCount;	   //��ǰ����
 	public byte					m_nLevel;
 	public byte					m_nLayedNum;   //��������
 	public byte					m_nReqSkillLevel;
 
 	public short				m_nCurPinJiValue;//// ����Ʒ��ֵ [9/16/2011 zzh+]
 	public uint					m_nBasePrice;
 	public int					m_nScriptID;
 	public int					m_nSkillID;
 	public int					m_bCosSelf;		//�Ƿ������Լ�
 	public int					m_nReqSkill;
 	public byte					m_TargetType;	//��Ʒѡ������
 	public byte					m_nGrade;		// [2010-11-18] by: cfp+ // ���εȼ�
 	public int					m_nRecipeId;	// [2010-11-18] by: cfp+ // �䷽ID
 	
 	public int GetTileNum()		{return m_nCount;}
 
 	public int GetMaxTileNum()		{return m_nLayedNum;}
 
 	public void	CleanUp()
 	{
 		m_nCurPinJiValue = 0;//// ����Ʒ��ֵ [9/16/2011 zzh+]
 		m_nLevel			=	0;
 		m_nBasePrice		=	0;
 		m_nLayedNum			=	0;
 		m_nScriptID			=	-1;
 		m_nSkillID			=	0;
 		m_nCount			=	0;
 		m_bCosSelf			=	0;
 		m_nReqSkill			=	-1;
 		m_nReqSkillLevel	=	byte.MaxValue;
 		m_TargetType		=	0;
 	}

#region ClassCanbeSerialized
    public int getSize()
    {
        return sizeof(byte)*6+sizeof(short)+sizeof(int)*5+sizeof(uint);
    }
    public bool readFromBuff(ref NetInputBuffer buff)
    {
        buff.ReadByte(ref m_nCount);
        buff.ReadByte(ref m_nLevel);
        buff.ReadByte(ref m_nLayedNum);
        buff.ReadByte(ref m_nReqSkillLevel);
        buff.ReadShort(ref m_nCurPinJiValue);
        buff.ReadUint(ref m_nBasePrice);
        buff.ReadInt(ref m_nScriptID);
        buff.ReadInt(ref m_nSkillID);
        buff.ReadInt(ref m_bCosSelf);
        buff.ReadInt(ref m_nReqSkill);
        buff.ReadByte(ref m_TargetType);
        buff.ReadByte(ref m_nGrade);
        buff.ReadInt(ref m_nRecipeId);
        return true;
    }
    public int writeToBuff(ref NetOutputBuffer buff)
    {
        return getSize();
    }
#endregion
 };

[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class CHARM_INFO : ClassCanbeSerialized
{
    public _ITEM_ATTR m_Attr;

    public uint m_nBasePrice;

    public byte m_nCount;	   //��ǰ����

    public int GetTileNum() { return m_nCount; }


    public void CleanUp()
    {
        m_nBasePrice = 0;
        m_nCount = 0;
    }

    #region ClassCanbeSerialized
    public int getSize()
    {
        return m_Attr.getSize() + sizeof(byte) + sizeof(uint);
    }
    public bool readFromBuff(ref NetInputBuffer buff)
    {
        m_Attr.readFromBuff(ref buff);
        buff.ReadUint(ref m_nBasePrice);
        buff.ReadByte(ref m_nCount);
        return true;
    }
    public int writeToBuff(ref NetOutputBuffer buff)
    {
        return getSize();
    }
    #endregion
};
 
 public class EQUIP_INFO 
 {
 	public short			m_SetNum;					//��װ���
 	public uint				m_BasePrice;				//�۳��۸�
 	public byte				m_MaxNum;					//��װ
 	public byte				m_EquipPoint;				//��Ʒװ���
 	public byte				m_MaxDurPoint ;				//����;�ֵ
 	public byte				m_NeedLevel ;				//����ȼ�
 	public byte				m_nDangCi ;					////m_nDangCi[9/16/2011 zzh+]
 	public int				m_nNextDangCiItemSN;		////[9/16/2011 zzh+]
    public int     			m_nScriptID;                //�ű�ID [2011-7-28] by: cfp+
    public short   			m_nSkillID;                 //����ID [2011-7-28] by: cfp+
 	//////////////////////////////////////////////////////////////////////////
 	//����Ϊ�̶�����
 	public byte				m_GemMax ;					//���ʯ����
    public byte             m_EquipEnhanceLevel ;       //ǿ���ȼ� [2011-7-19] by: cfp+
 	public byte				m_FaileTimes ;				//����ʧ�ܴ���
 	public byte				m_CurDurPoint ;				//��ǰ�;�ֵ
 	public byte				m_CurSoulType ;					////��ӡ����[9/16/2011 zzh+]
 	public ushort			m_CurDamagePoint;			//��ǰ���˶�
 	public byte				m_AttrCount;				//���Ե�����
 	public byte				m_StoneCount;				//��Ƕ��ʯ������
 	public _ITEM_ATTR[]		m_pAttr = new _ITEM_ATTR[GAMEDEFINE.MAX_ITEM_ATTR];		//��Ʒ����
 	public _ITEM_GEMINFO[]	m_pGemInfo = new _ITEM_GEMINFO[GAMEDEFINE.MAX_ITEM_GEM] ;	//��Ʒ����Ƕ�ı�ʯ
 	
     //ǿ���ȼ� [2011-9-7] by: cfp+
     //תΪ�����ƣ�ǰ4λ�洢����ʧ�ܵ����ǿ���ȼ���
     //��4λ�洢���ǵ�ǰ��ǿ���ȼ�
     public void SetEnhanceLevel(byte ucLevel)
     {
         m_EquipEnhanceLevel = (byte)((m_EquipEnhanceLevel & 0xF0) |( ucLevel & 0x0F ));
     }
     public byte GetEnhanceLevel()
     {
         return (byte)( m_EquipEnhanceLevel & 0x0F );
     }
 
     public void SetEnhanceFailLevel( byte ucLevel )
     {
         m_EquipEnhanceLevel = (byte)(( m_EquipEnhanceLevel & 0x0F ) | ( (ucLevel & 0x0F ) <<4 ));
     }
     public byte GetEnhanceFailLevel( )
     {
         return (byte)( m_EquipEnhanceLevel>>4 );
     }
 
 	//BOOL				CanUseSkill();
 	//WORD				GetUseSkillId();
 
 	//BOOL				CanRandSkill();
 	//WORD				GetRandSkillId();
 
 };
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class TALISMAN_INFO: ClassCanbeSerialized
{
     public _ITEM_ATTR m_Attr;
     public uint m_uPrice;
     public uint m_uBaseExp;  //���Ժ���
     public uint m_uCurExp;   //��ǰ����

#region ClassCanbeSerialized
     public int getSize()
     {
         return sizeof(uint)*3 + m_Attr.getSize();
     }
     public bool readFromBuff(ref NetInputBuffer buff)
     {
         m_Attr.readFromBuff(ref buff);
         buff.ReadUint(ref m_uPrice);
         buff.ReadUint(ref m_uBaseExp);
         buff.ReadUint(ref m_uCurExp);
         return true;
     }
     public int writeToBuff(ref NetOutputBuffer buff)
     {
         m_Attr.writeToBuff(ref buff);
         buff.WriteUint(m_uPrice);
         buff.WriteUint(m_uBaseExp);  //���Ժ���
         buff.WriteUint(m_uCurExp);   //��ǰ����
         return getSize();
     }
#endregion
};

//��Ʒ����ֵ
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)] //��1�ֽڶ���
public class _TALISMAN_ITEM : ClassCanbeSerialized
{
    public byte m_nIndex;
    public _ITEM m_nItemData = new _ITEM();
    #region ClassCanbeSerialized
    public int getSize()
    {
        return sizeof(byte) + m_nItemData.getSize();
    }
    public bool readFromBuff(ref NetInputBuffer buff)
    {
        if(buff.ReadByte(ref m_nIndex)!= sizeof(byte))return false;
        if(!m_nItemData.readFromBuff(ref buff))return false;
        return true;
    }
    public int writeToBuff(ref NetOutputBuffer buff)
    {
        return  getSize();
    }
    #endregion
    public static int getMaxSize()
    {
        return sizeof(byte) + _ITEM.GetMaxSize();
    }
};
// 
// 
// #define MAX_ITEM_LIST_COUNT			8
// 
// public struct _ITEM_LIST 
// {
// 	INT				m_ListCount;
// 	_ITEM_TYPE		m_ListType[MAX_ITEM_LIST_COUNT];
// 	INT				m_TypeCount[MAX_ITEM_LIST_COUNT];
// 
// 	_ITEM_LIST()
// 	{
// 		memset(this,0,sizeof(_ITEM_LIST));
// 	}
// 	
// 	VOID	AddType(_ITEM_TYPE it,INT Count)
// 	{
// 		Assert(Count>0);
// 		Assert(!it.isNull());
// 		
// 		BOOL bFindSame = FALSE;
// 		for(INT i=0;i<m_ListCount;i++)
// 		{
// 			if(m_ListType[i]==it)
// 			{
// 				m_TypeCount[i]+=Count;
// 				bFindSame = TRUE;
// 				break;
// 			}
// 		}
// 
// 		if(!bFindSame)
// 		{
// 			m_TypeCount[m_ListCount] = Count;
// 			m_ListType[m_ListCount]  = it;	
// 			m_ListCount++;
// 		}
// 	}
// 	
// 	VOID Init()
// 	{
// 		memset(this,0,sizeof(_ITEM_LIST));
// 	}
// };
// 
// 
// //��Ʒ��Ϣ, ����������Ʒ����������
// #define	ITEMISVALID(ITEM) (ITEM.m_ItemIndex != 0 )//��Ʒ�Ƿ�Ϸ�
// #define	ITEMPTRISVALID(PITEM) ( (PITEM == NULL) ? FALSE:PITEM->m_ItemIndex )//��Ʒָ���Ƿ�Ϸ�
// #define	ITEMREFPTRISVALID(PITEM) ( (PITEM == NULL) ? FALSE:PITEM->GetItemTableIndex() )//��Ʒָ���Ƿ�Ϸ�
// 
// #define MAX_FIX_ATTR_LENGTH		1000 //100�������� [2011-9-28] by: cfp+
// #define MAX_PET_SKILL_LENGTH	50
// #define MAX_ITEM_CREATOR_NAME	13
// #define MAX_ITEM_PARAM			3
// #define MAX_ITEM_STRING_LENGTH	255
// 
// 
public class ItemDefine
{
    public  readonly static int MAX_ITEM_CREATOR_NAME =13;
    public  readonly static int MAX_ITEM_PARAM = 3;
}
public enum ItemParamValue
{
	IPV_CHAR	=	0,
	IPV_SHORT	=	1,
	IPV_INT		=	2	
};
 
 public class _ITEM : ClassCanbeSerialized 
 {
     // ��Ʒ��ȫ����ˮ by: Allan 9-12-2010
    public _ITEM_GUID			m_ItemGUID ;				//��Ʒ�̶���Ϣ�����ܸı䣩
    public uint m_ItemIndex;				//��Ʒ����
                                                    /*
                                                    |	    1 ~ 10000		��ɫװ��
                                                    |	10001 ~ 20000		��ɫװ��	
                                                    |	20001 ~ 30000		��ɫװ��
                                                    |	30001 ~ 35000		ҩƿ
                                                    |	35001 ~ 40000		��ʯ
                                                    |						...
                                                    */

    public byte m_RulerID;
    public byte m_nsBind;
    public byte[] m_Creator = new byte[ItemDefine.MAX_ITEM_CREATOR_NAME];
    public byte[] m_Param = new byte[ItemDefine.MAX_ITEM_PARAM * sizeof(int)];

    public EQUIP_INFO m_Equip = null;
    public GEM_INFO m_Gem     = null;
    public MEDIC_INFO m_Medic = null;
    public TALISMAN_INFO m_Talisman = null;
    public CHARM_INFO m_Charm = null;//��ӡ zzy
        //STORE_MAP_INFO		m_StoreMap;

  	
    public byte ItemClass( )
    {
        return (byte)(m_ItemIndex / 10000000);
    }
 
    public byte ItemType()
    {
        return (byte)((m_ItemIndex % 100000) / 1000);
    }
 
    public byte GetQual()
    {
        return (byte)((m_ItemIndex % 10000000) / 100000);
    }
 
    public uint	GetIndex()	//  [2010-12-15] by: cfp+
    {
        return (m_ItemIndex % 1000);
    }
 
    public EQUIP_INFO GetEquipData()
    {
        if (m_Equip == null)
            m_Equip = new EQUIP_INFO();
        return	m_Equip;
    }
 
    public GEM_INFO GetGemData()
    {
        if (m_Gem == null)
            m_Gem = new GEM_INFO();
        return	m_Gem;
    }
 
    public MEDIC_INFO GetMedicData()
    {
        if (m_Medic == null)
            m_Medic = new MEDIC_INFO();
        return m_Medic;
    }

    public CHARM_INFO GetCharmData()
    {
        if (m_Charm == null)
            m_Charm = new CHARM_INFO();
        return m_Charm;
    }
    //STORE_MAP_INFO*	GetStoreMapData()	const
    //{
    //    return (STORE_MAP_INFO*)(&m_StoreMap);
    //}
    //////////////////////////////////////////////////////////////////////////
    // ��Ʒ��������
    // �ܵ�����Ʒ		���� ʵ�ʴ�С
    // ���ܵ�����Ʒ		���� 1
    //
    public byte	GetItemCount()
    {
        	ITEM_CLASS bClass = (ITEM_CLASS)ItemClass();

	        if(bClass==ITEM_CLASS.ICLASS_EQUIP)
	        {
		        return 1;
	        }
	        else if(bClass==ITEM_CLASS.ICLASS_GEM)
	        {
		        return 1;
	        }
	        else if(bClass==ITEM_CLASS.ICLASS_COMITEM)
	        {
		        return GetMedicData().m_nCount;	
	        }
	        else if(bClass==ITEM_CLASS.ICLASS_MATERIAL)
	        {
		        return GetMedicData().m_nCount;	
	        }
	        else if(bClass == ITEM_CLASS.ICLASS_TASKITEM)
	        {
		        return GetMedicData().m_nCount;	
	        }
            else if (bClass == ITEM_CLASS.ICLASS_SYMBOLITEM)
            {
                return 1;
            }
	        else
		        return 0;
    }
 
    public void	SetItemCount(int nCount)
    {
        ITEM_CLASS bClass = (ITEM_CLASS)ItemClass();

	    if(bClass==ITEM_CLASS.ICLASS_COMITEM)
	    {
		    GetMedicData().m_nCount	=	(byte)nCount;	
	    }
	    else if(bClass==ITEM_CLASS.ICLASS_MATERIAL)
	    {

		    GetMedicData().m_nCount	=	(byte)nCount;	
	    }
	    else if(bClass==ITEM_CLASS.ICLASS_TASKITEM)
	    {
		    GetMedicData().m_nCount	=	(byte)nCount;	
	    }
	    else
	    {
		    return;
	    }
    }
 
    public byte	GetItemTileMax()
    {
        ITEM_CLASS	bClass = (ITEM_CLASS)ItemClass();
	    switch(bClass) 
	    {
	    case ITEM_CLASS.ICLASS_EQUIP:
		    return 1;
	    case ITEM_CLASS.ICLASS_GEM:
		    return 1;
	    case ITEM_CLASS.ICLASS_COMITEM:
	    case ITEM_CLASS.ICLASS_MATERIAL:
	    case ITEM_CLASS.ICLASS_TASKITEM:
		    {
			    return GetMedicData().m_nLayedNum;
		    }
	    case ITEM_CLASS.ICLASS_STOREMAP:
		    {
			    return 1;
		    }
        case ITEM_CLASS.ICLASS_SYMBOLITEM:
            {
                return 1;
            }
	    default:
		    {
			    return 0;
		    }
	    }
    }
 	
    public bool isFullTile()
    {
        return GetItemCount()>=GetItemTileMax();
    }
 
 
    //���ӵ�������
    public bool	IncCount(uint nCount)
    {
        ITEM_CLASS	bClass = (ITEM_CLASS)ItemClass();

	    switch(bClass)
        {
	    case ITEM_CLASS.ICLASS_EQUIP:
		    {
			    return false;
		    }
	    case ITEM_CLASS.ICLASS_GEM:
		    {
			    return false;
		    }
	    case ITEM_CLASS.ICLASS_COMITEM:
	    case ITEM_CLASS.ICLASS_MATERIAL:
	    case ITEM_CLASS.ICLASS_TASKITEM:
		    {

			    GetMedicData().m_nCount+=(byte)nCount;	
			    return true;
		    }
        case ITEM_CLASS.ICLASS_SYMBOLITEM:
            {
                return false;
            }
	    default:
		    return false;
	    }
    }
    //���ٵ�������
    public bool	DecCount(uint nCount )
    {
        ITEM_CLASS	bClass = (ITEM_CLASS)ItemClass();
	    switch(bClass) 
	    {
	    case ITEM_CLASS.ICLASS_EQUIP:
		    {
			    return false;
		    }
	    case ITEM_CLASS.ICLASS_GEM:
		    {
			    return false;
		    }
	    case ITEM_CLASS.ICLASS_TASKITEM:
	    case ITEM_CLASS.ICLASS_MATERIAL:
	    case ITEM_CLASS.ICLASS_COMITEM:
		    {

			    GetMedicData().m_nCount-=(byte)nCount;	
			    return true;
             }
        case ITEM_CLASS.ICLASS_SYMBOLITEM:
            {
                return false;
            }
	    default:
		    return false;
	    }
    }
 
 
    public bool	IsNullType()
    {
        return m_ItemIndex == 0;
    }
 	
#region ClassCanbeSerialized members
        public int getSize()
        {
            int CreatorSize = GetCreatorVar() ? (sizeof(byte) * ItemDefine.MAX_ITEM_CREATOR_NAME) : 0;
            ITEM_CLASS IC = (ITEM_CLASS)ItemClass();
            if (IC == ITEM_CLASS.ICLASS_EQUIP)
            {

                return m_ItemGUID.getSize()+	//// _ITEM.m_ItemGUID
                    sizeof(uint) +		//// _ITEM.m_ItemIndex
                    sizeof(byte) +		//// _ITEM.m_RulerID
                    sizeof(byte) +		//// _ITEM.m_nsBind
                    sizeof(int) * ItemDefine.MAX_ITEM_PARAM +	//// _ITEM.m_Param
                    //////////////////////////////////////////////////////////////////////////
                    sizeof(short) * 2 +								//// SHORT+USHORT (m_SetNum;m_CurDamagePoint)
                    sizeof(uint) * 2 +								//// m_BasePrice;m_nNextDangCiItemSN;;;
                    m_Equip.m_pAttr[0].getSize() * m_Equip.m_AttrCount +		//// _ITEM.m_pAttr
                    m_Equip.m_pGemInfo[0].getSize() * GAMEDEFINE.MAX_ITEM_GEM +  /*it.m_Equip.m_StoneCount+*/ // _ITEM.m_pGemInfo��Ϊ��Ƕ��Ҫ����ҪMAX_ITEM_GEM�� [2011-9-28] by: cfp+						
                    sizeof(byte) * 12 +								//// (m_MaxNum,m_EquipPoint,m_MaxDurPoint,m_NeedLevel,m_nDangCi,
                    //// m_GemMax,m_EquipEnhanceLevel,m_FaileTimes,m_CurDurPoint, m_AttrCount,m_StoneCount, m_CurSoulType)

                    CreatorSize;										//// _ITEM.m_Creator

            }
            else if (IC == ITEM_CLASS.ICLASS_GEM)
            {
                return m_ItemGUID.getSize() +
                        sizeof(uint) +
                        sizeof(byte) +
                        sizeof(byte) +
                        sizeof(int) * ItemDefine.MAX_ITEM_PARAM +
                        CreatorSize +
                        m_Gem.getSize();
            }
            else if (IC == ITEM_CLASS.ICLASS_COMITEM)
            {

                return m_ItemGUID.getSize() +
                    sizeof(uint) +
                    sizeof(char) +
                    sizeof(char) +
                    sizeof(int) * ItemDefine.MAX_ITEM_PARAM +
                    CreatorSize +
                    m_Medic.getSize();
            }
            else if (IC == ITEM_CLASS.ICLASS_MATERIAL)
            {
                return m_ItemGUID.getSize() +
                    sizeof(uint) +
                    sizeof(char) +
                    sizeof(char) +
                    sizeof(int) * ItemDefine.MAX_ITEM_PARAM +
                    CreatorSize +
                    m_Medic.getSize();
            }
            else if (IC == ITEM_CLASS.ICLASS_TASKITEM)
            {
                return m_ItemGUID.getSize() +
                    sizeof(uint) +
                    sizeof(char) +
                    sizeof(char) +
                    sizeof(int) * ItemDefine.MAX_ITEM_PARAM +
                    CreatorSize +
                    m_Medic.getSize();
            }
            else if (IC == ITEM_CLASS.ICLASS_TALISMAN)
            {
                return m_ItemGUID.getSize() +
                                sizeof(uint) +
                                sizeof(char) +
                                sizeof(char) +
                                sizeof(int) * ItemDefine.MAX_ITEM_PARAM +
                                CreatorSize +
                                m_Talisman.getSize();
            }
            else if(IC == ITEM_CLASS.ICLASS_SYMBOLITEM)
            {
                return m_ItemGUID.getSize() +
                        sizeof(uint) +
                        sizeof(byte) +
                        sizeof(byte) +
                        sizeof(int) * ItemDefine.MAX_ITEM_PARAM +
                        CreatorSize +
                        m_Charm.getSize();
            }
            //else if (it.ItemClass() == ICLASS_STOREMAP)
            //{
            //    return sizeof(_ITEM_GUID) +
            //        sizeof(UINT) +
            //        sizeof(CHAR) +
            //        sizeof(CHAR) +
            //        sizeof(INT) * MAX_ITEM_PARAM +
            //        CreatorSize +
            //        sizeof(STORE_MAP_INFO);

            //}
            else
                return m_ItemGUID.getSize() +
                        sizeof(uint) +
                        sizeof(byte) +
                        sizeof(int) * ItemDefine.MAX_ITEM_PARAM +
                        CreatorSize +
                        sizeof(byte);

        }

        public static int GetMaxSize()
        {
            int CreatorSize =  sizeof(byte) * ItemDefine.MAX_ITEM_CREATOR_NAME;


            return _ITEM_GUID.GetMaxSize() +	//// _ITEM.m_ItemGUID
                    sizeof(uint) +		//// _ITEM.m_ItemIndex
                    sizeof(byte) +		//// _ITEM.m_RulerID
                    sizeof(byte) +		//// _ITEM.m_nsBind
                    sizeof(int) * ItemDefine.MAX_ITEM_PARAM +	//// _ITEM.m_Param
                    //////////////////////////////////////////////////////////////////////////
                    sizeof(short) * 3 +								//// SHORT+USHORT (m_SetNum;m_CurDamagePoint)
                    sizeof(uint) * 3 +								//// m_BasePrice;m_nNextDangCiItemSN;;;
                    _ITEM_ATTR.getMaxSize() * GAMEDEFINE.MAX_ITEM_ATTR +		//// _ITEM.m_pAttr 
                    _ITEM_GEMINFO.getMaxSize() * GAMEDEFINE.MAX_ITEM_GEM +  /*it.m_Equip.m_StoneCount+*/ // _ITEM.m_pGemInfo��Ϊ��Ƕ��Ҫ����ҪMAX_ITEM_GEM�� [2011-9-28] by: cfp+						
                    sizeof(byte) * 12 +								//// (m_MaxNum,m_EquipPoint,m_MaxDurPoint,m_NeedLevel,m_nDangCi,
                    //// m_GemMax,m_EquipEnhanceLevel,m_FaileTimes,m_CurDurPoint, m_AttrCount,m_StoneCount, m_CurSoulType)

                    CreatorSize;										//// _ITEM.m_Creator

        }

        public bool readFromBuff(ref NetInputBuffer buff)
        {

            m_ItemGUID.readFromBuff(ref buff);
            buff.ReadUint(ref m_ItemIndex);
            buff.ReadByte(ref m_RulerID);
            buff.ReadByte(ref m_nsBind);
            buff.Read(ref m_Param, ItemDefine.MAX_ITEM_PARAM*sizeof(int));
     
	        if(GetCreatorVar())
	        {
                buff.Read(ref m_Creator, ItemDefine.MAX_ITEM_CREATOR_NAME);
	        }
	        if((ITEM_CLASS)ItemClass()==ITEM_CLASS.ICLASS_EQUIP)
	        {
                if (m_Equip == null)
                    m_Equip = new EQUIP_INFO();
                buff.ReadByte(ref m_Equip.m_CurDurPoint);
                buff.ReadByte(ref m_Equip.m_CurSoulType);
                short damage=0;
                buff.ReadShort(ref damage);
                m_Equip.m_CurDamagePoint = (ushort) damage;
                buff.ReadByte(ref m_Equip.m_MaxDurPoint);
                buff.ReadUint(ref m_Equip.m_BasePrice);
                buff.ReadByte(ref m_Equip.m_EquipPoint);
                buff.ReadByte(ref m_Equip.m_AttrCount);
                buff.ReadShort(ref m_Equip.m_SetNum);
                buff.ReadByte(ref m_Equip.m_MaxNum);
		        if(m_Equip.m_AttrCount>GAMEDEFINE.MAX_ITEM_ATTR)		
			        m_Equip.m_AttrCount	= GAMEDEFINE.MAX_ITEM_ATTR;

		        for(int j = 0;j<m_Equip.m_AttrCount;j++)
		        {
                    m_Equip.m_pAttr[j].readFromBuff(ref buff);
		        }
                buff.ReadByte(ref m_Equip.m_StoneCount);
                if(m_Equip.m_StoneCount> GAMEDEFINE.MAX_ITEM_GEM)
                    m_Equip.m_StoneCount = GAMEDEFINE.MAX_ITEM_GEM;


                //��Ƕϵͳ�޸ģ��������MAX_ITEM_GEM�� [2011-9-28] by: cfp+
                for(int j = 0;j<GAMEDEFINE.MAX_ITEM_GEM;j++)
		        {
                    m_Equip.m_pGemInfo[j].readFromBuff(ref buff);
		        }

                buff.ReadByte(ref m_Equip.m_NeedLevel);
                buff.ReadByte(ref m_Equip.m_GemMax);
                buff.ReadByte(ref m_Equip.m_nDangCi);
                buff.ReadInt( ref m_Equip.m_nNextDangCiItemSN);
                buff.ReadByte(ref m_Equip.m_FaileTimes);
                buff.ReadByte(ref m_Equip.m_EquipEnhanceLevel);
	        }
	        else if((ITEM_CLASS)ItemClass()==ITEM_CLASS.ICLASS_GEM)
	        {
                if (m_Gem == null) m_Gem = new GEM_INFO();
                m_Gem.readFromBuff(ref buff);
	        }
	        else if((ITEM_CLASS)ItemClass()==ITEM_CLASS.ICLASS_COMITEM)
	        {
                if (m_Medic == null) m_Medic = new MEDIC_INFO();
                m_Medic.readFromBuff(ref buff);
	        }
	        else if((ITEM_CLASS)ItemClass()==ITEM_CLASS.ICLASS_MATERIAL)
	        {
                if (m_Medic == null) m_Medic = new MEDIC_INFO();
                m_Medic.readFromBuff(ref buff);
	        }
	        else if((ITEM_CLASS)ItemClass()==ITEM_CLASS.ICLASS_TASKITEM)
	        {
                if (m_Medic == null) m_Medic = new MEDIC_INFO();
                m_Medic.readFromBuff(ref buff);
	        }
	        else if((ITEM_CLASS)ItemClass()==ITEM_CLASS.ICLASS_STOREMAP)
	        {
		        //iStream.Read((CHAR*)(GetStoreMapData()),sizeof(STORE_MAP_INFO));
	        }
            else if ((ITEM_CLASS)ItemClass() == ITEM_CLASS.ICLASS_TALISMAN)
            {
            	if(m_Talisman == null) m_Talisman =  new TALISMAN_INFO();
                m_Talisman.readFromBuff(ref buff);
            }
            else if ((ITEM_CLASS)ItemClass() == ITEM_CLASS.ICLASS_SYMBOLITEM )
            {
                if (m_Charm == null) m_Charm = new CHARM_INFO();
                m_Charm.readFromBuff(ref buff);
            }
            return true;
        }

        public int writeToBuff(ref NetOutputBuffer buff)
        {
            return getSize();
        }
#endregion

 	
    public bool	GetItemBind()
    {
       if( (m_nsBind & (int)ITEM_EXT_INFO.IEI_BIND_INFO )!= 0)
		    return true;
	   return false;
    }
    public void	SetItemBind(bool bBind)
    {
        if(bBind)
		    m_nsBind |= (byte)ITEM_EXT_INFO.IEI_BIND_INFO;
	    else
        {
            int n = ~(int)ITEM_EXT_INFO.IEI_BIND_INFO;
            m_nsBind &= (byte)n;//�������һ�㲻��ִ��
        }
    }
 
    public bool	GetItemIdent()
    {
        if( (m_nsBind & (int)ITEM_EXT_INFO.IEI_IDEN_INFO) != 0)
		    return true;
	    return false;
    }
    public void	SetItemIdent(bool bIdent)
    {
        if(bIdent)
		    m_nsBind |= (byte)ITEM_EXT_INFO.IEI_IDEN_INFO;
	    else
        {
            int n = ~(int)ITEM_EXT_INFO.IEI_IDEN_INFO;
            m_nsBind &= (byte)n;//�������һ�㲻��ִ��
        }
    }
 
    public bool	GetItemPLock()	//��������
    {
        if( (m_nsBind & (int)ITEM_EXT_INFO.IEI_PLOCK_INFO) != 0)
		    return true;
	    return false;
    }
    public void	SetItemPLock(bool bLock)
    {
        if(bLock)
		    m_nsBind |= (byte)ITEM_EXT_INFO.IEI_PLOCK_INFO;
	    else
        {
            int n = ~(int)ITEM_EXT_INFO.IEI_PLOCK_INFO;
            m_nsBind &= (byte)n;//�������һ�㲻��ִ��
        }
    }
 	
    public int	GetItemFailTimes()
    {
        return m_Equip.m_FaileTimes;
    }
    public void	SetItemFailTimes(int nTimes)
    {
        m_Equip.m_FaileTimes = (byte)nTimes;
    }
     
     //{----------------------------------------------------------------------------
     // [2011-7-19] by: cfp+ װ��ǿ�����
     public byte	GetEquipEnhanceLevel()
     {
         return m_Equip.GetEnhanceLevel();
     }
     public void	SetEquipEnhanceLevel(byte nLevel)
     {
          m_Equip.SetEnhanceLevel(nLevel) ;
     }
 
     public byte	GetEquipEnhanceFailLevel()
     {
          return m_Equip.GetEnhanceFailLevel();
     }
     public void	SetEquipEnhanceFailLevel(byte nLevel)
     {
         m_Equip.SetEnhanceFailLevel(nLevel) ;
     }
     //----------------------------------------------------------------------------}
    //������
    public bool	GetCreatorVar()
    {
        if( (m_nsBind & (int)ITEM_EXT_INFO.IEL_CREATOR)!=0)
		    return true;
	    return false;
    }
    public void	SetCreatorVar(bool bCreator)
    {
        if(bCreator)
		    m_nsBind |= (byte)ITEM_EXT_INFO.IEL_CREATOR;
	    else
        {
            int n = ~(int)ITEM_EXT_INFO.IEL_CREATOR;
            m_nsBind &= (byte)n;//�������һ�㲻��ִ��
        }
    }
 
    public int	GetItemParamValue(uint Start, ref ItemParamValue ipv)
    {
        int TotalSize = sizeof(int)*ItemDefine.MAX_ITEM_PARAM;
	    int nSize;
	    switch(ipv)
	    {
	    case ItemParamValue.IPV_CHAR:
		    {
			    nSize = 1;
			    if( Start <= ((uint)TotalSize - (uint)nSize) )
			    {
                    return m_Param[Start];
			    }
		    }
		    break;
	    case ItemParamValue.IPV_SHORT:
		    {
			    nSize = 2;
			    if( Start <= ( (uint)TotalSize - (uint)nSize ) )
			    {
				   return BitConverter.ToInt16(m_Param, (int)Start);
			    }
			
		    }
		    break;
	    case ItemParamValue.IPV_INT:
		    {
			    nSize = 4;
			    if( Start <= ( (uint)TotalSize - (uint)nSize ) )
			    {
				    return BitConverter.ToInt32(m_Param, (int)Start);
			    }
		    }
		    break;
	    default:
		    break;
	    }

	    return -1;
    }
    public void	SetItemParam(uint start,ref ItemParamValue ipv,int value)
    {
        int TotalSize = sizeof(int)*ItemDefine.MAX_ITEM_PARAM;
	    int size;

	    switch(ipv)
	    {
	    case ItemParamValue.IPV_CHAR:
		    {
			    size = 1;
			    if( start <= ( (uint)TotalSize - (uint)size ) )
			    {
				    m_Param[start] = (byte)(value);
			    }
		    }
		    break;
	    case ItemParamValue.IPV_SHORT:
		    {
			    size = 2;
			    if(start<=((uint)TotalSize - (uint)size))
			    {
				    byte[] b = BitConverter.GetBytes(value);
                    Array.Copy(b, 0, m_Param, start, b.Length);
			    }
		    }
		    break;
	    case ItemParamValue.IPV_INT:
		    {
			    size = 4;
			    if(start<=((uint)TotalSize - (uint)size))
			    {
				    byte[] b = BitConverter.GetBytes(value);
                    Array.Copy(b, 0, m_Param, start, b.Length);
			    }
		    }
		    break;
	    default:
		    break;
	    }                         
    }
 };
// 
// //pItem			��Ҫת����_ITEM
// //pOut			ת�������������
// //OutLength		������ݵĳ���
// //BuffLength	pOut ��ʵ�ʳ���
// BOOL		Item2String(_ITEM* pItem,CHAR* pOut,INT& OutLength,INT BuffLength);
// BOOL		String2Item(CHAR* pIn,INT BuffLength,_ITEM* pItem);
// 
// 
// //A,B ����
// VOID	SwapItem(_ITEM* pItemA,_ITEM* pItemB);
// //A ����B �ռ�
// //ͬʱA ���
// VOID	OverWriteItem(_ITEM* pItemA,_ITEM* pItemB);
// 


// 
// 
// 
// public struct _ITEM_EQUIP : public _ITEM
// {
// 
// 
// 	_ITEM_EQUIP( )
// 	{
// 	};
// };
// 
// public struct _ITEM_EXPEND : public _ITEM
// {
// 
// 	_ITEM_EXPEND( )
// 	{
// 	};
// };
// 
// public struct _ITEM_MATERIAL : public _ITEM
// {
// 
// 	_ITEM_MATERIAL( )
// 	{
// 	};
// };
// 
// public struct _ITEM_GEM : public _ITEM
// {
// 
// 	_ITEM_GEM( )
// 	{
// 	};
// };
// 
// public struct _ITEM_CURIOSA : public _ITEM
// {
// 
// 	_ITEM_CURIOSA( )
// 	{
// 	};
// };
// 
// public struct _ITEM_TALISMAN : public _ITEM
// {
// 
// 	_ITEM_TALISMAN( )
// 	{
// 	};
// };
// 
// public struct _ITEM_GUILDITEM : public _ITEM
// {
// 
// 	_ITEM_GUILDITEM( )
// 	{
// 	};
// };
// 
// public struct _ITEM_TASKITEM : public _ITEM
// {
// 
// 	_ITEM_TASKITEM( )
// 	{
// 	};
// };
// 
// 
// public struct  ItemBoxContaner
// {
// 
// 	ItemBoxContaner(){
// 		memset(m_Item,0,sizeof(_ITEM)*MAX_BOXITEM_NUMBER);
// 		memset(m_nQuality,0,sizeof(INT)*MAX_BOXITEM_NUMBER);
// 		m_nCount = 0;
// 	}
// 
// 	VOID	AddItemType(_ITEM_TYPE& it,INT iQuality)
// 	{
// 		if (m_nCount >= MAX_BOXITEM_NUMBER) return;
// 		m_Item[m_nCount].m_ItemIndex = it.ToSerial();
// 		m_nQuality[m_nCount]	=	iQuality;
// 		m_nCount++;
// 	}
// 	_ITEM				m_Item[MAX_BOXITEM_NUMBER]; 
// 	INT					m_nQuality[MAX_BOXITEM_NUMBER];
// 	INT					m_nCount;
// 	UINT				m_uDropType;
// };
// 
// 
// #endif
