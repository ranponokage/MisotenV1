using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Dungeon
{
    public class RoomGenerator : MonoBehaviour
    {
        public enum Direction
        {
            up, down, left, right
        }

        [SerializeField] DungeonSettingSO _dungeonSettingSO;

        [SerializeField] Transform _roomRoot;

        [Header("Room Info")]
        [SerializeField] Room _roomPrefab;
        [SerializeField] int _roomNumber;
        [SerializeField] WallType _wallType;

        [Header("Room Position")]
        [SerializeField] Transform _generatorPoint;
        [SerializeField] float _xOffset;
        [SerializeField] float _yOffset;
        [SerializeField] LayerMask _roomLayer;

        [Header("Item")]
        [SerializeField] GameObject _endPoint;
        [SerializeField] List<GameObject> _items;
        [SerializeField] int _maxItem = 10;

        [Tooltip("min : 1 , max : maxStep - 2, maxStep in Canvas")]
        [SerializeField] int _itemStepToStart = 1;
        [SerializeField] int _itemStepToEnd = 2;

        [Header("PlayerSpawnPoint")]
        [SerializeField] public Transform SpawnPoint_P1;
        [SerializeField] public Transform SpawnPoint_P2;



        private Room _endRoom;
        private int _maxStep;
        private const float _checlRadius = 3f;

        private Direction _direction { get; set; }
        private List<Room> _rooms = new List<Room>();

        private List<Room> _farRooms = new List<Room>();
        private List<Room> _lessFarRooms = new List<Room>();
        private List<Room> _oneWayRooms = new List<Room>();
        private List<Room> _itemRooms = new List<Room>();


        public int MaxStep { get => _maxStep; set => _maxStep = value; }


        private void SetupDungeon()
        {
            if (_dungeonSettingSO == null)
                return;

            _roomNumber = _dungeonSettingSO.RoomNumber;
            _items = _dungeonSettingSO.Items;
            _maxItem = _dungeonSettingSO.maxItem;
        }

        private void Awake()
        {
            SetupDungeon();


            for (int i = 0; i < _roomNumber; i++)
            {
                var room = Instantiate(_roomPrefab.gameObject, _generatorPoint.position, Quaternion.identity,_roomRoot);
                _rooms.Add(room.GetComponent<Room>());
                //change point position
                ChangePointPosition();
            }

            //Instantiate(_startPoint, _rooms[0].transform.position, Quaternion.identity);
            //Instantiate(_endRoom, _rooms[_roomNumber-1].transform.position, Quaternion.identity);

            foreach (var room in _rooms)
            {
                SetupRoom(room);
            }

            FindEndRoom();
            FindItemRooms();

            Instantiate(_endPoint, _endRoom.transform.position, Quaternion.identity);
        }

        private void ChangePointPosition()
        {
            bool IfCreated = false;
            do
            {
                _direction = (Direction)UnityEngine.Random.Range(0, 4);

                switch (_direction)
                {
                    case Direction.up:
                        _generatorPoint.position += new Vector3(0, 0, _yOffset);
                        break;
                    case Direction.down:
                        _generatorPoint.position += new Vector3(0, 0, -_yOffset);
                        break;
                    case Direction.left:
                        _generatorPoint.position += new Vector3(-_xOffset, 0, 0);
                        break;
                    case Direction.right:
                        _generatorPoint.position += new Vector3(_xOffset, 0, 0);
                        break;
                    default:
                        break;
                }

                IfCreated = IfPositionCreated();
            }
            while (IfCreated);
            //while (Physics.OverlapSphere(_generatorPoint.position, _checlRadius, _roomLayer).Length > 0);
        }

        private bool IfPositionCreated()
        {
            foreach (var room in _rooms)
            {
                if (room.transform.position == _generatorPoint.position)
                { return true; }
            }
            return false;
        }

        private void SetupRoom(Room newRoom)
        {
            Vector3 newRoomposition = newRoom.transform.position;

            newRoom.RoomUp = Physics.OverlapSphere(newRoomposition + new Vector3(0, 0, _yOffset), _checlRadius, _roomLayer).Length > 0;
            newRoom.RoomDown = Physics.OverlapSphere(newRoomposition + new Vector3(0, 0, -_yOffset), _checlRadius, _roomLayer).Length > 0;
            newRoom.RoomLeft = Physics.OverlapSphere(newRoomposition + new Vector3(-_xOffset, 0, 0), _checlRadius, _roomLayer).Length > 0;
            newRoom.RoomRight = Physics.OverlapSphere(newRoomposition + new Vector3(_xOffset, 0, 0), _checlRadius, _roomLayer).Length > 0;

            //For Debug
            newRoom.UpdateRoom(_xOffset, _yOffset);

            switch (newRoom.DoorNumber)
            {
                case 1:
                    if (newRoom.RoomUp)
                        Instantiate(_wallType.SingleUp, newRoomposition, Quaternion.identity,_roomRoot);
                    if (newRoom.RoomDown)
                        Instantiate(_wallType.SingleDown, newRoomposition, Quaternion.identity, _roomRoot);
                    if (newRoom.RoomLeft)
                        Instantiate(_wallType.SingleLeft, newRoomposition, Quaternion.identity, _roomRoot);
                    if (newRoom.RoomRight)
                        Instantiate(_wallType.SingleRight, newRoomposition, Quaternion.identity, _roomRoot);
                    break;
                case 2:
                    if (newRoom.RoomLeft && newRoom.RoomUp)
                        Instantiate(_wallType.DoubleLU, newRoom.transform.position, Quaternion.identity, _roomRoot);
                    if (newRoom.RoomLeft && newRoom.RoomRight)
                        Instantiate(_wallType.DoubleLR, newRoom.transform.position, Quaternion.identity, _roomRoot);
                    if (newRoom.RoomLeft && newRoom.RoomDown)
                        Instantiate(_wallType.DoubleLD, newRoom.transform.position, Quaternion.identity, _roomRoot);
                    if (newRoom.RoomUp && newRoom.RoomRight)
                        Instantiate(_wallType.DoubleUR, newRoom.transform.position, Quaternion.identity, _roomRoot);
                    if (newRoom.RoomUp && newRoom.RoomDown)
                        Instantiate(_wallType.DoubleUD, newRoom.transform.position, Quaternion.identity, _roomRoot);
                    if (newRoom.RoomRight && newRoom.RoomDown)
                        Instantiate(_wallType.DoubleRD, newRoom.transform.position, Quaternion.identity, _roomRoot);
                    break;
                case 3:
                    if (newRoom.RoomLeft && newRoom.RoomUp && newRoom.RoomRight)
                        Instantiate(_wallType.TripleLUR, newRoomposition, Quaternion.identity, _roomRoot);
                    if (newRoom.RoomLeft && newRoom.RoomRight && newRoom.RoomDown)
                        Instantiate(_wallType.TripleLRD, newRoomposition, Quaternion.identity, _roomRoot);
                    if (newRoom.RoomDown && newRoom.RoomUp && newRoom.RoomRight)
                        Instantiate(_wallType.TripleURD, newRoomposition, Quaternion.identity, _roomRoot);
                    if (newRoom.RoomLeft && newRoom.RoomUp && newRoom.RoomDown)
                        Instantiate(_wallType.TripleLUD, newRoomposition, Quaternion.identity, _roomRoot);
                    break;
                case 4:
                    if (newRoom.RoomLeft && newRoom.RoomUp && newRoom.RoomRight && newRoom.RoomDown)
                        Instantiate(_wallType.Cross, newRoomposition, Quaternion.identity, _roomRoot);
                    break;

            }
        }
        private void FindEndRoom()
        {
            //Get Max Step
            for (int i = 0; i < _rooms.Count; i++)
            {
                if (_rooms[i].StepToStart > MaxStep)
                    MaxStep = _rooms[i].StepToStart;
            }

            foreach (var room in _rooms)
            {
                if (room.StepToStart == MaxStep)
                    _farRooms.Add(room);
                if (room.StepToStart == MaxStep - 1)
                    _lessFarRooms.Add(room);
            }

            for (int i = 0; i < _farRooms.Count; i++)
            {
                if (_farRooms[i].DoorNumber == 1)
                    _oneWayRooms.Add(_farRooms[i]);
            }

            for (int i = 0; i < _lessFarRooms.Count; i++)
            {
                if (_lessFarRooms[i].DoorNumber == 1)
                    _oneWayRooms.Add(_lessFarRooms[i]);
            }

            if (_oneWayRooms.Count != 0)
            {
                _endRoom = _oneWayRooms[UnityEngine.Random.Range(0, _oneWayRooms.Count)];
            }
            else
            {
                _endRoom = _farRooms[UnityEngine.Random.Range(0, _farRooms.Count)];

            }
        }
        private void FindItemRooms()
        {
            foreach (var room in _rooms)
            {
                if (room.StepToStart >= _itemStepToStart && room.StepToStart <= MaxStep - _itemStepToEnd)
                    _itemRooms.Add(room);
            }

            var itemRoomIndex = Utility.RandomTool.GetIndexRandomNum(0, _itemRooms.Count);

            for (int i = 0; i < _maxItem; i++)
            {
                var randomItemIndex = UnityEngine.Random.Range(0, _items.Count);
                //to do Random Item
                Instantiate(_items[randomItemIndex], _itemRooms[itemRoomIndex[i]].transform.position, Quaternion.identity);
            }
        }
    }
}


