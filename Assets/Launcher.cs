using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] Transform projectilePrefab; //Prefab for projectile being fired
    [SerializeField] Transform SpawnPoint; //Transform for where the projectile will spawn
    [SerializeField] LineRenderer LineRenderer; //Line renderer to draw trajectory

    [SerializeField] float Launchforce = 1.5f;
    [SerializeField] float trajectoryTimeStep = 0.05f;
    [SerializeField] int trajectoryStepCount = 15;

    Vector2 velocity, startMousePos, currentMousePos;
    //initial velocity, starting position of mouse at left click, position of mouse when click held

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Calculation of where the mouse position is when left click initiated
        }

        if (Input.GetMouseButton(0))
        {
            currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            velocity = (startMousePos - currentMousePos) * Launchforce;

            DrawTrajectory();
        }

        if (Input.GetMouseButtonUp(0)) //When left click is released by user
        {
            FireProjectile();
        }
        Rotate();
    }

    void DrawTrajectory()
    {
        Vector3[] position = new Vector3[trajectoryStepCount]; //Vector3 array that stores position vectors
        for (int i = 0; i < trajectoryStepCount; i++)
        {
            float t = i * trajectoryTimeStep; //calculate time elapsed since projectile launched
            Vector3 pos = (Vector2)SpawnPoint.position + velocity * t + 0.5f * Physics2D.gravity * t * t; //position of vector at a given time
            position[i] = pos;
        }

        LineRenderer.SetPositions(position); //Set line renderer to positions in array


    }

    void Rotate()//Rotates the bow with the direction of the mouse
    {
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void FireProjectile()
    {
        
        Transform pr = Instantiate(projectilePrefab, SpawnPoint.position, Quaternion.identity); //Instantiates new prevab for arrow
        pr.GetComponent<Rigidbody2D>().velocity = velocity; //gives projectile velocity calculated when trajectory drawn
        
    }

    void ClearTrajectory()//Clear the trajectory line after fired
    {
        LineRenderer.positionCount = 0;
    }

}
