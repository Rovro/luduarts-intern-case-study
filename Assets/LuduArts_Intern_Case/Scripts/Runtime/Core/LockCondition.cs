using UnityEngine;

namespace FPSGame.Runtime.Interaction.Locks
{
    /// <summary>
    /// Bir kapýnýn açýlmasý için gereken herhangi bir koþulu temsil eder.
    /// (Örn: Anahtar var mý? Þalter indi mi? Elektrik var mý?)
    /// </summary>
    public abstract class LockCondition : MonoBehaviour
    {
        /// <summary>
        /// Koþul saðlandý mý?
        /// </summary>
        /// <param name="interactor">Etkileþime geçen obje (Player).</param>
        /// <returns>True ise koþul tamamdýr.</returns>
        public abstract bool IsMet(GameObject interactor);

        /// <summary>
        /// Koþul saðlanmazsa oyuncuya gösterilecek hata mesajý.
        /// </summary>
        public abstract string DenialMessage { get; }

        /// <summary>
        /// Kilit açýldýktan sonra bu koþul listeden silinsin mi?
        /// (Anahtar için EVET, Þalter için HAYIR)
        /// </summary>
        public virtual bool ShouldRemoveAfterUnlock => false; // Varsayýlan: Hayýr

        /// <summary>
        /// Koþul saðlandýðýnda (örn: anahtarý envanterden silmek için) yapýlacak iþlem.
        /// </summary>
        public virtual void OnUnlock(GameObject interactor) { }

    }
}