using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

namespace GreenButtonGames.TankProject.Actions
{
    [ActionCategory("TankProject")]
    public class ObjectSpriteDirectionChanger : FsmStateAction
    {
        [Tooltip("Компонент рендера спрайтов")]
        public SpriteRenderer spriteRenderer;

        [ActionSection("Направления")]
        [Tooltip("Кнопка для перемещения объекта вверх")]
        public FsmBool upInput;
        [Tooltip("Кнопка для перемещения объекта влево")]
        public FsmBool leftInput;
        [Tooltip("Кнопка для перемещения объекта вниз")]
        public FsmBool downInput;
        [Tooltip("Кнопка для перемещения объекта вправо")]
        public FsmBool rightInput;

        [ActionSection("Спрайты направлений")]
        [Tooltip("Массив спрайтов для перемещения вверх")]
        public Sprite[] upSprites;
        [Tooltip("Массив спрайтов для перемещения вниз")]
        public Sprite[] downSprites;
        [Tooltip("Массив спрайтов для перемещения влево")]
        public Sprite[] leftSprites;
        [Tooltip("Массив спрайтов для перемещения вправо")]
        public Sprite[] rightSprites;

        [Tooltip("Обновлять каждый кадр")]
        public bool everyFrame;

        int upChangeValue;
        int downChangeValue;
        int leftChangeValue;
        int rightChangeValue;

        public override void Reset()
        {
            upInput = false;
            leftInput = false;
            downInput = false;
            rightInput = false;

            everyFrame = true;
        }

        public override void OnEnter()
        {
            MovementObject();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            MovementObject();

            if (!everyFrame)
            {
                Finish();
            }
        }

        void MovementObject()
        {
            if (spriteRenderer != null)
            {
                if (leftInput.Value)
                {
                    if (upInput.Value == false || downInput.Value == false)
                    {
                        if (leftSprites.Length > 0)
                        {
                            leftChangeValue = Mathf.Clamp(leftChangeValue, 0, leftSprites.Length - 1);

                            if (leftChangeValue < leftSprites.Length - 1)
                            {
                                leftChangeValue += 1;
                            }
                            else
                            {
                                leftChangeValue = 0;
                            }

                            spriteRenderer.sprite = leftSprites[leftChangeValue];
                        }
                    }
                }
                else if (rightInput.Value)
                {
                    if (upInput.Value == false || downInput.Value == false)
                    {
                        if (rightSprites.Length > 0)
                        {
                            rightChangeValue = Mathf.Clamp(rightChangeValue, 0, rightSprites.Length - 1);

                            if (rightChangeValue < rightSprites.Length - 1)
                            {
                                rightChangeValue += 1;
                            }
                            else
                            {
                                rightChangeValue = 0;
                            }

                            spriteRenderer.sprite = rightSprites[rightChangeValue];
                        }
                    }
                }

                if (upInput.Value)
                {
                    if (upSprites.Length > 0)
                    {
                        upChangeValue = Mathf.Clamp(upChangeValue, 0, upSprites.Length - 1);

                        if (upChangeValue < upSprites.Length - 1)
                        {
                            upChangeValue += 1;
                        }
                        else
                        {
                            upChangeValue = 0;
                        }

                        spriteRenderer.sprite = upSprites[upChangeValue];
                    }
                }
                else if (downInput.Value)
                {
                    if (downSprites.Length > 0)
                    {
                        downChangeValue = Mathf.Clamp(downChangeValue, 0, downSprites.Length - 1);

                        if (downChangeValue < downSprites.Length - 1)
                        {
                            downChangeValue += 1;
                        }
                        else
                        {
                            downChangeValue = 0;
                        }

                        spriteRenderer.sprite = downSprites[downChangeValue];
                    }
                }
            }
        }
    }
}