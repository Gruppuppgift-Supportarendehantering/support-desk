# SupportDesk

SupportDesk är en skrivbordsapplikation för att registrera och hantera kunders supportärenden. Applikationen är utvecklad som grupparbete i C# med .NET 10 samt WinUI 3 och sparar information lokalt i JSON-filer.

## Funktioner

- Registrera och visa kunder.
- Skapa och visa supportärenden kopplade till kunder.
- Söka och filtrera supportärenden.
- Ändra ett ärendes prioritet, status och ansvarig handläggare.
- Lägga till och visa kommentarer på ärenden, antingen på redigeringssidan eller direkt på ärendekortet.
- Översikt med antal ärenden per status och filter på status.
- Spara och läsa data lokalt i JSON-format.

## Regler

- En kund måste ha ett giltigt namn och en giltig e-postadress. Fälten kan inte vara tomma.
- Ett ärende måste kopplas med en kund vid skapandet.
- Ett ärende måste ha en handläggare innan det kan få status Pågående eller Löst.
- Rubrik och beskrivning får inte vara tomma.


## Teknik och struktur

Projektet använder C# och WinUI. Lösningen är uppdelad i lagren Presentation, Application, Infrastructure och Domain enligt DDD-struktur:

- **Presentation** tar emot användarens inmatning och anropar relevant service i Application.
- **Application** samordnar arbetet, exempelvis att skapa ett objekt och be relevant repository att spara det.
- **Infrastructure** innehåller implementationen av repositoryt, som serialiserar objekten till JSON och sparar dem i en fil. Vid hämtning läser repositoryt filen och deserialiserar JSON-datan tillbaka till objekt.
- **Domain** innehåller de centrala modellerna och deras affärsregler. Dessa objekt används när Application utför sitt arbete.

**Anropsflöde vid hämtning:**

**Presentation** -> **Application** -> **Repository** -> **JSON**

Repositoryt läser filen och omvandlar JSON-datan till objekt. Objekten returneras till Application, som skickar resultatet vidare till Presentation för att visas i gränssnittet.



## Lagring

Kunder och supportärenden sparas lokalt i JSON-filer. I den aktuella implementationen ligger filerna i:

`Dokument\SupportDesk`

`supporttickets.json` - supportärenden och deras kommentarer.
`customers.json` - kunder.

## AI-använding

Vi har använt oss av både ChatGPT och Claude som ett bollplank och felsökning under projekts gång. Koden har alltid granskats och begripits innan implementation. 


## Komma igång

1. Klona repot.
2. Öppna `support-desk.slnx` i Visual Studio.
3. Kontrollera att nödvändiga .NET- och Windows-komponenter för projektet är installerade.
4. Välj `SupportDesk.Presentation` som startprojekt.
5. Bygg lösningen och starta applikationen.

**Via terminalen när du befinner dig i rotmappen**

```powershell
dotnet run --project .\SupportDesk.Presentation\SupportDesk.Presentation.csproj --launch-profile "SupportDesk.Presentation (Unpackaged)" -p:Platform=x64 -p:WindowsPackageType=None
```

## Tester

Enhetstesterna körs i Test Explorer eller med:
```powershell
dotnet test
```

**Testerna kontrollerar bland annat:** 
- att ett nytt ärende får status Nytt 
- att tom rubrik avvisas
- att ett ärende utan handläggare inte kan bli Pågående
- att en tillåten statusändring fungerar
- att sökningen ignorerar stora och små bokstäver
- att statusfiltret bara visar vald status
- att JSON-datan kvarstår under programmet gång
- att JSON-datan inte är korrupt

Senaste körning: 24 av 24 tester godkända.

## Manuell kontroll

1. **Skapa en kund** - går till Kunder, antingen med knappen "Kunder" eller via navigeringsmenyn till vänster. Klickar på plusknappen, skriver in namn och e-post och väljer Lägg till.
2. **Skapa ett ärende** - klickar på "Skapa ett ärende" i ärendeöversikten eller väljer det i menyn till vänster. Skriver rubrik och beskrivning, väljer kund i listan och en prioritet, och klickar på Skapa ärende.
3. **Hantera ärendet** - markerar ärendet i översikten och klickar på "Öppna markerat ärende". Väljer en handläggare och sätter status till Pågående. Utan handläggare visas ett felmeddelande.
4. **Avsluta ärendet** - redigerar ärendet och sätter status till Löst. Statussammanställningen högst upp i översikten visar att ett ärende till är löst.
5. **Övrigt** - kunder kan redigeras om namn eller e-post blivit fel. Kommentarer läggs till antingen med snabbfunktionen på ärendekortet eller med knappen "Lägg till kommentar" i Redigera ärende.

## Utvecklare

- Simon Öbom - kundhantering
- Leon Jonsson - ärenderegistrering
- Dennis Brofalk - ärendehantering

Överblicken gjorde vi tillsammans då vi endast var tre medlemmar.
