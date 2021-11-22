from django.db import models

# Create your models here.
class Sensor(models.Model):
    temperature = models.IntegerField(blank=True, null=True)
    pressure = models.IntegerField(blank=True, null=True)
    remaning_battery = models.IntegerField(blank=True, null=True)
    status = models.CharField(max_length=30, null=True)

class Tire(models.Model):
    remaining_life = models.IntegerField(blank=True, null=True)
    baseline_pressure = models.IntegerField(blank=True, null=True) 
    sensor = models.ForeignKey(Sensor, on_delete=models.CASCADE, blank=True, null=True)
    fill_material = models.CharField(max_length=30, blank=True, null=True)
    tread_depth = models.IntegerField(blank=True, null=True)
    revolutions = models.IntegerField(blank=True, null=True)
    
class Vehicle(models.Model):
    model = models.CharField(max_length=50, blank=True, null=True)
    ambient_temperature = models.IntegerField(blank=True, null=True)
    distance_driven = models.IntegerField(blank=True, null=True)
    machine_hours = models.IntegerField(blank=True, null=True)
    payload = models.FloatField(blank=True, null=True)
    consumed_fuel = models.FloatField(blank=True, null=True)
    weight = models.FloatField(blank=True, null=True)
    machine_hours = models.IntegerField(blank=True, null=True)
    tires = models.ManyToManyField(Tire, blank=True, null=True)
    


     
    


