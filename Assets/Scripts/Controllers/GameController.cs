//////////////////////////////////////////////////////
// Assignment/Lab/Project: SGD285-JumpyStreet
// Name: Julian Davis
// Section: 2021FA.SGD.285
// Instructor: Aurore Wold
// Date: 10/25/2021
//////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject[] gameVehicles;
    public GameObject[] carsleft;
    public GameObject[] carsright;
    public GameObject[] boatsleft;
    public GameObject[] boatsright;
    public GameObject miscleft;
    public GameObject miscright;
    public GameObject train;

    public GameObject prize;
    public int numberOfPrizes;
    public float min, max;
    private List<GameObject> prizes;

    public GameObject[] gameTiles;

    void Start()
    {
        StartCoroutine(DelayedSpawn());
    }

    public void VehicleControl()
    {
        for (int ti = 0; ti < gameTiles.Length; ti++)
        {
            if (gameTiles[ti].name == "WhiteTile")
            {
                // Make more misc items
                Instantiate(miscleft, gameTiles[ti].transform, false);
                Instantiate(miscright, gameTiles[ti].transform, false);
            }

            else if (gameTiles[ti].name == "BlueTile")
            {
                // Make more boats
                int le = Random.Range(0, 2);
                Instantiate(boatsleft[le], gameTiles[ti].transform, false);
            }

            else if (gameTiles[ti].name == "PurpleTile")
            {
                // Make more boats
                int de = Random.Range(0, 1);
                Instantiate(boatsright[de], gameTiles[ti].transform, false);
            }

            else if (gameTiles[ti].name == "RedTile")
            {
                // Make more cars
                int ca = Random.Range(0, 8);
                Instantiate(carsleft[ca], gameTiles[ti].transform, false);
                Instantiate(carsright[ca], gameTiles[ti].transform, false);
            }

            else if (gameTiles[ti].name == "OrangeTile")
            {
                // Make more cars
                int car = Random.Range(0, 8);
                Instantiate(carsleft[car], gameTiles[ti].transform, false);
                Instantiate(carsright[car], gameTiles[ti].transform, false);
            }

            else if (gameTiles[ti].name == "YellowTile")
            {
                // Only one train at a time
            }

            else 
            {
            // this would be green gameTiles, but we don't do anything to them
            }
        }

    }

    void PlacePrizes()
    {
        prizes = new List<GameObject>();
        for (int i = 0; i < numberOfPrizes; i++)
        {
            Instantiate(prize, GeneratePosition(), Quaternion.identity);
            prizes.Add(prize);
        }

        Vector3 GeneratePosition()
        {
            float x, y, z;
            x = UnityEngine.Random.Range(-4f, 4f);
            y = -5.47f;
            z = UnityEngine.Random.Range(min, max);
            return new Vector3(x, y, z);
        }
    }

    IEnumerator DelayedSpawn()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        yield return new WaitForSeconds(3.0f);
        VehicleControl();
        Debug.Log("Step 1 : " + Time.time);

        //yield on a new YieldInstruction that waits for 3 seconds.
        yield return new WaitForSeconds(3.0f);
        PlacePrizes();
        Debug.Log("Step 2 : " + Time.time);

        //yield on a new YieldInstruction that waits for 3 seconds.
        yield return new WaitForSeconds(5.0f);
        VehicleControl();
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

}
