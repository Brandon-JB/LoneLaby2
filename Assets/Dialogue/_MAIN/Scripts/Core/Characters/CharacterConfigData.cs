using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS
{
    [System.Serializable]
    public class CharacterConfigData
    {
        public string name;
        public string alias;
        public Character.CharacterType characterType;
        public Sprite charNameHolder;
        public AudioClip charVoice;
        public Sprite nameOnSide;

        public CharacterConfigData Copy()
        {
            CharacterConfigData result = new CharacterConfigData();

            result.name = name;
            result.alias = alias;
            result.characterType = characterType;
            result.charNameHolder = charNameHolder;
            result.charVoice = charVoice;
            result.nameOnSide = nameOnSide;

            return result;
        }

        public static CharacterConfigData Default //the default values if a character isnt found
        {
            get
            {
                CharacterConfigData result = new CharacterConfigData();

                result.name = "";
                result.alias = "";
                result.characterType = Character.CharacterType.Text;

                return result;
            }
        }
        
    }
}