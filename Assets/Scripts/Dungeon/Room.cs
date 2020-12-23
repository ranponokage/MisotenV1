using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Dungeon
{
    public class Room : MonoBehaviour
    {
        [SerializeField] GameObject _doorLeft;
        [SerializeField] GameObject _doorRight;
        [SerializeField] GameObject _doorUp;
        [SerializeField] GameObject _doorDown;

        private TMP_Text _tmp;

        public int DoorNumber { get; set; }
        public bool RoomLeft { get; set; }
        public bool RoomRight { get; set; }
        public bool RoomUp { get; set; }
        public bool RoomDown { get; set; }
        public int StepToStart { get; set; }


        // Start is called before the first frame update
        void Start()
        {
            _doorLeft.SetActive(RoomLeft);
            _doorRight.SetActive(RoomRight);
            _doorUp.SetActive(RoomUp);
            _doorDown.SetActive(RoomDown);
        }

        // Update is called once per frame
        public void UpdateRoom(float xoffset, float yoffset)
        {
            StepToStart = (int)(Mathf.Abs(transform.position.x / xoffset))
                + (int)(Mathf.Abs(transform.position.z / yoffset));

            if (RoomLeft)
                DoorNumber++;
            if (RoomRight)
                DoorNumber++;
            if (RoomUp)
                DoorNumber++;
            if (RoomDown)
                DoorNumber++;

            _tmp = GetComponentInChildren<TMP_Text>();
            _tmp.SetText(StepToStart.ToString());
        }
    }
}