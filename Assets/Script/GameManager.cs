
using System;
using System.Collections;
using System.Linq;
using PackageWallet.Runtime;
using PackageWallet.Runtime.WalletStorage;
using UnityEngine;

namespace Script
{
    public class GameManager : MonoBehaviour
    {
        private IWalletStorage _walletStorage;
        private Coroutine _coroutine;

        
        private void Awake()
        {
            /* storage  playerPrefs */
           // _walletStorage = new WalletStoragePlayerPrefs("playerPrefsKey");
            
            /* storage file */
            //_walletStorage = new WalletStorageFile(Application.dataPath + "/walletItems.json");
            
            /* storage file binary */
            _walletStorage = new WalletStorageBinary(Application.dataPath + "/walletItems.data");
            
            /* storage server */
            // _walletStorage = new WalletStorageServer(this,"https://vk01-cossacks-files.agium.com/AniWorks/wallet.php");
             Wallet.GetStorage( _walletStorage);
            
            
             
            Wallet.SetItem += OnSetItem;
            
            
            // for ui
            _coroutine = StartCoroutine(UpdateWalletItems());
        }
        
        /// <summary>
        /// save to storage in update
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        private void OnSetItem(string arg1, float arg2)
        {
            Wallet.SaveStorage( _walletStorage);
        }

        private void OnDestroy()
        {
            Wallet.SetItem -= OnSetItem;
        }


        
        
        
        
        
        
        
        
        
        
        
        
        
        
        #region FOR TEST UI 
        private bool _autoUpdate = true, _autoSave = true;
        
        public void AutoSave()
        {
            _autoUpdate = !_autoUpdate;
            
            if (_autoUpdate)
            {
                Wallet.SetItem += OnSetItem;
            }
            else
            {
                Wallet.SetItem -= OnSetItem;
            }
           
        }
        
        public void AutoUpdate()
        {
            _autoSave = !  _autoSave;


            if (_autoSave)
            {
                _coroutine = StartCoroutine(UpdateWalletItems());
                return;
            }
           
            if(_coroutine!=null)
                StopCoroutine( _coroutine);
        }
        
        public void Clear()
        {
            Wallet.GetItems.ForEach(
                item =>
                {
                    Wallet.SetValue(item.key, 0);
                });
        }
        
        private IEnumerator UpdateWalletItems()
        {
            string[] keys = FindObjectsOfType<PlayerWallet>().Select(playerWallet => playerWallet.wallet.key).ToArray();
            var updateWalletItems = new WaitForSeconds(1);
           

            while (true)
            {
                yield return updateWalletItems;
                for (var i = 0; i < keys.Length; i++)
                {
                   float value =  Wallet.GetValue(keys[i]);
                   Wallet.SetValue(keys[i],++value+i);
                }
            }


        }
        
        #endregion
        
    }
}
