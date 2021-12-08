from django.contrib import admin
from .models import Sensor, Tire, Location, Vehicle, CompanyAdministrator, FleetManager, Company

# Register your models here.
class SensorAdmin(admin.ModelAdmin):
    model = Sensor

class TireAdmin(admin.ModelAdmin):
    model = Tire

class LocationAdmin(admin.ModelAdmin):
    model = Location

class VehicleAdmin(admin.ModelAdmin):
    model = Vehicle

class CompanyAdministratorAdmin(admin.ModelAdmin):
    model = CompanyAdministrator

class FleetManagerAdmin(admin.ModelAdmin):
    model = FleetManager

class CompanyAdmin(admin.ModelAdmin):
    model = Company

admin.site.register(Sensor, SensorAdmin)
admin.site.register(Tire, TireAdmin)
admin.site.register(Location, LocationAdmin)
admin.site.register(Vehicle, VehicleAdmin)
admin.site.register(CompanyAdministrator, CompanyAdministratorAdmin)
admin.site.register(FleetManager, FleetManagerAdmin)
admin.site.register(Company, CompanyAdmin)