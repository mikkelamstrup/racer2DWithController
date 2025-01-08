# Racer2DWithController

**Racer2DWithController** er et projekt, der kombinerer Unity med en ekstern ESP32-mikrocontroller 
for at skabe en interaktiv 2D-racerbil-simulator. 
Simulatoren giver brugeren mulighed for at styre en bil i Unity ved hjælp af potentiometer og knapper, 
der er forbundet til ESP32.

---

## Indhold

Projektet indeholder følgende filer:

### **1. CarController.cs**
Unity-script, der styrer bilens bevægelse i spillet. 

- **Funktionalitet:**
  - Styrer bilens fremadgående eller baglæns bevægelse.
  - Håndterer bilens rotation baseret på brugerinput.
- **Metoder:**
  - `SetMoveInput(float input)`: Modtager input til fremadgående/baglæns bevægelse.
  - `SetRotationInput(float input)`: Modtager input til bilens rotation.

### **2. SerialController.cs**
Unity-script, der håndterer seriel kommunikation mellem Unity og ESP32.

- **Funktionalitet:**
  - Modtager data fra ESP32 via den serielle port.
  - Behandler data og oversætter dem til input til bilen i Unity.
- **Konfigurationsparametre:**
  - `portName`: Portnavn for den serielle forbindelse (f.eks. `/dev/cu.usbserial-0001` på mac eller `COM3` på windows).
  - `baudRate`: Baudrate for den serielle forbindelse (skal matche ESP32's firmware).

### **3. controller.cpp**
Arduino-skript (firmware) til ESP32, der indsamler data fra sensorer og sender dem via seriel kommunikation.

- **Hardwarekomponenter:**
  - Potentiometer til at styre bilens retning.
  - To knapper: én til fremad og én til baglæns bevægelse.
- **Funktionalitet:**
  - Læser analog data fra potentiometeret.
  - Læser knapinput med debounce for præcision.
  - Sender data i formatet `potValue,forward,backward` til Unity via seriel kommunikation.

---

## Installation

### **Krav**
1. Unity (testet med version 2021.3 eller nyere).
2. ESP32-board og Arduino IDE (med ESP32-tilføjelser installeret).
3. En computer med en seriel port til at forbinde ESP32.

### **Opsætning**

1. **ESP32 Firmware:**
   - Upload `controller.cpp` til ESP32 ved hjælp af Arduino IDE.
   - Sørg for at tilpasse `potPin`, `forwardButton`, og `backwardButton` til din hardwareopsætning.
   - Juster portindstillinger og baudrate i Arduino IDE, så de matcher dit setup.

2. **Unity Projekt:**
   - Tilføj `CarController.cs` og `SerialController.cs` til et GameObject i Unity.
   - Konfigurer `SerialController` til at bruge den korrekte port (`portName`) og baudrate (`baudRate`).
   - Link `SerialController` til en instans af `CarController`.

---

## Brug

1. Tilslut ESP32 til computeren.
2. Start Unity-projektet i Play-mode.
3. Brug potentiometeret og knapperne på ESP32 til at kontrollere bilen i Unity:
   - Drej potentiometeret for at ændre bilens retning.
   - Tryk på "fremad"- eller "baglæns"-knappen for at bevæge bilen.

---

## Fejlfinding

- **Unity kan ikke finde ESP32:**
  - Kontrollér, at `portName` er korrekt konfigureret i `SerialController.cs`.
  - Sørg for, at ESP32 er korrekt tilsluttet, og at porten ikke er brugt af andre programmer.

- **Ingen bevægelse i bilen:**
  - Sørg for, at ESP32 sender data i det forventede format: `potValue,forward,backward`.
  - Kontrollér, at forbindelsen mellem `SerialController` og `CarController` er korrekt opsat.

---

## Fremtidige Forbedringer

- Tilføj support til flere input-enheder.
- Implementer trådløs kommunikation (via Bluetooth eller Wi-Fi).
- Udvid simulatoren med baner.

---

## Bidrag

Bidrag er velkomne! Fork projektet, lav ændringer og send en pull request.

---

## Licens

Dette projekt er under [MIT-licensen](https://opensource.org/licenses/MIT).