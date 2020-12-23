using UnityEngine;

namespace Dungeon
{
    [System.Serializable]
    public class WallType
    {
        public GameObject SingleLeft, SingleRight, SingleUp, SingleDown,
                          DoubleLU, DoubleLR, DoubleLD, DoubleUR, DoubleUD, DoubleRD,
                          TripleLUR, TripleLUD, TripleURD, TripleLRD,
                          Cross;
    }
}
