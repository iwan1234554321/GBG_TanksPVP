using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

namespace GreenButtonGames.TankProject.Actions
{
    [ActionCategory("TankProject")]
    public class GetKeyEvent : FsmStateAction
    {
        [RequiredField]
        [ObjectType(typeof(KeyCode))]
        public FsmEnum keyCode;

        public FsmEvent eventTrue;
        public FsmEvent eventFalse;
        [UIHint(UIHint.Variable)]
        public FsmBool storeResult;

        public override void Reset()
        {
            eventTrue = null;
            eventFalse = null;
            keyCode = KeyCode.None;
            storeResult = null;
        }

        public override void OnUpdate()
        {
            bool key = Input.GetKey((KeyCode)keyCode.Value);

            Fsm.Event(key ? eventTrue : eventFalse);

            storeResult.Value = key;
        }
    }
}