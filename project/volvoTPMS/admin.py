from django.contrib import admin
from .models import Sensor,Tire,Vehicle

# Register your models here.
class SensorAdmin(admin.ModelAdmin):
    model = Sensor

class TireAdmin(admin.ModelAdmin):
    model = Tire

class VehicleAdmin(admin.ModelAdmin):
    model = Vehicle

admin.site.register(Sensor, SensorAdmin)
admin.site.register(Tire, TireAdmin)
admin.site.register(Vehicle, VehicleAdmin)