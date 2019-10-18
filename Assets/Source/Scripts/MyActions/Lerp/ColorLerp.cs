using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

namespace GreenButtonGames.TankProject.Actions
{
    [ActionCategory("TankProject")]
    public class ColorLerp : FsmStateAction
    {
        public FsmColor colorStart;
        public FsmColor colorEnd;

        public FsmFloat lerpValue;

        public FsmColor storeColor;

        public bool everyFrame;

        public override void OnEnter()
        {
            LerpColor();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            LerpColor();

            if (!everyFrame)
            {
                Finish();
            }
        }

        void LerpColor()
        {
            if(colorStart != null && colorEnd != null && lerpValue != null)
            {
                storeColor.Value = Color.Lerp(colorStart.Value, colorEnd.Value, lerpValue.Value);
            }
        }
    }
}