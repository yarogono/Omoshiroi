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

        Debug.Log($"{movePacket.ObjectId} Position : {movePacket.PosInfo}");

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
    /// 서버에서 받아온 [오브젝트 ID, 최대 체력과 회복 직전 현재 체력, 체력 변동량] 데이터를 이용해 작업을 수행하는 핸들러.
    /// 해당 오브젝트를 식별하고, 그 오브젝트의 체력 상황을 데이터대로 갱신해주면 된다.
    /// </summary>
    /// <param name="session"></param>
    /// <param name="packet"></param>
    public static void S_HpRecoveryHandler(PacketSession session, IMessage packet)
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
        stats.Hp = damagePacket.CurrentHp + damagePacket.ChangeAmount;
    }

    public static void S_AimHandler(PacketSession session, IMessage packet)
    {
        S_Aim aimPacket = packet as S_Aim;

        GameObject gameObject = ObjectManager.Instance.FindById(aimPacket.ObjectId);

        if (gameObject == null)
        {
            return;
        }
        if (ObjectManager.Instance.pilotSync.Id == aimPacket.ObjectId)
        {
            return;
        }
    }

    public static void S_BattleHandler(PacketSession session, IMessage packet)
    {
        S_Battle battlePacket = packet as S_Battle;

        GameObject gameObject = ObjectManager.Instance.FindById(battlePacket.ObjectId);

        if (gameObject == null)
        {
            return;
        }
        if (ObjectManager.Instance.pilotSync.Id == battlePacket.ObjectId)
        {
            return;
        }
    }

    public static void S_AttackHandler(PacketSession session, IMessage packet)
    {
        S_Attack attackPacket = packet as S_Attack;

        GameObject gameObject = ObjectManager.Instance.FindById(attackPacket.ObjectId);

        if (gameObject == null)
        {
            return;
        }
        if (ObjectManager.Instance.pilotSync.Id == attackPacket.ObjectId)
        {
            return;
        }
    }

    public static void S_DieHandler(PacketSession session, IMessage packet) { }

    // /// <summary>
    // /// 클라이언트 측의 FarmingBox 인벤토리 데이터 요청에 대한 응답을 처리하는 Handler.
    // /// 받아온 인벤토리 데이터를 토대로 해당 object ID 를 가지는 FarmingBox 인벤토리의 데이터를 갱신한다.
    // /// </summary>
    // /// <param name="packet">FarmingBox 의 object ID, Dictionary<int, InventoryItem> 을 포함한다.</param>
    // public static void S_FarmingBoxOpenHandler(PacketSession session, IMessage packet) { }

    // /// <summary>
    // /// 서버 측이 클라이언트에서 받아온 FarmingBox 인벤토리 데이터에 대한 응답을 처리하는 Handler.
    // /// 받아오는 별다른 내용이 없으므로, 딱히 처리할 내용도 없을 듯 하다.
    // /// </summary>
    // public static void S_FarmingBoxCloseHandler(PacketSession session, IMessage packet) { }
}
