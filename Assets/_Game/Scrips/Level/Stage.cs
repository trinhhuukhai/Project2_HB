using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum ColorType { Default , Black , Red, Blue , Green, Yellow , Orange , Brown, Violet}

public class Stage : MonoBehaviour
{
    public Transform[] brickPoints; //list diem brickPoint

    public List<Vector3> emptyPoint = new List<Vector3>(); //list vi tri emptyPoint

    [SerializeField] Brick brickPrefab;

    internal void OnInit()
    {
        for (int i = 0; i < brickPoints.Length; i++)
        {
            emptyPoint.Add(brickPoints[i].position);
        }
    }

    public void InitColor(ColorType colorType, int amount)
    {
        //int amout = brickPoints.Length / LevelManager.Ins.CharacterAmount;
        for (int i = 0; i < amount; i++)
        {
            NewBrick(colorType);
        }
    }

    public void NewBrick(ColorType colorType)
    {
        if (emptyPoint.Count > 0 ) 
        { 
            int rand = Random.Range(0, emptyPoint.Count);
            //Brick brick = Instantiate(brickPrefab, emptyPoint[Random.Range(0, emptyPoint.Count)], Quaternion.identity);
            Brick brick = Instantiate(brickPrefab, emptyPoint[rand], Quaternion.identity);
            brick.stage = this;
            brick.ChangeColor(colorType);
            emptyPoint.RemoveAt(rand);
            bricks.Add(brick);
        }
    }

    internal void RemoveBricks(Brick brick)
    {
        emptyPoint.Add(brick.transform.position);
        bricks.Remove(brick);
    }



    public List<Brick> bricks = new List<Brick>();
    
    private double CalculateDistance(Bot t, Brick brick)
    {
        float deltaX = t.transform.position.x - brick.transform.position.x;
        float deltaZ = t.transform.position.z - brick.transform.position.z;
        return Math.Sqrt(deltaX * deltaX + deltaZ * deltaZ);
    }
    internal Brick SeekBrickPoint(Bot t, ColorType colorType) // tim vi tri brick danh cho Bot 
    {
        Brick closestBrick = null;
        double minDistance = double.MaxValue;
        foreach (var brick in bricks)
        {
            if (brick.colorType == colorType)
            {
                double distance = CalculateDistance(t, brick);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestBrick = brick;
                }
            }
        }
    
        return closestBrick;
    }
}
 