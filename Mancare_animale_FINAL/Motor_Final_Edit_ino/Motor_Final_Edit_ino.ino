#include <Servo.h>
#include <SoftwareSerial.h>
#include "Ardunity.h"
#include "GenericServo.h"
#include "GenericTone.h"
#include "DigitalOutput.h"

SoftwareSerial swSerial(10, 11);
GenericServo servo2(2, 2, false);
GenericTone tone1(1, 8);
DigitalOutput dOutput0(0, 13, LOW, true);

void setup()
{
  ArdunityApp.attachController((ArdunityController*)&servo2);
  ArdunityApp.attachController((ArdunityController*)&tone1);
  ArdunityApp.attachController((ArdunityController*)&dOutput0);
  ArdunityApp.resolution(256, 1024);
  ArdunityApp.timeout(5000);
  swSerial.begin(9600);
  ArdunityApp.begin((Stream*)&swSerial);
}

void loop()
{
  ArdunityApp.process();
}
