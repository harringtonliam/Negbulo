using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using RPG.Control;
using RPG.Core;


namespace RPG.SceneManagement
{
    public class InScenePortal : MonoBehaviour
    {
        [SerializeField] float fadeTime = 1f;

        [SerializeField] Transform spawnPoint;
        [SerializeField] string destinationIdentifier;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeTime);

            SavingWrapper saveingWrapper = FindObjectOfType<SavingWrapper>();
            DisablePlayerControl();
            InScenePortal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return fader.FadeIn(fadeTime);

            EnablePlayerControl();
        }

        private void UpdatePlayer(InScenePortal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            Debug.Log("UpdatePlayer " + otherPortal.spawnPoint.position.ToString());
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private InScenePortal GetOtherPortal()
        {
            foreach (InScenePortal portal in FindObjectsOfType<InScenePortal>())
            {
                if (portal == this) continue;
                if (portal.destinationIdentifier != destinationIdentifier) continue;

                return portal;
            }

            return null;
        }


        private void DisablePlayerControl()
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        private void EnablePlayerControl()
        {
            try
            {
                GameObject player = GameObject.FindWithTag("Player");
                player.GetComponent<PlayerController>().enabled = true;
            }
            catch (Exception ex)
            {
                Debug.Log("InScenePortal EnablePlayerController " + ex.Message);
            }
        }
    }


}

