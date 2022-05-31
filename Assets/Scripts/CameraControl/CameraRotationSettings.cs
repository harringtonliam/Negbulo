using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.CameraControl
{
    public class CameraRotationSettings : MonoBehaviour
    {
        [SerializeField] CameraRotationValues cameraRotationValues = new CameraRotationValues();

        [System.Serializable]
        public class CameraRotationValues
        {
            public float cameraXRotation;
            public float cameraYRotation;
            public float cameraZRotation;
        }

        public CameraRotationValues GetCameraRotationSettings()
        {
            return cameraRotationValues;
        }
    }

}


