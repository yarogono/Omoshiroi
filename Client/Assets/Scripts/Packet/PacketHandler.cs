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

    public static void S_LeaveGameHandler(PacketSession session, IMessage packet) { }

    public static void S_SpawnHandler(PacketSession session, IMessage packet)
    {
        S_Spawn spawnPacket = packet as S_Spawn;

        foreach (ObjectInfo obj in spawnPacket.Objects)
        {
            Debug.Log(obj.ObjectId);
            ObjectManager.Instance.Add(obj);
        }
    }

    public static void S_DespawnHandler(PacketSession session, IMessage packet) { }

    public static void S_MoveHandler(PacketSession session, IMessage packet)
    {
        S_Move movePacket = packet as S_Move;

        GameObject gameObject = ObjectManager.Instance.FindById(movePacket.ObjectId);

        Debug.Log("check1");

        if (gameObject == null)
        {
            Debug.Log("check2");
            return;
        }

        if (ObjectManager.Instance.pilotPlayerController.Id == movePacket.ObjectId)
        {
            Debug.Log("check3");
            return;
        }

        t_PlayerController playerController = gameObject.GetComponent<t_PlayerController>();
        if (playerController == null)
        {
            Debug.Log("check3");
            return;
        }

        Debug.Log("check4");

        Debug.Log(
            $"{movePacket.ObjectId} => x: {movePacket.PosInfo.PosX} y: {movePacket.PosInfo.PosY} state : {movePacket.PosInfo.State}"
        );
        playerController.PosInfo = movePacket.PosInfo;
        // Debug.Log(
        //     $"{playerController.Id} => x: {playerController.PosInfo.PosX} y: {playerController.PosInfo.PosY} state: {playerController.PosInfo.State}"
        // );
    }

    public static void S_ChangeHpHandler(PacketSession session, IMessage packet) { }

    public static void S_DieHandler(PacketSession session, IMessage packet) { }
}
