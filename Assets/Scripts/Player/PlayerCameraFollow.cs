using UnityEngine;

namespace Player
{
    public class PlayerCameraFollow : MonoBehaviour
    {  
        [SerializeField] private new Transform camera;
     
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

       private  void Update()
        {
            float cameraRotationY = camera.rotation.eulerAngles.y;
            var cameraTransform = transform;
            var cameraRotation = cameraTransform.rotation;
            Vector3 newRotation = new Vector3(cameraRotation.eulerAngles.x, cameraRotationY, cameraRotation.eulerAngles.z);
            transform.rotation = Quaternion.Euler(newRotation);
        }
    }
}
