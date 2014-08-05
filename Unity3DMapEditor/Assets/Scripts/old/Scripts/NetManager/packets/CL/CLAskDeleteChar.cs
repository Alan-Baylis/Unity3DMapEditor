using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Network;
using Network.Handlers;

namespace Network.Packets
{
	public class CLAskDeleteChar  : PacketBase
	{
		//���ü̳нӿ�
		public override bool readFromBuff(ref NetInputBuffer buff){return true;}
		public override int writeToBuff(ref NetOutputBuffer buff)
        {
            buff.WriteUint(m_GUID);
            return NET_DEFINE.PACKET_HEADER_SIZE + getSize();
        }

		public override short getPacketID() { return (short)PACKET_DEFINE.PACKET_CL_ASKDELETECHAR ; }
		public override int getSize()  
		{
			return 	sizeof(uint);

		}

		//ʹ�����ݽӿ�
        public byte[] SzAccount
        {
            get
            {
                return this.szAccount;
            }
            set
            {
                szAccount = value;
            }
        }
        public short PlayerID
        {
            get { return this.playerID; }
            set { playerID = value; }
        }
        public uint GUID
        {
            get { return this.m_GUID; }
            set { m_GUID = value;}
        }
		//����
	
		private uint					m_GUID;

		//��ҳ�id���ͻ��˲�����д
		private short				playerID;
		private byte[]				szAccount = new byte[NET_DEFINE.MAX_ACCOUNT+1] ;	//�û�����

	}

	public class CLAskDeleteCharFactory :  PacketFactory 
	{
		public override PacketBase CreatePacket()  { return new CLAskDeleteChar() ; }
        public override int GetPacketID() { return (short)PACKET_DEFINE.PACKET_CL_ASKDELETECHAR; }
		public override int GetPacketMaxSize()
		{ 
			return 	sizeof(uint);
		}
	}

}