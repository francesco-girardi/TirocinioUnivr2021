using System.Collections;
using System.Collections.Generic;
using Interactions.Items;
using UnityEngine;

namespace Interactions.Inventory {

    public class Inventory : MonoBehaviour {

        public static Inventory Instance { get; private set; }

        public List<Item> items = new List<Item>();

        private int space = int.MaxValue - 1;

        private PlayerManager playerManager;

        private PlayerLogic player;
        
        [HideInInspector]
        public float cd = 0f;
        private float prevTime;

        void Start() {
            prevTime = Time.time;
            playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
            // InvokeRepeating("ChangeStance", 2.0f, 10.0f);
            StartCoroutine(GetPlayer());

        }

        private IEnumerator GetPlayer() {
            yield return new WaitForSeconds(0.5f);
            player = playerManager.GetComponent<PlayerLogic>();
        }

        private void Update() {
            
            if (Input.GetKeyDown(KeyCode.M) && cd <= 0f) {
                foreach (var item in items) {
                    if (item.name == "rossa" && item.GetType().Equals(typeof(Potions))) {
                        Potions pozza = (Potions)item;
                        pozza.Use();
                        cd = pozza.cd;
                        RemoveItem(item);
                        break;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.N) && cd <= 0f) {
                foreach (var item in items) {
                    if (item.name == "rosa" && item.GetType().Equals(typeof(Potions))) {
                        Potions pozza = (Potions)item;
                        pozza.Use();
                        cd = pozza.cd;
                        RemoveItem(item);
                        break;
                    }
                }
            }
            if(cd > 0){
                cd = cd - (Time.time - prevTime);
            }

            prevTime = Time.time;
        }

        public bool AddItem(Item item) {

            if (space <= items.Count) {
                Debug.Log("Not enought room.");
                return false;
            }

            items.Add(item);

            return true;
        }

        public void RemoveItem(Item item) {
            items.Remove(item);
        }

        private void Awake() {
            #region Singleton
            if (Instance == null)
                Instance = this;
            else {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            #endregion
        }

    }

}
