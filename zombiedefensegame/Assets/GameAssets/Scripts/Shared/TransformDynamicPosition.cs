using System;
using TheyAreComing;
using UnityEngine;

namespace AmazingAssets.CurvedWorld.Example
{
    [ExecuteAlways]
    public class TransformDynamicPosition : MonoBehaviour
    {
        public Transform parent;

        public Vector3 offset;
        public bool recalculateRotation;

        [NonSerialized] private CurvedWorldController _curvedWorldController;

        private void Start()
        {
            if (!parent) parent = transform.parent;
            _curvedWorldController = GameManager.CurvedWorldController;
        }

        private void Update()
        {
            //Transform position
            transform.position = _curvedWorldController.TransformPosition(parent.position + offset);

            //Transform normal (calcualte rotation)
            if (recalculateRotation)
                transform.rotation =
                    _curvedWorldController.TransformRotation(parent.position + offset, parent.forward,
                        parent.right);
        }
    }
}