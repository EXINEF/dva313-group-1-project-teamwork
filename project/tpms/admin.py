from django.contrib import admin
from .models import Sensor, Tire, Location, Vehicle, CompanyAdministrator, FleetManager, Company

# Register your models here.
class SensorAdmin(admin.ModelAdmin):
    model = Sensor

    ordering = ('id', 'status', 'temperature', 'pressure', 'remaning_battery')
    list_display = ('id', 'status', 'temperature', 'pressure', 'remaning_battery')
    search_fields = ['id']

class TireAdmin(admin.ModelAdmin):
    model = Tire

    ordering = ('id', 'remaining_life', 'tread_depth', 'revolutions')
    list_display = ('id', 'remaining_life', 'tread_depth', 'revolutions')
    search_fields = ['id']

class LocationAdmin(admin.ModelAdmin):
    model = Location

class VehicleAdmin(admin.ModelAdmin):
    model = Vehicle

class CompanyAdministratorAdmin(admin.ModelAdmin):
    model = CompanyAdministrator

class FleetManagerAdmin(admin.ModelAdmin):
    model = FleetManager

    ordering = ('user', 'company', 'first_name', 'last_name')
    list_display = ('user', 'company', 'first_name', 'last_name')
    search_fields = ['user__username']

class CompanyAdmin(admin.ModelAdmin):
    model = Company

admin.site.register(Sensor, SensorAdmin)
admin.site.register(Tire, TireAdmin)
admin.site.register(Location, LocationAdmin)
admin.site.register(Vehicle, VehicleAdmin)
admin.site.register(CompanyAdministrator, CompanyAdministratorAdmin)
admin.site.register(FleetManager, FleetManagerAdmin)
admin.site.register(Company, CompanyAdmin)