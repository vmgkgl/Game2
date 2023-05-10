using UnityEngine;

public class NoFriction : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.sharedMaterial = new PhysicsMaterial2D("NoFriction");
        rb.sharedMaterial.friction = 0;
    }
}

