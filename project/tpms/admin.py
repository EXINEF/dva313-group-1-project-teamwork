from django.contrib import admin
from .models import Sensor, Tire, Location, Vehicle, CompanyAdministrator, FleetManager, Company, K1

# Register your models here.
class SensorAdmin(admin.ModelAdmin):
    model = Sensor

    ordering = ('id', 'company', 'status', 'temperature', 'pressure', 'remaning_battery', 'is_used')
    list_display = ('id', 'company', 'status', 'temperature', 'pressure', 'remaning_battery', 'is_used')
    search_fields = ['id']

class TireAdmin(admin.ModelAdmin):
    model = Tire

    ordering = ('id', 'company', 'remaining_life', 'tread_depth', 'revolutions', 'is_used')
    list_display = ('id', 'company', 'remaining_life', 'tread_depth', 'revolutions', 'is_used')
    search_fields = ['id']

class LocationAdmin(admin.ModelAdmin):
    model = Location

class VehicleAdmin(admin.ModelAdmin):
    model = Vehicle

    ordering = ('id', 'model', 'ambient_temperature', 'company')
    list_display = ('id', 'model', 'ambient_temperature', 'company')
    search_fields = ['id']

class CompanyAdministratorAdmin(admin.ModelAdmin):
    model = CompanyAdministrator

class FleetManagerAdmin(admin.ModelAdmin):
    model = FleetManager

class CompanyAdmin(admin.ModelAdmin):
    model = Company

class K1Admin(admin.ModelAdmin):
    ordering = ('distance', 'value')
    list_display = ('distance', 'value')
    model = K1

admin.site.register(Sensor, SensorAdmin)
admin.site.register(Tire, TireAdmin)
admin.site.register(Location, LocationAdmin)
admin.site.register(Vehicle, VehicleAdmin)
admin.site.register(CompanyAdministrator, CompanyAdministratorAdmin)
admin.site.register(FleetManager, FleetManagerAdmin)
admin.site.register(Company, CompanyAdmin)
admin.site.register(K1, K1Admin)