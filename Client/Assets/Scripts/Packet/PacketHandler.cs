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

    public static void S_ChangeHpHandler(PacketSession session, IMessage packet) { }

    public static void S_DieHandler(PacketSession session, IMessage packet) { }

    public static void S_SyncHandler(PacketSession session, IMessage packet)
    {
        S_Sync syncPacket = packet as S_Sync;

        Debug.Log($"{syncPacket.Player.ObjectId} Position : {syncPacket.Player.Position}");

        GameObject gameObject = ObjectManager.Instance.FindById(syncPacket.Player.ObjectId);

        if (gameObject == null)
            return;

        if (ObjectManager.Instance.pilotSync.Id == syncPacket.Player.ObjectId)
            return;

        SyncModule syncModule = gameObject.GetComponent<SyncModule>();
        if (syncModule == null)
            return;

        syncModule.Player = syncPacket.Player;
    }

    /// <summary>
    /// 서버에서 받아온 [오브젝트 ID, 최대 체력과 피격 직전 현재 체력, 체력 변동량] 데이터를 이용해 작업을 수행하는 핸들러.
    /// 해당 오브젝트를 식별하고, 그 오브젝트의 체력 상황을 데이터대로 갱신해주면 된다.
    /// </summary>
    /// <param name="session"></param>
    /// <param name="packet"></param>
    public static void S_HpDamageHandler(PacketSession session, IMessage packet)
    {
        S_HpDamage damagePacket = packet as S_HpDamage;

        GameObject gameObject = ObjectManager.Instance.FindById(damagePacket.ObjectId);

        if (gameObject == null)
        {
            return;
        }
        if (ObjectManager.Instance.pilotSync.Id == damagePacket.ObjectId)
        {
            return;
        }

        CharacterStats stats = gameObject.GetComponent<DataContainer>().Stats;

        stats.MaxHp = damagePacket.MaxHp;
        stats.Hp = damagePacket.CurrentHp - damagePacket.ChangeAmount;
    }

    /// <summary>
    /// 서버에서 받아온 [오브젝트 ID, 최대 체력과 피격 직전 현재 체력, 체력 변동량] 데이터를 이용해 작업을 수행하는 핸들러.
    /// 해당 오브젝트를 식별하고, 그 오브젝트의 체력 상황을 데이터대로 갱신해주면 된다.
    /// </summary>
    /// <param name="session"></param>
    /// <param name="packet"></param>
    public static void S_HpRecoveryHandler(PacketSession session, IMessage packet)
    {
        S_HpDamage damagePacket = packet as S_HpDamage;

        GameObject gameObject = ObjectManager.Instance.FindById(damagePacket.ObjectId);

        if (gameObject == null) { return; }
        if (ObjectManager.Instance.pilotSync.Id == damagePacket.ObjectId) { return; }

        CharacterStats stats = gameObject.GetComponent<DataContainer>().Stats;

        stats.MaxHp = damagePacket.MaxHp;
        stats.Hp = damagePacket.CurrentHp - damagePacket.ChangeAmount;
    }
}
