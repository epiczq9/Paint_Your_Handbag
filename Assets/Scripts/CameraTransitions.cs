using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraTransitions : MonoBehaviour
{
    public Transform[] views;
    [SerializeField] private float transitionSpeed;
    private Transform currentView;
    public UnityEvent cameraStoppedEvent;

    // Start is called before the first frame update
    void Start() {
        currentView = transform;
    }

    // Update is called once per frame
    void LateUpdate() {
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);

        Vector3 currentAngle = new Vector3(
            Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentView.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
            Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentView.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
            Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentView.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed)
            );
        transform.eulerAngles = currentAngle;

        if (CameraStopped()) {
            cameraStoppedEvent.Invoke();
        }
    }

    public bool CameraStopped() {
        return (Vector3.Distance(transform.position, currentView.position) < 0.5f);
    }

    public void TransitionCameraToPosition(int i) {
        currentView = views[i];
    }

    public void TransitionCameraToRandomPosition() {
        int randomPos = Random.Range(1, 6);
        currentView = views[randomPos];
    }
}
