## Özet

| Bilgi | Değer |
|-------|-------|
| Toplam prompt sayısı | 13 |
| Kullanılan araçlar | Gemini-pro |
| En çok yardım alınan konular | [Sistem kurma, yol gösterme, temiz kod yapısı] |
| Tahmini LLM ile kazanılan süre | 30-40 saat |

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

## Prompt 3: [Camera Remake]

**Araç:** [Gemini-Pro]
**Tarih/Saat:** 2026-01-30 21:05

**Prompt:**
```
Sahnemdeki MainCamerayı karakterimin prefabine atmak istemiyorum. Sahnemde ayrı bir şekilde durmalı. Prefab yapabiliriz kamerayı. Ne öneriyorsun? Ve kodu bu şekilde nasıl güncelleriz?
```

**Alınan Cevap (Özet):**
```
Harika bir nokta rovro-kun. FPS oyunlarında karakter prefab'inin içine kamerayı gömmek, özellikle multiplayer (her oyuncunun kendi kamerası olması durumu) veya sinematik geçişler söz konusu olduğunda esnekliği azaltır.

Kamerayı sahnede bağımsız (veya prefab) olarak tutup, oyun başladığında karaktere bağlamak endüstride sıkça kullanılan bir yöntemdir.

Önerim: "Runtime Re-parenting" (Çalışma Zamanında Ebeveynleme)
Kamera sahnede özgürce durur. Oyun başladığında (Awake veya Start), Player scripti "Benim gözlerim nerede?" diye sorar ve sahnedeki MainCamera'yı bulup kendi "Göz" (CameraRoot) noktasına child (çocuk) yapar.

Avantajları:

Prefab Temizliği: Karakter prefabinde kamera olmaz, sadece bir "Eye" noktası olur.

Jitter-Free: Kamera fiziksel olarak karakterin çocuğu olduğu için, hareket sırasında titreme (jitter) veya gecikme (lag) olmaz.

Esneklik: İstersen kamerayı başka bir sinematiğe çekip sonra tekrar oyuncuya verebilirsin.

Kodu Nasıl Güncelleriz?
FirstPersonController scriptini bu mantığa göre refactor ettim.

Yapılan Değişiklikler:

Awake metodunda Camera.main ile sahnedeki aktif kamera bulundu.

Kamera, m_CameraRoot objesinin altına taşındı (SetParent).

Pozisyon ve rotasyon sıfırlandı, böylece tam göz hizasına oturdu.

Coding Conventions (Prefixler, Regionlar, XML Docs) aynen uygulandı.
```

**Nasıl Kullandım:**
- [X] Direkt kullandım (değişiklik yapmadan)
- [ ] Adapte ettim (değişiklikler yaparak)
- [ ] Reddettim (kullanmadım)

**Açıklama:**
> [Karakter Prefabının içinde camera olmasını istemedim.]

**Yapılan Değişiklikler (adapte ettiyseniz):**
> [LLM cevabını nasıl değiştirdiğinizi açıklayın]

---

## Prompt 4: [Interaction Sistem Altyapısı]

**Araç:** [Gemini-Pro]
**Tarih/Saat:** YYYY-MM-DD HH:MM

**Prompt:**
```
Tamam karakterin kontrollerini hallettik. Projeme gerekli 3 kapi 3 anahtar ve 1 sandik prefabi ekledim. Colliderlarini duzenledim. Simdi Interaction sistemin altyapisini kurabiliriz. Bu sistemi kurarken; moduler olmasina dikkat etmeliyiz. Ben senin ilk yazdigin  InputManager scriptinden e tusunun interact ile alakali kodlarini kaldirdim. Simdi sisteme uygun olacak sekilde tekrardan eklemeliyiz. 
Beklenen Yapı:
- IInteractable interface
- InteractionDetector (raycast )
- Interaction range kontrolü
- Single interaction point (aynı anda tek nesne ile etkileşim)
**Teknik Detaylar:**
- Oyuncu belirli bir mesafeden nesnelerle etkileşime geçebilmeli
- Birden fazla interactable aynı range'de ise en yakın olanı seçilmeli
- Etkileşim input'u configurable olmalı (Inspector'dan değiştirilebilir)
Simdilik sadece etkilesime gecme sistemini kuralim. Etkilesime gecme kosullarini sonraki branchimde eklicem. Bunu goz onunde bulundurarak sistemi nasil kurmaliyiz tartisalim.

```

**Alınan Cevap (Özet):**
```
Harika bir ilerleme rovro-kun. Kapı, anahtar ve sandık prefablarının hazır olması çok iyi.

Interaction sistemi için Hybrid (Melez) bir yapı kuralım. İstediğin gibi hem Raycast (baktığın yeri kesin seçme) hem de Proximity/OverlapSphere (baktığın yerde bir şey yoksa yakınındakini seçme) yöntemlerini birleştireceğiz. Bu, oyuncu tam olarak ince bir anahtarın üzerine nişan almasa bile, yakındayken onu alabilmesini sağlar (Quality of Life).

Sistemi şu 3 adımda kuracağız:

InputManager: Kaldırdığın kodu geri ekleyeceğiz (Interaction için).

IInteractable: Tüm nesnelerin (Kapı, Sandık) uyacağı ortak sözleşme.

InteractionDetector: Oyuncunun neye baktığını veya neye yakın olduğunu bulan beyin.

Kodları CSharp_Coding_Conventions.md kurallarına tam uyarak yazdım.
```

**Nasıl Kullandım:**
- [X] Direkt kullandım (değişiklik yapmadan)
- [ ] Adapte ettim (değişiklikler yaparak)
- [ ] Reddettim (kullanmadım)

**Açıklama:**
> Interface tabanlı bir sistem kurdurmak istedim.

**Yapılan Değişiklikler (adapte ettiyseniz):**
> [LLM cevabını nasıl değiştirdiğinizi açıklayın]

---

## Prompt 5: [Olası Bugı önceden Çözmek]

**Araç:** [Gemini-Pro]
**Tarih/Saat:** 2026-01-30 23:09

**Prompt:**
```
m_PlayerCamera = GetComponentInChildren<Camera>(); bu sekilde atamaya calisiyorsun ama biz 
/// <summary>
/// Sahnedeki Main Camera'yı bulup CameraRoot altına taşır.
/// </summary>
private void InitializeCamera()
{
    m_PlayerCamera = Camera.main;

    if (m_PlayerCamera == null)
    {
        Debug.LogError("[FirstPersonController] No MainCamera found in the scene via Camera.main!");
        return;
    }

    m_PlayerCamera.transform.SetParent(m_CameraRoot);

    m_PlayerCamera.transform.localPosition = Vector3.zero;
    m_PlayerCamera.transform.localRotation = Quaternion.identity;
}
bu sekilde awakede camerayi tasiyoruz. Bunlarin cakisma ihtimali nedir?

```

**Alınan Cevap (Özet):**
```
Çok haklısın rovro-kun. Çok keskin bir noktayı yakaladın. Bu durum, Unity'de klasik bir "Race Condition" (Yarış Durumu) hatasıdır.

İki script de Awake içinde çalışıyor. Unity'nin hangi scriptin Awake metodunu önce çalıştıracağının garantisi yoktur (Script Execution Order ayarlanmadığı sürece).

Senaryo Analizi (Çakışma Riski)
Senaryo A (İstediğimiz): FirstPersonController önce çalışır -> Kamerayı CameraRoot içine taşır. Sonra InteractionDetector çalışır -> GetComponentInChildren kamerayı bulur. (Sorun yok).

Senaryo B (Hata): InteractionDetector önce çalışır -> GetComponentInChildren bakar, kamera henüz child değil (null döner). Kod Camera.main fallback'ine düşer. Çalışır ama "GetComponentInChildren" mantığı boşa düşmüş olur.

Çözüm: "Start" Metodunu Kullanmak
Unity'de tüm objelerin Awake metotları bittikten sonra Start metotları çalışmaya başlar. Bu yüzden, referans kurma (setup) işlemlerini Awake'te, diğer objelere bağımlı işlemleri Start'ta yapmak en güvenli yoldur.

InteractionDetector scriptinde kamera atamasını Awake yerine Start içine taşıyarak bu zamanlama sorununu kesin olarak çözüyoruz. Çünkü Start çalıştığında, FirstPersonController işini bitirmiş ve kamerayı yerine yerleştirmiş olacak.

İşte bu düzeltmeyi ve coding convention'ları içeren güncel kod:
```

**Nasıl Kullandım:**
- [X] Direkt kullandım (değişiklik yapmadan)
- [ ] Adapte ettim (değişiklikler yaparak)
- [ ] Reddettim (kullanmadım)

**Açıklama:**
> Sonradan bug çıkmasın diye erkenden düzelttirdim.

**Yapılan Değişiklikler (adapte ettiyseniz):**
> [LLM cevabını nasıl değiştirdiğinizi açıklayın]

---

## Prompt 6: [Ui Feedback text]

**Araç:** [Gemini-Pro]
**Tarih/Saat:** 2026-01-30 23:19

**Prompt:**
```
Simdilik sadece item a baktigimizda alinabilir yazsin. Henuz alma islemini bu branchde yapmak istemiyorum.
```

**Alınan Cevap (Özet):**
```
Anlaşıldı rovro-kun. Çok mantıklı bir adım. Kodları karmaşıklaştırmadan önce, sistemin nesneyi doğru algılayıp algılamadığını ve kullanıcıya doğru geri bildirimi (Prompt) verip vermediğini görselleştirelim.

"Alma işlemini" (Inventory logic) devre dışı bırakıp, sadece Görsel Geri Bildirim (UI Feedback) sistemini kuracağız.

Bunun için iki şey yapacağız:

InteractionUI: InteractionDetector'u dinleyen ve ekrana yazı yazan yeni bir script.

PickableItem (Revize): İçindeki alma kodlarını boşaltacağız, sadece Prompt verisini gönderecek.

Kodlar CSharp Coding Conventions standartlarına uygun olarak hazırlandı.
```

**Nasıl Kullandım:**
- [X] Direkt kullandım (değişiklik yapmadan)
- [ ] Adapte ettim (değişiklikler yaparak)
- [ ] Reddettim (kullanmadım)

**Açıklama:**
> Adım adım ilerlemek istedim. Sistem ve branchler karışmasın diye.

**Yapılan Değişiklikler (adapte ettiyseniz):**
> [LLM cevabını nasıl değiştirdiğinizi açıklayın]

## Prompt 7: [Konu Başlığı]

**Araç:** [Gemini-Pro]
**Tarih/Saat:** 2026-01-30 23:53

**Prompt:**
```
interactionui i bu sekilde yapmayalim. Updatede surekli kontrol etmemeli. Biz anahtara baktigimizda event yayinlayalim. interactionui da abone olup bu kosul saglaninca ekrana yazdirmali.
```

**Alınan Cevap (Özet):**
```
Kesinlikle doğru bir karar rovro-kun. Update döngüsünde her karede UI kontrolü yapmak (Polling) yerine, durum değiştiğinde haber veren (Observer Pattern) bir yapı kurmak hem performans hem de mimari açıdan çok daha temizdir.

Bu değişiklik için:

InteractionDetector: OnTargetChanged eventi yayınlayacak. Sadece baktığımız nesne değiştiğinde (A nesnesinden B nesnesine veya boşluğa geçtiğimizde) bu event tetiklenecek.

InteractionUI: Bu eventi dinleyecek ve sadece tetiklendiğinde ekrana yazı yazacak. Update metoduna ihtiyacı kalmayacak.

Kodları standartlarımıza uygun şekilde refactor ettim.
```

**Nasıl Kullandım:**
- [X] Direkt kullandım (değişiklik yapmadan)
- [ ] Adapte ettim (değişiklikler yaparak)
- [ ] Reddettim (kullanmadım)

**Açıklama:**
> Event tabablıya geçirdim performans artışı olsun diye.

**Yapılan Değişiklikler (adapte ettiyseniz):**
> [LLM cevabını nasıl değiştirdiğinizi açıklayın]

## Prompt 8: [Konu Başlığı]

**Araç:** [Gemini-Pro]
**Tarih/Saat:** 2026-01-31 00:43

**Prompt:**
```
Simdi anahtarlari toplayip bu anahtarlari envanterde tutmamiz gerekiyor.
Bu envanter sistemi:
-ayni itemlar ust uste binecek
-farkli itemler farkli yer kaplayacak
-ust uste binen itemlarin kac adet olabilecegi inspectordan ayarlanmali
-envanterde kac farkli item tutabilecegimiz inspectordan ayarlanmali
-toplanan itemleri uida liste seklinde gorunmeli
-ve her zaman elimizde duracak bir currentObject olmali. ilerde kapilari acarken bu currentObjecti kullanarak dogru kapilari acitiricaz
```

**Alınan Cevap (Özet):**
```
Harika bir plan rovro-kun. İstediğin özellikler (Stacking, Slot Limiti, Current Item seçimi), klasik bir RPG/FPS envanter sisteminin temelini oluşturuyor.

Bu yapıyı kurmak için şu adımları izleyeceğiz:

ItemData Güncellemesi: Eşyaların "üst üste kaç tane binebileceği" bilgisini ekleyeceğiz.

InventorySlot Sınıfı: "Hangi eşyadan kaç tane var" bilgisini tutan ara bir sınıf oluşturacağız.

InventoryController (Core): Ekleme, çıkarma, istifleme ve aktif eşya seçme mantığını buraya kuracağız.

InventoryUI: Envanterdeki durumu ekrana liste olarak basacak.

Kodları CSharp Coding Conventions standartlarına göre revize ettim.
```

**Nasıl Kullandım:**
- [X] Direkt kullandım (değişiklik yapmadan)
- [ ] Adapte ettim (değişiklikler yaparak)
- [ ] Reddettim (kullanmadım)

**Açıklama:**
> Envanter sistemini anlatıp ona göre kodu istedim.

**Yapılan Değişiklikler (adapte ettiyseniz):**
> [LLM cevabını nasıl değiştirdiğinizi açıklayın]

## Prompt 9: [Item Toplama Sistem Değişikliği]

**Araç:** [Gemini-Pro]
**Tarih/Saat:** 2026-01-31 01:16

**Prompt:**
```
Tamam topluyor guzel. ancak bunu gelistirmeliyiz. Itemler yok olmamali. Karakterin ustunde bir conteynirin altinda toplanmalilar. Ve sadece currentobject gozukmeli(setactive True). Ayrica elimizdeki currentObject karakter prefabimdeki currentObjectHandle objesinin konumuna gitmeli. Sonucta elimizdeki objeyi duzgun yerde gormeliyiz. Bunu yaptiktan sonra elimizde hangi objenin gozukecegini secebilme mekanigini eklicez ona gore yazalim bu belirttigim sistemi.
```

**Alınan Cevap (Özet):**
```
Rovro-kun, bu harika bir adım. Eşyaları yok etmek yerine "fiziksel" olarak üzerimizde taşımak (Visual Inventory), oyunun gerçekçiliğini çok artırır.

Bu sistemi kurmak için mantığı şöyle güncelliyoruz:

InventoryContainer: Player üzerinde eşyaların fiziksel olarak duracağı görünmez bir kutu.

CurrentObjectHandle: Seçili eşyanın (CurrentItem) elimizde duracağı tam pozisyon (Right Hand transform).

Physical Storage: Toplanan eşyalar InventoryContainer altına taşınacak ve kapatılacak (SetActive(false)).

Visual Handling: Sadece seçili olan eşya (CurrentItem), InventoryContainer'dan alınıp CurrentObjectHandle altına taşınacak ve açılacak (SetActive(true)).

Kodları CSharp Coding Conventions'a uygun olarak güncelledim.
```

**Nasıl Kullandım:**
- [X] Direkt kullandım (değişiklik yapmadan)
- [ ] Adapte ettim (değişiklikler yaparak)
- [ ] Reddettim (kullanmadım)

**Açıklama:**
> Envanter sistemi yapmak istediğimden yok olmasınlar diye container oluşturdum.

**Yapılan Değişiklikler (adapte ettiyseniz):**
> [LLM cevabını nasıl değiştirdiğinizi açıklayın]

---

## Prompt 10: [Konu Başlığı]

**Araç:** [Gemini-Pro]
**Tarih/Saat:** YYYY-MM-DD HH:MM

**Prompt:**
```
Bu asamada tamam. Ancak yine duzeltmemiz gereken bir sey var. Envanter hep acik durmamali ve esya secmeyi envanteri acip itemin ustune tiklayarak yapmak istiyorum. I tusuna basinca envanter acilmali. sonrasinda hangi itema tikliyorsam elime o gelmeli. Sana soyledigim tum sistemleri kurarken event bazli yapmaya calis. 
```

**Alınan Cevap (Özet):**
```
Harika bir fikir rovro-kun. Envanterin "T" anında açılıp, fare ile etkileşim kurulabilir olması, modern bir oyun hissi için şarttır. Ayrıca "Event-Driven" mimariden şaşmayacağız.

Bu sistem için yapmamız gereken değişiklikler:

InputManager: 'I' tuşunu dinleyen yeni bir event ekleyeceğiz.

InventoryItemUI (Yeni Script): Listedeki her bir satır artık tıklanabilir bir buton olacak. Tıklandığında "Ben seçildim" diye bağıracak.

InventoryUI: Envanteri aç/kapa yapacak, fare imlecini (Cursor) yönetecek ve tıklama olaylarını dinleyip InventoryController'a "Şunu ele al" emrini verecek.

Kodları CSharp Coding Conventions standartlarına göre hazırladım.
```

**Nasıl Kullandım:**
- [X] Direkt kullandım (değişiklik yapmadan)
- [ ] Adapte ettim (değişiklikler yaparak)
- [ ] Reddettim (kullanmadım)

**Açıklama:**
> Envanter sistemini daha ui dostu yapmak için yazdım.

**Yapılan Değişiklikler (adapte ettiyseniz):**
> [LLM cevabını nasıl değiştirdiğinizi açıklayın]

---

## Prompt 11: [UI İyileştirmesi]

**Araç:** [Gemini-Pro]
**Tarih/Saat:** 2026-01-31 01:44

**Prompt:**
```
01.44
Son bir adim var. Itemleri envanterime topladigimda dizilmeli gorunus acisindan cok kotu duruyor. daha duzgun bir sekilde siralayamaz miyiz?
```

**Alınan Cevap (Özet):**
```
aklısın rovro-kun. Alt alta yazılmış düz metinler (Vertical List), modern bir oyun için oldukça "ham" durur. FPS oyunlarında ve çoğu RPG'de standart olan **"Grid (Izgara) Slot Sistemi"**ne geçiş yapmalıyız.

Bu sistemde envanterin, Minecraft veya Diablo gibi "kutucuklar" (Slotlar) halinde görünecek. Her kutucuğun içinde eşyanın İkonu (Resmi) ve köşesinde Adedi yazacak.

Bunu yapmak için kodda çok az, Unity Editor tarafında ise görsel değişiklikler yapacağız.
```

**Nasıl Kullandım:**
- [X] Direkt kullandım (değişiklik yapmadan)
- [ ] Adapte ettim (değişiklikler yaparak)
- [ ] Reddettim (kullanmadım)

**Açıklama:**
> Ui daha güzel dursun diye.

**Yapılan Değişiklikler (adapte ettiyseniz):**
> [LLM cevabını nasıl değiştirdiğinizi açıklayın]

---

## Prompt 11: [Koşul Sistemi]

**Araç:** [Gemini-Pro]
**Tarih/Saat:** 2026-01-31 02:24

**Prompt:**
```
Simdi benim elimde 3 kapi(blue, red, green) ve 3 anahtar(blue, red, green) var. Bu kapilar sadece kendi rengini acmali. Ama sistemi oyle kurmalisin ki sonradan yeni kapi ve anahtar eklemek istedigimde ekleyebileyim rahatca. Ayrica bazi kapilar sadece anahtar ile acilmayacak ayri bi switch ile 2. kosulu gerceklestirmek gerekicek. 2. kosulu sonradan eklicem ama sen yine de buna uygun sistem yaz.
```

**Alınan Cevap (Özet):**
```
Rovro-kun, bu sistem tam istediğin esneklikte olacak. Kapıların açılma mantığını "Tek bir anahtar" kontrolü yerine "Koşul Listesi" (List of Conditions) mantığına oturtacağız.

Bu mimarinin adı Strategy Pattern'e benzer. Kapı, "Beni açmak için ne gerekiyor?" diye kendi üzerindeki koşul listesine soracak.

Şimdi sadece **"Anahtar Koşulu"**nu ekleyeceğiz.

İleride **"Şalter Koşulu"**nu (Switch) yazdığında, kapı kodunu hiç değiştirmeden sadece listeye o koşulu da ekleyeceksin.

İşte "Blue Key" + "Red Key" + "Switch" gibi kombinasyonları destekleyen modüler yapı:
```

**Nasıl Kullandım:**
- [X] Direkt kullandım (değişiklik yapmadan)
- [ ] Adapte ettim (değişiklikler yaparak)
- [ ] Reddettim (kullanmadım)

**Açıklama:**
> Kapılar kendi koşullarına göre açılması gerekiyordu. 

**Yapılan Değişiklikler (adapte ettiyseniz):**
> [LLM cevabını nasıl değiştirdiğinizi açıklayın]

---

## Prompt 13: [Switch Koşulu]

**Araç:** [Gemini-Pro]
**Tarih/Saat:** 2026-01-31 03:26

**Prompt:**
```
Tamamdir simdi salter kosulunu ekleyelim. Salter cekildiginde true olmali. kapi ise bu kosulu kontrol etmeli. tabi moduler yapmistik hangi kapiya eklersem o kapi icin gecerli olmali.
```

**Alınan Cevap (Özet):**
```
Harika! Tam olarak planladığımız "Strategy Pattern" yapısının meyvelerini şimdi toplayacağız rovro-kun.

Sistemimiz şu şekilde çalışacak:

SwitchController: Sahnede fiziksel olarak duran, tıklandığında kolu inip kalkan ve IsActive durumunu değiştiren nesne.

SwitchCondition: Kapının üzerindeki LockCondition listesine eklenecek olan, "Şu şalter açık mı?" diye kontrol eden mantık.

Kapının koduna (DoorController) tek bir satır bile dokunmadan bu özelliği ekleyeceğiz.
```

**Nasıl Kullandım:**
- [X] Direkt kullandım (değişiklik yapmadan)
- [ ] Adapte ettim (değişiklikler yaparak)
- [ ] Reddettim (kullanmadım)

**Açıklama:**
> Key koşuluna ek olarak switch koşulu da eklettirdim.

**Yapılan Değişiklikler (adapte ettiyseniz):**
> [LLM cevabını nasıl değiştirdiğinizi açıklayın]

---

## Prompt 13: [Switch Davranış Düzeltmesi]

**Araç:** [Gemini-Pro]
**Tarih/Saat:** 2026-01-31 03:47

**Prompt:**
```
Salteri geri kapattigimda kapiyi halen acip kapatabiliyorum. Yapamamam lazim
```

**Alınan Cevap (Özet):**
```
Sorun şuradan kaynaklanıyor rovro-kun: Bizim mevcut kodumuzda kapı bir kez açıldığında m_IsLocked = false yapıyoruz. Bu, kapının kilidini sonsuza kadar kırıyor.

Anahtar mantığında bu doğrudur (anahtarı kullandın, kilit açıldı). Ancak Şalter (Switch) mantığında bu yanlıştır; şalter kapanırsa kilit geri gelmelidir.

Bunu çözmek için sisteme "Kalıcı Kilit" (Key) ve "Geçici Kilit" (Switch) ayrımı getirmemiz lazım.

Yapman gereken değişiklikler sırasıyla şunlar:

1. LockCondition.cs (Base Class Güncellemesi)
Ana sınıfa yeni bir özellik ekliyoruz: ShouldRemoveAfterUnlock.

Eğer true ise (Anahtar gibi): Kilit açılınca bu koşul listeden silinir.

Eğer false ise (Şalter gibi): Kilit açılsa bile koşul listede kalır ve her seferinde tekrar kontrol edilir.
```

**Nasıl Kullandım:**
- [X] Direkt kullandım (değişiklik yapmadan)
- [ ] Adapte ettim (değişiklikler yaparak)
- [ ] Reddettim (kullanmadım)

**Açıklama:**
> Switch kapanınca kapı açılmaması gerekiyordu.

**Yapılan Değişiklikler (adapte ettiyseniz):**
> [LLM cevabını nasıl değiştirdiğinizi açıklayın]

---


## Genel Değerlendirme

### LLM'in En Çok Yardımcı Olduğu Alanlar
1. Kod
2. Ui
3. BugFix

### LLM'in Yetersiz Kaldığı Alanlar
1. Optimize
2. Tek seferde perfect kod hazırlama

### LLM Kullanımı Hakkında Düşüncelerim
> Pek öğrendim diyemem. Yazdırdığı kodları tekrardan inceleyip sistemi daha iyi anlamaya çalışıp, daha optimize edebilirim.
> 3-4 gün
> Daha spesifik promptlar yazdırarak temele odaklanırım.


