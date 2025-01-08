const int potPin = 34;       // Potentiometer
const int forwardButton = 25; // Fremad-knap
const int backwardButton = 26; // Baglæns-knap

// Funktion til software-debounce
bool debounceRead(int pin) {
    bool state = digitalRead(pin); // Læs første gang
    delay(10);                     // Vent for at eliminere støj
    return digitalRead(pin) == state ? state : !state; // Bekræft stabil tilstand
}

void setup() {
    Serial.begin(9600); // Opsæt seriel kommunikation
    pinMode(forwardButton, INPUT_PULLUP); // Opsæt knap som input med pull-up-modstand
    pinMode(backwardButton, INPUT_PULLUP); // Opsæt knap som input med pull-up-modstand
}

void loop() {
    // Læs potentiometerets værdi
    int potValue = analogRead(potPin);

    // Læs knapper med debounce
    bool forward = debounceRead(forwardButton) == LOW;
    bool backward = debounceRead(backwardButton) == LOW;

    // Prioriter fremad, hvis begge knapper er trykket
    if (forward && backward) {
        backward = false; // Ignorer baglæns, hvis fremad også er trykket
    }

    // Debugging: Vis værdierne i Serial Monitor
    Serial.print(potValue);
    Serial.print(forward);
    Serial.println(backward);

    // Send data som en streng til Serial Monitor
    String output = String(potValue) + "," + String(forward) + "," + String(backward);
    Serial.println(output);

    delay(50); // Send data med 20 Hz
}