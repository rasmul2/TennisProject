// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Launcher.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking Demos
// </copyright>
// <summary>
//  Used in "PUN Basic tutorial" to handle typical game management requirements
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;
using UnityEngine.SceneManagement; 

using ExitGames.Client.Photon;

namespace ExitGames.Demos.DemoAnimator
{
	/// <summary>
	/// Game manager.
	/// Connects and watch Photon Status, Instantiate Player
	/// Deals with quiting the room and the game
	/// Deals with level loading (outside the in room synchronization)
	/// </summary>
	public class GameManager : Photon.PunBehaviour {

		#region Public Variables

		static public GameManager Instance;

        [Tooltip("The prefab to use for representing the player")]
        public GameObject playerPrefab;

        public bool isLookingGlass;

        public GameObject ball;

		#endregion

		#region Private Variables

		private GameObject instance;
        private GameObject instantiatedPlayer;
        private Vector3 playerposition;

        public bool movingtowards2 = true;
		#endregion

		#region MonoBehaviour CallBacks

		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during initialization phase.
		/// </summary>
		void Start()
		{
			Instance = this;

			// in case we started this demo with the wrong scene being active, simply load the menu scene
			if (!PhotonNetwork.connected)
			{
				SceneManager.LoadScene("PunBasics-Launcher");

				return;
			}

			if (playerPrefab == null) { // #Tip Never assume public properties of Components are filled up properly, always check and inform the developer of it.
				
				Debug.LogError("<Color=Red><b>Missing</b></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
			} else {
				

				if (PlayerManager.LocalPlayerInstance==null)
				{
					Debug.Log("We are Instantiating LocalPlayer from "+SceneManagerHelper.ActiveSceneName);

                    int times = 1;
                    Quaternion rotation = Quaternion.identity;
                    if (isLookingGlass) { times = -1; rotation = Quaternion.Euler(0, 180, 0); };
                    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                    instantiatedPlayer = PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 0f, -25f*times), rotation, 0);
                    playerposition = instantiatedPlayer.transform.position;

                    if (PhotonNetwork.isMasterClient)
                    {
                        instantiatedPlayer.GetComponentInChildren<PaddleCollider>().swingTime = Time.time;
                        instantiatedPlayer.GetComponentInChildren<PaddleCollider>().swung = true;
                    }

                    if (PhotonNetwork.playerList.Length == 1)
                    {
                        if (GameObject.Find("Player1(Clone)") != null)
                        {
                            GameObject.Find("Player2(Clone)").GetComponent<GameManager>().movingtowards2 = GameObject.Find("Player1(Clone)").GetComponent<GameManager>().movingtowards2;
                        }
                        else
                        {
                            GameObject.Find("Player1(Clone)").GetComponent<GameManager>().movingtowards2 = GameObject.Find("Player2(Clone)").GetComponent<GameManager>().movingtowards2;
                        }
                    }


                }
                else{

					Debug.Log("Ignoring scene load for "+ SceneManagerHelper.ActiveSceneName);
				}

				
			}

		}

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity on every frame.
        /// </summary>
        void Update()
        {



            // "back" button of phone equals "Escape". quit app if that's pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitApplication();
            }
        }
	

		#endregion

		#region Photon Messages

		/// <summary>
		/// Called when a Photon Player got connected. We need to then load a bigger scene.
		/// </summary>
		/// <param name="other">Other.</param>
		public override void OnPhotonPlayerConnected( PhotonPlayer other  )
		{
			Debug.Log( "OnPhotonPlayerConnected() " + other.NickName); // not seen if you're the player connecting

			if ( PhotonNetwork.isMasterClient ) 
			{
				Debug.Log( "OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient ); // called before OnPhotonPlayerDisconnected

				LoadArena();
			}
		}

		/// <summary>
		/// Called when a Photon Player got disconnected. We need to load a smaller scene.
		/// </summary>
		/// <param name="other">Other.</param>
		public override void OnPhotonPlayerDisconnected( PhotonPlayer other  )
		{
			Debug.Log( "OnPhotonPlayerDisconnected() " + other.NickName ); // seen when other disconnects

			if ( PhotonNetwork.isMasterClient ) 
			{
				Debug.Log( "OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient ); // called before OnPhotonPlayerDisconnected
				
				LoadArena();
			}
		}

		/// <summary>
		/// Called when the local player left the room. We need to load the launcher scene.
		/// </summary>
		public override void OnLeftRoom()
		{
			SceneManager.LoadScene("PunBasics-Launcher");
		}

		#endregion

		#region Public Methods

		public void LeaveRoom()
		{
			PhotonNetwork.LeaveRoom();
		}

		public void QuitApplication()
		{
			Application.Quit();
		}

		#endregion

		#region Private Methods

		void LoadArena()
		{
			if ( ! PhotonNetwork.isMasterClient ) 
			{
				Debug.LogError( "PhotonNetwork : Trying to Load a level but we are not the master Client" );
			}

			Debug.Log( "PhotonNetwork : Loading Level : " + PhotonNetwork.room.PlayerCount ); 

			//PhotonNetwork.LoadLevel("SampleScene");
		}

		#endregion

	}

}