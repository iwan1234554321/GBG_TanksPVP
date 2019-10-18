using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

namespace GreenButtonGames.TankProject.Actions
{
    [ActionCategory("TankProject")]
    public class GetPlayersCountInCurrentRoom : FsmStateAction
    {
        [Tooltip("True if we are in a room.")]
        public FsmBool inRoom;

        [Tooltip("the number of players inthe room.")]
        public FsmInt playerCount;

        [Tooltip("Предпочтительное количество игроков в комнате")]
        public FsmInt preferPlayerCount;

        [Tooltip("События вызываемые при разных количествах игроков")]
        public FsmEvent allPlayersReady;
        public FsmEvent allPlayersNotReady;

        [Tooltip("Обновлять каждый кадр")]
        public bool everyFrame;

        public override void OnEnter()
        {
            GetRoomInfo();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            GetRoomInfo();

            if (!everyFrame)
            {
                Finish();
            }
        }

        void GetRoomInfo()
        {
            Room _room = PhotonNetwork.CurrentRoom;
            bool _isInRoom = _room != null;
            inRoom.Value = _isInRoom;

            if (_isInRoom == false) return;

            playerCount.Value = _room.PlayerCount;

            if(playerCount.Value == preferPlayerCount.Value)
            {
                Fsm.Event(allPlayersReady);
            }
            else
            {
                Fsm.Event(allPlayersNotReady);
            }
        }
    }
}