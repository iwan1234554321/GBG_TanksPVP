using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour, IPunObservable
{
    [Header("Урон пули")]
    [Range(0, 10)] public int DamageBullet = 1;

    private GameObject _objectDestroy;

    private PhotonView _photonView;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<ObjectDestroyController>())
        {
            _objectDestroy = collision.collider.gameObject;

            if (PhotonNetwork.OfflineMode)
            {
                DestroyBulletRPC();
            }
            else
            {
                _photonView.RPC("DestroyBulletRPC", RpcTarget.All);
            }
        }
        else if (collision.collider.GetComponentInParent<ObjectHealth>())
        {
            _objectDestroy = collision.collider.GetComponentInParent<ObjectHealth>().gameObject;

            if (PhotonNetwork.OfflineMode)
            {
                DestroyBulletRPC();
            }
            else
            {
                _photonView.RPC("DestroyBulletRPC", RpcTarget.All);
            }
        }
        else
        {
            if (PhotonNetwork.OfflineMode)
            {
                DestroyBulletRPC();
            }
            else
            {
                _photonView.RPC("DestroyBulletRPC", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void DestroyBulletRPC()
    {
        if(_objectDestroy != null)
        {
            if (_objectDestroy.GetComponent<ObjectDestroyController>())
            {
                _objectDestroy.GetComponent<ObjectDestroyController>().DestroyObject(gameObject);
            }
            else if (_objectDestroy.GetComponent<ObjectHealth>())
            {
                _objectDestroy.GetComponent<ObjectHealth>().ApplyDamage(DamageBullet, gameObject);
            }
        }
        else
        {
            //if (_photonView.IsMine)
                PhotonNetwork.Destroy(gameObject);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
