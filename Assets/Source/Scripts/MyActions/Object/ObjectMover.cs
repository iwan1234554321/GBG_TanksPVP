using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

namespace GreenButtonGames.TankProject.Actions
{
    [ActionCategory("TankProject")]
    public class ObjectMover : FsmStateAction
    {
        [CheckForComponent(typeof(Rigidbody2D))]
        [Tooltip("Объект который должен перемещаться")]
        public FsmOwnerDefault moveObject;

        [Tooltip("Скорость перемещения")]
        public FsmFloat speedMove = 1;

        [ActionSection("Звуковое сопровождение")]
        public FsmGameObject audioSource;
        public AudioClip moveSound;
        public AudioClip stopSound;
        private bool soundMove;
        private bool soundStop;

        [ActionSection("Управление")]
        [ObjectType(typeof(KeyCode))]
        [Tooltip("Кнопка для перемещения объекта вверх")]
        public FsmEnum upInput;
        [ObjectType(typeof(KeyCode))]
        [Tooltip("Кнопка для перемещения объекта влево")]
        public FsmEnum leftInput;
        [ObjectType(typeof(KeyCode))]
        [Tooltip("Кнопка для перемещения объекта вниз")]
        public FsmEnum downInput;
        [ObjectType(typeof(KeyCode))]
        [Tooltip("Кнопка для перемещения объекта вправо")]
        public FsmEnum rightInput;

        [ActionSection("Направление")]
        public FsmBool upStore;
        public FsmBool leftStore;
        public FsmBool downStore;
        public FsmBool rightStore;

        [Tooltip("Обновлять каждый кадр")]
        public bool everyFrame;

        private Vector2 directionMovement;

        private GameObject previousGo; // remember so we can get new controller only when it changes.
        private Rigidbody2D rigidbody2D;

        public override void Awake()
        {
            Fsm.HandleFixedUpdate = true;
        }

        public override void Reset()
        {
            moveObject = null;

            speedMove = 1;

            upInput = KeyCode.None;
            leftInput = KeyCode.None;
            downInput = KeyCode.None;
            rightInput = KeyCode.None;

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

        public override void OnFixedUpdate()
        {
            MovementObject();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            if (Input.GetKey(leftInput.Value.ToString().ToLower()))
            {
                if (Input.GetKey(upInput.Value.ToString().ToLower()) || Input.GetKey(downInput.Value.ToString().ToLower()))
                {
                    directionMovement.x = 0;

                    leftStore.Value = false;
                }
                else
                {
                    directionMovement.x = -1;

                    leftStore.Value = true;
                }
            }
            else if (Input.GetKey(rightInput.Value.ToString().ToLower()))
            {
                if (Input.GetKey(upInput.Value.ToString().ToLower()) || Input.GetKey(downInput.Value.ToString().ToLower()))
                {
                    directionMovement.x = 0;

                    rightStore.Value = false;
                }
                else
                {
                    directionMovement.x = 1;

                    rightStore.Value = true;
                }
            }
            else
            {
                directionMovement.x = 0;

                rightStore.Value = false;
                leftStore.Value = false;
            }

            if (Input.GetKey(upInput.Value.ToString().ToLower()))
            {
                directionMovement.y = 1;

                upStore.Value = true;
            }
            else if (Input.GetKey(downInput.Value.ToString().ToLower()))
            {
                directionMovement.y = -1;

                downStore.Value = true;
            }
            else
            {
                directionMovement.y = 0;

                upStore.Value = false;
                downStore.Value = false;
            }

            if(Input.GetKey(upInput.Value.ToString().ToLower()) || Input.GetKey(downInput.Value.ToString().ToLower())
                || Input.GetKey(leftInput.Value.ToString().ToLower()) || Input.GetKey(rightInput.Value.ToString().ToLower()))
            {
                PlaySound(true);
            }
            else
            {
                PlaySound(false);
            }

            if (!everyFrame)
            {
                Finish();
            }
        }

        void MovementObject()
        {
            var go = Fsm.GetOwnerDefaultTarget(moveObject);
            if (go == null) return;

            if (go != previousGo)
            {
                rigidbody2D = go.GetComponent<Rigidbody2D>();
                previousGo = go;
            }

            if (rigidbody2D != null)
            {
                rigidbody2D.position += (directionMovement * speedMove.Value) * Time.deltaTime;
            }
        }

        void PlaySound(bool loop)
        {
            if (audioSource != null && moveSound != null && stopSound != null)
            {
                if (loop)
                {
                    soundStop = true;

                    if (!audioSource.Value.GetComponent<AudioSource>().isPlaying)
                    {
                        if (soundMove)
                        {
                            audioSource.Value.GetComponent<AudioSource>().clip = moveSound;
                            audioSource.Value.GetComponent<AudioSource>().Play();
                            audioSource.Value.GetComponent<AudioSource>().loop = true;

                            soundMove = false;
                        }
                    }
                }
                else
                {
                    soundMove = true;

                    if (soundStop)
                    {
                        audioSource.Value.GetComponent<AudioSource>().loop = false;
                        audioSource.Value.GetComponent<AudioSource>().PlayOneShot(stopSound);

                        soundStop = false;
                    }
                }
            }
        }
    }
}