# Ski Game

## Apraksts

Šis projekts ir 3D slēpošanas spēles prototips Unity vidē. Spēlētājs kontrolē slēpotāju, brauc lejā pa trasi, izvairās no šķēršļiem, izbrauc cauri starta un finiša zonām, kā arī mēģina iegūt pēc iespējas labāku laiku.

Spēlē ir izveidota trase ar kokiem, akmeņiem, sniegavīriem, karodziņiem, lēcienu un UI sistēmu, kas parāda laiku, labāko rezultātu un līderu sarakstu.

## Controls

* **A** — pagriezties pa kreisi
* **D** — pagriezties pa labi
* **Space** — īslaicīgs ātruma boost

## Galvenās funkcijas

* Izveidots slēpošanas līmenis ar trasi, dekorācijām, šķēršļiem un karodziņiem.
* Spēlētājs pārvietojas, izmantojot `Rigidbody`, nevis tiešu `transform` pārvietošanu.
* Slēpotājs var pagriezties pa kreisi un pa labi, bet nevar pārsniegt 90 grādu pagrieziena robežu.
* Spēlētāja ātrums mainās atkarībā no tā, cik taisni viņš brauc lejā pa nogāzi.
* Kad spēlētājs ir gaisā, slēpošanas skaņa apstājas.
* Starta zona sāk sacensību taimeri.
* Finiša zona aptur taimeri un parāda gala rezultātu.
* Ja spēlētājs izbrauc garām karodziņam pa nepareizo pusi, laikam tiek pievienota +1 sekunde.
* UI parāda pašreizējo laiku, labāko laiku, soda paziņojumu un finiša ekrānu.
* Labākais laiks tiek saglabāts ar `PlayerPrefs`.
* Līderu saraksts saglabā un parāda 5 labākos laikus.
* Finiša ekrānā ir pogas, lai restartētu līmeni vai izietu no spēles.

## Šķēršļi

Spēlē ir vairāki šķēršļu tipi:

* **Akmeņi** — ja spēlētājs tiem uzbrauc, spēlētājs tiek atsists atpakaļ.
* **Koki** — darbojas kā šķēršļi un izraisa knockback efektu.
* **Sniegavīri** — pēc sadursmes pazūd no spēles.
* Sadursmes laikā tiek atskaņota skaņa, lai spēlētājs skaidrāk saprastu, ka ir notikusi kļūda.

## Bonus funkcijas

Projektā tika pievienoti vairāki uzlabojumi:

* Ātruma boost ar **Space** pogu.
* Boost nevar izmantot nepārtraukti, jo tam ir cooldown.
* Boost statusa teksts maina krāsu:

  * zaļš — boost ir gatavs;
  * oranžs — boost tiek izmantots;
  * sarkans — boost atjaunojas.
* Boost laikā tiek izmantoti particle efekti.
* Pievienota slēpošanas skaņa, kas skan tikai tad, kad spēlētājs slēpo pa zemi.
* Ja spēlētājs ir gaisā pēc lēciena, slēpošanas skaņa apstājas.
* Pievienots soda paziņojums UI, kad spēlētājs izbrauc nepareizo karodziņa pusi.
* Pievienotas skaņas sadursmēm ar šķēršļiem.

## Līderu saraksts

Līderu saraksts saglabā 5 labākos rezultātus vienam līmenim. Kad spēlētājs pabeidz sacensību, viņa gala laiks tiek pievienots sarakstam. Rezultāti tiek sakārtoti no labākā līdz sliktākajam laikam.

Dati tiek saglabāti ar `PlayerPrefs`, tāpēc rezultāti nepazūd pēc spēles restartēšanas.

## Kā spēlēt

1. Sāc spēli un brauc lejā pa trasi.
2. Izbrauc cauri starta karodziņiem, lai sāktu taimeri.
3. Brauc garām krāsainajiem karodziņiem pa pareizo pusi.
4. Izvairies no akmeņiem, kokiem un sniegavīriem.
5. Izmanto boost, lai īslaicīgi palielinātu ātrumu.
6. Izbrauc cauri finiša zonai, lai pabeigtu sacensību.
7. Pēc finiša apskati savu gala laiku un līderu sarakstu.

## Izmantotās Unity sistēmas

* `Rigidbody` spēlētāja kustībai
* `Collider` un `Trigger` zonas sadursmēm un karodziņiem
* `PlayerPrefs` rezultātu saglabāšanai
* `TextMeshPro` UI tekstiem
* `AudioSource` skaņas efektiem
* `Particle System` boost efektam
* `SceneManager` līmeņa restartēšanai un spēles izvēlnes pogām

## Build informācija

Projekts paredzēts kā Windows build.

Iesniegumā iekļauts:

* Windows spēles build
* Git projekta links
* README fails ar projekta aprakstu
