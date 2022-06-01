using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.CameraControl
{
    public class CameraRotationSettings : MonoBehaviour
    {
        [SerializeField] float cameraXRotation = 52f;
        [SerializeField] float cameraYRotation = 0f;
        [SerializeField] float cameraZRotation = 0f;

        public float CameraXRotation {  get { return cameraXRotation; } }
        public float CameraYRotation { get { return cameraYRotation; } }
        public float CameraZRotation { get { return cameraZRotation; } }

    }

}


