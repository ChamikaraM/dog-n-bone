using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
// Controls the player(Dog) movement and collision detection
public class PlayerController : MonoBehaviour
{
    public bool hideCursor = true;

    Rigidbody2D rb; // dog's physics body
    Camera cam;
    bool inputEnabled = true; // can player control the dog?

    // Optional safe-cast to prevent tunneling through walls on very fast mouse moves
    public bool useSafeCast = false;
    RaycastHit2D[] castResults = new RaycastHit2D[1];
    ContactFilter2D wallFilter;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        wallFilter.useLayerMask = true;
        wallFilter.layerMask = LayerMask.GetMask("Wall");
        wallFilter.useTriggers = false;
    }

    void OnEnable()
    {
        if (hideCursor) Cursor.visible = false;
    }

    void OnDisable()
    {
        Cursor.visible = true;
    }

    public void SetInputEnabled(bool enabled) => inputEnabled = enabled;

    void FixedUpdate()
    {
        if (!inputEnabled) return;

        // Smoothly move dog towards targetPos using Rigidbody2D
        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 target = mouseWorld;
        Vector2 current = rb.position;
        Vector2 delta = target - current;
        float dist = delta.magnitude;
        if (dist <= Mathf.Epsilon) return;

        if (useSafeCast)
        {
            // Prevent huge teleports through thin walls. We still allow touching walls
            // (Game Over), but this reduces the chance of tunneling on very fast mouse moves.
            int hits = rb.Cast(delta.normalized, wallFilter, castResults, dist);
            if (hits > 0)
            {
                float safe = Mathf.Max(0f, castResults[0].distance - 0.005f);
                rb.MovePosition(current + delta.normalized * safe);
                return;
            }
        }

        // snap dog to mouse each physics step
        rb.MovePosition(target);
    }

    // Called when dog collides with a solid wall
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Dog collided with: " + col.collider.name + " on layer " +
              LayerMask.LayerToName(col.collider.gameObject.layer));

        if (col.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Debug.Log("Hit a Wall → calling GameOver()");
            GameManager.Instance.GameOver();
        }
    }

    // Called when dog overlaps a trigger collider 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Goal"))
        {
            GameManager.Instance.Win();
        }
    }
}
