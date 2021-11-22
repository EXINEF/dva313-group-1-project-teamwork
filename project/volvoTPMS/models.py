from django.db import models
from django.contrib.auth.models import User

# Create your models here.
class Sensor(models.Model):
    id = models.CharField(max_length=50, primary_key=True)
    temperature = models.IntegerField(blank=True, null=True)
    pressure = models.IntegerField(blank=True, null=True)
    remaning_battery = models.IntegerField(blank=True, null=True)
    status = models.CharField(max_length=30, null=True)

    def __str__(self):
        return 'Sensor: '+self.id

class Tire(models.Model):
    id = models.CharField(max_length=50, primary_key=True)
    sensor = models.ForeignKey(Sensor, on_delete=models.CASCADE, blank=True, null=True)
    
    remaining_life = models.IntegerField(blank=True, null=True)
    baseline_pressure = models.IntegerField(blank=True, null=True) 
    fill_material = models.CharField(max_length=30, blank=True, null=True)
    tread_depth = models.IntegerField(blank=True, null=True)
    revolutions = models.IntegerField(blank=True, null=True)

    def __str__(self):
        return 'Tire: '+self.id

class Company(models.Model):
    name = models.CharField(max_length=50, blank=True, null=True)
    description = models.TextField(blank=True, null=True)

    fleet_managers = models.ManyToManyField(User, blank=True)

    def __str__(self):
        return self.name
        
class Vehicle(models.Model):
    id = models.CharField(max_length=50, primary_key=True)
    tires = models.ManyToManyField(Tire, blank=True)
    company = models.ForeignKey(Company,on_delete=models.CASCADE,default=None)

    model = models.CharField(max_length=50, blank=True, null=True)
    ambient_temperature = models.IntegerField(blank=True, null=True)
    distance_driven = models.IntegerField(blank=True, null=True)
    machine_hours = models.IntegerField(blank=True, null=True)
    payload = models.FloatField(blank=True, null=True)
    consumed_fuel = models.FloatField(blank=True, null=True)
    weight = models.FloatField(blank=True, null=True)
    machine_hours = models.IntegerField(blank=True, null=True)

    def __str__(self):
        return 'Vehicle model: '+self.model + ' ID: '+self.id
    

    



    


     
    


