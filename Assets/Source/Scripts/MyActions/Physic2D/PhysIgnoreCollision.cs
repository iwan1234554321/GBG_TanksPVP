using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

namespace GreenButtonGames.TankProject.Actions
{
    [ActionCategory("TankProject")]
    public class PhysIgnoreCollision : FsmStateAction
    {
        [Tooltip("Игнорируемая коллизия")]
        public FsmGameObject ignoreCollision;

        [Tooltip("Коллайдер который будет игнорировать коллизию")]
        public FsmGameObject ignoreCollisionObject;

        public override void OnEnter()
        {
            IgnoreCollision();

            Finish();
        }

        void IgnoreCollision()
        {
            Physics2D.IgnoreCollision(ignoreCollision.Value.GetComponent<Collider2D>(), ignoreCollisionObject.Value.GetComponent<Collider2D>());
        }
    }
}