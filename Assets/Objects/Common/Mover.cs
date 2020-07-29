using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeReference] public Rigidbody Subject;
    [SerializeField] public float Speed = 1f;
    [SerializeField] public float JumpHeight = 1f;

    public Vector2 Motion { get; protected set; }
    public bool IsMoving { get; protected set; }

    // jumpstuff:
    public bool IsJumping { get; protected set; }
    public bool IsJumpInProgress { get; protected set; }
    public bool IsGrounded { get; protected set; }

    private Vector3 _jumpVelocity;
    private Collider _ground;

    void OnEnable()
    {
        if (this.Subject == null)
        {
            if (this.TryGetComponent(out Rigidbody gameObjectRB))
            {
                Debug.LogWarning($"{this.name}: taking RB of gameObject!");
                this.Subject = gameObjectRB;
            }
            else
            {
                Debug.LogError($"{this.name}: can't find rigidbody!");
            }
        }

        this._jumpVelocity = Mover.CalculateJumpVelocity(this.JumpHeight) * (-Physics.gravity.normalized);
    }

    public void SetMotion(Vector2 motion)
    {
        if (motion == Vector2.zero)
        {
            this.IsMoving = false;
        }
        else
        {
            this.IsMoving = true;
        }

        this.Motion = motion.normalized;
    }

    public void SetJumping(bool jumping)
    {
        this.IsJumping = jumping;
    }

    public void Stop()
    {
        this.Motion = Vector3.zero;
        this.IsMoving = false;
    }

    private static Vector3 TranslateMotion(Vector2 motion, float y = 0f)
    {
        return new Vector3(motion.x, y, motion.y);
    }
    private static Vector2 TranslateMotion(Vector3 motion)
    {
        return new Vector2(motion.x, motion.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 wantedVelocity = Mover.TranslateMotion(this.Motion * this.Speed, this.Subject.velocity.y);

        this.Subject.velocity = Vector3.Lerp(this.Subject.velocity, wantedVelocity, 0.1f);

        if (this.IsJumping && this.IsGrounded && (this.IsJumpInProgress == false))
        {
            this.Subject.velocity += this._jumpVelocity;
            this.IsJumpInProgress = true;
        }
        else
        {
            this.IsJumpInProgress = false;
        }
    }
    
    void DebugCollision(bool enter, Collision collision)
    {
        string e = "exit";
        Color line = Color.blue;

        if (enter)
        {
            e = "enter";
            line = Color.red;
        }

        foreach (ContactPoint pt in collision.contacts)
        {
            float cos = Mover.CalculateDotProductCosine(
                Physics.gravity, pt.normal,
                out float dot
                );
            print($"{this.name}: [{e}] {collision.collider.name} -> normal: {pt.normal}; dot: {dot}; cos: {cos}");

            Debug.DrawLine(pt.point, pt.point + pt.normal, line, 1f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.DebugCollision(true, collision);
        // when colliding with a potential ground.

        foreach (ContactPoint pt in collision.contacts)
        {
            // if the normal direction has something to do with gravity - it's good enough to be a ground!
            float dotCosinus = Mover.CalculateDotProductCosine(Physics.gravity, pt.normal);

            if (dotCosinus < 0f)  // => the normal of the collision has a force that goes against gravity.
            {
                this.IsGrounded = true;
                this._ground = collision.collider;
                break; // no reason to look for other grounds..?
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        this.DebugCollision(false, collision);
        // when not touching the ground anymore.
        if (collision.collider == this._ground)
        {
            this.IsGrounded = false;
            this._ground = collision.collider;
        }
    }

    //////////////////////////////////////// UTILS:
    public static float CalculateJumpVelocity(
        float desiredJumpHeigth,
        float gravity
        )
    {
        return Mathf.Sqrt(-2 * gravity * desiredJumpHeigth);
    }
    public static float CalculateJumpVelocity(float desiredJumpHeigth)
    {
        return Mover.CalculateJumpVelocity(
            desiredJumpHeigth,
            -Physics.gravity.magnitude
            );
    }

    public static float CalculateDotProductCosine(
        Vector3 one,
        Vector3 another,
        out float dot
        )
    {
        dot = Vector3.Dot(one, another);
        return dot / (one.magnitude * another.magnitude); ;
    }
    public static float CalculateDotProductCosine(Vector3 one, Vector3 another)
    {
        return Mover.CalculateDotProductCosine(one, another, out float dot);
    }
}
