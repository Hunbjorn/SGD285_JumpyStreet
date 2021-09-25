using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int score;
    public int highscore;
    public GameObject[] gameVehicles;
    public GameObject[] carsleft;
    public GameObject[] carsright;
    public GameObject[] boatsleft;
    public GameObject[] boatsright;
    public GameObject miscleft;
    public GameObject miscright;
    public GameObject train;

    public Text scoreText;
    public Text highScoreText;
    public GameObject[] gameTiles;

    void Start()
    {
        StartCoroutine(DelayedSpawn());
    }

    void Update() 
    {
    }

    public void VehicleControl()
    {
        for (int ti = 0; ti < 21; ti++)
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
                int ri = Random.Range(0, 1);
                Instantiate(boatsright[ri], gameTiles[ti].transform, false);
            }

            else if (gameTiles[ti].name == "PurpleTile")
            {
                // Make more boats
                int si = Random.Range(0, 2);
                Instantiate(boatsleft[si], gameTiles[ti].transform, false);
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

    IEnumerator DelayedSpawn()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        VehicleControl();
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(10.0f);
        VehicleControl();
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

}
