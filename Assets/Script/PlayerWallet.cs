using System;
using System.Globalization;
using PackageWallet.Runtime;
using Script.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class PlayerWallet:MonoBehaviour
    {
        public WalletItemScriptable wallet;

        public Image image;
        public TextMeshProUGUI text;


        private void Awake()
        {
            Wallet.SetItem += OnSetItem;
            Wallet.GetFromStorage += OnGetFromStorage;
            image.sprite = wallet.sprite;
        }

        private void OnDisable()
        {
            Wallet.SetItem -= OnSetItem;
            Wallet.GetFromStorage -= OnGetFromStorage;
        }

        private void OnGetFromStorage()
        {
            if(!Wallet.ContainsKey(wallet.key)) return;
            float value = Wallet.GetValue(wallet.key);
            UpdateValue(value);
        }

        private void OnSetItem(string key, float value)
        {
            if(wallet.key != key) return;
            UpdateValue(value);
        }

        private void UpdateValue(float value)
        {
            text.text = value.ToString(CultureInfo.InvariantCulture);
        }
    }
}