using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Network;
using Network.Handlers;

namespace Network.Packets
{

    public class CGCharAskBaseAttrib : PacketBase
    {
	    //���ü̳нӿ�
        public override bool readFromBuff(ref NetInputBuffer buff)
        {
            buff.ReadUint(ref m_ObjID);
            return true;
        }
        public override int writeToBuff(ref NetOutputBuffer buff)
        {
            buff.WriteUint(m_ObjID);
            return NET_DEFINE.PACKET_HEADER_SIZE + getSize();
        }

	    public override short getPacketID()
        {
            return (short)PACKET_DEFINE.PACKET_CG_CHARASKBASEATTRIB ; 
        }
	     public override int getSize() 
         { 
             return sizeof(uint) ; 
         }
 
	//ʹ�����ݽӿ�
	    public   uint				getTargetID() { return m_ObjID; }
	    public  void				setTargetID(uint idTarget) { m_ObjID = idTarget; }
	    //����
	    uint					m_ObjID;	//�Է���ObjID
    }


    class CGCharAskBaseAttribFactory :  PacketFactory 
    {
   
    	public override   PacketBase		CreatePacket() { return new CGCharAskBaseAttrib() ; }
	    public override int GetPacketID() {  return (short)PACKET_DEFINE.PACKET_CG_CHARASKBASEATTRIB ; }
	    public override int GetPacketMaxSize() { return sizeof(uint) ; }
    }



}
