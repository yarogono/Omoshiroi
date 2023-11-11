using Google.Protobuf;
using Google.Protobuf.Protocol;
using Server.Game.Object;
using System;

namespace Server.Game.Room
{
    public class GameRoom : JobSerializer
    {
        public int RoomId { get; set; }

        Dictionary<int, Player> _players = new Dictionary<int, Player>();


        public void Init(int mapId)
        {
            //EnterGame(monster);
        }

        // 누군가 주기적으로 호출해줘야 한다
        public void Update()
        {
            Flush();
        }

        public void EnterGame(GameObject gameObject)
        {
            if (gameObject == null)
                return;

            GameObjectType type = ObjectManager.GetObjectTypeById(gameObject.Id);

            if (type == GameObjectType.Player)
            {
                Player player = gameObject as Player;
                _players.Add(gameObject.Id, player);

                player.Room = this;

                // 본인한테 정보 전송
                {
                    S_EnterGame enterPacket = new S_EnterGame();
                    enterPacket.Player = player.Info;
                    player.Session.Send(enterPacket);

                    S_Spawn spawnPacket = new S_Spawn();
                    foreach (Player p in _players.Values)
                    {
                        if (player != p)
                            spawnPacket.Objects.Add(p.Info);
                    }

                    player.Session.Send(spawnPacket);
                }

                {
                    S_FarmingBoxSpawn farmingBoxSpawnPacket = new S_FarmingBoxSpawn();
                }
            }

            // 타인한테 정보 전송
            {
                S_Spawn spawnPacket = new S_Spawn();
                spawnPacket.Objects.Add(gameObject.Info);
                Broadcast(spawnPacket, gameObject.Id);
            }
        }

        public void LeaveGame(int objectId)
        {
            GameObjectType type = ObjectManager.GetObjectTypeById(objectId);

            if (type == GameObjectType.Player)
            {
                Player player = null;
                if (_players.Remove(objectId, out player) == false)
                    return;

                player.Room = null;

                // 본인한테 정보 전송
                {
                    S_LeaveGame leavePacket = new S_LeaveGame();
                    leavePacket.PlayerId = player.Id;
                    player.Session.Send(leavePacket);
                }
            }

            // 타인한테 정보 전송
            {
                S_Despawn despawnPacket = new S_Despawn();
                despawnPacket.ObjectIds.Add(objectId);
                Broadcast(despawnPacket, objectId);
            }
        }

        public void HandleMove(Player player, C_Move movePacket)
        {
            if (player == null)
                return;

            // TODO : 검증
            ObjectInfo info = player.Info;

            PositionInfo posInfo = movePacket.PosInfo;
            VelocityInfo velInfo = movePacket.VelInfo;
            info.PosInfo = posInfo;
            info.State = movePacket.State;

            // 다른 플레이어한테도 알려준다
            S_Move resMovePacket = new S_Move();
            resMovePacket.ObjectId = player.Id;
            resMovePacket.PosInfo = new PositionInfo(posInfo);
            resMovePacket.VelInfo = new VelocityInfo(velInfo);
            resMovePacket.State = movePacket.State;

            Broadcast(resMovePacket, player.Id);
        }

        public void HandleAim(Player player, C_Aim aimPacket)
        {
            VelocityInfo velInfo = aimPacket.VelInfo;
            int playerId = player.Id;

            S_Aim resAimPacket = new S_Aim() { ObjectId = playerId, State = aimPacket.State };
            resAimPacket.VelInfo = new VelocityInfo() { VelX = velInfo.VelX, VelY = velInfo.VelY, VelZ = velInfo.VelZ };

            Broadcast(resAimPacket, playerId);
        }

        public void HandleBattle(Player player, C_Battle battlePacket)
        {
            int playerId = player.Id;
            PositionInfo posInfo = battlePacket.PosInfo;
            VelocityInfo velInfo = battlePacket.VelInfo;
            

            S_Battle resBattle = new S_Battle() { ObjectId = playerId, AnimTime = battlePacket.AnimTime, State = battlePacket.State};
            resBattle.PosInfo = new PositionInfo() { PosX = posInfo.PosX, PosY = posInfo.PosY, PosZ = posInfo.PosZ };
            resBattle.VelInfo = new VelocityInfo() { VelX = velInfo.VelX, VelY = velInfo.VelY, VelZ = velInfo.VelZ };
            Broadcast(resBattle, playerId);
        }

        public void HandleAttack(Player player, C_Attack attackPacket)
        {
            int playerId = player.Id;
            PositionInfo posInfo = attackPacket.PosInfo;
            VelocityInfo velInfo = attackPacket.VelInfo;

            S_Attack resAttack = new S_Attack() { ObjectId = playerId, ComboIndex = attackPacket.ComboIndex };
            resAttack.PosInfo = new PositionInfo() { PosX = posInfo.PosX, PosY = posInfo.PosY, PosZ = posInfo.PosZ };
            resAttack.VelInfo = new VelocityInfo() { VelX = velInfo.VelX, VelY = velInfo.VelY, VelZ = velInfo.VelZ };

            Broadcast(resAttack, playerId);
        }


        // TODO
        public Player FindPlayer(Func<GameObject, bool> condition)
        {
            foreach (Player player in _players.Values)
            {
                if (condition.Invoke(player))
                    return player;
            }

            return null;
        }

        public void Broadcast(IMessage packet)
        {
            foreach (Player p in _players.Values)
            {
                p.Session.Send(packet);
            }
        }

        public void Broadcast(IMessage packet, int exceptPlayerId)
        {
            foreach (Player p in _players.Values)
            {
                if (p.Id != exceptPlayerId)
                    p.Session.Send(packet);
            }
        }
    }
}
