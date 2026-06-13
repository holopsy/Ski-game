# Ski Game

## Apraksts

Šis projekts ir Unity vidē izstrādāts 3D slēpošanas spēles prototips. Spēlētājs kontrolē slēpotāju, brauc lejup pa trasi, izvairās no šķēršļiem un cenšas sasniegt finišu pēc iespējas īsākā laikā.

Projektā ir galvenā izvēlne un divi spēlējami līmeņi. Katrā līmenī ir sava trase, šķēršļi, laika uzskaite, labākais rezultāts un atsevišķs līderu saraksts.

## Controls

* **A** - pagriezties pa kreisi
* **D** - pagriezties pa labi
* **Space** - īslaicīgs ātruma boost

## Galvenā izvēlne

* **Play** poga sāk spēli un ielādē pirmo līmeni.
* **Quit Game** poga atver iziešanas apstiprinājuma logu.
* **Yes** poga aizver spēli.
* **No** poga aizver apstiprinājuma logu.

## Līmeņi

Projektā ir divi spēlējami līmeņi:

* **Level 1** - trase ar kokiem, akmeņiem, karodziņiem, lēcieniem un citiem šķēršļiem.
* **Level 2** - atšķirīga trase ar lielāku šķēršļu un sniegavīru daudzumu.

Pēc pirmā līmeņa pabeigšanas spēlētājs var restartēt līmeni, aizvērt spēli vai turpināt uz nākamo līmeni.

## Spēlētāja kustība

* Spēlētājs pārvietojas, izmantojot `Rigidbody`.
* Slēpotājs automātiski brauc uz priekšu.
* Spēlētājs var pagriezties pa kreisi un pa labi.
* Maksimālais pagrieziena leņķis ir 90 grādi.
* Ātrums mainās atkarībā no braukšanas virziena.
* Kustība darbojas tikai tad, kad spēlētājs atrodas uz zemes.
* Sadursmes laikā spēlētājs var tikt atsists atpakaļ.
* Knockback laikā vadība uz īsu brīdi tiek atspējota.

## Sacensību sistēma

* Starta zona sāk sacensību taimeri.
* Finiša zona aptur taimeri.
* Pēc finiša spēle tiek apturēta.
* Tiek parādīts finiša panelis un gala rezultāts.
* Finišā tiek atskaņota skaņa.
* Slēpošanas skaņa pēc finiša tiek apturēta.

## Karodziņi un sodi

Ja spēlētājs izbrauc karodziņam gar nepareizo pusi:

* Gala laikam tiek pievienota viena sekunde.
* Ekrānā parādās `+1s Penalty!` paziņojums.
* Tiek atskaņota kļūdas skaņa.

## Šķēršļi

Spēlē ir vairāki šķēršļu veidi:

* **Akmeņi** - izraisa spēlētāja knockback.
* **Koki** - darbojas kā fiziski šķēršļi.
* **Sniegavīri** - met sniega bumbas spēlētāja virzienā.
* **Sniega bumbas** - atsit spēlētāju un uz 0,5 sekundēm atspējo vadību.

Sadursmes laikā tiek atskaņota skaņa.

## Sniegavīru sistēma

* Sniegavīri met sniega bumbas spēlētāja virzienā.
* Metieni nedaudz paredz spēlētāja kustību.
* Katram sniegavīram ir nejaušs 3 līdz 5 sekunžu cooldown.
* Sniegavīri nemet sniega bumbas vienlaicīgi.
* Sniega bumbas pēc noteikta laika automātiski pazūd.
* Sniega bumba pēc trāpījuma spēlētājam tiek iznīcināta.

## Ātruma boost

* Boost tiek aktivizēts ar **Space**.
* Boost īslaicīgi palielina spēlētāja ātrumu.
* Pēc izmantošanas sākas cooldown.
* Boost laikā tiek izmantoti particle efekti.
* Tiek atskaņota boost skaņa.

Boost statusa krāsas:

* **Zaļš** - boost ir gatavs.
* **Oranžs** - boost tiek izmantots.
* **Sarkans** - boost atjaunojas.

## Skaņas sistēma

Projektā ir pievienotas šādas skaņas:

* Slēpošanas skaņa.
* Boost skaņa.
* Sadursmes skaņa.
* Nepareizi izbraukta karodziņa skaņa.
* Finiša skaņa.

Slēpošanas skaņa tiek apturēta, kad spēlētājs atrodas gaisā vai sacensība ir pabeigta.

## Lietotāja interfeiss

UI parāda:

* Pašreizējo sacensību laiku.
* Labāko laiku.
* Boost statusu.
* Soda paziņojumu.
* Gala rezultātu.
* Piecus labākos rezultātus.
* Restartēšanas pogu.
* Nākamā līmeņa pogu.
* Spēles aizvēršanas pogu.
* Iziešanas apstiprinājuma logu.

## Līderu saraksts

* Katrā līmenī ir atsevišķs līderu saraksts.
* Level 1 un Level 2 rezultāti netiek sajaukti.
* Katrā sarakstā tiek saglabāti pieci labākie laiki.
* Rezultāti tiek sakārtoti no ātrākā līdz lēnākajam.
* Katram līmenim ir savs labākā laika ieraksts.
* Dati tiek saglabāti ar `PlayerPrefs`.

## Veiktspējas optimizācija

Projektā tika veikti vairāki optimizācijas uzlabojumi:

* Izdzēsts testa skripts, kas katrā kadrā veica ļoti daudz `GameObject.Find` izsaukumu.
* Sniegavīri vairs nemet sniega bumbas bez pārtraukuma.
* Sniega bumbas pēc noteikta laika tiek iznīcinātas.
* Vides objekti ir atzīmēti kā `Static`.
* No Level 2 izņemti bojāti un nevajadzīgi objekti.
* Samazināta nevajadzīgu objektu uzkrāšanās spēles laikā.

## Kā spēlēt

1. Galvenajā izvēlnē nospied **Play**.
2. Brauc lejup pa trasi.
3. Izbrauc cauri starta zonai, lai sāktu taimeri.
4. Brauc garām karodziņiem pa pareizo pusi.
5. Izvairies no kokiem, akmeņiem, sniegavīriem un sniega bumbām.
6. Izmanto boost, lai īslaicīgi palielinātu ātrumu.
7. Izbrauc cauri finiša zonai.
8. Apskati gala laiku un līderu sarakstu.
9. Restartē līmeni vai turpini uz nākamo līmeni.

## Izmantotās Unity sistēmas

* `Rigidbody` spēlētāja un sniega bumbu fizikai.
* `Collider` un `Trigger` sadursmēm un sacensību zonām.
* `Physics.Raycast` zemes pārbaudei.
* `PlayerPrefs` rezultātu saglabāšanai.
* `TextMeshPro` UI tekstiem.
* `AudioSource` skaņas efektiem.
* `Particle System` boost efektam.
* `SceneManager` līmeņu pārslēgšanai.
* Unity Input System UI pogu ievadei.
* Cinemachine kameras sekošanai spēlētājam.
* Unity Profiler veiktspējas pārbaudei.
