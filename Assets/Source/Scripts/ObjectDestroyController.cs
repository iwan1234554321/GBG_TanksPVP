using UnityEngine;
using Photon.Pun;

public class ObjectDestroyController : MonoBehaviour, IPunObservable
{
    [Header("Здоровье объекта")]
    [Range(0, 10)] public int Health;

    private GameObject BulletObject;
    private PhotonView _photonView;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    public void DestroyObject(GameObject Bullet)
    {
        BulletObject = Bullet;

        if (PhotonNetwork.OfflineMode)
        {
            DestroyRPC();
        }
        else
        {
            _photonView.RPC("DestroyRPC", RpcTarget.All);
        }
    }

    [PunRPC]
    void DestroyRPC()
    {
        if (BulletObject != null)
        {
            PhotonNetwork.Destroy(BulletObject);
        }

        if(Health == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            Health -= 1;
            Health = Mathf.Clamp(Health, 0, 10);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}