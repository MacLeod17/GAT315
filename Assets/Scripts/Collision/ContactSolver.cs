using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ContactSolver
{
    public static void Resolve(List<Contact> contacts)
    {
        foreach (Contact contact in contacts)
        {
            float totalInverseMass = contact.bodyA.inverseMass + contact.bodyB.inverseMass;
            Vector2 separation = contact.normal * contact.depth / totalInverseMass;

            contact.bodyA.position += separation * contact.bodyA.inverseMass;
            contact.bodyB.position -= separation * contact.bodyB.inverseMass;
        }
    }
}
