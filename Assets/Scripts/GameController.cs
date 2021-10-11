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
    public GameObject prize;
    public int numberOfPrizes;
    public float min, max;
    public GameObject miscleft;
    public GameObject miscright;
    public GameObject train;

    public GameObject prefab;
    public Terrain terrain;
    public float yOffset = 0.5f;

    private float terrainWidth;
    private float terrainLength;

    private float xTerrainPos;
    private float zTerrainPos;

    public Text scoreText;
    public Text highScoreText;
    public GameObject[] gameTiles;

    void Start()
    {
        StartCoroutine(DelayedSpawn());

        PlacePrizes();

        //Get terrain size
        terrainWidth = terrain.terrainData.size.x;
        terrainLength = terrain.terrainData.size.z;

        //Get terrain position
        xTerrainPos = terrain.transform.position.x;
        zTerrainPos = terrain.transform.position.z;

        generateObjectOnTerrain();
    }

    void Update() 
    {
    }

    void PlacePrizes() 
    {
    for (int i = 0; i < numberOfPrizes; i++) 
        {
            Instantiate(prize, GeneratePosition(), Quaternion.identity);
        }

    Vector3 GeneratePosition()
    {
        float x, y, z;
        x = UnityEngine.Random.Range(-4f, 4f);
        y = -5.46f;
        z = UnityEngine.Random.Range(-4f, 4f);
        return new Vector3(x,y,z);
    }
    }

    void generateObjectOnTerrain()
    {
        //Generate random x,z,y position on the terrain
        float randX = UnityEngine.Random.Range(xTerrainPos, xTerrainPos + terrainWidth);
        float randZ = UnityEngine.Random.Range(zTerrainPos, zTerrainPos + terrainLength);
        float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ));

        //Apply Offset if needed
        yVal = yVal + yOffset;

        //Generate the Prefab on the generated position
        GameObject objInstance = (GameObject)Instantiate(prefab, new Vector3(randX, yVal, randZ), Quaternion.identity);
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
