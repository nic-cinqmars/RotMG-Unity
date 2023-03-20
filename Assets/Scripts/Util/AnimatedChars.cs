using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RotmgClient.Util
{
    public class AnimatedChars
    {
        private static readonly Dictionary<string, List<AnimatedChar>> nameToAnimatedCharList = new Dictionary<string, List<AnimatedChar>>();

        public static void AddAnimatedChar(string listName, string imageSheetPath, string maskSheetPath, int sheetWidth, int sheetHeight, int charWidth, int charHeight, int firstDirection)
        {
            Texture2D imageSheet = Resources.Load<Texture2D>(imageSheetPath);
            Texture2D maskSheet = Resources.Load<Texture2D>(maskSheetPath);

            List<AnimatedChar> animatedChars = new List<AnimatedChar>();
            MaskedImageSet maskedImageSet = new MaskedImageSet(imageSheet, maskSheet, sheetWidth, sheetHeight);
            foreach (MaskedImage maskedImage in maskedImageSet.GetImages())
                animatedChars.Add(new AnimatedChar(maskedImage, charWidth, charHeight, firstDirection));

            nameToAnimatedCharList.Add(listName, animatedChars);
        }

        public static AnimatedChar GetAnimatedChar(string listName, int index)
        {
            if (nameToAnimatedCharList.TryGetValue(listName, out List<AnimatedChar> animatedChars))
            {
                if (index >= animatedChars.Count)
                {
                    return null;
                }
                else
                {
                    return animatedChars[index];
                }
            }
            return null;
        }
    }
}
