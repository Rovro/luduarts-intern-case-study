## Özet

| Bilgi | Değer |
|-------|-------|
| Toplam prompt sayısı | X |
| Kullanılan araçlar | ChatGPT / Claude / Copilot |
| En çok yardım alınan konular | [liste] |
| Tahmini LLM ile kazanılan süre | X saat |

---

## Prompt COPYTEMPLATE: [Konu Başlığı]

**Araç:** [ChatGPT-4 / Claude / GitHub Copilot]
**Tarih/Saat:** YYYY-MM-DD HH:MM

**Prompt:**
```
[Yazdığınız prompt - tam metin]
```

**Alınan Cevap (Özet):**
```
[Cevabın özeti veya önemli kısımlar - çok uzunsa kısaltabilirsiniz]
```

**Nasıl Kullandım:**
- [ ] Direkt kullandım (değişiklik yapmadan)
- [ ] Adapte ettim (değişiklikler yaparak)
- [ ] Reddettim (kullanmadım)

**Açıklama:**
> [Bu promptu neden yaptığınızı ve cevabın nasıl yardımcı olduğunu açıklayın.
> Eğer reddettiyseniz, neden uygun bulmadığınızı belirtin.]

**Yapılan Değişiklikler (adapte ettiyseniz):**
> [LLM cevabını nasıl değiştirdiğinizi açıklayın]

---

## Prompt 1: [InteractionSystem Mimari Önerileri]

**Araç:** [Gemini-Pro]
**Tarih/Saat:** 2026-30-01 18:59

**Prompt:**
```
PROMPT1 :
Unity 6000.0.23f1 sürümünde 3d fps oyunumda interaction sistem kurucaz. Henüz kod örneği atmanı istemiyorum. Ben sana ne zaman ve hangi kod gerektiğini söylicem. Şimdilik altta belirtiğim yönergeleri iyice kavra. Optimize ve modüler bir yapı kurmak istiyorum. Bunun için gidebileceğim yolları ve artı eksilerini listele.

Yapı kısaca :
### 1. Core Interaction System
Temel etkileşim altyapısını oluşturun:

Beklenen Yapı:

- IInteractable interface

- InteractionDetector (raycast veya trigger-based)

- Interaction range kontrolü

- Single interaction point (aynı anda tek nesne ile etkileşim)


**Teknik Detaylar:**

- Oyuncu belirli bir mesafeden nesnelerle etkileşime geçebilmeli

- Birden fazla interactable aynı range'de ise en yakın olanı seçilmeli

- Etkileşim input'u configurable olmalı (Inspector'dan değiştirilebilir)

### 2. Interaction Types (En az 3 tür)

| Tür | Açıklama | Örnek Kullanım |

| **Instant** | Tek tuş basımı ile anında | Pickup item, button press |

| **Hold** | Basılı tutma gerektiren | Chest açma, kapı kilidi kırma |

| **Toggle** | Açık/kapalı durumlar | Light switch, door |

Her interaction type için base class veya interface oluşturulmalıdır.
### 3. Interactable Objects (En az 4 nesne)

Aşağıdaki interactable nesneleri implement edin:
#### 3.1 Door (Kapı)

- Açılıp kapanabilen kapı

- Locked/Unlocked state

- Kilitli ise "Anahtar gerekli" mesajı

- Toggle interaction type

#### 3.2 Key Pickup (Anahtar)

- Instant interaction ile toplanabilir

- Basit envantere eklenmeli

- Farklı kapılar için farklı anahtarlar (en az 2 tip)

#### 3.3 Switch/Lever (Anahtar/Kol)

- Toggle interaction

- Başka bir nesneyi tetikleyebilmeli (örn: kapıyı açar)

- Event-based connection

#### 3.4 Chest/Container (Sandık)

- Hold interaction ile açılır (örn: 2 saniye basılı tut)

- İçinde item bulunabilir

- Açıldıktan sonra tekrar açılamaz

### 4. UI Feedback
Kullanıcıya görsel geri bildirim sağlayın:

| Özellik | Açıklama |

|---------|----------|

| Interaction Prompt | "Press E to Open" gibi dinamik text |

| Dynamic Text | Nesneye göre değişen mesaj |

| Hold Progress Bar | Basılı tutma için ilerleme göstergesi |

| Out of Range | Menzil dışı feedback |

| Cannot Interact | Etkileşim yapılamıyor feedback (örn: kilitli kapı) |

### 5. Simple Inventory
Basit bir envanter sistemi:
- Key toplama ve saklama

- Locked door + key kontrolü

- Toplanan item'ların UI'da listelenmesi (basit liste yeterli)

- ScriptableObject ile item tanımları
---

| Animation entegrasyonu (kapı açılma, chest açılma)

| Sound effects integration points

| Multiple keys / color-coded locks

| Interaction highlight (outline veya material swap)

| Save/Load interaction states

| Chained interactions (switch -> door açılır)
```

**Alınan Cevap (Özet):**
```
Rovro-kun İçin Benim Önerim: "Hybrid Yaklaşım"
Senin belirttiğin gereksinimler (Hold, Instant, Toggle ayrımı ve Inventory entegrasyonu) için Yol 1 ve Yol 2'nin karışımı en sağlıklısıdır.

Core: IInteractable interface'i zorunlu kılınacak (Player sadece bunu tanır).

Base: InteractableBase (Abstract Class) bu interface'i implemente edecek. Ortak işler (Interaction Type enum'ı, Hold süresi, Prompt text) burada duracak.

Specifics: Door, Chest gibi scriptler bu Base'den türetilecek.

Events: Switch -> Door bağlantısı için C# Events veya UnityEvents kullanılacak (gevşek bağlılık için).

Inventory: ScriptableObject tabanlı olacak (Item datası kodun içinde değil, asset dosyasında duracak).

Bu Yapının Avantajları:

Performance: Raycast sadece belirli layer'a atılır (LayerMask).

Scalability: Yeni bir interaction tipi (örn: Quick Time Event) eklemek istersen Base class'ı bozmadan genişletebilirsin.

Clean Code: Player scripti temiz kalır, sadece "Input geldi, Interact fonksiyonunu çalıştır" der.

Şimdi bu mimariyi kafanda oturttuysan, kodlamaya nereden başlamamı istersin rovro-kun?

Core System: IInteractable, InteractableBase ve InteractionDetector (Player tarafı).

Inventory Data: ScriptableObject yapısı ve Item tanımları.

Interactables: Kapı veya Sandık gibi somut örnekler.
```

**Nasıl Kullandım:**
- [ ] Direkt kullandım (değişiklik yapmadan)
- [X] Adapte ettim (değişiklikler yaparak)
- [ ] Reddettim (kullanmadım)

**Açıklama:**
> Bu promptu genel sistem hakkında ön fikir sahibi olmak için yazdım. Ancak henüz kod yazdırıp projeye entegre etmedim.
---

## Prompt 2: [Konu Başlığı]

**Araç:** [Gemini-Pro]
**Tarih/Saat:** 2026-30-01 19:10

**Prompt:**
```
PROMPT 2 :
Bu konuya sonra geri dönücez. Önce yeni InputSystem ile 3d fps PlayerController yapalım. Bu kuracağımız genel yapıya göre en iyi Controller sistemi nasıl olur.

```

**Alınan Cevap (Özet):**
```
Harika bir karar rovro-kun. Interaction sistemini temiz bir zemin üzerine kurmak için Input System ile çalışan, Logic (Mantık) ve Input (Girdi) katmanlarını ayıran modüler bir FirstPersonController hazırlayalım.

Bu yapı, ileride Interaction sistemini entegre ederken işimizi çok kolaylaştıracak. Çünkü Interaction scripti, hareket kodlarının içine girmek zorunda kalmadan sadece Input'u dinleyecek.

Mimari Yaklaşım
Şu an kuracağımız yapı 3 parçadan oluşacak:

Input Actions Asset: Unity'nin yeni sistemi, tuş atamalarını yöneten dosya.

InputManager (Script): Girdileri okuyan ve bunları diğer scriptlere servis eden aracı katman.

FirstPersonController (Script): Sadece fizik ve hareketten sorumlu olan beyin.


```

**Nasıl Kullandım:**
- [ ] Direkt kullandım (değişiklik yapmadan)
- [X] Adapte ettim (değişiklikler yaparak)
- [ ] Reddettim (kullanmadım)

**Açıklama:**
> Karakterimizin temel hareket kodlarını yazdırdım. Bana hazır 2 copy-paste uygun script dosyası(InputManager.cs, FirsPersonController.cs) attı. Ancak henüz ihtiyacım olmadığı özellikleri de eklemiş(Interact input).
> Benim mecvut branch'im sadece temel hareket üzerine kurulu.

**Yapılan Değişiklikler (adapte ettiyseniz):**
> Interact ile alakı olan satırları çıkarttım. Sonradan kendim sisteme uygun eklemek istiyorum.

---

## Genel Değerlendirme

### LLM'in En Çok Yardımcı Olduğu Alanlar
1. [Alan 1]
2. [Alan 2]
3. [Alan 3]

### LLM'in Yetersiz Kaldığı Alanlar
1. [Alan 1 - neden yetersiz kaldığı]
2. [Alan 2]

### LLM Kullanımı Hakkında Düşüncelerim
> [Bu case boyunca LLM kullanarak neler öğrendiniz?
> LLM'siz ne kadar sürede bitirebilirdiniz?
> Gelecekte LLM'i nasıl daha etkili kullanabilirsiniz?]


