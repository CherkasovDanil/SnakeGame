using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Food
{
    [Serializable]
    public struct FoodModel
    {
        public int ID;
        public Sprite Sprite;
    }

    [CreateAssetMenu(fileName = "FoodConfig", menuName = "Config/FoodConfig", order = 2)]
    public class FoodConfig : ScriptableObject
    {
        [SerializeField] private FoodModel[] foodModels;
        
        private Dictionary<int, FoodModel> _dict;
        
        [NonSerialized] private bool _isInited;
        
        public FoodModel? Get(int id)
        {
            if(!_isInited)
            {
                Init();
            }
            
            if( _dict.ContainsKey(id))
            {
                return _dict[id];
            }

            Debug.LogError($"Model with id {id} not found, returned null");
            return null;
        }
        
        private void Init()
        {
            _dict = new Dictionary<int, FoodModel>();
            foreach (var model in foodModels)
            {
                _dict.Add(model.ID, model);
            }
            _isInited = true;
        }
    }
}