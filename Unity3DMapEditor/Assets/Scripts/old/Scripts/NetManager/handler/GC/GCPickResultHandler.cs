

using System;
using System.Collections.Generic;

using Network.Packets;

namespace Network.Handlers
{
    public class GCPickResultHandler: HandlerBase
	{
        public override NET_RESULT_DEFINE.PACKET_EXE Execute(PacketBase Packet, ref Peer pPlayer)
        {
            if (GameProcedure.GetActiveProcedure() == (GameProcedure)GameProcedure.s_ProcMain)
	        {
                LogManager.Log("Receive GCPickResult Packet");
                GCPickResult pPacket = (GCPickResult)Packet;
		        CObjectManager pObjectManager = CObjectManager.Instance;
		        CDataPool pDataPool = GameProcedure.s_pDataPool;
		        //if( pPacket.getItemBoxId() != pDataPool.GetItemBoxID() )
        		
		        bool bLog = (GameProcedure.s_pUISystem != null)?true:false;
		        // ����ÿһ����Ʒ
		        if( pPacket.getResult() == (byte)PickResultCode.PICK_SUCCESS )
		        {
			        int nIndex = 0;
			        CObject_Item pItem = CDataPool.Instance.ItemBox_GetItem(
											        pPacket.getItemID().m_World,
											        pPacket.getItemID().m_Server,
											        pPacket.getItemID().m_Serial,
											        ref nIndex);

			        if(pItem == null) 
			        {
				        if(bLog) GameProcedure.s_pEventSystem.PushEvent( GAME_EVENT_ID.GE_INFO_SELF, "�ڲ����󣬷Ƿ�����ƷGUID!");
				        return NET_RESULT_DEFINE.PACKET_EXE.PACKET_EXE_CONTINUE;
			        }

			        // �ȱ�����Ʒ���ƣ�������ܻ�ɾ������������Ʒ [6/13/2011 ivan edit]
			        string strItemName = pItem.GetName();

			        //���������Ʒת�ƣ�(�����������Ƶ�������),��ı�ͻ���id���Ա�ʾ�ڿͻ����ǲ�ͬ����
                    ObjectSystem.Instance.ChangeItemClientID(pItem);
			        ((CObject_Item)pItem).SetTypeOwner(ITEM_OWNER.IO_MYSELF_PACKET);

			        CObject_Item pItemBag = CDataPool.Instance.UserBag_GetItem(pPacket.getBagIndex());
			        if(pItemBag != null)
			        {//�ж���,Ӧ��һ����һ����Ʒ
				        if(pItem.GetIdTable() == pItemBag.GetIdTable())
				        {//ͬһ����Ʒ
					        //CDataPool.GetMe().ItemBox_SetItem(nIndex, NULL, FALSE);
					        // ��ɾ����й¶�ڴ� [6/10/2011 ivan edit]
					        CDataPool.Instance.ItemBox_SetItem(nIndex, null, true);
				        }
				        else
				        {
                            return NET_RESULT_DEFINE.PACKET_EXE.PACKET_EXE_CONTINUE;
				        }
			        }
			        else
			        {//û����
				        //�������ݳ�
				        CDataPool.Instance.UserBag_SetItem(pPacket.getBagIndex(), pItem, true, true);
                        CDataPool.Instance.ItemBox_SetItem(nIndex, null, false);

                        CEventSystem.Instance.PushEvent(GAME_EVENT_ID.GE_GET_NEWEQUIP, pItem.GetID());
			        }
        			
			        //֪ͨActionButton
			        CActionSystem.Instance.UserBag_Update();
			        CActionSystem.Instance.ItemBox_Update();

			        //����ui�¼�
			        if(bLog)
			        {
                        //ADDTALKMSG(strItemName); //todo
			        }

                    CEventSystem.Instance.PushEvent(GAME_EVENT_ID.GE_LOOT_SLOT_CLEARED);
                    CEventSystem.Instance.PushEvent(GAME_EVENT_ID.GE_PACKAGE_ITEM_CHANGED, pPacket.getBagIndex());

			        //���������գ��ر�
                    if (CDataPool.Instance.ItemBox_GetNumber() == 0)
			        {
                        CEventSystem.Instance.PushEvent(GAME_EVENT_ID.GE_LOOT_CLOSED);
			        }
		        }
		        else
		        {
			        switch( pPacket.getResult() )
			        {
                        case (byte)PickResultCode.PICK_BAG_FULL:
				        {
					        if(bLog) 
					        {
						        GameProcedure.s_pEventSystem.PushEvent( GAME_EVENT_ID.GE_INFO_SELF, ("��������!"));
					        }
					        //CSoundSystemFMod._PlayUISoundFunc(96); todo
				        }
				        break;
                        case (byte)PickResultCode.PICK_INVALID_OWNER:
				        {
					        if(bLog) GameProcedure.s_pEventSystem.PushEvent( GAME_EVENT_ID.GE_INFO_SELF, ("�������Լ��ı���!"));
				        }
				        break;
                        case (byte)PickResultCode.PICK_INVALID_ITEM:
				        {
					        if(bLog) GameProcedure.s_pEventSystem.PushEvent( GAME_EVENT_ID.GE_INFO_SELF, ("�Ƿ���Ʒ!"));
				        }
				        break;
                        case (byte)PickResultCode.PICK_TOO_FAR:
				        {
					        if(bLog) GameProcedure.s_pEventSystem.PushEvent( GAME_EVENT_ID.GE_INFO_SELF, ("����̫Զ!"));
				        }
				        break;
			        default:
				        break;
			        }
		        }
	        }
	        return NET_RESULT_DEFINE.PACKET_EXE.PACKET_EXE_CONTINUE ;
        }
        public override int GetPacketID()
        {
            return (int)PACKET_DEFINE.PACKET_GC_PICKRESULT;
        }
	};

}

