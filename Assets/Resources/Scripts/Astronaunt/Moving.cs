using UnityEngine;
using UnityEngine.XR;

namespace Assets.Scripts
{
    public class Moving : MonoBehaviour
    {
        [SerializeField] private float MaxVelocity = 3f;
        [SerializeField] private float RotationSpeed = 0.02f;
        private Rigidbody2D body;
        public bool facingRight;
        private bool needFreeze;

        private HandUse handObject;
        private Oxygen playerOxygen;

        void Start()
        {
            handObject = GetComponent<HandUse>();
            body = GetComponent<Rigidbody2D>();
            playerOxygen = GetComponentInParent<Oxygen>();
        }

        void Update()
        {
            if(needFreeze)
            {
                body.velocity = Vector2.zero;
                return;
            }
            // Mouse control
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z += Camera.main.nearClipPlane;

            var jump = Input.GetAxis("Jump");
            if (Input.GetKeyDown(KeyCode.Space))
                OxygenJump(jump);
            if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                body.velocity = Vector2.zero;
                playerOxygen.RemoveOxygen(4f);
            }
            // Right
            if (((mousePosition.x < transform.position.x && mousePosition.y > transform.position.y)
                 || (mousePosition.x < transform.position.x && mousePosition.y < transform.position.y))
                && facingRight)
            {
                FlipBody();
            }
            //Left
            else
            if (((mousePosition.x > transform.position.x && mousePosition.y > transform.position.y) 
                 || (mousePosition.x > transform.position.x && mousePosition.y < transform.position.y))
                && !facingRight)
            {
                FlipBody();
            }
        }

        void FixedUpdate()
        {
            if (needFreeze)
                return;
            var yAxis = Input.GetAxis("Vertical");
            var xAxis = Input.GetAxis("Horizontal");

            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
                playerOxygen.RemoveOxygen(0.1f);

            TrustForward(yAxis);

            switch (facingRight)
            {
                case false:
                    Rotate(transform, xAxis * -RotationSpeed);
                    break;
                case true:
                    Rotate(transform, xAxis * RotationSpeed);
                    break;
            }
        }

        private void TrustForward(float amount)
        {
            body.AddForce(transform.up * amount * MaxVelocity);
        }

        private void OxygenJump(float amount)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z += Camera.main.nearClipPlane;

            var vector = -(transform.position - mousePosition);
            body.AddForce(vector * amount * MaxVelocity * 16);

            playerOxygen.RemoveOxygen(2f);
        }

        public void FlipBody()
        {
            facingRight = !facingRight;
            transform.Rotate(Vector3.up * 180);
        }

        private void Rotate(Transform t, float amount)
        {
            t.Rotate(0,0,amount);
        }

        public void Freeze()
        {
            needFreeze = true;
        }

        public void UnFreeze()
        {
            needFreeze = false;
        }

        public bool GetFreezeStatus()
        {
            return needFreeze;
        }
    }
}
