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
        CameraRotationSettings.CameraRotationValues cameraRotationValues;


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
            if (rotated)
            {
                return;
            }

            float newYRotation = rotationOfGameObject;//' + rotationOffset;
            Debug.Log("rotate camera RoationOfGameObject= "+ rotationOfGameObject.ToString() + " new rotation " + newYRotation.ToString() + " camera rotation " + followCamera.transform.rotation.y);
            if (!Mathf.Approximately(cameraRotationValues.cameraYRotation, followCamera.transform.rotation.y))
            {
                var newCameraRotation = Quaternion.Euler(cameraRotationValues.cameraXRotation, cameraRotationValues.cameraYRotation, cameraRotationValues.cameraZRotation);

               followCamera.transform.rotation = newCameraRotation;
               // rotated = true;
            }

        }

        private void CheckForCameraRotationSettings()
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 1f);

            Debug.Log("CheckForCameraRotator  hit=" + hit.transform.gameObject);

            Debug.Log("CheckForCameraRotator parent  hit=" + hit.transform.parent.gameObject);

            CameraRotationSettings cameraRotationSettings = hit.transform.GetComponent<CameraRotationSettings>();
            if (cameraRotationSettings == null)
            {
                cameraRotationSettings = hit.transform.parent.GetComponent<CameraRotationSettings>();
            }

            if (cameraRotationSettings != null)
            {
                cameraRotationValues = cameraRotationSettings.GetCameraRotationSettings();
                StartCameraRotation();
            }

        }

        private void StartCameraRotation()
        {
            rotated = false;
            RotateCamera();
        }
    }
}