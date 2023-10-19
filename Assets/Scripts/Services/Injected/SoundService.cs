using System;
using Scripts;
using Services.Attributes;
using UnityEngine;

namespace Services.Injected
{
    [ServiceInject(typeof(ISoundService))]
    public class SoundService : MonoBehaviour, ISoundService
    {

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _fruit;
        [SerializeField] private AudioClip _enemy;

        public void PlayFruit()
        {
            if (_audioSource != null)
            {
                _audioSource.PlayOneShot(_fruit);
            }
        }

        public void PlayEnemy()
        {
            if (_audioSource != null)
            {
                _audioSource.PlayOneShot(_enemy);
            }
        }

        protected void OnEnable()
        {
            ServiceInjector.InjectServicesFromObject(gameObject);
        }

    }
}