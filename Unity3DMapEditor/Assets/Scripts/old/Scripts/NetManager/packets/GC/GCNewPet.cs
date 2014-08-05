
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

using Network;
using Network.Handlers;
namespace Network.Packets
{

	public class GCNewPet : PacketBase
	{
		//���ü̳нӿ�
		public override bool readFromBuff(ref NetInputBuffer buff)
        {
            buff.ReadUint(ref m_ObjID);
            m_posWorld.readFromBuff(ref buff);
            buff.ReadFloat(ref m_fDir);
            buff.ReadFloat(ref m_fMoveSpeed);
            return true;
        }
		public override int writeToBuff(ref NetOutputBuffer buff)
        {
            buff.WriteUint(m_ObjID);
            m_posWorld.writeToBuff(ref buff);
            buff.WriteFloat(m_fDir);
            buff.WriteFloat(m_fMoveSpeed);
            return NET_DEFINE.PACKET_HEADER_SIZE + getSize();
        }

		public override short getPacketID() { return (short)PACKET_DEFINE.PACKET_GC_NEWPET ; }
		public override int getSize()
		{
			return	sizeof(uint) + sizeof(float) + WORLD_POS.GetMaxSize() + sizeof(float);
		}
		//ʹ�����ݽӿ�
		public void				setObjID(uint id) { m_ObjID = id; }
		public uint				getObjID() { return m_ObjID; }

		public void				setWorldPos(ref WORLD_POS pos) { m_posWorld = pos; }
		public  WORLD_POS	getWorldPos() { return m_posWorld; }

		public float				getDir(){ return m_fDir; }
		public void				setDir( float fDir ){ m_fDir = fDir; }

		public void				setMoveSpeed( float fMoveSpeed ){ m_fMoveSpeed = fMoveSpeed; }
		public float				getMoveSpeed(){ return m_fMoveSpeed; }

		uint			m_ObjID;		// ObjID
		WORLD_POS		m_posWorld;		// λ��
		float			m_fDir;			// ����
		float			m_fMoveSpeed;	// �ƶ��ٶ�
	};


	public class GCNewPetFactory :  PacketFactory 
	{
		public override PacketBase CreatePacket() { return new GCNewPet() ; }
        public override int GetPacketID() { return (int)PACKET_DEFINE.PACKET_GC_NEWPET; }
		public override int GetPacketMaxSize()
		{
			return	sizeof(uint) + sizeof(float) + WORLD_POS.GetMaxSize() + sizeof(float);
		}
	}
}