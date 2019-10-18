using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ObjectHealth : MonoBehaviour, IPunObservable
{
    [Tooltip("Жизненные очки объекта")]
    [Range(0, 10)] public int Health = 5;

    public TextMesh TextMeshComponent;

    private Text _healthUI;

    private int _damage;
    private GameObject _bullet;

    private PhotonView _photonView;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if(_healthUI == null)
        {
            _healthUI = GameObject.Find("HealthPoint").GetComponent<Text>();
        }
        else
        {
            if(TextMeshComponent != null)
            {
                TextMeshComponent.text = Health.ToString();
            }

            if (_photonView.IsMine)
            {
                if (Health > 0)
                {
                    _healthUI.text = Health.ToString();
                }
                else
                {
                    _healthUI.text = "Destroyed";
                }
            }
        }
    }

    public void ApplyDamage(int Damage, GameObject Bullet)
    {
        _damage = Damage;
        _bullet = Bullet;

        if (Health > 0)
        {
            _photonView.RPC("ApplyDamageRPC", RpcTarget.All);
            //Health = Mathf.Clamp(Health, 0, 10);

            //Health -= _damage;
        }

        if (Bullet != null)
        {
            //if (_photonView.IsMine)
                PhotonNetwork.Destroy(Bullet);
        }
    }

    [PunRPC]
    public void ApplyDamageRPC()
    {
        Health = Mathf.Clamp(Health, 0, 10);

        Health -= _damage;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //if (stream.IsWriting)
        //{
        //    stream.SendNext(Health);
        //}
        //else
        //{
        //    Health = (int)stream.ReceiveNext();
        //}
    }
}