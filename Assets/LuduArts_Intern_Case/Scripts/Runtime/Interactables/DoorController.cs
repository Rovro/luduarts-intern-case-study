using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPSGame.Runtime.Interaction.Locks;

namespace FPSGame.Runtime.Interaction.Interactables
{
    public class DoorController : MonoBehaviour, IInteractable
    {
        #region Fields

        [Header("References")]
        [Tooltip("Dönecek olan kapý mesh'i (Eðer script parent'ta ise buraya child mesh'i at).")]
        [SerializeField] private Transform m_DoorMesh;

        [Header("Door Settings")]
        [Tooltip("Kapý açýldýðýnda Z ekseninde kaç derece dönecek?")]
        [SerializeField] private float m_OpenAngle = 90f;

        [Tooltip("Açýlma hýzý.")]
        [SerializeField] private float m_AnimationSpeed = 2f;

        [Header("Lock System")]
        [SerializeField] private bool m_IsLocked = false;
        [SerializeField] private List<LockCondition> m_LockConditions;

        // Internal State
        private bool m_IsOpen = false;
        private Quaternion m_ClosedRotation;
        private Quaternion m_OpenRotation;
        private Coroutine m_AnimationCoroutine;

        #endregion

        #region Properties

        public string InteractionPrompt
        {
            get
            {
                if (m_IsOpen) return "Close Door";
                if (m_IsLocked) return "Locked";
                return "Open Door";
            }
        }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Eðer inspector'dan atanmadýysa kendi transformunu kullan
            if (m_DoorMesh == null) m_DoorMesh = transform;

            // Kapalý hali mevcut rotasyonudur
            m_ClosedRotation = m_DoorMesh.localRotation;

            // DÜZELTME BURADA: Z ekseninde (0, 0, Angle) döndürüyoruz
            // Quaternion.Euler(X, Y, Z) -> Biz Z'yi kullanýyoruz.
            m_OpenRotation = m_ClosedRotation * Quaternion.Euler(0, 0, m_OpenAngle);
        }

        #endregion

        #region Methods

        public bool OnInteract(GameObject interactor)
        {
            // Kapý açýksa kapat (Kilit durumuna bakma)
            if (m_IsOpen)
            {
                CloseDoor();
                return true;
            }

            // Kapalýysa ve Kilitliyse (Switch hala listede olduðu için Locked dönecek)
            if (m_IsLocked)
            {
                // Tekrar kontrol et! Switch hala açýk mý?
                if (CheckLocks(interactor))
                {
                    // Þalter açýk, kapýyý açabiliriz
                    UnlockAndOpen(interactor);
                    return true;
                }
                else
                {
                    // Þalter kapalý, açma
                    return false;
                }
            }

            // Hiç kilit yoksa direkt aç
            OpenDoor();
            return true;
        }

        private bool CheckLocks(GameObject interactor)
        {
            foreach (var condition in m_LockConditions)
            {
                if (!condition.IsMet(interactor))
                {
                    Debug.Log($"Door Locked: {condition.DenialMessage}");
                    return false;
                }
            }
            return true;
        }

        private void UnlockAndOpen(GameObject interactor)
        {
            // Listeyi tersten dönüyoruz çünkü eleman silebiliriz
            for (int i = m_LockConditions.Count - 1; i >= 0; i--)
            {
                var condition = m_LockConditions[i];

                // Koþulun açýlma olayýný tetikle (Örn: Anahtarý sil)
                condition.OnUnlock(interactor);

                // Eðer bu kalýcý bir kilit deðilse (Örn: Anahtar), listeden at.
                // Ama þalter (Switch) ise listede kalsýn.
                if (condition.ShouldRemoveAfterUnlock)
                {
                    m_LockConditions.RemoveAt(i);
                }
            }

            // Eðer listede hiç koþul kalmadýysa, kapý artýk tamamen kilitsizdir.
            // Ama listede hala Switch varsa, m_IsLocked TRUE kalmaya devam etmeli!
            if (m_LockConditions.Count == 0)
            {
                m_IsLocked = false;
            }
            else
            {
                // Listede hala Switch var, yani kapý teknik olarak hala "Kilitli" statüsünde.
                // Sadece þu anlýk koþullar saðlandýðý için açýlýyor.
                m_IsLocked = true;
            }

            OpenDoor();
        }

        private void OpenDoor()
        {
            m_IsOpen = true;
            if (m_AnimationCoroutine != null) StopCoroutine(m_AnimationCoroutine);
            m_AnimationCoroutine = StartCoroutine(AnimateDoor(m_OpenRotation));
        }

        private void CloseDoor()
        {
            m_IsOpen = false;
            if (m_AnimationCoroutine != null) StopCoroutine(m_AnimationCoroutine);
            m_AnimationCoroutine = StartCoroutine(AnimateDoor(m_ClosedRotation));
        }

        // SENÝN ÝSTEDÝÐÝN DÜZELTME
        private IEnumerator AnimateDoor(Quaternion targetRotation)
        {
            // Quaternion.Angle iki rotasyon arasýndaki farký verir. 
            // Slerp fonksiyonu Z eksenine göre hesaplanan targetRotation'a doðru gider.
            while (Quaternion.Angle(m_DoorMesh.localRotation, targetRotation) > 0.1f)
            {
                m_DoorMesh.localRotation = Quaternion.Slerp(
                    m_DoorMesh.localRotation,
                    targetRotation,
                    Time.deltaTime * m_AnimationSpeed);
                yield return null;
            }
            m_DoorMesh.localRotation = targetRotation;
        }

        #endregion
    }
}