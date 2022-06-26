using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Movement
{
    public class PropMover : MonoBehaviour
    {
        [SerializeField] Transform propToMove;
        [SerializeField] Transform endPosition = null;
        [SerializeField] Transform startPosition = null;
        [SerializeField] float movementSpeed = 1f;


        private bool moveTowardStart;
        private bool moveTowardEnd;


        // Update is called once per frame
        void Update()
        {
            if (moveTowardStart)
            {
                MoveToStart();
            }
            else if (moveTowardEnd)
            {
                MoveToEnd();
            }
        }

        public void TriggerMoveToStart()
        {
            moveTowardStart = true;
        }

        public void TriggerMoveToEnd()
        {
            moveTowardEnd = true;
        }

        private void MoveToStart()
        {
            propToMove.position = Vector3.MoveTowards(propToMove.position, startPosition.position, movementSpeed * Time.deltaTime);
            float distanceToClosed = Vector3.Distance(propToMove.position, startPosition.position);
            if (Mathf.Approximately(distanceToClosed, 0f))
            {
                moveTowardStart = false;
            }
        }

        private void MoveToEnd()
        {
            propToMove.position = Vector3.MoveTowards(propToMove.position, endPosition.position, movementSpeed * Time.deltaTime);
            float distanceToClosed = Vector3.Distance(propToMove.position, endPosition.position);
            if (Mathf.Approximately(distanceToClosed, 0f))
            {
                moveTowardEnd = false;
            }
        }
    }
}


