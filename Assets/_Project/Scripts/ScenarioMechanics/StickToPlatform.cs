using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToPlatform : MonoBehaviour
{
    private bool IsCollisionOnTop(Collision2D collision)
    {
        Vector3 contactNormal = collision.GetContact(0).normal;
        float angle = Vector3.Angle(contactNormal, Vector3.up);

        return Mathf.Approximately(angle, 180);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Character character;
        bool isCharacer = collision.gameObject.TryGetComponent(out character);
        if (isCharacer && IsCollisionOnTop(collision))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Character character;
        bool isCharacer = collision.gameObject.TryGetComponent(out character);
        if (isCharacer)
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
