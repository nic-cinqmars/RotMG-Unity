using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RotmgClient.Util
{
    public class MaskedImage
    {
        private Texture2D image;
        private Texture2D mask;

        public MaskedImage(Texture2D image, Texture2D mask)
        {
            this.image = image;
            this.mask = mask;
        }
        
        public MaskedImage Mirror()
        {
            Texture2D flippedImage = null;
            Texture2D flippedMask = null;
            if (image != null)
            {
                flippedImage = Texture2DUtil.Mirror(image);
            }
            if (mask != null)
            {
                flippedMask = Texture2DUtil.Mirror(mask);
            }

            return new MaskedImage(flippedImage, flippedMask);
        }

        public float AmountTransparent()
        {
            return Texture2DUtil.AmountTransparent(image);
        }

        public Texture2D GetImage()
        {
            return image;
        }

        public Texture2D GetMask()
        {
            return mask;
        }
    }
}
