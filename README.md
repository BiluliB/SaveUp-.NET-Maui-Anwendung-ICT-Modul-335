# SaveUp

## Haftungsausschluss

Bitte beachten Sie, dass das Frontend für die SaveUp Anwendung derzeit in Entwicklung ist und möglicherweise nur teilweise funktionsfähig ist. Es wird daran gearbeitet, die volle Funktionalität so schnell wie möglich bereitzustellen. Danke für Ihr Verständnis.

## Überblick

SaveUp ist eine Anwendung, die speziell für die Verwaltung und Nachverfolgung von Einsparungen entwickelt wurde. Der Hauptfokus liegt auf dem Frontend, wobei das Backend als unterstützende Komponente dient. Dieses Projekt umfasst die Anwendungsentwicklung und Integrationstests, um eine effiziente Verwaltung der Einsparungen zu gewährleisten.

## Hauptfunktionen

- **Verwaltung von Einsparungen:** Funktionen zur Erstellung, Abrufung und Verwaltung von Einsparungseinträgen.
- **Datenbank-Setup:** Die MongoDB-Datenbank wird leer erstellt und beim Eintragen automatisch aufgebaut.

### Technologie-Stack

- **ASP.NET Core** für die Backend-API.
- **MongoDB NoSQL** für das Datenbankmanagement.
- **.NET MAUI** für das plattformübergreifende Frontend.

## Einrichtung und Installation der SaveUp Anwendung

### Voraussetzungen:

- Installieren Sie das .NET 8.0 SDK von der [offiziellen .NET-Website](https://dotnet.microsoft.com/download/dotnet/8.0).
- Installieren Sie MongoDB von der [offiziellen MongoDB-Website](https://www.mongodb.com/try/download/community) auf Ihrem System.
- Ein geeigneter Code-Editor, wie [Visual Studio](https://visualstudio.microsoft.com/vs/) oder [Visual Studio Code](https://code.visualstudio.com/).

### Klonen oder Herunterladen des Projekts:

1. Öffnen Sie Ihre Kommandozeile oder Ihr Terminal.
2. Navigieren Sie zu dem Verzeichnis, in dem Sie das Projekt speichern möchten.
3. Führen Sie den folgenden Befehl aus, um das Repository zu klonen:
   ```sh
   git clone https://github.com/BiluliB/SaveUp.git
   ```
4. Navigieren Sie in das geklonte Verzeichnis:
   ```sh
   cd SaveUp
   ```

### Abhängigkeiten installieren:

1. Öffnen Sie die Kommandozeile oder das Terminal im Projektverzeichnis.
2. Führen Sie den folgenden Befehl aus, um die benötigten NuGet-Pakete zu installieren:
   ```sh
   dotnet restore
   ```

### Datenbankkonfiguration:

Befolgen Sie diese Schritte, um die MongoDB-Datenbank korrekt einzurichten und zu konfigurieren:

1. **Leere Datenbank:** Die MongoDB-Datenbank wird leer erstellt und beim Eintragen automatisch aufgebaut.

### Syncfusion DatePicker Lizenzkey

Erstelle eine `syncfusion_license.txt` Datei und speichere sie im `Resources` Ordner des SaveUp .NET Maui Projekt. Füge den Lizenzschlüssel in die Datei ein.

Der Lizenzschlüssel kann bei https://www.syncfusion.com/ angefordert werden.

### Starten der Anwendung:

1. Starten Sie MongoDB:
   - Unter Windows:
     ```sh
     net start MongoDB
     ```
   - Unter macOS und Linux:
     ```sh
     brew services start mongodb/brew/mongodb-community
     ```
2. Starten Sie die Anwendung über Ihre Entwicklungsumgebung (Visual Studio oder Visual Studio Code):
   - Rechtsklick auf das Projekt in der Solution Explorer und wählen Sie `Debug` -> `Start Without Debugging`.

## Testen der SaveUp Anwendung:

### Testen mit Swagger:

- Swagger-Integration: Die API umfasst Swagger zum Testen von Endpunkten über eine benutzerfreundliche Oberfläche.
- Zugriff auf Swagger: Starten Sie die API und navigieren Sie zur Swagger-UI-URL, um die API zu testen und die Dokumentation anzusehen.

### API-Endpunkte

Die API bietet Endpunkte für die Verwaltung von Einsparungen. Diese können mit Postman oder Swagger getestet werden, wie im Repository bereitgestellt.

- **POST /api/SavedMoney:** Erstellen eines neuen Einsparungseintrags.
- **GET /api/SavedMoney:** Abrufen aller Einsparungseinträge.
- **GET /api/SavedMoney/{id}:** Abrufen eines spezifischen Einsparungseintrags.
- **GET /api/SavedMoney/today:** Abrufen aller Einsparungseinträge von heute.

## Framework- und NuGet-Abhängigkeiten

**Abhängigkeiten des Projekts**

- **Target Framework:** .NET 8.0
- **NuGet-Pakete:**
  - AutoMapper.Extensions.Microsoft.DependencyInjection (Version 12.0.1)
  - Microsoft.AspNetCore.OpenApi (Version 8.0.1)
  - MongoDB.Driver (Version 2.23.1)
  - Newtonsoft.Json (Version 13.0.3)
  - Serilog.AspNetCore (Version 8.0.0)
  - Serilog.Settings.Configuration (Version 8.0.0)
  - Serilog.Sinks.File (Version 5.0.0)
  - Swashbuckle.AspNetCore (Version 6.5.0)
  - System.IdentityModel.Tokens.Jwt (Version 7.2.0)
  - Microsoft.Maui.Controls (Version abhängig von MauiVersion)
  - Microsoft.Maui.Controls.Compatibility (Version abhängig von MauiVersion)
  - Microsoft.Extensions.Logging.Debug (Version 8.0.0)
  - SkiaSharp.Svg (Version 1.60.0)
  - Svg (Version 3.4.7)
  - Syncfusion.Maui.Calendar (Version 26.1.35)

### Hinweise

- Stellen Sie sicher, dass alle NuGet-Pakete auf die neueste kompatible Version aktualisiert sind.
- Überprüfen Sie die Kompatibilität der NuGet-Pakete mit dem .NET 8.0 Framework.

Dieses README bietet einen Überblick über die SaveUp Anwendung, einschließlich Einrichtung, Funktionen, Testen und weiteren wesentlichen Aspekten.

### Mitwirkende

Die Entwicklung der SaveUp App wurde von:

- **BiluliB** - Entwickler und Projektleiter

erstellt.
