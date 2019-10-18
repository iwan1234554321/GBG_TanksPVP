using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

namespace GreenButtonGames.TankProject.Actions
{
    [ActionCategory("TankProject")]
    public class TimeSpeed : FsmStateAction
    {
        public FsmFloat storeTime;

        public FsmFloat multiple;

        public override void OnUpdate()
        {
            SpeedTime();
        }

        void SpeedTime()
        {
            storeTime.Value = Time.time * multiple.Value;
        }
    }
}