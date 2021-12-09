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

class Sensor(models.Model):
    ALL_SENSOR_STATUS = (
        ('WORKING', 'WORKING'),
        ('WARNING', 'WARNING'),
        ('DANGER', 'DANGER'),
        ('LOST', 'LOST'),
        ('BROKEN', 'BROKEN'),
    )
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
        return 'Location:%d LAT:%f LONG:%f' % (self.id, self.latitude, self.longitude)
        
class Vehicle(models.Model):
    id = models.CharField(max_length=50, primary_key=True)

    model = models.CharField(max_length=50, blank=True) 
    ambient_temperature = models.FloatField(blank=True, null=True, default=0)
    consumed_fuel = models.FloatField(blank=True, null=True, default=0)
    distance_driven_empty = models.FloatField(blank=True, null=True, default=0)
    distance_driven_loaded = models.FloatField(blank=True, null=True, default=0)
    machine_hours_empty = models.IntegerField(blank=True, null=True, default=0)
    machine_hours_loaded = models.IntegerField(blank=True, null=True, default=0)
    payload_empty = models.FloatField(blank=True, null=True, default=0)
    payload_loaded = models.FloatField(blank=True, null=True, default=0)
    creation_datetime = models.DateTimeField(auto_now_add=True, blank=True, null=True)

    tires = models.ManyToManyField(Tire, blank=True)
    locations = models.ManyToManyField(Location, blank=True)
    company = models.ForeignKey(Company, on_delete=models.DO_NOTHING, blank=True, null=True)

    def __str__(self):
        return 'Vehicle model: '+ self.model + ' ID: '+ self.id

class CompanyAdministrator(models.Model):
    user = models.OneToOneField(User, null=True, on_delete=models.CASCADE)
    first_name = models.CharField(max_length=50, blank=True) 
    last_name = models.CharField(max_length=50, blank=True) 
    email = models.CharField(max_length=50, blank=True)
    phone = models.CharField(max_length=50, blank=True) 
    creation_datetime = models.DateTimeField(auto_now_add=True, blank=True, null=True)

    company = models.ForeignKey(Company, on_delete=models.DO_NOTHING, blank=True, null=True)
    def __str__(self):
        return 'Company Administrator: %s %s' % (self.first_name, self.last_name)       

class FleetManager(models.Model):
    user = models.OneToOneField(User, null=True, on_delete=models.CASCADE)
    first_name = models.CharField(max_length=50, blank=True) 
    last_name = models.CharField(max_length=50, blank=True)
    email = models.CharField(max_length=50, blank=True) 
    phone = models.CharField(max_length=50, blank=True) 
    creation_datetime = models.DateTimeField(auto_now_add=True, blank=True, null=True)

    company = models.ForeignKey(Company, on_delete=models.DO_NOTHING, blank=True, null=True)
    def __str__(self):
        return 'Fleet Manager: %s %s' % (self.first_name, self.last_name)      


        


    

    



    


     
    


