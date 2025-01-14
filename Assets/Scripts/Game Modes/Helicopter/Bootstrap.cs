using System;
using Additionals;
using ScriptableObjects.Sources.GameModes.Helicopter;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameModes.Helicopter
{
    public class Bootstrap : MonoBehaviour
    {
        [Header("Letter set")]
        [SerializeField] private LetterSet _letterSet;
        
        [Header("Helicopter")]
        [SerializeField] private Helicopter _helicopter;
        
        [Header("Letter")]
        [SerializeField] private Letter _letter;
        
        [Header("Letter Variants")]
        [SerializeField] private LetterVariant[] _letterVariants;
        
        [Header("Scene Loader")]
        [SerializeField] private SceneLoader _sceneLoader;
        
        private LetterCompare _letterCompare;
        private LetterDistributor _letterDistributor;
        private VictoryHandler _victoryHandler;

        private LetterInfo _correctLetterInfo;

        private void Awake()
        {
            _correctLetterInfo = GetRandomLetterFromSet();

            _letterDistributor = new LetterDistributor(_letterSet, _letterVariants, _correctLetterInfo.Character);
            _letterCompare = new LetterCompare(_correctLetterInfo.Character);
            _victoryHandler = new VictoryHandler(_letter);
        }

        private void Start()
        {
            _letterDistributor.Distribute();
            
            _helicopter.Init(_letterCompare);
            _letter.Init(_correctLetterInfo, _helicopter, _letterDistributor.CorrectLetterVariant.transform.position);
        }

        private LetterInfo GetRandomLetterFromSet()
        {
            int index = Random.Range(0, _letterSet.LetterInfos.Length);

            return _letterSet.LetterInfos[index];
        }
    }
}