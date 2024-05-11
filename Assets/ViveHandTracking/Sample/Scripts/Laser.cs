using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ViveHandTracking.Sample {

  class Laser : MonoBehaviour {
    protected const float angularVelocity = 50.0f;

    public Color color = Color.red;
    public Color emissionColor = new Color(0.3f, 0, 0, 1);
    public GameObject laser = null;
    public GameObject light = null;
    public bool isLeft = false;

    protected bool visible = false;
    protected Renderer hit = null;
    protected bool skeletonRotation = true;

    IEnumerator Start() {
      laser.GetComponent<LineRenderer>().material.SetColor("_TintColor", color);
      if (light != null) {
        light.SetActive(false);
        light.GetComponent<Light>().color = color;
      }
      if (!skeletonRotation) yield break;
      while (GestureProvider.Status == GestureStatus.NotStarted) yield return null;
      if (GestureProvider.HaveSkeleton) laser.transform.localRotation = Quaternion.Euler(-30, 0, 0);
    }

    protected virtual void Update() {
      var hand = isLeft ? GestureProvider.LeftHand : GestureProvider.RightHand;
      if (hand == null) {
        laser.SetActive(false);
        return;
      }

      transform.position = hand.position;

      // smooth rotation for skeleton mode
      if (laser.activeSelf && GestureProvider.HaveSkeleton)
        transform.rotation = Quaternion.RotateTowards(transform.rotation, hand.rotation,
                                                      angularVelocity * Time.deltaTime);
      else
        transform.rotation = hand.rotation;

      laser.SetActive(visible);
    }

    public void OnStateChanged(int state) {
      if (state == 2)
        visible = true;
      else {
        visible = false;
        if (hit != null) StopHit();
      }
    }

    void OnTriggerEnter(Collider other) {
      if (!other.gameObject.name.StartsWith("Cube")) return;
      if (hit != null) StopHit();
      hit = other.GetComponent<Renderer>();
      if (hit != null) {
        hit.material.EnableKeyword("_EMISSION");
        hit.material.SetColor("_EmissionColor", emissionColor);
      }
    }

    virtual protected void OnTriggerExit(Collider other) {
      light.SetActive(false);
      if (hit != null && hit == other.GetComponent<Renderer>()) StopHit();
    }

    void StopHit() {
      hit.material.DisableKeyword("_EMISSION");
      hit = null;
    }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(laser.transform.position, laser.transform.forward * 50);
        }
    }

}
