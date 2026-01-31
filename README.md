# Interaction System - [Cemil Azizoğlu]

> Ludu Arts Unity Developer Intern Case

## Proje Bilgileri

| Bilgi | Değer |
|-------|-------|
| Unity Versiyonu | 6000.0.23f1 |
| Render Pipeline | URP |
| Case Süresi | 12 saat |
| Tamamlanma Oranı | %60 |

---

## Kurulum

1. Repository'yi klonlayın:
```bash
git clone https://github.com/Rovro/luduarts-intern-case-study.git
```

2. Unity Hub'da projeyi açın
3. `Assets/LuduArts_Intern_Case/Scenes/TestScene.unity` sahnesini açın
4. Play tuşuna basın

---

## Nasıl Test Edilir

### Kontroller

| Tuş | Aksiyon |
|-----|---------|
| WASD | Hareket |
| Mouse | Bakış yönü |
| E | Etkileşim |
| I | Envanter |
| MouseSolTuş | Envanterde item seçme |

### Test Senaryoları

1. **Door Test:**
   - 1.odada kapıya yaklaşın, "Press E to Open" mesajını görün
   - E'ye basın, kapı açılmasın
   - Kapıya uygun yanındaki anahtarı alın. Tekrar basın, açılsın.
   - Tekrar basın kapı kapansın.

3. **Birden Çok Koşul Testi:**
   - 2. odada kırmızı anahtarları alın.
   - Yerdeki tüm kırmızı anahtarları aldıktan sonra 'I' ya basıp envanteri açın.
   - Kapıyı açmayı deneyin açılmasın
   - Diğer gerekli yeşil anahtarı alın
   - Kapıyı açın

4. **Switch + Key Test:**
   - 3.odadaki mavi anahtar ile şalterin yanındaki kapıyı açmayı deneyin
   - Kapı açılmasın ve şalteri açın
   - Gidip kapıyı açın.

---

## Mimari Kararlar

### Interaction System Yapısı

```
[Mimari diyagram veya açıklama]
```

**Neden bu yapıyı seçtim:**
> [Interface yapılarını seviyorum ve daha rahat okunup sistemler kurulduğunu düşünüyorum.]

**Alternatifler:**
> [BaseClass sistemini yapabilirdim ama okunması zor ve proje büyüdükçe karmaşıklaşıyor]

**Trade-off'lar:**
> [Avantajı : Geniş çaplı birbirlerinden ayrı sistemler kurulabilir. Dezavantajı : Yapıyı düzgün kurmak zor ve iyi algoritma kurmayı gerektiriyor.]

### Kullanılan Design Patterns

| Pattern | Kullanım Yeri | Neden |
|---------|---------------|-------|
| [Observer] | [Event system] | [Açıklama] |
| [State] | [Door states] | [Açıklama] |
| [vb.] | | |

---

## Ludu Arts Standartlarına Uyum

### C# Coding Conventions

| Kural | Uygulandı | Notlar |
|-------|-----------|--------|
| m_ prefix (private fields) | [x] / [x] | |
| s_ prefix (private static) | [x] / [ ] | |
| k_ prefix (private const) | [x] / [ ] | |
| Region kullanımı | [x] / [x] | |
| Region sırası doğru | [x] / [ ] | |
| XML documentation | [x] / [ ] | |
| Silent bypass yok | [x] / [ ] | |
| Explicit interface impl. | [x] / [ ] | |

### Naming Convention

| Kural | Uygulandı | Örnekler |
|-------|-----------|----------|
| P_ prefix (Prefab) | [x] / [x] | P_Door, P_Chest |
| M_ prefix (Material) | [x] / [x] | M_Door_Wood |
| T_ prefix (Texture) | [x] / [x] | |
| SO isimlendirme | [x] / [x] | |

### Prefab Kuralları

| Kural | Uygulandı | Notlar |
|-------|-----------|--------|
| Transform (0,0,0) | [x] / [x] | |
| Pivot bottom-center | [x] / [x] | |
| Collider tercihi | [x] / [X] | Box > Capsule > Mesh |
| Hierarchy yapısı | [x] / [X] | |

## Tamamlanan Özellikler

### Zorunlu (Must Have)

- [x] / [X] Core Interaction System
  - [x] / [X] IInteractable interface
  - [x] / [X] InteractionDetector
  - [x] / [X] Range kontrolü

- [x] / [] Interaction Types
  - [x] / [X] Instant
  - [x] / [ ] Hold
  - [x] / [ ] Toggle

- [x] / [] Interactable Objects
  - [x] / [X] Door (locked/unlocked)
  - [x] / [X] Key Pickup
  - [x] / [X] Switch/Lever
  - [x] / [ ] Chest/Container

- [x] / [] UI Feedback
  - [x] / [X] Interaction prompt
  - [x] / [X] Dynamic text
  - [x] / [ ] Hold progress bar
  - [x] / [ ] Cannot interact feedback

- [x] / [X] Simple Inventory
  - [x] / [X] Key toplama
  - [x] / [X] UI listesi

### Bonus (Nice to Have)

- [X] Animation entegrasyonu
- [ ] Sound effects
- [x] Multiple keys / color-coded
- [ ] Interaction highlight
- [ ] Save/Load states
- [X] Chained interactions

---

## Bilinen Limitasyonlar

### Tamamlanamayan Özellikler
1. [Chest] - [Süre yetmedi. Şiddetli diş ağrısı da cabası]
2. [Hold] - [Süre yetmedi. Şiddetli diş ağrısı da cabası]

### Bilinen Bug'lar
1. Karşılaşmadım.

### İyileştirme Önerileri
1. [StateMachine] - [StateMachine ile daha okunaklı esnek yazabilirdim]

---

## İletişim

| Bilgi | Değer |
|-------|-------|
| Ad Soyad | [Cemil Azizoğlu] |
| E-posta | [gs.cemil1974@gmail.com] |
| LinkedIn | [https://www.linkedin.com/in/cemil-azizo%C4%9Flu-539503244/] |
| GitHub | [https://github.com/Rovro] |

---

*Bu proje Ludu Arts Unity Developer Intern Case için hazırlanmıştır.*
