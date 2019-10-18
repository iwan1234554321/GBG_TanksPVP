using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

namespace GreenButtonGames.TankProject.Actions
{
    [ActionCategory("TankProject")]
    public class ObjectInstantiate : FsmStateAction
    {
        public FsmGameObject objectInstance;
        public FsmGameObject objectInstancePoint;

        public FsmGameObject storeObject;

        public override void OnEnter()
        {
            Instance();

            Finish();
        }

        void Instance()
        {
            if(objectInstance.Value != null && objectInstancePoint.Value != null)
            {
                GameObject Clone = Object.Instantiate(objectInstance.Value, objectInstancePoint.Value.transform.position, objectInstancePoint.Value.transform.rotation);

                if(storeObject != null)
                {
                    storeObject.Value = Clone;
                }
            }
        }
    }
}