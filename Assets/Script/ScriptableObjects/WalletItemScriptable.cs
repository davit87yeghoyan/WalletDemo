using UnityEngine;
using UnityEngine.Serialization;

namespace Script.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Wallet Item", menuName = "Wallet/Create Wallet Item", order = 1)]
    public class WalletItemScriptable : ScriptableObject
    {
        public string key;
        public Sprite sprite;
    }
}