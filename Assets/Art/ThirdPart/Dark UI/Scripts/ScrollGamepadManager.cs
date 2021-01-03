using UnityEngine;
using UnityEngine.UI;
using Rewired;

namespace Michsky.UI.Dark
{
    public class ScrollGamepadManager : MonoBehaviour
    {
        [Header("SLIDER")]
        public Scrollbar scrollbarObject;
        public float changeValue = 0.05f;

        [Header("INPUT")]
        public string inputAxis = "Xbox Right Stick Vertical";
        //public string inputAxis = "UIY";
        public bool invertAxis = false;

        private Player _player;

        private void Awake()
        {
            //_player = ReInput.players.GetPlayer(0);
        }

        void Update()
        {
            float h = Input.GetAxis(inputAxis);

            //float h = _player.GetAxis(inputAxis);

            if (invertAxis == false)
            {
                if (h == 1)
                    scrollbarObject.value -= changeValue;

                else if (h == -1)
                    scrollbarObject.value += changeValue;
            }

            else
            {
                if (h == 1)
                    scrollbarObject.value += changeValue;

                else if (h == -1)
                    scrollbarObject.value -= changeValue;
            }
        }
    }
}