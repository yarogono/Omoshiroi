using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Protocol;
using Server.Game.Object;
using System;

namespace Server.Game.Room
{
    public class GameRoom : JobSerializer
    {
        public int RoomId { get; set; }

        Dictionary<int, Player> _players = new Dictionary<int, Player>();
        Dictionary<int, FarmingBox> _farmingBox = new Dictionary<int, FarmingBox>();

        public void Init()
        {
            FarmingBox farmingBox = ObjectManager.Instance.Add<FarmingBox>();
            farmingBox.items.Add(new FarmingBoxItem() { ItemId = 10001, Quantity = 2 });
            farmingBox.items.Add(new FarmingBoxItem() { ItemId = 10001, Quantity = 1 });
            _farmingBox.Add(farmingBox.Id, farmingBox);
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

                    foreach (var item  in _farmingBox)
                    {
                        S_FarmingBoxSpawn farmingBoxSpawnPacket = new S_FarmingBoxSpawn();
                        ObjectInfo objectInfo = new ObjectInfo();
                        objectInfo.PosInfo = new PositionInfo() { PosX = 2, PosY = 0, PosZ = 2 };
                        objectInfo.ObjectId = item.Key;
                        farmingBoxSpawnPacket.BoxInfos.Add(objectInfo);

                        Broadcast(farmingBoxSpawnPacket);
                    }
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

        public void HandleComboAttack(Player player, C_ComboAttack comboAttackPacket)
        {
            int playerId = player.Id;
            int comboIndex = comboAttackPacket.ComboIndex;
            PositionInfo posInfo = comboAttackPacket.PosInfo;
            DirectionInfo dirInfo = comboAttackPacket.DirInfo;
            
            S_ComboAttack resBattle = new S_ComboAttack() { ObjectId = playerId, ComboIndex = comboIndex };
            resBattle.PosInfo = new PositionInfo() { PosX = posInfo.PosX, PosY = posInfo.PosY, PosZ = posInfo.PosZ };
            resBattle.DirInfo = new DirectionInfo() { DirX = dirInfo.DirX, DirY = dirInfo.DirY, DirZ = dirInfo.DirZ };
            Broadcast(resBattle, playerId);
        }

        public void HandleMakeAttackArea(Player player, C_MakeAttackArea makeAttackAreaPacket)
        {
            int playerId = player.Id;
            int comboIndex = makeAttackAreaPacket.ComboIndex;
            PositionInfo posInfo = makeAttackAreaPacket.PosInfo;
            VelocityInfo velInfo = makeAttackAreaPacket.VelInfo;

            S_MakeAttackArea resMakeAttackArea = new S_MakeAttackArea() { ObjectId = playerId, ComboIndex = comboIndex };
            resMakeAttackArea.PosInfo = new PositionInfo() { PosX = posInfo.PosX, PosY = posInfo.PosY, PosZ = posInfo.PosZ };
            resMakeAttackArea.VelInfo = new VelocityInfo() { VelX = velInfo.VelX, VelY = velInfo.VelY, VelZ = velInfo.VelZ };

            Broadcast(resMakeAttackArea, playerId);
        }

        public void HandleDodge(Player player, C_Dodge dodgePacket)
        {
            int playerId = player.Id;
            PositionInfo posInfo = dodgePacket.PosInfo;
            VelocityInfo velInfo = dodgePacket.VelInfo;

            S_Dodge resDodgePacket = new S_Dodge() { ObjectId = playerId };
            resDodgePacket.PosInfo = new PositionInfo() { PosX = posInfo.PosX, PosY = posInfo.PosY, PosZ = posInfo.PosZ };
            resDodgePacket.VelInfo = new VelocityInfo() { VelX = velInfo.VelX, VelY = velInfo.VelY, VelZ = velInfo.VelZ };

            Broadcast(resDodgePacket, playerId);
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

        public FarmingBox FindFarmingBox(int farmingBoxId)
        {
            FarmingBox findFarmingBox = null;
            _farmingBox.TryGetValue(farmingBoxId, out findFarmingBox);

            return findFarmingBox;
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

        internal void FarmingBoxOpen(Player player, int farmingBoxId)
        {
            FarmingBox farmingBox = FindFarmingBox(farmingBoxId);
            if (farmingBox == null)
                return;

            if (farmingBox.IsOpen == true)
                return;

            S_FarmingBoxOpen resFarmingBoxOpenPacket = new S_FarmingBoxOpen();
            resFarmingBoxOpenPacket.FarmingBoxId = farmingBox.Id;
            resFarmingBoxOpenPacket.IsOpen = farmingBox.IsOpen;

            foreach (FarmingBoxItem item in farmingBox.items)
                resFarmingBoxOpenPacket.Items.Add(item);

            player.Session.Send(resFarmingBoxOpenPacket);

            if (farmingBox.IsOpen == false)
                farmingBox.IsOpen = true;
        }

        internal void FarmingBoxClose(C_FarmingBoxClose farmingBoxClosePacket)
        {
            int farmingBoxId = farmingBoxClosePacket.FarmingBoxId;
            FarmingBox farmingBox = FindFarmingBox(farmingBoxId);
            if (farmingBox == null)
                return;

            farmingBox.items.Clear();
            foreach (var item in farmingBoxClosePacket.Items)
                farmingBox.items.Add(item);

            if (farmingBox.IsOpen == true)
                farmingBox.IsOpen = false;
        }
    }
}
