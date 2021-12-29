from django.db import models
from django.contrib.auth.models import User

from .algorithms import attentionValueCalculator


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
    is_used = models.BooleanField(default=False, blank = True, null=True)
    creation_datetime = models.DateTimeField(auto_now_add=True, blank=True, null=True)

    company = models.ForeignKey(Company, on_delete=models.DO_NOTHING, blank=True, null=True)

    def __str__(self):
        return 'ID:%s used:%s by %s'%(self.id,self.is_used,self.company)

class Tire(models.Model):
    id = models.CharField(max_length=50, primary_key=True)
    remaining_life = models.FloatField(blank=True, null=True)
    baseline_pressure = models.FloatField(blank=True, null=True) 
    fill_material = models.CharField(max_length=30, blank=True, null=True)
    tread_depth = models.FloatField(blank=True, null=True)
    revolutions = models.FloatField(blank=True, null=True)
    is_used = models.BooleanField(default=False, blank = True, null=True)
    creation_datetime = models.DateTimeField(auto_now_add=True, blank=True, null=True)
    
    sensor = models.OneToOneField(Sensor, blank=True, null=True, on_delete=models.DO_NOTHING)
    company = models.ForeignKey(Company, on_delete=models.DO_NOTHING, blank=True, null=True)

    """
        # TODO we need something to save the history of the previous vehicle that used this tire
    
        we need Tire ID, VehicleID, STATUS(USED, NOT_USED), (same parameter that describe how muche the wheel was used, maybe remaining life or else)
        
        EXAMPLE: history = models.ManyToManyField(TireVehicleHistory, ect...)
        
        class TireVehicleHistory(models.Model):
            tire = models.ForeignKey(Tire, etc...)
            status = (USED, NOT_USED)
            vehicle = models.ForeignKey(Vehicle, etc...)
            1-parameters to save for see the deterioration of the wheel
            ...
            ...
            n-parameters   

    """
    
    def __str__(self):
        return 'ID:%s used:%s by %s'%(self.id,self.is_used,self.company)

class Location(models.Model):
    latitude = models.FloatField()
    longitude = models.FloatField()
    creation_datetime = models.DateTimeField(auto_now_add=True, blank=True, null=True)

    def __str__(self):
        return 'Location:%d LAT:%f LONG:%f' % (self.id, self.latitude, self.longitude)

# rapresents a Wheel Loader (cause have 4 wheels)       
class Vehicle(models.Model):
    SPECC_TYPE = (
        ('NEUTRAL', 'NEUTRAL'),
        ('OVER', 'OVER'),
        ('UNDER', 'UNDER'),
    )
    id = models.CharField(max_length=50, primary_key=True)
    model = models.CharField(max_length=50, blank=True) 
    ambient_temperature = models.FloatField(blank=True, null=True, default=0)
    consumed_fuel = models.FloatField(blank=True, null=True, default=0)
    distance_driven_empty = models.FloatField(blank=True, null=True, default=0)
    distance_driven_loaded = models.FloatField(blank=True, null=True, default=0)
    machine_hours_empty = models.IntegerField(blank=True, null=True, default=0)
    machine_hours_loaded = models.IntegerField(blank=True, null=True, default=0)
    buckets = models.IntegerField(blank=True, null=True, default=0)
    payload = models.FloatField(blank=True, null=True, default=0)
    creation_datetime = models.DateTimeField(auto_now_add=True, blank=True, null=True)
    tkph = models.FloatField(blank=True, null=True, default=0) #This is the TKPH that is precalculated. This attribute should be set on tires in the future.
    weight = models.FloatField(blank=True, null=True, default=0)

    tire_left_front = models.OneToOneField(Tire, related_name='tire_left_front', blank=True, null=True, on_delete=models.DO_NOTHING)
    tire_left_rear = models.OneToOneField(Tire, related_name='tire_left_rear', blank=True, null=True, on_delete=models.DO_NOTHING)
    tire_right_front = models.OneToOneField(Tire, related_name='tire_right_front',  blank=True, null=True, on_delete=models.DO_NOTHING)
    tire_right_rear = models.OneToOneField(Tire, related_name='tire_right_rear',  blank=True, null=True, on_delete=models.DO_NOTHING)

    tire_specc = models.CharField(max_length=30, blank=True, null=True, choices=SPECC_TYPE)
    locations = models.ManyToManyField(Location, blank=True)
    company = models.ForeignKey(Company, on_delete=models.DO_NOTHING, blank=True, null=True)

    def __str__(self):
        return 'ID:%s model:%s by %s STATUS:%s'%(self.id,self.model,self.company,self.getStatus())
    
    def getStatus(self):
        if self.tire_left_front.sensor.remaining_battery < 10:
            return 'DANGER'
        if self.tire_left_rear.sensor.remaining_battery < 10:
            return 'DANGER'
        if self.tire_right_front.sensor.remaining_battery < 10:
            return 'DANGER'
        if self.tire_right_rear.sensor.remaining_battery < 10:
            return 'DANGER'
        if self.tire_left_front.sensor.remaining_battery < 20:
            return 'WARNING'
        if self.tire_left_rear.sensor.remaining_battery < 20:
            return 'WARNING'
        if self.tire_right_front.sensor.remaining_battery < 20:
            return 'WARNING'
        if self.tire_right_rear.sensor.remaining_battery < 20:
            return 'WARNING'
        if self.tire_specc != 'NEUTRAL':
            return 'WARNING'
        return 'OK'

    def getType(self):
        return 'Wheel Loader'

    def setAllTiresUsed(self):
        self.tire_left_front.is_used = True
        self.tire_left_rear.is_used = True
        self.tire_right_front.is_used = True
        self.tire_right_rear.is_used = True
        self.save()

class CompanyAdministrator(models.Model):
    user = models.OneToOneField(User, on_delete=models.CASCADE)
    phone = models.CharField(max_length=50, blank=True, null=True) 
    creation_datetime = models.DateTimeField(auto_now_add=True, blank=True, null=True)

    company = models.ForeignKey(Company, on_delete=models.DO_NOTHING, blank=True, null=True)
    
    def __str__(self):
        return 'Company Administrator: %s ( %s %s ) COMPANY: %s' % (self.user.username, self.user.first_name, self.user.last_name, self.company)       

class FleetManager(models.Model):
    HOME_VIEW = (
        ('SIMPLE', 'SIMPLE'),
        ('EXTENDED', 'EXTENDED'),
    )
    user = models.OneToOneField(User, on_delete=models.CASCADE)
    phone = models.CharField(max_length=50, blank=True, null=True) 
    home_view = models.CharField(max_length=30, choices=HOME_VIEW)
    creation_datetime = models.DateTimeField(auto_now_add=True, blank=True, null=True)
    
    company = models.ForeignKey(Company, on_delete=models.DO_NOTHING, blank=True, null=True)

    def __str__(self):
        return 'Fleet Manager: %s ( %s %s ) COMPANY: %s' % (self.user.username, self.user.first_name, self.user.last_name,self.company)      

class K1(models.Model):
    distance = models.IntegerField(primary_key=True)
    value = models.FloatField(blank=True, null=True)
        


    

    



    


     
    


