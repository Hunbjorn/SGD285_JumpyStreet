using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] gameTiles;
    public Material[] materials;
    public GameObject[] carsleft;
    public GameObject[] carsright;
    public GameObject[] boatsleft;
    public GameObject[] boatsright;
    public GameObject miscleft;
    public GameObject miscright;
    public GameObject train;
    public Transform[] spawnPoints;


    public GameObject vehicle;

    //    public Transform prefab;
    //    public Vector3 spawnPoint;
    //    public int columns;
    //    public int rows;
    private int whiteTileCount;
    private int greenTileCount;
    private int redTileCount;
    private int blueTileCount;
    private int yellowTileCount;
    private int orangeTileCount;
    private int purpleTileCount;

    // Start is called before the first frame update
    void Start()
    {
        GenerategameTiles();
    }

    public void GenerategameTiles()
    {
        for (int i = 0; i < gameTiles.Length; i++)
        {
            // Get the Mesh Renderer Component from this gameObject
            MeshRenderer meshRenderer = gameTiles[i].GetComponent<MeshRenderer>();
            Material oldMaterial = meshRenderer.material;
            Debug.Log("Applied Material: " + oldMaterial.name);
            int random = Random.Range(0, 7);
            switch (random)
            {
                case 0:
                // Set the new material on the GameObject
                    meshRenderer.material = materials[0];
                    gameTiles[i].name = "WhiteTile";
                    whiteTileCount++;
                    // Make the misc items
                    Instantiate(miscleft, gameTiles[i].transform, false);
                    Instantiate(miscright, gameTiles[i].transform, false);
                    break;

                case 1:
                // Set the new material on the GameObject
                    meshRenderer.material = materials[1];
                    gameTiles[i].name = "GreenTile";
                    greenTileCount++;
                    break;

                case 2:
                // Set the new material on the GameObject
                    meshRenderer.material = materials[2];
                    gameTiles[i].name = "BlueTile";
                    blueTileCount++;
                    // Make the boats
                    int l = Random.Range(0, 2);
                    Instantiate(boatsleft[l], gameTiles[i].transform, false);
                    int r = Random.Range(0, 1);
                    Instantiate(boatsright[r], gameTiles[i].transform, false);
                    break;

                case 3:
                // Set the new material on the GameObject
                    meshRenderer.material = materials[3];
                    gameTiles[i].name = "RedTile";
                    redTileCount++;
                    // Make the cars
                    int c = Random.Range(0, 8);
                    Instantiate(carsleft[c], gameTiles[i].transform, false);
                    Instantiate(carsright[c], gameTiles[i].transform, false);
                    break;

                case 4:
                    // Set the new material on the GameObject
                    meshRenderer.material = materials[6];
                    gameTiles[i].name = "PurpleTile";
                    purpleTileCount++;
                    // Make the boats
                    int s = Random.Range(0, 2);
                    Instantiate(boatsleft[s], gameTiles[i].transform, false);
                    int d = Random.Range(0, 1);
                    Instantiate(boatsright[d], gameTiles[i].transform, false);
                    break;

                case 5:
                // Set the new material on the GameObject
                    meshRenderer.material = materials[5];
                    gameTiles[i].name = "OrangeTile";
                    orangeTileCount++;
                    // Make the cars
                    int v = Random.Range(0, 8);
                    Instantiate(carsleft[v], gameTiles[i].transform, false);
                    Instantiate(carsright[v], gameTiles[i].transform, false);
                    break;

                case 6:
                    // Set the new material on the GameObject
                    meshRenderer.material = materials[4];
                    gameTiles[i].name = "YellowTile";
                    yellowTileCount++;
                    // Make the train
                    Instantiate(train, gameTiles[i].transform, false);
                    break;

                default:
                // Set the new material on the GameObject
                    meshRenderer.material = materials[0];
                    gameTiles[i].name = "WhiteTile";
                    whiteTileCount++;
                    // Make the misc items
                    Instantiate(miscleft, gameTiles[i].transform, false);
                    Instantiate(miscright, gameTiles[i].transform, false);
                    break;
            }
        } 

        Debug.Log("white gameTiles = " + whiteTileCount);
        Debug.Log("red gameTiles = " + redTileCount);
        Debug.Log("green gameTiles = " + greenTileCount);
        Debug.Log("blue gameTiles = " + blueTileCount);
        Debug.Log("yellow gameTiles = " + yellowTileCount);
        Debug.Log("orange gameTiles = " + orangeTileCount);
        Debug.Log("purple gameTiles = " + purpleTileCount);

    }

}
