using System.Collections.Generic;
using UnityEngine;

namespace GLUBall
{
    public class DemoClass : MonoBehaviour
    {
        [SerializeField]
        private bool m_ShowGameObjectList = false;
        public bool ShowgameObjectList
        {
            get { return m_ShowGameObjectList; }
        }

        [SerializeField]
        [HideInInspector]
        private List<GameObject> m_GameObjectList = new List<GameObject>();
    }
}