using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Sources.GameModes.Helicopter;
using UnityEngine;

namespace GameModes.Helicopter
{
    public class LetterDistributor
    {
        public LetterDistributor(LetterSet letterSet, LetterVariant[] letterVariants, char correctLetter)
        {
            _letterInfos = letterSet.LetterInfos.ToList();
            _letterVariants = letterVariants;
            _correctLetter = correctLetter;

            RemoveCorrectLetterInfo();
        }

        private readonly LetterVariant[] _letterVariants;
        private readonly char _correctLetter;

        private readonly List<LetterInfo> _letterInfos;

        public LetterVariant CorrectLetterVariant { get; private set; }
        
        public void Distribute()
        {
            int correctVariantIndex = Random.Range(0, _letterVariants.Length);

            CorrectLetterVariant = _letterVariants[correctVariantIndex];
            
            foreach (LetterVariant variant in _letterVariants)
            {
                if (variant == CorrectLetterVariant)
                {
                    variant.Init(_correctLetter);
                    continue;
                }
                
                variant.Init(GetRandomFreeCharacter());
            }
        }

        private char GetRandomFreeCharacter()
        {
            int index = Random.Range(0, _letterInfos.Count);

            char result = _letterInfos[index].Character;

            _letterInfos.RemoveAt(index);

            return result;
        }

        private void RemoveCorrectLetterInfo()
        {
            LetterInfo correctLetterInfo = _letterInfos.First(info => info.Character == _correctLetter);
            _letterInfos.Remove(correctLetterInfo);
        }
    }
}