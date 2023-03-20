using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace RotmgClient.Objects
{
    public class Player : RotmgGameObject
    {
        override public void Setup(XmlNode xml)
        {
            Debug.Log("Calling Player setup");
            base.Setup(xml);
        }

        void Update()
        {
            base.Update();
        }
    }
}
