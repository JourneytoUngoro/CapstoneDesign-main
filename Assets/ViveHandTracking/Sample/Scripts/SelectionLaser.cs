using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ViveHandTracking.Sample {

  class SelectionLaser : Laser {
    public bool debugging;

    private int state = 0;
    private bool handPinched = false;
    private bool initialGrab = true;
    private Vector3 offset = Vector3.zero;
    private RaycastHit raycastHit;

    void Awake() { skeletonRotation = false; }

    protected override void Update() {
      int newState = GetState();
      if (newState != state) {
        state = newState;
        // always show laser for state 1 & 2
        OnStateChanged(state == 0 ? 0 : 2);
        if (state == 2) {
          handPinched = true;
        }
        else {
          handPinched = false;
          initialGrab = true;
        }
      }

      var hand = isLeft ? GestureProvider.LeftHand : GestureProvider.RightHand;
      laser.SetActive(visible);
      if (laser.activeSelf) {
        laser.transform.position = hand.pinch.pinchStart;
        laser.transform.rotation = hand.pinch.pinchRotation;
      }

      int layerMask = 1 << LayerMask.NameToLayer("UI");
      if (Physics.Raycast(laser.transform.position, laser.transform.forward, out raycastHit, Mathf.Infinity, layerMask)) {
        light.SetActive(true);
        light.transform.position = raycastHit.point;
      }
      else light.SetActive(false);
    }

    int GetState() {
      var hand = isLeft ? GestureProvider.LeftHand : GestureProvider.RightHand;
      if (hand == null) return 0;
      if (hand.pinch.isPinching) return 2;
      return 1;
    }

    void OnTriggerStay(Collider other) {
      GameObject hitObject = other.gameObject;

      if (hitObject.layer == LayerMask.NameToLayer("UI")) {
        if (handPinched || debugging) {
          if (hitObject.TryGetComponent(out Toggle toggle)) {
            toggle.isOn = !toggle.isOn;
            toggle.onValueChanged.Invoke(toggle.isOn);
            handPinched = false;
          }
          else if (hitObject.TryGetComponent(out Button button)) {
            button.onClick.Invoke();
            handPinched = false;
          }
          else if (hitObject.TryGetComponent(out Slider slider)) {
            RectTransform rectTransform = slider.GetComponent<RectTransform>();
            float scale = slider.transform.parent.localScale.x;
            float startingPoint = rectTransform.position.x - rectTransform.sizeDelta.x / 2.0f * scale;
            slider.value = Mathf.Clamp((raycastHit.point.x - startingPoint) / (rectTransform.sizeDelta.x * scale), 0.0f, 1.0f);
            slider.onValueChanged.Invoke(slider.value);
          }
          else if (hitObject.TryGetComponent(out Dropdown dropdown)) {
            Button dropdownButton = hitObject.GetComponent<Button>();
            dropdownButton.onClick.Invoke();
            handPinched = false;
          }
          else if (hitObject.TryGetComponent(out TMP_InputField inputField)) {
            inputField.onSelect.Invoke(inputField.text);
            handPinched = false;
          }
          else if (hitObject.TryGetComponent(out RawImage rawImage)) {
            RaycastHit raycastHit;
            int layerMask = 1 << LayerMask.NameToLayer("UI Canvas");
            Physics.Raycast(laser.transform.position, laser.transform.forward, out raycastHit, Mathf.Infinity, layerMask);
            RectTransform rectTransform = hitObject.GetComponent<RectTransform>();
            if (initialGrab) {
              offset = rectTransform.position - raycastHit.point;
              initialGrab = false;
            }
            rectTransform.position = raycastHit.point + offset;
          }
        }
      }
    }
  }
}
