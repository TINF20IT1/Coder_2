using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerManager : MonoBehaviour
{
    // blank implementation for later use
    public class MobileDevice
    {
        public int Id { get; }
        public MobileDevice(int id)
        {
            this.Id = id;
        }
    }

    private GameObject playerPrefab;
    // count of max players, have to tes what number fits best
    public int maxPlayer = 4;

    List<GameObject> players;
    List<MobileDevice> devices;

    // connect the mobile devices to this server
    void ConnectToDevices() {
        devices = new List<MobileDevice>
        {
            new MobileDevice(0),
        };
    }

    private void Awake()
    {
        playerPrefab = Resources.Load<GameObject>("Player/Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        players = new List<GameObject>();

        ConnectToDevices();
        Debug.Log($"Device count: {devices.Count}");

        foreach (var device in devices)
        {
            var p = Instantiate<GameObject>(playerPrefab);
            p.GetComponent<PlayerController>().device = device;
            players.Add(p);
        }

        Assert.AreEqual(players.Count, devices.Count, "Expected as many players as devices");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
