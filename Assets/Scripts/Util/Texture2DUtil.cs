using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RotmgClient.Util
{
    public static class Texture2DUtil
    {
        public static Texture2D Mirror(Texture2D original)
        {
            int width = original.width;
            int height = original.height;

            Texture2D flipped = new Texture2D(width, height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    flipped.SetPixel(width - i - 1, j, original.GetPixel(i, j));
                }
            }

            return flipped;
        }

        public static float AmountTransparent(Texture2D image)
        {
            float transparentPixels = 0;

            for (int i = 0; i < image.width; i++)
            {
                for (int j = 0; j < image.height; j++)
                {
                    if (image.GetPixel(i, j).a == 0)
                    {
                        transparentPixels++;
                    }
                }
            }

            return transparentPixels / (image.width * image.height);
        }
    }
}
