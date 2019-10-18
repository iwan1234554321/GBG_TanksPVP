using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

namespace GreenButtonGames.TankProject.Actions
{
    [ActionCategory("TankProject")]
    public class ObjectSetTextMeshString : FsmStateAction
    {
        public TextMesh textMesh;

        public FsmString text;

        public FsmColor colorText;

        public bool everyFrame;

        public override void OnEnter()
        {
            SetText();

            if(!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            SetText();

            if (!everyFrame)
            {
                Finish();
            }
        }

        void SetText()
        {
            if(textMesh != null)
            {
                textMesh.text = text.Value;

                if(colorText != null)
                {
                    textMesh.color = colorText.Value;
                }
            }
        }
    }
}