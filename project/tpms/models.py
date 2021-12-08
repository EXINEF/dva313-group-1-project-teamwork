from django.db import models
from django.contrib.auth.models import User

class Company(models.Model):
    name = models.CharField(max_length=50, blank=True, null=True)
    description = models.TextField(blank=True, null=True)

    creation_datetime = models.DateTimeField(auto_now_add=True, blank=True, null=True)

    def __str__(self):
        return self.name

    # change name in the admin panel
    class Meta: verbose_name_plural = 'Companies'

ALL_SENSOR_STATUS = (
    ('WORKING', 'WORKING'),
    ('WARNING', 'WARNING'),
    ('DANGER', 'DANGER'),
    ('LOST', 'LOST'),
    ('BROKEN', 'BROKEN'),
)

class Sensor(models.Model):
    id = models.CharField(max_length=50, primary_key=True)
    temperature = models.FloatField(blank=True, null=True)
    pressure = models.FloatField(blank=True, null=True)
    remaning_battery = models.FloatField(blank=True, null=True)
    status = models.CharField(max_length=30, null=True, default="WORKING", choices=ALL_SENSOR_STATUS)
    creation_datetime = models.DateTimeField(auto_now_add=True, blank=True, null=True)

    company = models.ForeignKey(Company, on_delete=models.DO_NOTHING, blank=True, null=True)

    def __str__(self):
        return 'Sensor: ' + self.id

class Tire(models.Model):
    id = models.CharField(max_length=50, primary_key=True)
    remaining_life = models.FloatField(blank=True, null=True)
    baseline_pressure = models.FloatField(blank=True, null=True) 
    fill_material = models.CharField(max_length=30, blank=True, null=True)
    tread_depth = models.FloatField(blank=True, null=True)
    revolutions = models.FloatField(blank=True, null=True)
    creation_datetime = models.DateTimeField(auto_now_add=True, blank=True, null=True)
    
    sensor = models.ForeignKey(Sensor, on_delete=models.DO_NOTHING, blank=True, null=True)
    company = models.ForeignKey(Company, on_delete=models.DO_NOTHING, blank=True, null=True)

    def __str__(self):
        return 'Tire: '+self.id

class Location(models.Model):
    latitude = models.FloatField(blank=True, null=True)
    longitude = models.FloatField(blank=True, null=True)
    creation_datetime = models.DateTimeField(auto_now_add=True, blank=True, null=True)

    def __str__(self):
        return 'Location: '+self.id
        
class Vehicle(models.Model):
    id = models.CharField(max_length=50, primary_key=True)

    model = models.CharField(max_length=50, blank=True) 
    ambient_temperature = models.FloatField(blank=True, null=True)
    consumed_fuel = models.FloatField(blank=True, null=True)
    distance_driven_empty = models.FloatField(blank=True, null=True)
    distance_driven_loaded = models.FloatField(blank=True, null=True)
    machine_hours_empty = models.IntegerField(blank=True, null=True)
    machine_hours_loaded = models.IntegerField(blank=True, null=True)
    payload_empty = models.FloatField(blank=True, null=True)
    payload_loaded = models.FloatField(blank=True, null=True)
    creation_datetime = models.DateTimeField(auto_now_add=True, blank=True, null=True)

    tires = models.ManyToManyField(Tire, blank=True)
    locations = models.ManyToManyField(Location, blank=True)
    company = models.ForeignKey(Company, on_delete=models.DO_NOTHING, blank=True, null=True)

    def __str__(self):
        return 'Vehicle model: '+ self.model + ' ID: '+ self.id

    def getSensorsTemperatures(self):
        tires = self.tires.all()
        sensors = []
        for tire in tires:
            sensors.append(tire.sensor.temperature)
        return sensors

class CompanyAdministrator(models.Model):
    user = models.OneToOneField(User, null=True, on_delete=models.CASCADE)
    creation_datetime = models.DateTimeField(auto_now_add=True, blank=True, null=True)

    company = models.ForeignKey(Company, on_delete=models.DO_NOTHING, blank=True, null=True)
    def __str__(self):
        return 'Company Administrator: ' + self.name       

class FleetManager(models.Model):
    user = models.OneToOneField(User, null=True, on_delete=models.CASCADE)
    creation_datetime = models.DateTimeField(auto_now_add=True, blank=True, null=True)

    company = models.ForeignKey(Company, on_delete=models.DO_NOTHING, blank=True, null=True)
    def __str__(self):
        return 'Fleet Manager: ' + self.name


        


    

    



    


     
    


