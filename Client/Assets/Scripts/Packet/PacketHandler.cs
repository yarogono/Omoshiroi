using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using UnityEngine;

public class PacketHandler
{
    public static void S_EnterGameHandler(PacketSession session, IMessage packet)
    {
        S_EnterGame enterGamePacket = packet as S_EnterGame;

        ObjectManager.Instance.Add(enterGamePacket.Player, pilotPlayer: true);
    }

    public static void S_LeaveGameHandler(PacketSession session, IMessage packet)
    {
        S_LeaveGame leaveGamePacket = packet as S_LeaveGame;

        int pilotPlayerId = ObjectManager.Instance.pilotSync.Id;
        if (pilotPlayerId != leaveGamePacket.PlayerId)
            return;

        ObjectManager.Instance.Clear();
    }

    public static void S_SpawnHandler(PacketSession session, IMessage packet)
    {
        S_Spawn spawnPacket = packet as S_Spawn;

        foreach (ObjectInfo obj in spawnPacket.Objects)
            ObjectManager.Instance.Add(obj, pilotPlayer: false);
    }

    public static void S_DespawnHandler(PacketSession session, IMessage packet)
    {
        S_Despawn despawnPacket = packet as S_Despawn;

        foreach (int playerId in despawnPacket.ObjectIds)
            ObjectManager.Instance.Remove(playerId);
    }

    public static void S_MoveHandler(PacketSession session, IMessage packet)
    {
        S_Move movePacket = packet as S_Move;

        GameObject gameObject = ObjectManager.Instance.FindById(movePacket.ObjectId);

        if (gameObject == null)
            return;

        if (ObjectManager.Instance.pilotSync.Id == movePacket.ObjectId)
            return;

        SyncModule syncModule = gameObject.GetComponent<SyncModule>();
        if (syncModule == null)
            return;

        syncModule.PosInfo = movePacket.PosInfo;
    }

    public static void S_ChangeHpHandler(PacketSession session, IMessage packet) { }

    public static void S_DieHandler(PacketSession session, IMessage packet) { }
}
