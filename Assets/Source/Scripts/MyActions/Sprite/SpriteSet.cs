using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

namespace GreenButtonGames.TankProject.Actions
{
    [ActionCategory("TankProject")]
    public class SpriteSet : FsmStateAction
    {
        [Tooltip("Компонент рендера спрайтов")]
        public SpriteRenderer spriteRenderer;

        [Tooltip("Назначаемый спрайт")]
        public Sprite setSprite;

        public override void OnEnter()
        {
            SetSprite();

            Finish();
        }

        void SetSprite()
        {
            if(spriteRenderer != null && setSprite != null)
            {
                spriteRenderer.sprite = setSprite;
            }
        }
    }
}