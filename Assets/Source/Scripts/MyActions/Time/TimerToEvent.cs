using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

namespace GreenButtonGames.TankProject.Actions
{
    [ActionCategory("TankProject")]
    public class TimerToEvent : FsmStateAction
    {
        [TooltipAttribute("Время через которое состояние переключится")]
        public FsmFloat time = 1;

        [TooltipAttribute("Скорость времени")]
        public FsmFloat speedTime = 1;

        [TooltipAttribute("Событие")]
        public FsmEvent eventSwitch;

        private bool onTimer = true;
        private float timer;

        public override void OnEnter()
        {
            onTimer = true;
            timer = time.Value;
        }

        public override void OnUpdate()
        {
            if(onTimer)
            {
                if (timer > 0)
                {
                    timer -= speedTime.Value * Time.deltaTime;
                }
                else
                {
                    Fsm.Event(eventSwitch);
                    timer = time.Value;
                    onTimer = false;
                }
            }
        }
    }
}