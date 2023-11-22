using UnityEngine;

namespace Assets.Scripts
{
    public class Moving : MonoBehaviour
    {
        [SerializeField] private float MaxVelocity = 3f;
        [SerializeField] private float RotationSpeed = 0.02f;
        private Rigidbody2D body;
        private bool facingRight = false;
        void Start()
        {
            body = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            // Mouse control
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z += Camera.main.nearClipPlane;
            if (mousePosition.x < transform.position.x && facingRight)
            {
                FlipBody();
            }
            else
            if (mousePosition.x > transform.position.x && !facingRight)
            {
                FlipBody();
            }
        }

        void FixedUpdate()
        {
            var yAxis = Input.GetAxis("Vertical");
            var xAxis = Input.GetAxis("Horizontal");
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
            body.velocity = transform.up * amount * MaxVelocity;
        }

        private void FlipBody()
        {
            facingRight = !facingRight;
            transform.Rotate(Vector3.up * 180);
        }

        private void Rotate(Transform t, float amount)
        {
            t.Rotate(0,0,amount);
        }
    }
}
