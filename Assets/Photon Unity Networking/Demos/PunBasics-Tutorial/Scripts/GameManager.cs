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
		public GameObject playerPrefabLookingGlass;
        public GameObject playerPrefabOculus;

        public bool isLookingGlass;

        public GameObject ball;

		#endregion

		#region Private Variables

		private GameObject instance;
        private GameObject[] instantiatedplayers;
        private bool towardsPlayer2 = true;

        private Vector3 player1position;
        private Vector3 player2position;

        public int player2score;
        public int player1score;
		#endregion

		#region MonoBehaviour CallBacks

		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during initialization phase.
		/// </summary>
		void Start()
		{
			Instance = this;
            instantiatedplayers = new GameObject[2];

			// in case we started this demo with the wrong scene being active, simply load the menu scene
			if (!PhotonNetwork.connected)
			{
				SceneManager.LoadScene("PunBasics-Launcher");

				return;
			}

			if (playerPrefabOculus == null || playerPrefabLookingGlass == null) { // #Tip Never assume public properties of Components are filled up properly, always check and inform the developer of it.
				
				Debug.LogError("<Color=Red><b>Missing</b></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
			} else {
				

				if (PlayerManager.LocalPlayerInstance==null)
				{
					Debug.Log("We are Instantiating LocalPlayer from "+SceneManagerHelper.ActiveSceneName);

                    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                    if (isLookingGlass)
                    {
                       instantiatedplayers[1] = PhotonNetwork.Instantiate(this.playerPrefabLookingGlass.name, new Vector3(0f, 0f, 25f), Quaternion.Euler(new Vector3(0, 180, 0)), 0);
                        player1position = instantiatedplayers[1].transform.position;
                        instantiatedplayers[0].GetComponent<PaddleCollider>().swung = true;
                        instantiatedplayers[0].GetComponent<PaddleCollider>().swingTime = Time.time;
                    }
                    else
                    {
                        instantiatedplayers[0] = (PhotonNetwork.Instantiate(this.playerPrefabOculus.name, new Vector3(0f, 0f, -25f), Quaternion.identity, 0));
                        player2position = instantiatedplayers[0].transform.position;
                    }
				}else{

					Debug.Log("Ignoring scene load for "+ SceneManagerHelper.ActiveSceneName);
				}

				
			}

		}

		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity on every frame.
		/// </summary>
		void Update()
		{
            //Debug.Log("Instantiated player count" + instantiatedplayers.Count);
            if(instantiatedplayers[0] != null && instantiatedplayers[1] != null)
            {
                if (towardsPlayer2)
                {
                    float distcov = (Time.time - instantiatedplayers[0].GetComponent<PaddleCollider>().swingTime) * 5;
                    ball.transform.position = Vector3.Lerp(player1position, player2position, distcov / Vector3.Distance(ball.transform.position, player2position));
                }
                else
                {
                    float distcov = (Time.time - instantiatedplayers[1].GetComponent<PaddleCollider>().swingTime) * 5;
                    ball.transform.position = Vector3.Lerp(player2position, player1position, distcov / Vector3.Distance(ball.transform.position, player1position));
                }

                if(Vector3.Distance(ball.transform.position, player1position) < 0.2)
                {
                    if(instantiatedplayers[0].GetComponent<PaddleCollider>().swung == false)
                    {
                        player2score++;
                        instantiatedplayers[1].GetComponent<PaddleCollider>().GetComponentInChildren<Text>().text = player2score.ToString();
                    }

                    if (instantiatedplayers[1].GetComponent<PaddleCollider>().swung == false)
                    {
                        player1score++;
                        instantiatedplayers[0].GetComponent<PaddleCollider>().GetComponentInChildren<Text>().text = player1score.ToString();
                    }
                }
            }
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

			PhotonNetwork.LoadLevel("SampleScene");
		}

		#endregion

	}

}