using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlaneMaker
{
    class Intersections
    {
        static Color[] avionTextureData;
        static Color[] missileTextureData;	

        public static bool intersectPixel(Texture2D missileTexture, Texture2D avionTexture,
                                    Rectangle rectangleA, Rectangle rectangleB)
        {
            try
            {
                int top = Math.Max(rectangleA.Top, rectangleB.Top);
                int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
                int left = Math.Max(rectangleA.Left, rectangleB.Left);
                int right = Math.Min(rectangleA.Right, rectangleB.Right);

                // Extract collision data
                avionTextureData = new Color[avionTexture.Width * avionTexture.Height];
                avionTexture.GetData(avionTextureData);
                missileTextureData =
                    new Color[missileTexture.Width * missileTexture.Height];
                missileTexture.GetData(missileTextureData);

                for (int y = top; y < bottom; y++)
                {
                    for (int x = left; x < right; x++)
                    {
                        // Get the color of both pixels at this point
                        Color colorA = missileTextureData[(x - rectangleA.Left) +
                                             (y - rectangleA.Top) * rectangleA.Width];
                        Color colorB = avionTextureData[(x - rectangleB.Left) +
                                             (y - rectangleB.Top) * rectangleB.Width];

                        // If both pixels are not completely transparent,
                        if (colorA.A != 0 && colorB.A != 0)
                        {
                            // then an intersection has been found
                            return true;
                        }
                    }
                }

                // No intersection found
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
