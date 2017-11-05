using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class scr_DiamondSquare : MonoBehaviour{

    public static Random rnd = new Random();
    public static int w, h;

    public float[,] Generate(int width, int height, int radius, float amplitude)
    {
        w = width;

        h = height;

        float[,] heightmap = InitializeArray(w, h);

        heightmap = DiamondSquare(heightmap, radius, amplitude);

        return heightmap;
    }

    static float[,] InitializeArray(int width, int height)
    {

        float[,] nArr = new float[height, width];

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                nArr[x, y] = 0;
            }
        }

        //initialize corner values
        nArr[0, 0] = rnd.Next(2, 10);
        nArr[width - 1, 0] = rnd.Next(2, 10);
        nArr[0, height - 1] = rnd.Next(2, 10);
        nArr[width - 1, height - 1] = rnd.Next(2, 10);

        Debug.Log("Corners >> TL >> " + nArr[0, 0] + " TR >>" + nArr[width - 1, 0] + " BL >> " + nArr[0, height - 1] + " BR >> " + nArr[width - 1, height - 1]);

        return nArr;
    }

    static float[,] DiamondSquare(float[,] hm, int rad, float amp)
    {
        hm = Squares(hm, rad, amp);
        hm = Diamonds(hm, rad, amp);

        if (rad / 2 >= 1)
        {
            return DiamondSquare(hm, rad / 2, amp);
        }
        else
        {
            return hm;
        }
    }

    static float[,] Squares(float[,] hm, int rad, float amp)
    {

        for (int x = rad; x < w; x += (rad * 2))
        {
            for (int y = rad; y < h; y += (rad * 2))
            {
                hm = SquareStep(hm, x, y, rad, amp);
            }
        }

        return hm;
    }

    static float[,] Diamonds(float[,] hm, int rad, float amp)
    {
        int yIteration = 0;
        for (int y = 0; y < h; y += rad)
        {
            int shift = yIteration % 2 == 0 ? rad : 0;
            for (int x = shift; x < w; x += (rad * 2))
            {
                hm = DiamondStep(hm, x, y, rad, amp);
            }
            yIteration++;
        }

        return hm;
    }

    static float[,] SquareStep(float[,] hm, int x, int y, int rad, float amp)
    {
        float nHeight = (Average4(
            hm[x - rad < 0 ? 0 : x - rad, y - rad < 0 ? 0 : y - rad],
            hm[x - rad < 0 ? 0 : x - rad, y + rad > h - 1 ? h - 1 : y + rad],
            hm[x + rad > w - 1 ? w - 1 : x + rad, y - rad < 0 ? 0 : y - rad],
            hm[x + rad > w - 1 ? w - 1 : x + rad, y + rad > h - 1 ? h - 1 : y + rad],true)
            + (float)rnd.NextDouble()*amp);

        hm[x, y] = (float)Math.Round(nHeight, 2);

        return hm;
    }

    static float[,] DiamondStep(float[,] hm, int x, int y, int rad, float amp)
    {
        float nHeight = (Average4(
            hm[x - rad < 0 ? 0 : x - rad, y],
            hm[x + rad > w - 1 ? w - 1 : x + rad, y],
            hm[x, y - rad < 0 ? 0 : y - rad],
            hm[x, y + rad > h - 1 ? h - 1 : y + rad],false)
            + (float)rnd.NextDouble() * amp);

        hm[x, y] = (float)Math.Round(nHeight, 2);

        return hm;
    }

    private static float Average4(float a, float b, float c, float d, bool isSquare)
    {
        float x = ((a + b + c + d) / 4);
        Debug.Log(a + " , " + b + " , " + c + " , " + d + " = " + x + " << " + isSquare);
        return x;
    }
}

