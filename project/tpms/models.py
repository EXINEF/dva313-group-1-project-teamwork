from django.db import models
from django.contrib.auth.models import User

from datetime import datetime, timedelta, timezone
from django.db.models import Q

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
        ('LOST', 'LOST'),
        ('BROKEN', 'BROKEN'),
    )

    id = models.CharField(max_length=50, primary_key=True)
    temperature = models.FloatField(blank=True, null=True, default=0)
    pressure = models.FloatField(blank=True, null=True, default=0)
    status = models.CharField(max_length=30, null=True, default="WORKING", choices=ALL_SENSOR_STATUS)
    is_used = models.BooleanField(default=False, blank = True, null=True)
    creation_datetime = models.DateTimeField(auto_now_add=True, blank=True, null=True)

    company = models.ForeignKey(Company, on_delete=models.DO_NOTHING, blank=True, null=True)

    def __str__(self):
        return 'ID:%s used:%s by %s'%(self.id,self.is_used,self.company)

    def getStatus(self):
        status = 'OK'
        problems = ''
        if self.temperature > 75:
            status = 'WARNING'
            problems += 'Sensor Temperature is High\n'
        if self.getRemaningBattery() < 20:
            status = 'WARNING'
            problems += 'Sensor Battery is Low\n'    
        if self.status != 'WORKING':
            status = 'DANGER'
            problems += 'Sensor is not WORKING\n'
        if self.temperature > 80:
            status = 'DANGER'
            problems += 'Sensor Temperature is Very High\n'
        if self.getRemaningBattery() < 10:
            status = 'DANGER'
            problems += 'Sensor Battery is Very Low\n'
        return status, problems

    def getStatusStatus(self):
        return self.getStatus()[0]
    
    def getStatusMotivation(self):
        return self.getStatus()[1]

    def getRemaningBattery(self):
        delta = datetime.now(timezone.utc) - self.creation_datetime
        days = delta.days
        if(days >+ 730):
            return 0
        return int((1 - (days / 730)) * 100)

    def getExpiredDate(self):
        return self.creation_datetime + timedelta(weeks=52*3)

    def getTireThatMountIt(self):
        tire = Tire.objects.get(sensor=self)
        print(tire)
        if tire is not None:    
            return tire

class Tire(models.Model):
    id = models.CharField(max_length=50, primary_key=True)
    remaining_life = models.FloatField(blank=True, null=True, default=0)
    baseline_pressure = models.FloatField(blank=True, null=True, default=0) 
    fill_material = models.CharField(max_length=30, blank=True, null=True)
    tread_depth = models.FloatField(blank=True, null=True, default=0)
    revolutions = models.FloatField(blank=True, null=True, default=0)
    is_used = models.BooleanField(default=False, blank = True, null=True)
    creation_datetime = models.DateTimeField(auto_now_add=True, blank=True, null=True)
    
    sensor = models.OneToOneField(Sensor, blank=True, null=True, on_delete=models.SET_NULL)
    company = models.ForeignKey(Company, on_delete=models.DO_NOTHING, blank=True, null=True)

    def getStatus(self):
        status = 'OK'
        problems = ''
        if self.sensor is None:
            status = 'DANGER'
            problems += 'Tire has no sensor\n'
            return status, problems
        pressurePercentage = self.getPressurePercentage()
        if pressurePercentage < 90 and pressurePercentage >= 80:
            status = 'WARNING'
            problems += 'Tire pressure is Low\n'
        if pressurePercentage > 125 and pressurePercentage <= 140:
            status = 'WARNING'
            problems += 'Tire pressure is High\n'
        if pressurePercentage < 80:
            status = 'DANGER'
            problems += 'Tire pressure is Very Low\n'
        if pressurePercentage > 140:
            status = 'DANGER'
            problems += 'Tire pressure is Very High\n'
        return status, problems

    def getStatusStatus(self):
        return self.getStatus()[0]
    
    def getStatusMotivation(self):
        return self.getStatus()[1]

    # ((pressure - baselinePressure) / baselinePressure) + 100
    def getPressurePercentage(self):
        if self.baseline_pressure == 0:
            return 0
        return ((self.sensor.pressure - self.baseline_pressure) / self.baseline_pressure) + 100

    def __str__(self):
        return 'ID:%s used:%s by %s'%(self.id,self.is_used,self.company)

    def getVehicleThatMountIt(self):
        vehicle = Vehicle.objects.get(Q(tire_left_front=self) |Q(tire_left_rear=self) |Q(tire_right_front=self) |Q(tire_right_rear=self))
        if vehicle is not None:    
            return vehicle

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

    tire_left_front = models.OneToOneField(Tire, related_name='tire_left_front', blank=True, null=True, on_delete=models.SET_NULL)
    tire_left_rear = models.OneToOneField(Tire, related_name='tire_left_rear', blank=True, null=True, on_delete=models.SET_NULL)
    tire_right_front = models.OneToOneField(Tire, related_name='tire_right_front',  blank=True, null=True, on_delete=models.SET_NULL)
    tire_right_rear = models.OneToOneField(Tire, related_name='tire_right_rear',  blank=True, null=True, on_delete=models.SET_NULL)

    tire_specc = models.CharField(max_length=30, default='NEUTRAL', blank=True, null=True, choices=SPECC_TYPE)
    locations = models.ManyToManyField(Location, blank=True)
    company = models.ForeignKey(Company, on_delete=models.DO_NOTHING, blank=True, null=True)

    def __str__(self):
        return 'ID:%s model:%s by %s STATUS:%s'%(self.id,self.model,self.company,self.getStatus())
    
    def getStatus(self):
        status = 'OK'
        problems = ''
        if self.tire_left_front is None or self.tire_left_rear is None or self.tire_right_front is None or self.tire_right_rear is None:
            status = 'DANGER'
            problems +=  'Vehicle is missing tires\n'
            return status, problems
        if self.tire_left_front.sensor is None or self.tire_left_rear.sensor  is None or self.tire_right_front.sensor  is None or self.tire_right_rear.sensor  is None:
            status = 'DANGER'
            problems +=  'Vehicle is missing sensors\n' 
            return status, problems       
        if self.tire_left_front.getStatus()[0] != 'OK':
            status = self.tire_left_front.getStatus()[0]
            problems += 'Left Front ' + self.tire_left_front.getStatus()[1]
        if self.tire_left_rear.getStatus()[0] != 'OK':
            status = self.tire_left_rear.getStatus()[0]
            problems += 'Left Rear ' +self.tire_left_rear.getStatus()[1]
        if self.tire_right_front.getStatus()[0] != 'OK':
            status = self.tire_right_front.getStatus()[0]
            problems += 'Right Front ' +self.tire_right_front.getStatus()[1]
        if self.tire_right_rear.getStatus()[0] != 'OK':
            status = self.tire_right_rear.getStatus()[0]
            problems += 'Right Rear ' +self.tire_right_rear.getStatus()[1]
        if self.tire_specc != 'NEUTRAL':
            status = 'WARNING'
            problems +=  'Vehicle\'s tire specc is not NEUTRAL\n' 
        return status, problems

    def getStatusStatus(self):
        return self.getStatus()[0]
    
    def getStatusMotivation(self):
        return self.getStatus()[1]

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
    creation_datetime = models.DateTimeField(auto_now_add=True, blank=True, null=True)
    
    company = models.ForeignKey(Company, on_delete=models.DO_NOTHING, blank=True, null=True)

    def __str__(self):
        return 'Fleet Manager: %s ( %s %s ) COMPANY: %s' % (self.user.username, self.user.first_name, self.user.last_name,self.company)      

class K1(models.Model):
    distance = models.IntegerField(primary_key=True)
    value = models.FloatField(blank=True, null=True)
        


    

    



    


     
    


