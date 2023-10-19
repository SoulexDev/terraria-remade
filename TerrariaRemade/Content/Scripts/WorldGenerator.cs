using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using TerrariaRemade.Content.Engine;
using System.Diagnostics;
using System.Numerics;

namespace TerrariaRemade.Content.Scripts
{
    public static class WorldGenerator
    {
        public static NoiseSetting groundNoise;
        public static NoiseSetting hillNoise;

        public static NoiseSetting transitionJitter;

        public static NoiseSetting tinNoise;
        public static NoiseSetting ironNoise;
        public static NoiseSetting titaniumNoise;

        public static NoiseSetting caveNoiseOne;
        public static NoiseSetting caveNoiseTwo;

        public static int worldHeight = 32;
        public static int stoneDepth = 45;
        public static int caveDepth = 40;

        private static Random random = new Random(DateTime.Now.Millisecond);
        public static void GenerateWorld()
        {
            int seed = random.Next(int.MinValue + 10, int.MaxValue - 10);

            groundNoise = new NoiseSetting(seed, FastNoiseLite.NoiseType.Perlin, FastNoiseLite.FractalType.None, 3, 2, 0.001f, 16);
            hillNoise = new NoiseSetting(seed, FastNoiseLite.NoiseType.Perlin, FastNoiseLite.FractalType.FBm, 2, 2, 0.0005f, 16);

            transitionJitter = new NoiseSetting(seed, FastNoiseLite.NoiseType.OpenSimplex2, FastNoiseLite.FractalType.FBm, 4, 2, 0.0025f, 8);

            tinNoise = new NoiseSetting(seed + 1, FastNoiseLite.NoiseType.Value, FastNoiseLite.FractalType.None, 2, 1, 0.005f, 1);
            ironNoise = new NoiseSetting(seed - 1, FastNoiseLite.NoiseType.Value, FastNoiseLite.FractalType.None, 2, 1, 0.005f, 1);
            titaniumNoise = new NoiseSetting(seed + 2, FastNoiseLite.NoiseType.Value, FastNoiseLite.FractalType.None, 2, 1, 0.005f, 1);

            caveNoiseOne = new NoiseSetting(seed, FastNoiseLite.NoiseType.OpenSimplex2, FastNoiseLite.FractalType.FBm, 3, 1.5f, 0.001f, 1);
            caveNoiseTwo = new NoiseSetting(seed, FastNoiseLite.NoiseType.Cellular, FastNoiseLite.FractalType.Ridged, 2, 1, 0.001f, 1);

            for (int i = 0; i < TileManager.chunkAmount; i++)
            {
                TileMap map = new TileMap();

                map.transform.position.X = i * map.map.GetLength(0) * TileManager.tileSize * TileManager.scale;
                map.tileSize = TileManager.tileSize;
                map.scale = TileManager.scale;

                map.name = i.ToString();

                TileManager.chunks.Add(map);
            }
            GenerateBaseTerrain();
            GenerateOre();
            //GenerateCaves();
        }
        public static void GenerateBaseTerrain()
        {
            foreach (TileMap chunk in TileManager.chunks)
            {
                int xLength = chunk.map.GetLength(0);
                int yLength = chunk.map.GetLength(1);

                for (int x = 0; x < xLength; x++)
                {
                    float positionInNoiseMap = x * TileManager.tileSize * TileManager.scale + chunk.transform.position.X;
                    float noise = groundNoise.GetNoise(positionInNoiseMap, 0) + hillNoise.GetNoise(positionInNoiseMap, 0);

                    float transitionNoise = transitionJitter.GetNoise(positionInNoiseMap, 0) + noise/16;

                    int yPos = (int)(worldHeight - noise);

                    yPos = Math.Clamp(yPos, 0, yLength);

                    for (int y = yPos; y < yLength; y++)
                    {
                        int tileID = y == yPos ? 1 : y + transitionNoise > stoneDepth ? 3 : 2;

                        chunk.FillTile(x, y, tileID);
                    }
                }
            }
        }
        public static void GenerateOre()
        {
            foreach (TileMap chunk in TileManager.chunks)
            {
                int xLength = chunk.map.GetLength(0);
                int yLength = chunk.map.GetLength(1);

                for (int x = 0; x < xLength; x++)
                {
                    float positionInNoiseMapX = x * TileManager.tileSize * TileManager.scale + chunk.transform.position.X;
                    float transitionNoise = transitionJitter.GetNoise(positionInNoiseMapX, 0);

                    for (int y = worldHeight; y < yLength; y++)
                    {
                        float positionInNoiseMapY = y * TileManager.tileSize * TileManager.scale;

                        float noiseOne = tinNoise.GetNoise(positionInNoiseMapX, positionInNoiseMapY);
                        float noiseTwo = ironNoise.GetNoise(positionInNoiseMapX, positionInNoiseMapY);
                        float noiseThree = titaniumNoise.GetNoise(positionInNoiseMapX, positionInNoiseMapY);

                        if (y - transitionNoise > caveDepth && noiseThree < 0.1f)
                            chunk.FillTile(x, y, 6);

                        if (y - transitionNoise > caveDepth && noiseTwo < 0.1f)
                            chunk.FillTile(x, y, 5);

                        if (y - transitionNoise > caveDepth && noiseOne < 0.1f)
                            chunk.FillTile(x, y, 4);
                    }
                }
            }
        }
        public static void GenerateCaves()
        {
            foreach (TileMap chunk in TileManager.chunks)
            {
                int xLength = chunk.map.GetLength(0);
                int yLength = chunk.map.GetLength(1);

                for (int x = 0; x < xLength; x++)
                {
                    float positionInNoiseMapX = x * TileManager.tileSize * TileManager.scale + chunk.transform.position.X;
                    float transitionNoise = transitionJitter.GetNoise(positionInNoiseMapX, 0);

                    for (int y = worldHeight; y < yLength; y++)
                    {
                        float positionInNoiseMapY = y * TileManager.tileSize * TileManager.scale;
                        float noise = MathF.Max(caveNoiseOne.GetNoise(positionInNoiseMapX, positionInNoiseMapY), caveNoiseOne.GetNoise(positionInNoiseMapX, positionInNoiseMapY) + 0.1f);

                        if(y - transitionNoise > caveDepth && noise > 0.6f)
                            chunk.FillTile(x, y, 0);
                    }
                }
            }
        }
    }
    public class NoiseSetting
    {
        private FastNoiseLite noise;
        private float height = 16;

        public float GetNoise(float x, float y)
        {
            return (noise.GetNoise(x, y) + 1)/2 * height;
        }
        public NoiseSetting(int seed, FastNoiseLite.NoiseType noiseType, FastNoiseLite.FractalType fractalType, int octaves, float lacunarity, float frequency, float height)
        {
            noise = new FastNoiseLite(seed);
            noise.SetNoiseType(noiseType);
            noise.SetFractalType(fractalType);
            noise.SetFractalOctaves(octaves);
            noise.SetFractalLacunarity(lacunarity);
            noise.SetFrequency(frequency);
            this.height = height;
        }
    }
}