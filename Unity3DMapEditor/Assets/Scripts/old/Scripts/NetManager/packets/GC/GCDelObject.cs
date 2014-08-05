using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Network;
using Network.Handlers;

namespace Network.Packets
{
    public class GCDelObject : PacketBase
    {
	    //���ü̳нӿ�
        public override bool readFromBuff(ref NetInputBuffer buff)
        {
            buff.ReadUint(ref m_ObjID);
	        buff.ReadShort(ref m_idScene);
            return true;
        }

        public override int writeToBuff(ref NetOutputBuffer buff)
        {
            buff.WriteUint(m_ObjID);
            buff.WriteShort(m_idScene);
            return NET_DEFINE.PACKET_HEADER_SIZE + getSize();
        }

	    public override short		getPacketID() { return (short)PACKET_DEFINE.PACKET_GC_DELOBJECT; }
	    public override int		getSize() { return	sizeof(uint) + sizeof( short ); }

	    //ʹ�����ݽӿ�
	    public  void			setObjID(uint id) { m_ObjID = id; }
	    public  uint			getObjID() { return m_ObjID; }

	    public  void			setSceneID( short idScene ){ m_idScene = idScene; }
	    public  short		getSceneID(  ){ return m_idScene; }

	    uint			m_ObjID;	// ����Obj���͵�ObjID
	    short		m_idScene;	// ����ID
    };


    public class GCDelObjectFactory :   PacketFactory 
    {
	    public override PacketBase CreatePacket()  { return new GCDelObject() ; }
	    public override int GetPacketID() { return  (short)PACKET_DEFINE.PACKET_GC_DELOBJECT; }
        public override int GetPacketMaxSize() { return sizeof(uint) + sizeof(short); }
    };



}