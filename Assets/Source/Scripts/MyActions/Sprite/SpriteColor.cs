using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

namespace GreenButtonGames.TankProject.Actions
{
    [ActionCategory("TankProject")]
    public class SpriteColor: FsmStateAction
    {
        [Tooltip("Компонент рендера спрайтов")]
        public FsmGameObject spriteRenderer;

        [Tooltip("Назначаемый цвет")]
        public FsmColor setColor = Color.white;

        public bool everyFrame;

        public override void OnEnter()
        {
            SetColor();

            if(!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            SetColor();

            if (!everyFrame)
            {
                Finish();
            }
        }

        void SetColor()
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.Value.GetComponent<SpriteRenderer>().color = setColor.Value;
            }
        }
    }
}