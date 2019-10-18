using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

namespace GreenButtonGames.TankProject.Actions
{
    [ActionCategory("TankProject")]
    public class GetKeyTimeIntervalEvent : FsmStateAction
    {
        [HasFloatSlider(0, 10)]
        [Tooltip("Интервал времени инстанциирования объекта")]
        public FsmFloat intervalTimeSpawn = 2;

        [ObjectType(typeof(KeyCode))]
        [Tooltip("Кнопка для спавна объекта")]
        public FsmEnum spawnInput;

        [ActionSection("Событие инстанциирования")]
        public FsmEvent eventInstance;

        [Tooltip("Обновлять каждый кадр")]
        public bool everyFrame;

        private bool onInstance;
        private float timerInstance;

        public override void Reset()
        {
            intervalTimeSpawn = 2;

            spawnInput = KeyCode.None;

            everyFrame = true;
        }

        public override void OnEnter()
        {
            EventObject();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            EventObject();

            if (!everyFrame)
            {
                Finish();
            }
        }
        
        void EventObject()
        {
            if (onInstance)
            {
                timerInstance = 0;

                if (Input.GetKeyDown(spawnInput.Value.ToString().ToLower()))
                {
                    Fsm.Event(eventInstance);

                    onInstance = false;
                }
            }
            else
            {
                if (timerInstance < intervalTimeSpawn.Value)
                {
                    timerInstance += 1 * Time.deltaTime;
                }
                else
                {
                    onInstance = true;
                }
            }
        }
    }
}