from django.contrib import admin
from .models import Company, Sensor,Tire,Vehicle

# Register your models here.
class SensorAdmin(admin.ModelAdmin):
    model = Sensor

class TireAdmin(admin.ModelAdmin):
    model = Tire

class VehicleAdmin(admin.ModelAdmin):
    model = Vehicle

class CompanyAdmin(admin.ModelAdmin):
    model = Company

admin.site.register(Sensor, SensorAdmin)
admin.site.register(Tire, TireAdmin)
admin.site.register(Vehicle, VehicleAdmin)
admin.site.register(Company, CompanyAdmin)