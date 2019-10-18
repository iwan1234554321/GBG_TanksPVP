using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

namespace GreenButtonGames.TankProject.Actions
{
    [ActionCategory("TankProject")]
    public class ObjectDirectionTransform: FsmStateAction
    {
        [Tooltip("Объект который должен вращаться")]
        public FsmOwnerDefault rotateObject;

        [ActionSection("Направления")]
        [Tooltip("Кнопка для перемещения объекта вверх")]
        public FsmBool upInput;
        [Tooltip("Кнопка для перемещения объекта влево")]
        public FsmBool leftInput;
        [Tooltip("Кнопка для перемещения объекта вниз")]
        public FsmBool downInput;
        [Tooltip("Кнопка для перемещения объекта вправо")]
        public FsmBool rightInput;

        [ActionSection("Вращения объекта")]
        public float upRotation;
        public float downRotation;
        public float leftRotation;
        public float rightRotation;

        [ActionSection("Позиция объекта")]
        public Vector2 upPosition;
        public Vector2 downPosition;
        public Vector2 leftPosition;
        public Vector2 rightPosition;

        public FsmVector3 storeVectorDirection;
        public FsmFloat storeVectorDirectionX;
        public FsmFloat storeVectorDirectionY;

        [Tooltip("Обновлять каждый кадр")]
        public bool everyFrame;

        private GameObject previousGo; // remember so we can get new controller only when it changes.

        public override void Reset()
        {
            rotateObject = null;

            upInput = false;
            leftInput = false;
            downInput = false;
            rightInput = false;

            upRotation = 0;
            downRotation = 0;
            leftRotation = 0;
            rightRotation = 0;

            upPosition = Vector2.zero;
            downPosition = Vector2.zero;
            leftPosition = Vector2.zero;
            rightPosition = Vector2.zero;

            everyFrame = true;
        }

        public override void OnEnter()
        {
            RotationObject();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            RotationObject();

            if (!everyFrame)
            {
                Finish();
            }
        }

        void RotationObject()
        {
            var go = Fsm.GetOwnerDefaultTarget(rotateObject);
            if (go == null) return;

            if (go != previousGo)
            {
                previousGo = go;
            }

            if (previousGo != null)
            {
                if (leftInput.Value)
                {
                    if (upInput.Value == false || downInput.Value == false)
                    {
                        previousGo.transform.rotation = Quaternion.Euler(0, 0, leftRotation);
                        previousGo.transform.position = Owner.transform.position + new Vector3(leftPosition.x, leftPosition.y, 0);

                        if (storeVectorDirectionY != null)
                        {
                            storeVectorDirectionY.Value = 0;
                        }

                        if (storeVectorDirectionX != null)
                        {
                            storeVectorDirectionX.Value = -1;
                        }

                        if (storeVectorDirection != null)
                        {
                            storeVectorDirection.Value = new Vector3(-1, 0, 0);
                        }
                    }
                }
                else if (rightInput.Value)
                {
                    if (upInput.Value == false || downInput.Value == false)
                    {
                        previousGo.transform.rotation = Quaternion.Euler(0, 0, rightRotation);
                        previousGo.transform.position = Owner.transform.position + new Vector3(rightPosition.x, rightPosition.y, 0);

                        if (storeVectorDirectionY != null)
                        {
                            storeVectorDirectionY.Value = 0;
                        }

                        if (storeVectorDirectionX != null)
                        {
                            storeVectorDirectionX.Value = 1;
                        }

                        if (storeVectorDirection != null)
                        {
                            storeVectorDirection.Value = new Vector3(1, 0, 0);
                        }
                    }
                }
                //else
                //{
                //    if (storeVectorDirectionX != null)
                //    {
                //        storeVectorDirectionX.Value = 0;
                //    }
                //}

                if (upInput.Value)
                {
                    previousGo.transform.rotation = Quaternion.Euler(0, 0, upRotation);
                    previousGo.transform.position = Owner.transform.position + new Vector3(upPosition.x, upPosition.y, 0);

                    if (storeVectorDirectionY != null)
                    {
                        storeVectorDirectionY.Value = 1;
                    }

                    if (storeVectorDirectionX != null)
                    {
                        storeVectorDirectionX.Value = 0;
                    }

                    if (storeVectorDirection != null)
                    {
                        storeVectorDirection.Value = new Vector3(0, 1, 0);
                    }
                }
                else if (downInput.Value)
                {
                    previousGo.transform.rotation = Quaternion.Euler(0, 0, downRotation);
                    previousGo.transform.position = Owner.transform.position + new Vector3(downPosition.x, downPosition.y, 0);

                    if (storeVectorDirectionY != null)
                    {
                        storeVectorDirectionY.Value = -1;
                    }

                    if (storeVectorDirectionX != null)
                    {
                        storeVectorDirectionX.Value = 0;
                    }

                    if (storeVectorDirection != null)
                    {
                        storeVectorDirection.Value = new Vector3(0, -1, 0);
                    }
                }
                //else
                //{
                //    if (storeVectorDirectionY != null)
                //    {
                //        storeVectorDirectionY.Value = 0;
                //    }
                //}
            }
        }

    }
}