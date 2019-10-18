using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletInstance : MonoBehaviour, IPunObservable
{
    public PhotonView _PhotonView;
    public Rigidbody2D SpawnObject;
    public Transform SpawnPoint;
    public float Force = 2;
    //public int Damage = 1;

    public void InstanceRPC()
    {
        if(PhotonNetwork.OfflineMode)
        {
            InstantiateObject();
        }
        else
        {
            _PhotonView.RPC("InstantiateObject", RpcTarget.All);
        }
    }

    [PunRPC]
    void InstantiateObject()
    {
        if (_PhotonView.IsMine)
        {
            GameObject BulletClone = PhotonNetwork.Instantiate(SpawnObject.name, SpawnPoint.position, SpawnPoint.rotation);
            //Physics2D.IgnoreCollision(GetComponentInChildren<Collider2D>(), BulletClone.GetComponent<Collider2D>());

            //BulletClone.GetComponent<Bullet>().Owner = gameObject.GetComponentInChildren<Collider2D>();
            BulletClone.GetComponent<Rigidbody2D>().AddForce(SpawnPoint.up * Force, ForceMode2D.Impulse);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //throw new System.NotImplementedException();
    }
}