//////////////////////////////////////////////////////
// Assignment/Lab/Project: SGD285-JumpyStreet
// Name: Julian Davis
// Section: 2021FA.SGD.285
// Instructor: Aurore Wold
// Date: 10/25/2021
//////////////////////////////////////////////////////
using System;
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
    private Collider collider;
    private string lastTile;

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

    void Awake() 
    {
        lastTile = "Blank";
    }

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
            int random = UnityEngine.Random.Range(0, 7);
            switch (random)
            {
                case 0:
                    if (lastTile == "White")
                    {
                        // Set the new material on the GameObject
                        meshRenderer.material = materials[2];
                        gameTiles[i].name = "BlueTile";
                        gameTiles[i].tag = "Water";
                        blueTileCount++;
                        // Make the boats
                        collider = gameTiles[i].GetComponent<Collider>();
                        collider.GetComponent<Collider>().isTrigger = true;
                        int l = UnityEngine.Random.Range(0, boatsleft.Length);
                        Instantiate(boatsleft[l], gameTiles[i].transform, false);
                        lastTile = "Blue";
                        break;
                    }

                    else 
                    {
                        // Set the new material on the GameObject
                        meshRenderer.material = materials[0];
                        gameTiles[i].name = "WhiteTile";
                        whiteTileCount++;
                        // Make the misc items
                        Instantiate(miscleft, gameTiles[i].transform, false);
                        Instantiate(miscright, gameTiles[i].transform, false);
                        lastTile = "White";
                        break;
                    }

                case 1:
                    if (lastTile == "Green")
                    {
                        // Set the new material on the GameObject
                        meshRenderer.material = materials[3];
                        gameTiles[i].name = "RedTile";
                        redTileCount++;
                        // Make the cars
                        int c = UnityEngine.Random.Range(0, 8);
                        Instantiate(carsleft[c], gameTiles[i].transform, false);
                        Instantiate(carsright[c], gameTiles[i].transform, false);
                        lastTile = "Red";
                        break;
                    }

                    else 
                    {
                        // Set the new material on the GameObject
                        meshRenderer.material = materials[1];
                        gameTiles[i].name = "GreenTile";
                        greenTileCount++;
                        lastTile = "Green";
                        break;
                    }

                case 2:
                    if (lastTile == "Blue")
                    {
                        // Set the new material on the GameObject
                        meshRenderer.material = materials[5];
                        gameTiles[i].name = "OrangeTile";
                        orangeTileCount++;
                        // Make the cars
                        int v = UnityEngine.Random.Range(0, 8);
                        Instantiate(carsleft[v], gameTiles[i].transform, false);
                        Instantiate(carsright[v], gameTiles[i].transform, false);
                        lastTile = "Orange";
                        break;
                    }

                    else 
                    {
                        // Set the new material on the GameObject
                        meshRenderer.material = materials[2];
                        gameTiles[i].name = "BlueTile";
                        gameTiles[i].tag = "Water";
                        blueTileCount++;
                        // Make the boats
                        collider = gameTiles[i].GetComponent<Collider>();
                        collider.GetComponent<Collider>().isTrigger = true;
                        int l = UnityEngine.Random.Range(0, boatsleft.Length);
                        Instantiate(boatsleft[l], gameTiles[i].transform, false);
                        lastTile = "Blue";
                        break;
                    }

                case 3:
                    if (lastTile == "Red")
                    {
                        // Set the new material on the GameObject
                        meshRenderer.material = materials[6];
                        gameTiles[i].name = "PurpleTile";
                        gameTiles[i].tag = "Water";
                        purpleTileCount++;
                        // Make the boats
                        collider = gameTiles[i].GetComponent<Collider>();
                        collider.GetComponent<Collider>().isTrigger = true;
                        int d = UnityEngine.Random.Range(0, boatsright.Length);
                        Instantiate(boatsright[d], gameTiles[i].transform, false);
                        lastTile = "Purple";
                        break;
                    }

                    else 
                    {
                        // Set the new material on the GameObject
                        meshRenderer.material = materials[3];
                        gameTiles[i].name = "RedTile";
                        redTileCount++;
                        // Make the cars
                        int c = UnityEngine.Random.Range(0, 8);
                        Instantiate(carsleft[c], gameTiles[i].transform, false);
                        Instantiate(carsright[c], gameTiles[i].transform, false);
                        lastTile = "Red";
                        break;
                    }

                case 4:
                    if (lastTile == "Purple")
                    {
                        // Set the new material on the GameObject
                        meshRenderer.material = materials[0];
                        gameTiles[i].name = "WhiteTile";
                        whiteTileCount++;
                        // Make the misc items
                        Instantiate(miscleft, gameTiles[i].transform, false);
                        Instantiate(miscright, gameTiles[i].transform, false);
                        lastTile = "White";
                        break;
                    }

                    else
                    {
                        // Set the new material on the GameObject
                        meshRenderer.material = materials[6];
                        gameTiles[i].name = "PurpleTile";
                        gameTiles[i].tag = "Water";
                        purpleTileCount++;
                        // Make the boats
                        collider = gameTiles[i].GetComponent<Collider>();
                        collider.GetComponent<Collider>().isTrigger = true;
                        int d = UnityEngine.Random.Range(0, boatsright.Length);
                        Instantiate(boatsright[d], gameTiles[i].transform, false);
                        lastTile = "Purple";
                        break;
                    }

                case 5:
                    if (lastTile == "Orange")
                    {
                        // Set the new material on the GameObject
                        meshRenderer.material = materials[3];
                        gameTiles[i].name = "RedTile";
                        redTileCount++;
                        // Make the cars
                        int c = UnityEngine.Random.Range(0, 8);
                        Instantiate(carsleft[c], gameTiles[i].transform, false);
                        Instantiate(carsright[c], gameTiles[i].transform, false);
                        lastTile = "Red";
                        break;
                    }

                    else
                    {
                        // Set the new material on the GameObject
                        meshRenderer.material = materials[5];
                        gameTiles[i].name = "OrangeTile";
                        orangeTileCount++;
                        // Make the cars
                        int v = UnityEngine.Random.Range(0, 8);
                        Instantiate(carsleft[v], gameTiles[i].transform, false);
                        Instantiate(carsright[v], gameTiles[i].transform, false);
                        lastTile = "Orange";
                        break;
                    }

                case 6:
                    if (lastTile == "Yellow")
                    {
                        // Set the new material on the GameObject
                        meshRenderer.material = materials[3];
                        gameTiles[i].name = "RedTile";
                        redTileCount++;
                        // Make the cars
                        int c = UnityEngine.Random.Range(0, 8);
                        Instantiate(carsleft[c], gameTiles[i].transform, false);
                        Instantiate(carsright[c], gameTiles[i].transform, false);
                        lastTile = "Red";
                        break;
                    }

                    else
                    {
                        // Set the new material on the GameObject
                        meshRenderer.material = materials[4];
                        gameTiles[i].name = "YellowTile";
                        yellowTileCount++;
                        // Make the train
                        Instantiate(train, gameTiles[i].transform, false);
                        lastTile = "Yellow";
                        break;
                    }

                default:
                // Set the new material on the GameObject
                    meshRenderer.material = materials[0];
                    gameTiles[i].name = "WhiteTile";
                    whiteTileCount++;
                    // Make the misc items
                    Instantiate(miscleft, gameTiles[i].transform, false);
                    Instantiate(miscright, gameTiles[i].transform, false);
                    lastTile = "White";
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
