using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

namespace GreenButtonGames.TankProject.Actions
{
    [ActionCategory("TankProject")]
    [Tooltip("Добавляет, либо удаляет значения в выбранную переменную")]
    public class ObjectPlusMinusValues : FsmStateAction
    {
        [ActionSection("Изменяемое значение")]
        public FsmInt changedValue;

        public enum PlusMinusValue { plus, minus}
        [ActionSection("Тип функции")]
        public PlusMinusValue functionType;

        [ActionSection("Значение которое будем прибавлять в переменную")]
        public FsmInt plusMinusValue;

        public override void OnEnter()
        {
            Adder();

            Finish();
        }

        void Adder()
        {
            if(changedValue != null)
            {
                if(functionType == PlusMinusValue.plus)
                {
                    changedValue.Value += plusMinusValue.Value;
                }
                else if (functionType == PlusMinusValue.minus)
                {
                    changedValue.Value -= plusMinusValue.Value;
                }
            }
        }
    }
}