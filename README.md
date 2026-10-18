# SupportDesk

SupportDesk är en skrivbordsapplikation för att registrera och hantera kunders supportärenden. Applikationen är utvecklad som grupparbete i C# med .Net 10 samt WinUI 3 och sparar information lokalt i JSON-filer.

## Funktioner

- Registrera och visa kunder.
- Skapa och visa supportärenden kopplade till kunder.
- Söka och filtrera supportärenden.
- Ändra ett ärendes prioritet, status och ansvarig handläggare.
- Lägga till och visa kommentarer på ärenden.
- Spara och läsa data lokalt i JSON-format.
//den över eller den under?
- Skapa, redigera och söka bland supportärenden
- Ärenden har status (Nytt, Pågående, Löst), prioritet (Låg, Normal, Hög), handläggare och kommentarer
- Översikt med antal ärenden per status och filter på status
- Snabbkommentar direkt på ärendekortet
- Hantera kunder (skapa, redigera, lista)

## Regler

- Ett ärende måste ha en handläggare innan det kan bli Pågående eller Löst.
- Rubrik och beskrivning får inte vara tomma.


## Teknik och struktur

Projektet använder C# och WinUI. Lösningen är uppdelad i lager:

- **Domain** – centrala modeller och verksamhetsregler.
- **Application** – applikationens tjänster och DTO:er.
- **Infrastructure** – lagring och repository-implementationer.
- **Presentation** – användargränssnitt, vyer och ViewModels.
//den över eller den under?
| Projekt | Innehåll |
|---|---|
| `SupportDesk.Domain` | Entiteter och affärsregler (`SupportTicket`, `TicketComment`) |
| `SupportDesk.Application` | Tjänster och gränssnitt, sök- och filterlogik |
| `SupportDesk.Infrastructure` | Lagring i JSON-filer |
| `SupportDesk.Presentation` | WinUI-gränssnittet (vyer och ViewModels) |
| `SupportDesk.Tests` | Enhetstester (xUnit) |


## Lagring

Kunder och supportärenden sparas lokalt i JSON-filer. I den aktuella implementationen ligger filerna i:

`Dokument\SupportDesk`

Applikationen använder påhittade kunduppgifter och handläggare.
`supporttickets.json` | Alla supportärenden och deras kommentarer
`customers.json` | Alla kunder |


## Komma igång

1. Klona repot.
2. Öppna `SupportDesk.sln` i Visual Studio.
3. Kontrollera att nödvändiga .NET- och Windows-komponenter för projektet är installerade.
4. Välj `SupportDesk.Presentation (Package)` som startprojekt.
5. Bygg lösningen och starta applikationen.

## Tester

Enhetstesterna körs i Test Explorer eller med:
```bash
dotnet test

Testerna kontrollerar bland annat att ett nytt ärende får status Nytt, att tom rubrik avvisas, att ett ärende utan handläggare inte kan bli Pågående, att en tillåten statusändring fungerar, att sökningen ignorerar stora och små bokstäver och att statusfiltret bara visar vald status.

Senaste körning: 24 av 24 tester godkända. //Uppdatera antal och datum

## Utvecklare

- Simon Öbom
- Leon Jonsson
- Dennis Brofalk