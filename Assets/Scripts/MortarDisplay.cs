using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Only used at the start-scene for display purposes.
public class MortarDisplay : MonoBehaviour {
    public Transform mortarMuzzle;
    public Transform mortarBipod;
    public Transform mortarTube;
    public Transform mortarPiston;


    public float verticalSens;
    public float horizontalSens;

    private float verticalAngle = -25f;
    private float verticalSwitchTimer = 0f;
    private int verticalDirection = 1;
    // Use this for initialization
    void Start () {
        AimVertical(1);
	}

    private void FixedUpdate()
    {
        verticalSwitchTimer += Time.deltaTime;
        if (verticalSwitchTimer > 5f)
        {
            verticalSwitchTimer = -100f;
            StartCoroutine(AimVerticalTimer());
        }

        AimHorizontal(1);
        
    }

    IEnumerator AimVerticalTimer()
    {
        verticalDirection *= -1;
        float duration = 0f;
        while (duration < 5f)
        {
            duration += Time.deltaTime;
            AimVertical(verticalDirection);
            yield return null;
        }
        yield return new WaitForSeconds(2);
        verticalSwitchTimer = 0f;
        
    }

    public void AimVertical(int pitch)
    {
        verticalAngle += Time.deltaTime * verticalSens * pitch;
        verticalAngle = Mathf.Clamp(verticalAngle, -40, -12);
        mortarTube.rotation = Quaternion.Euler(verticalAngle, mortarTube.rotation.eulerAngles.y, mortarTube.rotation.eulerAngles.z);
        VerticalAimCompensation();

    }

    public void AimHorizontal(int direction)
    {
        float angleChange = Time.deltaTime * horizontalSens;
        transform.Rotate(0, angleChange * direction, 0);
    }


    private void VerticalAimCompensation()
    {

        mortarPiston.rotation = Quaternion.LookRotation(mortarBipod.position - mortarPiston.position) * Quaternion.Euler(180, 0, 0);
        mortarBipod.rotation = Quaternion.LookRotation(mortarPiston.position - mortarBipod.position) * Quaternion.Euler(90, 0, 0);
    }
}
