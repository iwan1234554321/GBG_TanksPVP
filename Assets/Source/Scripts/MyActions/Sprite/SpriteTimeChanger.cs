using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

namespace GreenButtonGames.TankProject.Actions
{
    [ActionCategory("TankProject")]
    public class SpriteTimeChanger : FsmStateAction
    {
        [Tooltip("Компонент рендера спрайтов")]
        public SpriteRenderer spriteRenderer;

        [Tooltip("Скорость обновления спрайтов")]
        public FsmFloat speedChange = 1;

        [Tooltip("Обновлять каждый кадр")]
        public bool everyFrame;

        [Tooltip("Событие по окончанию анимации")]
        public FsmEvent finishEvent;

        [Tooltip("Массив спрайтов")]
        public Sprite[] sprites;

        int changeValue;
        private float _time;

        public override void Awake()
        {
            Fsm.HandleFixedUpdate = true;
        }

        public override void OnEnter()
        {
            AnimateSprite();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnFixedUpdate()
        {
            AnimateSprite();

            if (!everyFrame)
            {
                Finish();
            }
        }

        void AnimateSprite()
        {
            if (spriteRenderer == null) return;

            if(spriteRenderer && sprites.Length > 0)
            {
                changeValue = Mathf.Clamp(changeValue, 0, sprites.Length-1);

                if(_time < 1)
                {
                    _time += speedChange.Value * Time.deltaTime;
                }
                else
                {
                    // Обновление спрайта
                    if (changeValue < sprites.Length - 1)
                    {
                        changeValue += 1;
                    }
                    else
                    {
                        changeValue = 0;

                        if (finishEvent != null)
                        {
                            Fsm.Event(finishEvent);
                        }
                    }

                    _time = 0;
                }

                spriteRenderer.sprite = sprites[changeValue];
            }
        }
    }
}