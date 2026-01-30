using UnityEngine;

namespace FPSGame.Runtime.Interaction
{
    /// <summary>
    /// Oyuncu tarafýndan etkileþime geçilebilen tüm nesnelerin uygulamasý gereken arayüz.
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// UI'da gösterilecek etkileþim metni (Örn: "Open Door", "Pick up Key").
        /// </summary>
        string InteractionPrompt { get; }

        /// <summary>
        /// Oyuncu etkileþime girdiðinde tetiklenir.
        /// </summary>
        /// <param name="interactor">Etkileþimi baþlatan obje (Genellikle Player).</param>
        /// <returns>Etkileþim baþarýlý oldu mu?</returns>
        bool OnInteract(GameObject interactor);
    }
}