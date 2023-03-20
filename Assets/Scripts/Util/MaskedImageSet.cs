﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RotmgClient.Util
{
    public class MaskedImageSet
    {
        private List<MaskedImage> images;

        public MaskedImageSet(Texture2D imageSheet, Texture2D maskSheet, int width, int height)
        {
            images = new List<MaskedImage>();
            ImageSet imageSet = new ImageSet(imageSheet, width, height);
            ImageSet maskSet = null;
            if (maskSheet != null)
            {
                maskSet = new ImageSet(maskSheet, width, height);
            }

            for (ushort i = 0; i < imageSet.SpriteCount(); i++)
            {
                Texture2D image = imageSet.GetSpriteFromIndex(i).texture;
                Texture2D mask = null;
                if (maskSet != null)
                {
                    mask = maskSet.GetSpriteFromIndex(i).texture;
                }
                images.Add(new MaskedImage(image, mask));
            }
        }

        public MaskedImageSet(MaskedImage maskedImage, int width, int height) : 
            this(maskedImage.GetImage(), maskedImage.GetMask(), width, height)
        {
        }
    }
}
