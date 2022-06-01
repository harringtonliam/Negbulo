using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.CameraControl
{
    public class CameraRotator : MonoBehaviour
    {
        [SerializeField] float camerRotationSpeed = 1f;

        CinemachineVirtualCamera followCamera;
        float rotationOfGameObject = 0f;
        bool rotated = false;
        CameraRotationSettings cameraRotationSettings = null;


        void Start()
        {

            followCamera = GameObject.FindGameObjectWithTag("FollowCamera").GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            CheckForCameraRotationSettings();
        }

        public void RotateCamera()
        {
            if (!Mathf.Approximately(cameraRotationSettings.CameraYRotation, followCamera.transform.rotation.y))
            {
                var newCameraRotation = Quaternion.Euler(cameraRotationSettings.CameraXRotation, cameraRotationSettings.CameraYRotation, cameraRotationSettings.CameraZRotation);

               followCamera.transform.rotation = newCameraRotation;
            }

        }

        private void CheckForCameraRotationSettings()
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 1f);

            if(hit.transform == null)
            {
                return;
            }

             cameraRotationSettings = hit.transform.GetComponent<CameraRotationSettings>();
            
            if (cameraRotationSettings == null)
            {
                cameraRotationSettings = hit.transform.parent.GetComponent<CameraRotationSettings>();
            }

            if (cameraRotationSettings != null)
            {
                 StartCameraRotation();
            }

        }

        private void StartCameraRotation()
        {
            RotateCamera();
        }
    }
}