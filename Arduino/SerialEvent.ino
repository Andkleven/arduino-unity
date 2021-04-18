const int ledPin = 13;
int incomingByte;
void (*resetFunc)(void) = 0;
bool ledPinOn = LOW;

void setup()
{
  Serial.begin(9600);
  pinMode(ledPin, OUTPUT);

  // Clearly indicate that the device has been reset,
  // as resetFunc() doesn't trigger the usual indication
  for (size_t i = 0; i < 10; i++)
  {
    digitalWrite(ledPin, HIGH);
    delay(50);
    digitalWrite(ledPin, LOW);
    delay(50);
  }
}

void loop()
{
  if (Serial.available() > 0)
  {
    incomingByte = Serial.read();

    switch (incomingByte)
    {
    case 'H':
      ledPinOn = HIGH;
      break;
    case 'L':
      ledPinOn = LOW;
      break;
    case 'R':
      resetFunc();
      break;
    }
  }
  digitalWrite(ledPin, ledPinOn);
  if (ledPinOn) {
    Serial.println("H");
  } else {
    Serial.println("L");
  }
  if (0<millis()%1000 <5) {
    Serial.println(String(millis()/1000));
  }
  
}
