using System;
using System.Collections.Generic;
using System.Media;
using System.Threading;

class Program
{
    static List<Karakter> karakterler = new List<Karakter>();
    static bool oyunBitti = false;
    static string oyunIsmi = "Prenses Başka Bir Kalede ";
    static Karakter seciliKarakter;
    static int coin = 0;
    static List<string> envanter = new List<string>();
    static Random rand = new Random();
    static bool savasDevam = true;
    static SoundPlayer player = new SoundPlayer("Sounds/background.wav");
    static bool sesAcik = true;

    static void Main(string[] args)
    {
        player.PlayLooping();
        BaslangicEkrani();
        while (!oyunBitti)
        {
            AnaMenu();
        }
    }

    static void BaslangicEkrani()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("********************************************");
        Console.WriteLine($"*           HOŞ GELDİNİZ!                 *");
        Console.WriteLine($"*       {oyunIsmi.ToUpper()}            *");
        Console.WriteLine("********************************************");
        Console.ResetColor();
        Console.WriteLine("oyun açılıyor lütfen bekleyiniz...");
        Thread.Sleep(3000);
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("********************************************");
        Console.WriteLine("*  PRENSESİ KURTARMA HİKAYESİNE HOŞ GELDİNİZ!  *");
        Console.WriteLine("********************************************");
        Console.WriteLine("Bir zamanlar barışçıl bir krallık vardı. Ancak, bu krallığın prensesi karanlık bir büyücü tarafından kaçırıldı.");
        Console.WriteLine("Sen, cesur bir savaşçı olarak, prensesi kurtarmak ve krallığı eski huzuruna kavuşturmak için yola çıkıyorsun.");
        Console.WriteLine("Yolculuğun boyunca, güçlü düşmanlarla karşılaşacak ve zorlu görevlerle başa çıkmak zorunda kalacaksın.");
        Console.WriteLine("Ama unutma, her zafer seni prensesine bir adım daha yakınlaştıracak.");
        Console.WriteLine("********************************************");
        Console.ResetColor();
        Console.WriteLine("Devam etmek için bir tuşa basın...");
        Console.ReadKey();
    }
    static void Ipucu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("=== İpuçları ===");
        Console.ResetColor();
        Console.WriteLine("1. Oyuna başlamak için ana menüden 'Oyuna Başla' seçeneğini seçin.");
        Console.WriteLine("2. Karakterinizi oluşturun veya mevcut karakterlerden birini seçin.");
        Console.WriteLine("3. Oyunda ilerlemek için savaşlarda düşmanları yenin.");
        Console.WriteLine("4. Sağlık, güç artırıcı ve diğer eşyaları marketten satın alabilir ve envanterinizde kullanabilirsiniz.");
        Console.WriteLine("5. Bölümleri geçtikçe karakterinizin seviyesi artacak.");
        Console.WriteLine("6. Ana menüye dönmek için savaş esnasında 'Ana Menü' seçeneğini kullanabilirsiniz.");
        Console.WriteLine("7. Güçlü saldırı ve savunma hakkınızı stratejik olarak kullanın. Her biri yalnızca bir kez kullanılabilir.");
        Console.WriteLine("8. Oyunu kazanmak için 20. bölüme kadar ilerleyin ve prensesi kurtarın.");
        Console.WriteLine("9. Karakterinizin sağlık puanlarını artırmak için sağlık iksiri kullanabilirsiniz.");
        Console.WriteLine("10. Macerayı yeniden yaşamak isterseniz, oyunun sonunda 'Evet' seçeneğini seçebilirsiniz.");
        Console.WriteLine("Ana menüye dönmek için bir tuşa basın...");
        Console.ReadKey();
    }



    static void AnaMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("=== ANA MENÜ ===");
        Console.ResetColor();
        Console.WriteLine("1- Yeni Karakter Oluştur");
        Console.WriteLine("2- Mevcut Karakterleri Görüntüle");
        Console.WriteLine("3- Karakter Sil");
        Console.WriteLine("4- Oyuna Başla");
        Console.WriteLine("5- Market");
        Console.WriteLine("6- Envanter");
        Console.WriteLine("7- Çıkış");
        Console.WriteLine("8- İpucu");
        Console.WriteLine($"9- Ses {(sesAcik ? "Kapat" : "Aç")}");
        Console.Write("Seçiminiz: ");

        string secim = Console.ReadLine();
        switch (secim)
        {
            case "1":
                KarakterOlustur();
                break;
            case "2":
                KarakterleriGoruntule();
                break;
            case "3":
                KarakterSil();
                break;
            case "4":
                if (karakterler.Count == 0)
                {
                    Console.WriteLine("Lütfen önce bir karakter oluşturun.");
                    Console.ReadKey();
                }
                else
                {
                    KarakterSec();
                    OyunaBasla();
                }
                break;
            case "5":
                Market();
                break;
            case "6":
                EnvanterGoruntule();
                break;
            case "7":
                Console.WriteLine("Oyundan çıkılıyor...");
                oyunBitti = true;
                break;
            case "8":
                Ipucu();
                break;
            case "9":
                SesAcKapa();
                break;
            default:
                Console.WriteLine("Geçersiz seçenek. Tekrar deneyin.");
                break;
        }
    }

    static void SesAcKapa()
    {
        if (sesAcik)
        {
            player.Stop();
            sesAcik = false;
        }
        else
        {
            player.PlayLooping();
            sesAcik = true;
        }
    }

    static void KarakterOlustur()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Karakter türünü seçin:");
        Console.WriteLine("1- Savaşçı (Yüksek sağlık ve güç)");
        Console.WriteLine("2- Okçu (Orta sağlık ve yüksek hız)");
        Console.WriteLine("3- Büyücü (Düşük sağlık, yüksek hasar)");
        Console.WriteLine("4- Test Savaşçısı (Çok yüksek sağlık ve güç)");
        Console.Write("Seçiminiz: ");
        Console.ResetColor();

        string secim = Console.ReadLine();
        string isim = "";
        bool validName = false;
        while (!validName)
        {
            try
            {
                Console.Write("Karakter ismini girin: ");
                isim = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(isim))
                {
                    throw new Exception("İsim boş olamaz.");
                }
                validName = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        Karakter karakter = secim switch
        {
            "1" => new Savasci(isim, 1, 120, 20), // Savaşçı için daha fazla sağlık, ama daha az güç
            "2" => new Okcu(isim, 1, 100, 15), // Okçu için dengeli sağlık ve güç
            "3" => new Buyucu(isim, 1, 80, 25), // Büyücü için düşük sağlık ama yüksek hasar
            "4" => new TestSavascisi(isim, 1, 1000, 200), // Test amaçlı karakter
            _ => null
        };

        if (karakter != null)
        {
            karakterler.Add(karakter);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{karakter.Isim} karakteri başarıyla oluşturuldu!");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine("Geçersiz seçim. Karakter oluşturulamadı.");
        }

        Console.WriteLine("Ana menüye dönmek için bir tuşa basın...");
        Console.ReadKey();
    }

    static void KarakterleriGoruntule()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        if (karakterler.Count == 0)
        {
            Console.WriteLine("Henüz bir karakter oluşturulmadı.");
        }
        else
        {
            Console.WriteLine("Mevcut Karakterler:");
            foreach (var karakter in karakterler)
            {
                karakter.DurumYazdir();
            }
        }
        Console.ResetColor();
        Console.WriteLine("Ana menüye dönmek için bir tuşa basın...");
        Console.ReadKey();
    }

    static void KarakterSil()
    {
        Console.Clear();
        if (karakterler.Count == 0)
        {
            Console.WriteLine("Silinecek karakter bulunmamaktadır.");
            Console.WriteLine("Ana menüye dönmek için bir tuşa basın...");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Silmek istediğiniz karakteri seçin:");
        for (int i = 0; i < karakterler.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {karakterler[i].Isim}");
        }

        Console.Write("Silmek istediğiniz karakterin numarasını girin: ");
        string secim = Console.ReadLine();
        if (int.TryParse(secim, out int index) && index >= 1 && index <= karakterler.Count)
        {
            Console.WriteLine($"Emin misiniz? '{karakterler[index - 1].Isim}' adlı karakteri silmek istiyor musunuz? (Evet/Hayır)");
            string onay = Console.ReadLine().ToLower();

            if (onay == "evet")
            {
                karakterler.RemoveAt(index - 1);
                Console.WriteLine("Karakter başarıyla silindi.");
            }
            else
            {
                Console.WriteLine("Silme işlemi iptal edildi.");
            }
        }
        else
        {
            Console.WriteLine("Geçersiz seçenek.");
        }

        Console.WriteLine("Ana menüye dönmek için bir tuşa basın...");
        Console.ReadKey();
    }

    static void KarakterSec()
    {
        Console.Clear();
        Console.WriteLine("Hangi karakterle oynamak istiyorsunuz?");
        for (int i = 0; i < karakterler.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {karakterler[i].Isim}");
        }

        Console.Write("Seçiminiz: ");
        string secim = Console.ReadLine();
        if (int.TryParse(secim, out int index) && index >= 1 && index <= karakterler.Count)
                    {
            seciliKarakter = karakterler[index - 1];
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{seciliKarakter.Isim} karakteri seçildi!");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine("Geçersiz seçim. İlk karakter seçildi.");
            seciliKarakter = karakterler[0];
        }
        Console.WriteLine("Devam etmek için bir tuşa basın...");
        Console.ReadKey();
    }

    static void Market()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("=== Market ===");
        Console.ResetColor();
        Console.WriteLine($"Coinleriniz: {coin}");
        Console.WriteLine("1- Can Satın Al (10 coin)");
        Console.WriteLine("2- Güç Artırıcı Satın Al (15 coin)");
        Console.WriteLine("3- Eşya Satın Al (20 coin)");
        Console.WriteLine("4- Ana Menüye Dön");
        Console.Write("Seçiminiz: ");

        string secim = Console.ReadLine();
        switch (secim)
        {
            case "1":
                if (seciliKarakter != null && coin >= 10)
                {
                    seciliKarakter.Saglik += 20;
                    coin -= 10;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("20 sağlık puanı satın alındı!");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("Yeterli coin yok veya seçili bir karakter yok. Lütfen oyuna başlayın.");
                }
                break;
            case "2":
                if (seciliKarakter != null && coin >= 15)
                {
                    seciliKarakter.Güç += 5;
                    coin -= 15;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("5 güç artışı satın alındı!");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("Yeterli coin yok veya seçili bir karakter yok. Lütfen oyuna başlayın.");
                }
                break;
            case "3":
                if (coin >= 20)
                {
                    Console.WriteLine("Satın almak istediğiniz eşyayı seçin:");
                    Console.WriteLine("1- Sağlık İksiri");
                    Console.WriteLine("2- Güç Yüzüğü");
                    Console.WriteLine("3- Hız Pelerini");
                    Console.Write("Seçiminiz: ");
                    string esyaSecim = Console.ReadLine();
                    string esya = esyaSecim switch
                    {
                        "1" => "Sağlık İksiri",
                        "2" => "Güç Yüzüğü",
                        "3" => "Hız Pelerini",
                        _ => null
                    };

                    if (esya != null)
                    {
                        envanter.Add(esya);
                        coin -= 20;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{esya} satın alındı ve envantere eklendi!");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz seçenek.");
                    }
                }
                else
                {
                    Console.WriteLine("Yeterli coin yok.");
                }
                break;
            case "4":
                return;
            default:
                Console.WriteLine("Geçersiz seçenek.");
                break;
        }
        Console.WriteLine("Devam etmek için bir tuşa basın...");
        Console.ReadKey();
    }

    static void EnvanterGoruntule()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== Envanteriniz ===");
        Console.ResetColor();
        if (envanter.Count == 0)
        {
            Console.WriteLine("Envanteriniz boş.");
        }
        else
        {
            foreach (var esya in envanter)
            {
                Console.WriteLine($"- {esya}");
            }
        }
        Console.WriteLine("Ana menüye dönmek için bir tuşa basın...");
        Console.ReadKey();
    }

    static void OyunaBasla()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Hikaye: Prensesi kurtarmak için zorlu bir maceraya atılıyorsunuz...");
        Console.ResetColor();
        Console.WriteLine("Oyundan çıkmak için 'q', devam etmek için herhangi bir tuşa basın...");

        if (Console.ReadLine().ToLower() == "q")
        {
            oyunBitti = true;
            return;
        }

        int bolum = 1;

        while (seciliKarakter.HayattaMi() && !oyunBitti)
        {
            Console.Clear();
            Console.WriteLine($"=== Bölüm {bolum} ===");

            if (bolum == 5 || bolum == 10 || bolum == 15)
            {
                HikayeDevam(bolum);
            }

            savasDevam = true;
            SavasEkrani(seciliKarakter, YeniDusman(bolum));
            if (!savasDevam) return;

            RastgeleOlay();
            seciliKarakter.Seviye++;

            if (bolum == 20)
            {
                PrensesKurtar();
                break;
            }

            bolum++;
        }

        if (!oyunBitti)
        {
            Console.WriteLine("Oyun sona erdi. Ana menüye dönmek için bir tuşa basın...");
            Console.ReadKey();
        }
    }

    static void HikayeDevam(int bolum)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        switch (bolum)
        {
            case 5:
                Console.WriteLine("Hikaye: Prensesin izini sürdünüz ve karanlık ormanın derinliklerine doğru ilerlemeye başladınız.");
                break;
            case 10:
                Console.WriteLine("Hikaye: Karanlık büyücünün kulesine yaklaştınız. Kuleye giden yolu bulmak için eski bir harita buldunuz.");
                break;
            case 15:
                Console.WriteLine("Hikaye: Kuleye yaklaştıkça, büyücünün tuzaklarını aşmanız gerekiyor. Ama cesaretiniz sizi ileriye taşıyor.");
                break;
        }
        Console.ResetColor();
        Console.WriteLine("Devam etmek için bir tuşa basın...");
        Console.ReadKey();
    }

    static void PrensesKurtar()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("********************************************");
        Console.WriteLine("*          TEBRİKLER! PRENSESİ KURTARDINIZ!          *");
        Console.WriteLine("********************************************");
        Console.WriteLine("Prensesi kurtararak krallığa huzur getirdiniz. Cesaretiniz ve kararlılığınız sayesinde tüm zorlukların üstesinden geldiniz.");
        Console.WriteLine("Krallık size minnettar! Oyunu başarıyla tamamladınız!");
        Console.WriteLine("********************************************");
        Console.ResetColor();
        Console.WriteLine("Bu macerayı yeniden yaşamak ister misiniz? (Evet/Hayır)");

        if (Console.ReadLine().ToLower() == "evet")
        {
            oyunBitti = false;
            BaslangicEkrani();
        }
        else
        {
            oyunBitti = true;
        }
    }

    static void SavasEkrani(Karakter oyuncu, Karakter dusman)
    {
        bool kullanildiGucluSaldiri = false;
        bool kullanildiSavunma = false;

        while (oyuncu.HayattaMi() && dusman.HayattaMi() && savasDevam)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("=== Savaş ===");
            Console.ResetColor();
            oyuncu.DurumYazdir();
            dusman.DurumYazdir();

            Console.WriteLine("Seçenekler: [s]aldır, [g]üçlü saldırı, [d]efans, [e]şya kullan, [m]arkete dön, [a]na menü");
            string secim = Console.ReadLine().ToLower();
            switch (secim)
            {
                case "s":
                    oyuncu.Saldir(dusman);
                    if (dusman.HayattaMi()) dusman.Saldir(oyuncu);
                    break;
                case "g":
                    if (!kullanildiGucluSaldiri)
                    {
                        oyuncu.GucluSaldir(dusman);
                        kullanildiGucluSaldiri = true;
                    }
                    else
                    {
                        Console.WriteLine("Güçlü saldırı hakkınızı zaten kullandınız!");
                    }
                    if (dusman.HayattaMi()) dusman.Saldir(oyuncu);
                    break;
                case "d":
                    if (!kullanildiSavunma)
                    {
                        oyuncu.SavunmaModu = true;
                        kullanildiSavunma = true;
                        Console.WriteLine("Savunma moduna geçtiniz! Alınan hasar azaltıldı ve +10 sağlık kazandınız.");
                        oyuncu.Saglik += 10;
                    }
                    else
                    {
                        Console.WriteLine("Savunma hakkınızı zaten kullandınız!");
                    }
                    if (dusman.HayattaMi()) dusman.Saldir(oyuncu);
                    break;
                case "e":
                    EsyaKullan();
                    break;
                case "m":
                    Market();
                    break;
                case "a":
                    Console.WriteLine("Ana menüye dönülüyor...");
                    savasDevam = false;
                    return;
                default:
                    Console.WriteLine("Geçersiz seçenek.");
                    break;
            }

            if (!oyuncu.HayattaMi() || !dusman.HayattaMi() || !savasDevam)
            {
                break;
            }
        }

        if (!oyuncu.HayattaMi())
        {
            Console.WriteLine("Karakteriniz öldü! Bölümü kaybettiniz.");
        }
        else if (!dusman.HayattaMi())
        {
            Console.WriteLine("Düşmanı yendiniz! Bölüm başarıyla tamamlandı.");
            coin += 10; // Düşmanı yendikten sonra coin kazan
            Console.WriteLine($"Tebrikler! 10 coin kazandınız. Mevcut coinleriniz: {coin}");
            oyuncu.Saglik += 10; // Düşmanı yendikten sonra sağlık artır
            Console.WriteLine("Sağlık puanınız 10 arttı.");
        }

        Console.WriteLine("Devam etmek için bir tuşa basın...");
        Console.ReadKey();
    }

    static void EsyaKullan()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("=== Eşya Kullan ===");
        Console.ResetColor();
        if (envanter.Count == 0)
        {
            Console.WriteLine("Envanterinizde kullanılabilecek eşya yok.");
        }
        else
        {
            Console.WriteLine("Kullanmak istediğiniz eşyayı seçin:");
            for (int i = 0; i < envanter.Count; i++)
            {
                Console.WriteLine($"{i + 1}- {envanter[i]}");
            }
            Console.Write("Seçiminiz: ");
            if (int.TryParse(Console.ReadLine(), out int secim) && secim >= 1 && secim <= envanter.Count)
            {
                string esya = envanter[secim - 1];
                switch (esya)
                {
                    case "Sağlık İksiri":
                        seciliKarakter.Saglik += 20;
                        Console.WriteLine("Sağlık İksiri kullandınız! Sağlık 20 arttı.");
                        break;
                    case "Güç Yüzüğü":
                        seciliKarakter.Güç += 5;
                        Console.WriteLine("Güç Yüzüğü kullandınız! Güç 5 arttı.");
                        break;
                    case "Hız Pelerini":
                        // Hız etkisi ekleyelim: Saldırı şansı iki katına çıkar
                        Console.WriteLine("Hız Pelerini kullandınız! Geçici olarak saldırı hızınız arttı.");
                        // Hız etkisini geçici olarak ekleyebiliriz
                        break;
                }
                envanter.RemoveAt(secim - 1); // Eşya kullanıldıktan sonra envanterden çıkar
            }
            else
            {
                Console.WriteLine("Geçersiz seçenek.");
            }
        }
        Console.WriteLine("Devam etmek için bir tuşa basın...");
        Console.ReadKey();
    }

    static void RastgeleOlay()
    {
        int olay = rand.Next(1, 5);
        switch (olay)
        {
            case 1:
                Console.WriteLine("Bir hazine sandığı buldunuz. Açmak ister misiniz? (Evet/Hayır)");
                if (Console.ReadLine().ToLower() == "evet")
                {
                    string[] esyalar = { "Altın Kolyeler", "Gizemli Kitap", "İksir Şişesi" };
                    string bulunanEsya = esyalar[rand.Next(esyalar.Length)];
                    envanter.Add(bulunanEsya);
                    Console.WriteLine($"{bulunanEsya} envanterinize eklendi!");
                }
                else
                {
                    Console.WriteLine("Hazine sandığı açılmadı.");
                }
                break;
            case 2:
                Console.WriteLine("Bir kasabalı size bir teklifte bulundu. Kabul ediyor musunuz? (Evet/Hayır)");
                if (Console.ReadLine().ToLower() == "evet")
                {
                    Console.WriteLine("Kasabalı size sağlık iksiri verdi. Sağlık 20 arttı!");
                    seciliKarakter.Saglik += 20;
                }
                else
                {
                    Console.WriteLine("Teklif reddedildi.");
                }
                break;
            case 3:
                Console.WriteLine("Bir bitki buldunuz. Toplar mısınız? (Evet/Hayır)");
                if (Console.ReadLine().ToLower() == "evet")
                {
                    Console.WriteLine("Bitkiyi topladınız. Güç 5 arttı!");
                    seciliKarakter.Güç += 5;
                }
                else
                {
                    Console.WriteLine("Bitki toplanmadı.");
                }
                break;
            case 4:
                Console.WriteLine("Bir karanlık ormana girdiniz ve sizi takip eden gölgeler fark ettiniz. Saldırılmak üzeresiniz! Savunma yapar mısınız? (Evet/Hayır)");
                if (Console.ReadLine().ToLower() == "evet")
                {
                    Console.WriteLine("Savunma yaptınız ve sağ salim kurtuldunuz. Ancak biraz hasar aldınız.");
                    seciliKarakter.Saglik -= 10;
                }
                else
                {
                    Console.WriteLine("Savunma yapmadınız ve ağır yaralandınız. Sağlığınız büyük ölçüde azaldı.");
                    seciliKarakter.Saglik -= 30;
                }
                break;
        }
        Console.WriteLine("Devam etmek için bir tuşa basın...");
        Console.ReadKey();
    }

    static Karakter YeniDusman(int bolum)
    {
        Random rand = new Random();
        int dusmanTuru = rand.Next(1, 4);

        return dusmanTuru switch
        {
            1 => new Savasci($"Düşman Savaşçı {bolum}", bolum, 30 + bolum * 5, 10 + bolum * 2), // Zorluk düşürüldü
            2 => new Okcu($"Düşman Okçu {bolum}", bolum, 25 + bolum * 4, 15 + bolum * 3),
            3 => new Buyucu($"Düşman Büyücü {bolum}", bolum, 20 + bolum * 3, 20 + bolum * 4),
            _ => null
        };
    }
}

public abstract class Karakter
{
    public string Isim { get; set; }
    public int Seviye { get; set; }
    public int Saglik { get; set; }
    public int Güç { get; set; }
    public bool SavunmaModu { get; set; }

    public Karakter(string isim, int seviye, int saglik, int güç)
    {
        Isim = isim;
        Seviye = seviye;
        Saglik = saglik;
        Güç = güç;
        SavunmaModu = false;
    }

    public abstract void Saldir(Karakter hedef);

    public void DurumYazdir()
    {
        Console.WriteLine($"{Isim} | Seviye: {Seviye} | Sağlık: {Saglik} | Güç: {Güç}");
    }

    public void SağlıkGüncelle(int hasar)
    {
        if (SavunmaModu) hasar = (int)(hasar * 0.7);
        Saglik -= hasar;
        if (Saglik < 0) Saglik = 0;
    }

    public bool HayattaMi() => Saglik > 0;

    public virtual void GucluSaldir(Karakter hedef)
    {
        Console.WriteLine($"{Isim} güçlü bir saldırı yaptı!");
        hedef.SağlıkGüncelle(Güç * 2);
    }
}

public class Savasci : Karakter
{
    public Savasci(string isim, int seviye, int saglik, int güç)
        : base(isim, seviye, saglik, güç) { }

    public override void Saldir(Karakter hedef)
    {
        Console.WriteLine($"{Isim} düşmana saldırdı!");
        hedef.SağlıkGüncelle(Güç);
    }
}

public class Okcu : Karakter
{
    public Okcu(string isim, int seviye, int saglik, int güç)
        : base(isim, seviye, saglik, güç) { }

    public override void Saldir(Karakter hedef)
    {
        Console.WriteLine($"{Isim} düşmana ok attı!");
        hedef.SağlıkGüncelle(Güç);
    }
}

public class Buyucu : Karakter
{
    public Buyucu(string isim, int seviye, int saglik, int güç)
        : base(isim, seviye, saglik, güç) { }

    public override void Saldir(Karakter hedef)
    {
        Console.WriteLine($"{Isim} düşmana büyü yaptı!");
        hedef.SağlıkGüncelle(Güç);
    }
}

public class TestSavascisi : Savasci
{
    public TestSavascisi(string isim, int seviye, int saglik, int güç)
        : base(isim, seviye, saglik, güç) { }
}



