using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSurface
{

    public static float[] GetTextureMix(Vector3 worldPos)
    {
        // returns an array containing the relative mix of textures
        // on the main terrain at this world position.
        //在当前的地形上返回一个指定点的相对混合的纹理的一个数组
        // The number of values in the array will equal the number
        // of textures added to the terrain.
        //返回的数组中的长度将等于已经添加到地形的材质数组

        Terrain terrain = Terrain.activeTerrain;
        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainPos = terrain.transform.position;

        // calculate which splat map cell the worldPos falls within (ignoring y)
        //计算指定的worldpos的网格贴图细节(忽略Y值),这里不明白他为什么要这么算,还请大侠们告知原理
        int mapX = (int)(((worldPos.x - terrainPos.x) / terrainData.size.x) * terrainData.alphamapWidth);
        int mapZ = (int)(((worldPos.z - terrainPos.z) / terrainData.size.z) * terrainData.alphamapHeight);

        // get the splat data for this cell as a 1x1xN 3d array (where N = number of textures)
        float[,,] splatmapData = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);

        // extract the 3D array data to a 1D array:
        float[] cellMix = new float[splatmapData.GetUpperBound(2) + 1];
        for (int n = 0; n < cellMix.Length; ++n)
        {
            cellMix[n] = splatmapData[0, 0, n];
        }
        return cellMix;
    }

    public static int GetMainTexture(Vector3 worldPos)
    {

        // returns the zero-based index of the most dominant texture
        // on the main terrain at this world position.
        float[] mix = GetTextureMix(worldPos);
        float maxMix = 0;
        int maxIndex = 0;
        // loop through each mix value and find the maximum
        //循环找到最大的那个就是当前贴图的index
        for (int n = 0; n < mix.Length; ++n)
        {
            if (mix[n] > maxMix)
            {
                maxIndex = n;
                maxMix = mix[n];
            }
        }

        return maxIndex;
    }
}
