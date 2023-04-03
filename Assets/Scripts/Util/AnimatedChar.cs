using Org.BouncyCastle.Crypto.Signers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RotmgClient.Util
{
    public class AnimatedChar
    {
        public static readonly int RIGHT = 0;
        public static readonly int LEFT = 1;
        public static readonly int DOWN = 2;
        public static readonly int UP = 3;
        public static readonly int NUM_DIR = 4;
        public static readonly int STAND = 0;
        public static readonly int WALK = 1;
        public static readonly int ATTACK = 2;
        public static readonly int NUM_ACTION = 3;

        private MaskedImage originalImage;
        private int width;
        private int height;
        private int firstDirection;
        private Dictionary<int, Dictionary<int, List<MaskedImage>>> directionToSprite;

        public AnimatedChar(MaskedImage originalImage, int width, int height, int firstDirection)
        {
            this.originalImage = originalImage;
            this.width = width;
            this.height = height;
            this.firstDirection = firstDirection;
            directionToSprite = new Dictionary<int, Dictionary<int, List<MaskedImage>>>();

            MaskedImageSet maskedImageSet = new MaskedImageSet(originalImage, width, height);
            if (firstDirection == RIGHT)
            {
                directionToSprite.Add(RIGHT, CreateDirectionDictionary(maskedImageSet, 0, false, false));
                directionToSprite.Add(LEFT, CreateDirectionDictionary(maskedImageSet, 0, true, false));
                if (maskedImageSet.ImageCount() >= 14)
                {
                    directionToSprite.Add(DOWN, CreateDirectionDictionary(maskedImageSet, 7, false, true));
                    if (maskedImageSet.ImageCount() >= 21)
                        directionToSprite.Add(UP, CreateDirectionDictionary(maskedImageSet, 14, false, true));
                }
            }
            else if (firstDirection == DOWN)
            {
                directionToSprite.Add(DOWN, CreateDirectionDictionary(maskedImageSet, 0, false, true));
                if (maskedImageSet.ImageCount() >= 14)
                {
                    directionToSprite.Add(RIGHT, CreateDirectionDictionary(maskedImageSet, 7, false, false));
                    directionToSprite.Add(LEFT, CreateDirectionDictionary(maskedImageSet, 7, true, false));
                    if (maskedImageSet.ImageCount() >= 21)
                        directionToSprite.Add(UP, CreateDirectionDictionary(maskedImageSet, 14, false, true));
                }
            }
            else
                Debug.Log("Invalid first direction!");
        }

        public MaskedImage GetImageFromDir(int direction, int state)
        {
            List<MaskedImage> directionImage = directionToSprite[direction][state];
            if (directionImage == null)
                return null;

            return directionImage[0];
        }

        public MaskedImage GetImageFromFacing()
        {
            //Todo
            return null;
        }

        private Dictionary<int, List<MaskedImage>> CreateDirectionDictionary(MaskedImageSet imageSet, ushort startIndex, bool flipped, bool sameWalkImage)
        {
            Dictionary<int, List<MaskedImage>> stateToSpriteDictionary = new Dictionary<int, List<MaskedImage>>();
            MaskedImage standImage = imageSet.GetMaskedImageFromIndex((ushort)(startIndex + 0));
            MaskedImage walkingImage1 = imageSet.GetMaskedImageFromIndex((ushort)(startIndex + 1));
            MaskedImage walkingImage2 = imageSet.GetMaskedImageFromIndex((ushort)(startIndex + 2));
            MaskedImage attackImage1 = imageSet.GetMaskedImageFromIndex((ushort)(startIndex + 4));
            MaskedImage attackImage2 = imageSet.GetMaskedImageFromIndex((ushort)(startIndex + 5));
            MaskedImage maskImage = imageSet.GetMaskedImageFromIndex((ushort)(startIndex + 6));

            if (walkingImage2.AmountTransparent() == 1)
            {
                walkingImage2 = null;
            }
            if (attackImage1.AmountTransparent() == 1)
            {
                attackImage1 = null;
            }
            if (attackImage2.AmountTransparent() == 1)
            {
                attackImage2 = null;
            }

            if (attackImage2 != null && maskImage.AmountTransparent() != 1)
            {
                // Mask thingy TODO
            }

            List<MaskedImage> standingImages = new List<MaskedImage>();
            if (flipped)
                standingImages.Add(standImage.Mirror());
            else
                standingImages.Add(standImage);
            stateToSpriteDictionary.Add(STAND, standingImages);

            List<MaskedImage> walkingImages = new List<MaskedImage>();
            if (flipped)
                walkingImages.Add(walkingImage1.Mirror());
            else
                walkingImages.Add(walkingImage1);
            if (walkingImage2 != null)
            {
                if (flipped)
                    walkingImages.Add(walkingImage2.Mirror());
                else
                    walkingImages.Add(walkingImage2);
            }
            else
            {
                if (sameWalkImage)
                {
                    if (flipped)
                        walkingImages.Add(walkingImage1);
                    else
                        walkingImages.Add(walkingImage1.Mirror());
                }
                else
                {
                    if (flipped)
                        walkingImages.Add(standImage.Mirror());
                    else
                        walkingImages.Add(standImage);
                }
            }
            stateToSpriteDictionary.Add(WALK, walkingImages);

            List<MaskedImage> attackImages;
            if (attackImage1 == null && attackImage2 == null)
                attackImages = walkingImages;
            else
            {
                attackImages = new List<MaskedImage>();
                if (attackImage1 != null)
                {
                    if (flipped)
                        attackImages.Add(attackImage1.Mirror());
                    else
                        attackImages.Add(attackImage1);
                }
                if (attackImage2 != null)
                {
                    if (flipped)
                        attackImages.Add(attackImage2.Mirror());
                    else
                        attackImages.Add(attackImage2);
                }
            }
            stateToSpriteDictionary.Add(ATTACK, attackImages);

            return stateToSpriteDictionary;
        }
    }
}
