using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.CameraControl
{
    public class CameraZoom : MonoBehaviour
    {

        [SerializeField] float zoomLerpSpeed = 10f;
        [SerializeField] float maxZoom = 90f;
        [SerializeField] float minZoom = 40f;
        CinemachineVirtualCamera followCamera;
        private float targetZoom;
        private float zoomFactor = 2f;


        // Start is called before the first frame update
        void Start()
        {
            followCamera = GameObject.FindGameObjectWithTag("FollowCamera").GetComponent<CinemachineVirtualCamera>();
        }

        // Update is called once per frame
        void Update()
        {
            float scrollData;
            scrollData = Input.GetAxis("Mouse ScrollWheel");

            targetZoom = targetZoom - scrollData * zoomFactor;
            targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
            followCamera.m_Lens.FieldOfView = Mathf.Lerp(followCamera.m_Lens.FieldOfView, targetZoom, Time.deltaTime * zoomLerpSpeed);
        }
    }
}


