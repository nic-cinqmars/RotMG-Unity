using System;
using System.Collections.Generic;
using System.Linq;
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
                directionToSprite.Add(RIGHT, CreateDirectionDictionary(maskedImageSet, 0, false));
            }
        }

        private Dictionary<int, List<MaskedImage>> CreateDirectionDictionary(MaskedImageSet imageSet, int index, bool flipped)
        {
            return new Dictionary<int, List<MaskedImage>>();
        }
    }
}
